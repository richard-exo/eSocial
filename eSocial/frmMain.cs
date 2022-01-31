using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Net;
using System.Configuration;

using eSocial.Controller;

namespace eSocial {

    public partial class frmMain : Form {

        int iMaxColChar = int.Parse(ConfigurationManager.AppSettings["maxColChar"]);
        int iIntProxEnvio = int.Parse(ConfigurationManager.AppSettings["intProxEnvio"]);

        controller controller;

        public frmMain() {

            InitializeComponent();
            
            controller = new controller(iMaxColChar);

            Text = "eSocial";
            lblVersao.Text = "eSocial - Layout " + ConfigurationManager.AppSettings["vLayoutEventos"].Replace("_", ".");
            lblStatus.Text = "Suspenso";

            mskIntervalo.Text = new TimeSpan(0, 0, iIntProxEnvio).ToString(@"mm\:ss");

            // Passa a referência dos controles para o controller
            controller.controls.addControl(controller.controls.enControls.btStart, btStart);
            controller.controls.addControl(controller.controls.enControls.btStop, btPause);
            controller.controls.addControl(controller.controls.enControls.chkCleanLog, chkCleanLog);
            controller.controls.addControl(controller.controls.enControls.chkEnviar, chkEnviar);
            controller.controls.addControl(controller.controls.enControls.chkConsultar, chkConsultar);
            controller.controls.addControl(controller.controls.enControls.chkTabela, chkTabela);
            controller.controls.addControl(controller.controls.enControls.chkNaoPeriodico, chkNaoPeriodico);
            controller.controls.addControl(controller.controls.enControls.chkPeriodico, chkPeriodico);
            controller.controls.addControl(controller.controls.enControls.lblStatus, lblStatus);
            controller.controls.addControl(controller.controls.enControls.txtLog, txtLog);
            controller.controls.addControl(controller.controls.enControls.txtIntervalo, mskIntervalo);

            controller.start();
        }

        private void btStart_Click(object sender, EventArgs e) {
            // A partir de 17/08/21, executamos o controller outra vez para carregar as configurações de marcação dos eventos
            ConfigurationManager.AppSettings["evento_tabela"] = (((CheckBox)chkTabela).Checked ? "1" : "0");
            ConfigurationManager.AppSettings["evento_naoPeriodico"] = (((CheckBox)chkNaoPeriodico).Checked ? "1" : "0");
            ConfigurationManager.AppSettings["evento_periodico"] = (((CheckBox)chkPeriodico).Checked ? "1" : "0");
            ConfigurationManager.AppSettings["evento_server"] = (cboServer.SelectedItem == null ? "1" : cboServer.SelectedItem.ToString());
            controller = new controller(iMaxColChar);

            controller.start();
        }
        private void btPause_Click(object sender, EventArgs e) { controller.stop(); }

        private void mskIntervalo_Enter(object sender, EventArgs e) {

            // #BUGFIX - O evento interno de focus do mask sobrepõe o comando 'selectAll', portanto utilizamos o BeginInvoke que é assíncrono.
            BeginInvoke((Action)(() => { mskIntervalo.SelectAll(); }));
        }
        private void mskIntervalo_Leave(object sender, EventArgs e) {

            if (mskIntervalo.Text.Length.Equals(0)) { mskIntervalo.Text = iIntProxEnvio.ToString().PadLeft(4, '0'); }
            else {
                if (int.Parse(mskIntervalo.Text).Equals(0)) { mskIntervalo.Text = iIntProxEnvio.ToString().PadLeft(4, '0'); }
                else { mskIntervalo.Text = mskIntervalo.Text.PadLeft(4, '0'); }
            }
        }

      private void chkEnviar_CheckedChanged(object sender, EventArgs e)
      {
         controller.stop();
         
         if (!chkEnviar.Checked)
         {
            chkTabela.Checked = false;
            chkNaoPeriodico.Checked = false;
            chkPeriodico.Checked = false;

            chkTabela.Enabled = false;
            chkNaoPeriodico.Enabled = false;
            chkPeriodico.Enabled = false;
         }
         else
         {
            chkTabela.Enabled = true;
            chkNaoPeriodico.Enabled = true;
            chkPeriodico.Enabled = true;
         }
      }

      private void chkConsultar_CheckedChanged(object sender, EventArgs e)
      {
         controller.stop();
      }

      private void chkPeriodico_CheckedChanged(object sender, EventArgs e)
      {
         controller.stop();
      }

      private void chkNaoPeriodico_CheckedChanged(object sender, EventArgs e)
      {
         controller.stop();
      }

      private void chkTabela_CheckedChanged(object sender, EventArgs e)
      {
         controller.stop();
      }
   }
}

