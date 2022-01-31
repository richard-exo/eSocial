using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using eSocial.Controller.WS;
using eSocial.Model;
using eSocial.Model.Eventos.Retorno;

namespace eSocial.Controller
{

   public sealed partial class controller : cBase
   {
      #region ############################################################################################################### Declarations

      BackgroundWorker thMain = new BackgroundWorker();

      model model = new model();

      log log; int _maxLogCols;
      Control txtIntervalo, chkCleanLog, chkEnviar, chkConsultar, chkTabela, chkNaoPeriodico, chkPeriodico, chkLerXmlConfig;

      bool bAborted;
      int intPreConsulta = int.Parse(ConfigurationManager.AppSettings["intPreConsulta"]);
      TimeSpan tIntervalo, tIntervaloPreConsulta;

      // Os tipos de eventos devem seguir a ordem abaixo:
      Dictionary<string, enTipoEvento> lTipoEvento = new Dictionary<string, enTipoEvento>() {
         { "Tabelas", enTipoEvento.eventosIniciais_1 },
         { "Não Periódicos", enTipoEvento.eventosNaoPeriodicos_2 },
         { "Periódicos", enTipoEvento.eventosPeriodicos_3 }};

      enStatus _status;
      public enum enStatus { execucao, consulta, intervaloPreConsulta, aguardando, suspenso, erro }

      #endregion

      public controller(int maxLogCols)
      {

         _maxLogCols = maxLogCols;
         tIntervaloPreConsulta = new TimeSpan(0, 0, intPreConsulta);

         // Thread principal 
         thMain.DoWork += thMain_DoWork;
         thMain.ProgressChanged += thMain_ProgressChanged;
         thMain.RunWorkerCompleted += thMain_RunWorkerCompleted;
         thMain.WorkerSupportsCancellation = true;
         thMain.WorkerReportsProgress = true;

      }

      // Inicia a execução.
      public void start()
      {

         // Inicia o log
         if (log == null)
         {
            log = new log(controls.getControl(controls.enControls.txtLog), _maxLogCols);
            txtIntervalo = controls.getControl(controls.enControls.txtIntervalo);
            chkCleanLog = controls.getControl(controls.enControls.chkCleanLog);
            chkEnviar = controls.getControl(controls.enControls.chkEnviar);
            chkConsultar = controls.getControl(controls.enControls.chkConsultar);
            chkTabela = controls.getControl(controls.enControls.chkTabela);
            chkNaoPeriodico = controls.getControl(controls.enControls.chkNaoPeriodico);
            chkPeriodico = controls.getControl(controls.enControls.chkPeriodico);
         }

         thMain.RunWorkerAsync();
      }

      // Aborta a execução.
      public void stop() { bAborted = true; }

      void status(enStatus status, int prog = 0)
      {
         _status = status;
         thMain.ReportProgress(prog); Thread.Sleep(100); // -1 indica que o loop será iniciado
      }

      // Pesquisa WS
      private void thMain_DoWork(object sender, DoWorkEventArgs e)
      {

         if (((CheckBox)chkCleanLog).Checked) { log.clear(); }

         int iErrTry = 1;
         char cTitulo = '#', cTipoEvento = '=', cEmpresa = '#', cEvento = '>';
         bool bHasReg = false;

         #region #################################################################################################################### Execução

         status(enStatus.execucao);

         if (((CheckBox)chkEnviar).Checked)
         {
            log.add("WS - Envio eventos", enLogMode.repeatLeft, cTitulo);

            // Itera os tipos de eventos na ordem 
            foreach (var tpEvento in lTipoEvento)
            {
               if (bAborted || hasError) { break; }

               bHasReg = false;
               log.add(tpEvento.Key, enLogMode.repeatLeft, cTipoEvento);

               // Itera as empresas do evento selecionado
               foreach (var lote in model.getEventosPendentes(tpEvento.Value))
               {
                  iErrTry = 1;
                  bHasReg = true;
                  retEnvioLoteEventos retEnvio = null;

                  // Lote para envio
                  if (!lote.isParcial)
                  {
                     while (retEnvio == null)
                     {
                        Thread.Sleep(1000); // Aguarda 1 segundo
                        retEnvio = new enviarLotesEventosWS(lote, tpEvento.Value).enviar();

                        // Após 5 tentativas com intervalos de 10 segundos cada
                        if (retEnvio == null && iErrTry >= 6)
                        {
                           log.clear();

                           status(enStatus.erro, 0);
                           log.add("WS - ERRO", enLogMode.repeatLeft, cTitulo);

                           iErrTry = 0;
                           string sErro = "Erro desconhecido...";
                           try { sErro = getErrors().Last().Item2; } catch { }

                           for (int i = 0; i < sErro.Count(); i++)
                           {
                              try { log.add(sErro.Substring(i, _maxLogCols), enLogMode.appendLine); }
                              catch { log.add(sErro.Substring(i), enLogMode.appendLine); }
                              i += _maxLogCols; i--;
                           }

                           log.add();

                           //TimeSpan tIntervalo = new TimeSpan(1, 0, 0);
                           TimeSpan tIntervalo = new TimeSpan(0, 1, 0);

                           while (tIntervalo.TotalSeconds > 0 && !bAborted)
                           {
                              Thread.Sleep(1000);
                              tIntervalo = tIntervalo.Subtract(new TimeSpan(0, 0, 1));
                              if (bAborted) { thMain.CancelAsync(); log.clear(); break; } else { thMain.ReportProgress(0, new Tuple<TimeSpan, string>(tIntervalo, "ERRO WS")); }
                           }
                        }
                        else if (retEnvio != null)
                        {
                           model.gravar_retornoWS(retEnvio);

                           log.add(cEmpresa + " " + lote.nomeEvento + " - " + lote.descricaoEvento, enLogMode.appendLine);
                           log.add();
                        }

                        if (iErrTry.Equals(0)) { break; }
                        else if (retEnvio == null && iErrTry <= 5)
                        {
                           log.add("Tentativa de comunicação " + iErrTry + "...", enLogMode.appendLine);
                           iErrTry++;
                           Thread.Sleep(10000); // Aguarda 10 segundos para consultar novamente
                        }

                        if (bAborted || iErrTry.Equals(0)) { break; }
                     }
                  }

                  // Lote gerado parcial
                  else { model.inserirParcial(lote); }

                  if (bAborted || iErrTry.Equals(0)) { break; }

                  log.addStatus(cEvento + " " + lote.nomeEmpresa, (lote.isParcial ? enCdResposta.aguardando_999.GetHashCode() : retEnvio.retornoEnvioLoteEventos.status.cdResposta.GetHashCode()), 1);
                  log.add();

               }

               if (iErrTry.Equals(0)) { break; }
               else if (!bHasReg) { bHasReg = false; log.add("Nenhum registro encontrado", enLogMode.repeatRight, '.', 1); }
            }
         }

         #endregion

         #region ####################################################################################################### Intervalo pré consulta

         if (!iErrTry.Equals(0))
         {

            status(enStatus.intervaloPreConsulta);

            tIntervaloPreConsulta = new TimeSpan(0, 0, intPreConsulta);

            while (tIntervaloPreConsulta.TotalSeconds > 0 && !(bAborted || hasError))
            {
               Thread.Sleep(1000);
               tIntervaloPreConsulta = tIntervaloPreConsulta.Subtract(new TimeSpan(0, 0, 1));
               if (bAborted || hasError) { thMain.CancelAsync(); } else { thMain.ReportProgress(0); }
            }
         }

         #endregion

         #region #################################################################################################################### Consulta

         status(enStatus.consulta);
         bHasReg = false;

         if (((CheckBox)chkConsultar).Checked)
         {
            log.add("WS - Consulta eventos", enLogMode.repeatLeft, cTitulo);

            foreach (var protocolo in model.getRecibosConsulta())
            {
               if (bAborted || hasError) { break; }

               iErrTry = 1;
               bHasReg = true;
               bool bConsultar = true;

               // Verifica se irá executar a próxima consulta
               try { if (DateTime.Now < DateTime.Parse(protocolo.Value.First().dadosConsulta.proximaConsulta)) { bConsultar = false; } }
               catch { }

               if (bConsultar)
               {
                  Thread.Sleep(1000); // Aguarda 1 segundo
                  retProcessamentoLote retConsulta = null;

                  while (retConsulta == null)
                  {
                     retConsulta = new consultarLotesEventosWS(protocolo.Value.ToList().First().tpAmb, protocolo.Value.ToList().First().certificado).consultar(protocolo.Key);

                     // Após 5 tentativas com intervalos de 10 segundos cada
                     if (retConsulta == null && iErrTry >= 6)
                     {
                        log.clear();

                        status(enStatus.erro, 0);
                        log.add("WS - ERRO", enLogMode.repeatLeft, cTitulo);

                        iErrTry = 0;
                        string sErro = "Erro desconhecido...";
                        try { sErro = getErrors().Last().Item2; } catch { }

                        for (int i = 0; i < sErro.Count(); i++)
                        {
                           try { log.add(sErro.Substring(i, _maxLogCols), enLogMode.appendLine); }
                           catch { log.add(sErro.Substring(i), enLogMode.appendLine); }
                           i += _maxLogCols; i--;
                        }

                        log.add();

                        //TimeSpan tIntervalo = new TimeSpan(1, 0, 0);
                        TimeSpan tIntervalo = new TimeSpan(0, 1, 0);

                        while (tIntervalo.TotalSeconds > 0 && !bAborted)
                        {
                           Thread.Sleep(1000);
                           tIntervalo = tIntervalo.Subtract(new TimeSpan(0, 0, 1));
                           if (bAborted) { thMain.CancelAsync(); log.clear(); break; } else { thMain.ReportProgress(0, new Tuple<TimeSpan, string>(tIntervalo, "ERRO WS")); }
                        }
                     }
                     else if (retConsulta != null)
                     {
                        model.gravar_retornoWS(retConsulta);
                        log.addStatus(cEvento + " " + protocolo.Value.First().nomeEvento + " - " + protocolo.Value.First().nomeEmpresa, retConsulta.retornoProcessamentoLoteEventos.status.cdResposta.GetHashCode(), 1);
                     }

                     if (iErrTry.Equals(0)) { break; }
                     else if (retConsulta == null && iErrTry <= 5)
                     {
                        log.add("Tentativa de comunicação " + iErrTry + "...", enLogMode.appendLine);
                        iErrTry++;
                        Thread.Sleep(10000); // Aguarda 10 segundos para consultar novamente
                     }

                     if (bAborted || iErrTry.Equals(0)) { break; }
                  }

                  if (retConsulta.retornoProcessamentoLoteEventos.retornoEventos.evento != null)
                  {
                     foreach (var evento in retConsulta.retornoProcessamentoLoteEventos.retornoEventos.evento)
                     {

                        if (bAborted || hasError || iErrTry.Equals(0)) { break; }

                        log.addStatus(
                            cEvento + " " + evento.Id.Substring(2, evento.Id.Length - 2),
                            evento.retornoEvento.retornoEvento.processamento.cdResposta.GetHashCode()
                            , 2);
                     }
                  }
                  log.add();
               }

               // Em processamento
               else { log.addStatus(cEvento + " " + protocolo.Value.First().nomeEmpresa, enCdResposta.aguardando_999.GetHashCode(), 1); }
            }

            if (!bHasReg) { bHasReg = false; log.add("Nenhum registro encontrado", enLogMode.repeatRight, '.', 1); } else { log.add(); }
         }


         #endregion

         #region #################################################################################################################### Intervalo

         status(enStatus.aguardando, -1);

         while (tIntervalo.TotalSeconds > 0 && !(bAborted || hasError))
         {
            Thread.Sleep(1000);
            tIntervalo = tIntervalo.Subtract(new TimeSpan(0, 0, 1));
            if (bAborted || hasError) { thMain.CancelAsync(); } else { thMain.ReportProgress(0); }
         }

         #endregion

         if (hasError)
         {
            log.clear();

            status(enStatus.erro, 0);
            log.add("ERRO", enLogMode.repeatLeft, cTitulo);

            foreach (var err in getErrors())
            {
               log.add(log: "> " + err.Item1, titlePost: enLogMode.appendLine, nivel: 1);
               log.add(log: "> " + err.Item2, titlePost: enLogMode.appendLine, nivel: 2);
               log.add();
            }

            TimeSpan tIntervalo = new TimeSpan(0, 1, 0);

            while (tIntervalo.TotalSeconds > 0 && !bAborted)
            {
               Thread.Sleep(1000);
               tIntervalo = tIntervalo.Subtract(new TimeSpan(0, 0, 1));
               if (bAborted) { thMain.CancelAsync(); log.clear(); break; } else { thMain.ReportProgress(0, new Tuple<TimeSpan, string>(tIntervalo, "ERRO")); }
            }

         }

         if (bAborted) { return; }

      }

      // Progresso da Thread manager
      private void thMain_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {

         switch (_status)
         {

            case enStatus.execucao:

               controls.getControl(controls.enControls.btStart).Enabled = false;
               controls.getControl(controls.enControls.btStop).Enabled = true;

               controls.getControl(controls.enControls.lblStatus).Text = "Executando";
               controls.getControl(controls.enControls.lblStatus).ForeColor = Color.Black;
               controls.getControl(controls.enControls.lblStatus).BackColor = Color.Orange;

               break;

            case enStatus.consulta:

               controls.getControl(controls.enControls.lblStatus).Text = "Consulta de recibos";
               controls.getControl(controls.enControls.lblStatus).ForeColor = Color.White;
               controls.getControl(controls.enControls.lblStatus).BackColor = Color.DarkGreen;

               break;

            case enStatus.intervaloPreConsulta:

               controls.getControl(controls.enControls.lblStatus).Text = "Intervalo para consulta - " + tIntervaloPreConsulta.ToString(@"mm\:ss");
               controls.getControl(controls.enControls.lblStatus).ForeColor = Color.White;
               controls.getControl(controls.enControls.lblStatus).BackColor = Color.DarkCyan;

               break;

            case enStatus.aguardando:

               // Obtém o tempo do intervalo antes de iniciar o loop
               if (e.ProgressPercentage.Equals(-1))
               {
                  try
                  {
                     tIntervalo = new TimeSpan(0,
                     int.Parse(txtIntervalo.Text.Substring(0, 2)),
                     int.Parse(txtIntervalo.Text.Substring(2, 2)));
                  }
                  catch { tIntervalo = new TimeSpan(0, 0, intPreConsulta); }
               }

               controls.getControl(controls.enControls.lblStatus).Text = "Aguardando - " + tIntervalo.ToString(@"mm\:ss");
               controls.getControl(controls.enControls.lblStatus).ForeColor = Color.White;
               controls.getControl(controls.enControls.lblStatus).BackColor = Color.MidnightBlue;

               break;

            case enStatus.erro:

               if (e.UserState != null)
               {
                  Tuple<TimeSpan, string> oErro = (Tuple<TimeSpan, string>)e.UserState;

                  controls.getControl(controls.enControls.lblStatus).Text = oErro.Item2 + " - " + oErro.Item1.ToString(@"mm\:ss");
                  controls.getControl(controls.enControls.lblStatus).ForeColor = Color.White;
                  controls.getControl(controls.enControls.lblStatus).BackColor = Color.DarkRed;
               }

               break;

         }

      }

      // Ao abortar e desativar todas as Threads.
      private void thMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {

         controls.getControl(controls.enControls.btStart).Enabled = true;
         controls.getControl(controls.enControls.btStop).Enabled = false;

         controls.getControl(controls.enControls.lblStatus).Text = "Suspenso";
         controls.getControl(controls.enControls.lblStatus).ForeColor = Color.White;
         controls.getControl(controls.enControls.lblStatus).BackColor = Color.Black;

         if (!bAborted) { thMain.RunWorkerAsync(); }
         bAborted = false;

      }
   }

   public enum enLogMode { append, appendLine, repeatLeft, repeatRight }
   public class log : cBase
   {
      int _maxLogCols;
      bool bLastAppend;
      Control _ctrl;
      StringBuilder _log = new StringBuilder();

      public log(Control ctrl, int maxLogCols) { _ctrl = ctrl; _maxLogCols = maxLogCols; }

      public void addStatus(string titulo, int cdResposta, int nivel = 0)
      {

         titulo += " ";

         string sResponse = (!getServerResponses.ContainsKey(cdResposta) ? "ERRO" : getServerResponses[cdResposta]);

         string sRet = " " + cdResposta.ToString() + " - " + sResponse;
         int maxEmpColLen = (_maxLogCols - sRet.Length);
         int iNomeEmpLen = (titulo.Length >= (_maxLogCols - sRet.Length) ? maxEmpColLen - 4 : titulo.Length);

         add(titulo.Substring(0, iNomeEmpLen).PadRight(_maxLogCols - (sRet.Length + nivel), '.') + sRet, enLogMode.appendLine, '-', nivel);
      }

      public void add(string log = "", enLogMode titlePost = enLogMode.append, char titleChar = '-', int nivel = 0)
      {

         for (int c = 0; c < nivel; c++) { log = " " + log; }
         if (log.Length > _maxLogCols) { log = log.Substring(0, _maxLogCols); }

         if (log.Trim().Equals("")) { _log.AppendLine(); return; }
         else if (titlePost.Equals(enLogMode.append)) { _log.Append(log.ToString()); bLastAppend = true; }
         else if (bLastAppend) { bLastAppend = false; _log.AppendLine(); }

         if (_log.Length >= int.MaxValue) { _log.Clear(); }

         switch (titlePost)
         {
            case enLogMode.appendLine: _log.AppendLine(log.ToString()); break;
            case enLogMode.repeatLeft: _log.AppendLine((" " + log).PadLeft(_maxLogCols, titleChar)); _log.AppendLine(); break;
            case enLogMode.repeatRight: _log.AppendLine((log + " ").PadRight(_maxLogCols, titleChar)); _log.AppendLine(); break;
         }

         lock (_ctrl)
         {
            lock (_log)
            {

               _ctrl.BeginInvoke((Action)(() =>
               {
                  try
                  {
                     _ctrl.Text = _log.ToString();
                     ((TextBox)_ctrl).SelectionStart = ((TextBox)_ctrl).Text.Length - 1;
                     ((TextBox)_ctrl).ScrollToCaret();
                  }
                  catch { }
               }));
            }
         }
      }
      public void clear()
      {

         lock (_ctrl)
         {
            lock (_log)
            {

               _log.Clear();

               _ctrl.BeginInvoke((Action)(() =>
               {
                  ((TextBox)_ctrl).Text = "";
                  _log.Clear();
               }));
            }
         }
      }
   }

}