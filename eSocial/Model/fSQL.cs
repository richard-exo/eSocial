using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSocial.Model {

   public sealed partial class fSQL {

      bool dsIsBusy;
      string iErr = "";
      bool bLastErr = false;
      bool iBClearQueue = false;

      private string sCatalog = "eSocial";
      private string sUserID = "esocial";
      private string sPassword = "#e$ocial$68bE32";
      //private string sDataSource = "sql.workoffice.com.br,5683";
      private string sDataSource = "sql.workoffice.com.br";
      private string sConnection;

      BlockingCollection<string> lSQL = new BlockingCollection<string>();
      BackgroundWorker bw = new BackgroundWorker();

      public fSQL() {
         sConnection =
         "Initial Catalog=" + sCatalog + ";" +
         "MultipleActiveResultSets=True" + ";" +
         "user id=" + sUserID + ";" +
         "password=" + sPassword + ";" +
         "Data Source=" + sDataSource + ";" +
         "Max Pool Size=;Min Pool Size=";

         bw.WorkerSupportsCancellation = true;
         bw.DoWork += Bw_DoWork;
      }

      public void addCMD(string cmd, bool exec = false) {
         lSQL.Add(cmd);
         if (exec) { execAsync(); }
      }

      public bool clearQueue
      {
         get { return iBClearQueue; }
         set { iBClearQueue = value; }
      }

      public int ctCMD { get { return lSQL.Count(); } }

      public void execAsync() { if (lSQL.Count > 0 && !bw.IsBusy) { try { bw.RunWorkerAsync(); } catch { } } }

      public void abort() { bw.CancelAsync(); }

      private void Bw_DoWork(object sender, DoWorkEventArgs e) {

         SqlConnection conn = new SqlConnection(sConnection);
         conn.Open();

         while (!bw.CancellationPending) {

            if (lSQL.Count.Equals(0)) { bw.CancelAsync(); break; }

            string obj = null;

            try {
               foreach (var item in lSQL) {

                  if (!conn.State.Equals(ConnectionState.Open)) { conn.Open(); }

                  if (iBClearQueue && !iErr.Equals("")) { break; }
                  obj = item;
                  new SqlCommand(item, conn).ExecuteNonQuery();
                  lSQL.TryTake(out obj);
               }
            }
            catch (SqlException err) {
               iErr = err.Message;
               bLastErr = true;
               if (iBClearQueue) { lSQL = new BlockingCollection<string>(); }
               else { lSQL.TryTake(out obj); }
            }
            finally {
               try { conn.Close(); }
               catch (Exception err) {
                  iErr = err.Message;
                  bLastErr = true;
                  if (iBClearQueue) { lSQL = new BlockingCollection<string>(); }
                  else { lSQL.TryTake(out obj); }
               }
            }
         }
      }

      public DataSet exec(string sCmd, string tableName = "retTable") {

         if (iBClearQueue && !iErr.Equals("")) { return null; }

         while (dsIsBusy) { }
         dsIsBusy = true;

         SqlConnection conn = new SqlConnection(sConnection);
         DataSet dsRet = new DataSet();

         try {
            SqlDataAdapter da = new SqlDataAdapter(sCmd, conn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsRet, tableName);
         }

         catch (SqlException err) {
            iErr = err.Message;
            bLastErr = true;
            if (iBClearQueue) { lSQL = new BlockingCollection<string>(); }
         }
         finally {

            try {
               if (conn.State != ConnectionState.Open)
                  conn.Close();
            }
            catch (Exception err) {
               iErr = err.Message;
               bLastErr = true;
               if (iBClearQueue) { lSQL = new BlockingCollection<string>(); }
            }
            dsIsBusy = false;
         }
         return dsRet;
      }
      public string lastError { get { bLastErr = false; var err = iErr; iErr = ""; return err; } }
      public bool hasErrors
      {
         get { return bLastErr; }
         set { bLastErr = value; iErr = (!value ? "" : iErr); }
      }
   }
}
