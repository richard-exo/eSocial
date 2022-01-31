namespace eSocial {
   partial class frmMain {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
         this.txtLog = new System.Windows.Forms.TextBox();
         this.lblStatus = new System.Windows.Forms.Label();
         this.btStart = new System.Windows.Forms.Button();
         this.btPause = new System.Windows.Forms.Button();
         this.lblIntervalo_cap = new System.Windows.Forms.Label();
         this.mskIntervalo = new System.Windows.Forms.MaskedTextBox();
         this.lblVersao = new System.Windows.Forms.Label();
         this.chkCleanLog = new System.Windows.Forms.CheckBox();
         this.chkEnviar = new System.Windows.Forms.CheckBox();
         this.chkConsultar = new System.Windows.Forms.CheckBox();
         this.chkNaoPeriodico = new System.Windows.Forms.CheckBox();
         this.chkTabela = new System.Windows.Forms.CheckBox();
         this.chkPeriodico = new System.Windows.Forms.CheckBox();
         this.cboServer = new System.Windows.Forms.ComboBox();
         this.label1 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // txtLog
         // 
         this.txtLog.BackColor = System.Drawing.Color.White;
         this.txtLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.txtLog.Location = new System.Drawing.Point(16, 101);
         this.txtLog.Margin = new System.Windows.Forms.Padding(4);
         this.txtLog.Multiline = true;
         this.txtLog.Name = "txtLog";
         this.txtLog.ReadOnly = true;
         this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.txtLog.Size = new System.Drawing.Size(533, 256);
         this.txtLog.TabIndex = 1;
         // 
         // lblStatus
         // 
         this.lblStatus.BackColor = System.Drawing.Color.Black;
         this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblStatus.ForeColor = System.Drawing.Color.White;
         this.lblStatus.Location = new System.Drawing.Point(16, 65);
         this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lblStatus.Name = "lblStatus";
         this.lblStatus.Size = new System.Drawing.Size(534, 30);
         this.lblStatus.TabIndex = 2;
         this.lblStatus.Text = "lblStatus";
         this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // btStart
         // 
         this.btStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
         this.btStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.btStart.Location = new System.Drawing.Point(17, 436);
         this.btStart.Margin = new System.Windows.Forms.Padding(4);
         this.btStart.Name = "btStart";
         this.btStart.Size = new System.Drawing.Size(260, 64);
         this.btStart.TabIndex = 4;
         this.btStart.Text = "Iniciar";
         this.btStart.UseVisualStyleBackColor = false;
         this.btStart.Click += new System.EventHandler(this.btStart_Click);
         // 
         // btPause
         // 
         this.btPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
         this.btPause.Enabled = false;
         this.btPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.btPause.Location = new System.Drawing.Point(292, 436);
         this.btPause.Margin = new System.Windows.Forms.Padding(4);
         this.btPause.Name = "btPause";
         this.btPause.Size = new System.Drawing.Size(260, 64);
         this.btPause.TabIndex = 4;
         this.btPause.Text = "Parar";
         this.btPause.UseVisualStyleBackColor = false;
         this.btPause.Click += new System.EventHandler(this.btPause_Click);
         // 
         // lblIntervalo_cap
         // 
         this.lblIntervalo_cap.AutoSize = true;
         this.lblIntervalo_cap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblIntervalo_cap.Location = new System.Drawing.Point(408, 397);
         this.lblIntervalo_cap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lblIntervalo_cap.Name = "lblIntervalo_cap";
         this.lblIntervalo_cap.Size = new System.Drawing.Size(85, 24);
         this.lblIntervalo_cap.TabIndex = 7;
         this.lblIntervalo_cap.Text = "Intervalo:";
         // 
         // mskIntervalo
         // 
         this.mskIntervalo.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
         this.mskIntervalo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
         this.mskIntervalo.Location = new System.Drawing.Point(498, 397);
         this.mskIntervalo.Margin = new System.Windows.Forms.Padding(4);
         this.mskIntervalo.Mask = "00:00";
         this.mskIntervalo.Name = "mskIntervalo";
         this.mskIntervalo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
         this.mskIntervalo.Size = new System.Drawing.Size(52, 26);
         this.mskIntervalo.TabIndex = 8;
         this.mskIntervalo.Text = "0000";
         this.mskIntervalo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         this.mskIntervalo.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
         this.mskIntervalo.Enter += new System.EventHandler(this.mskIntervalo_Enter);
         this.mskIntervalo.Leave += new System.EventHandler(this.mskIntervalo_Leave);
         // 
         // lblVersao
         // 
         this.lblVersao.BackColor = System.Drawing.Color.White;
         this.lblVersao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.lblVersao.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblVersao.Image = global::eSocial.Properties.Resources.logo;
         this.lblVersao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.lblVersao.Location = new System.Drawing.Point(16, 11);
         this.lblVersao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lblVersao.Name = "lblVersao";
         this.lblVersao.Size = new System.Drawing.Size(534, 49);
         this.lblVersao.TabIndex = 3;
         this.lblVersao.Text = "Layout";
         this.lblVersao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // chkCleanLog
         // 
         this.chkCleanLog.Checked = true;
         this.chkCleanLog.CheckState = System.Windows.Forms.CheckState.Checked;
         this.chkCleanLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
         this.chkCleanLog.Location = new System.Drawing.Point(439, 370);
         this.chkCleanLog.Margin = new System.Windows.Forms.Padding(4);
         this.chkCleanLog.Name = "chkCleanLog";
         this.chkCleanLog.Size = new System.Drawing.Size(110, 20);
         this.chkCleanLog.TabIndex = 11;
         this.chkCleanLog.Text = "Limpar LOG";
         this.chkCleanLog.UseVisualStyleBackColor = true;
         // 
         // chkEnviar
         // 
         this.chkEnviar.Checked = true;
         this.chkEnviar.CheckState = System.Windows.Forms.CheckState.Checked;
         this.chkEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
         this.chkEnviar.Location = new System.Drawing.Point(18, 375);
         this.chkEnviar.Margin = new System.Windows.Forms.Padding(4);
         this.chkEnviar.Name = "chkEnviar";
         this.chkEnviar.Size = new System.Drawing.Size(80, 20);
         this.chkEnviar.TabIndex = 12;
         this.chkEnviar.Text = "Enviar";
         this.chkEnviar.UseVisualStyleBackColor = true;
         this.chkEnviar.CheckedChanged += new System.EventHandler(this.chkEnviar_CheckedChanged);
         // 
         // chkConsultar
         // 
         this.chkConsultar.Checked = true;
         this.chkConsultar.CheckState = System.Windows.Forms.CheckState.Checked;
         this.chkConsultar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
         this.chkConsultar.Location = new System.Drawing.Point(114, 375);
         this.chkConsultar.Margin = new System.Windows.Forms.Padding(4);
         this.chkConsultar.Name = "chkConsultar";
         this.chkConsultar.Size = new System.Drawing.Size(120, 20);
         this.chkConsultar.TabIndex = 13;
         this.chkConsultar.Text = "Consultar";
         this.chkConsultar.UseVisualStyleBackColor = true;
         this.chkConsultar.CheckedChanged += new System.EventHandler(this.chkConsultar_CheckedChanged);
         // 
         // chkNaoPeriodico
         // 
         this.chkNaoPeriodico.Checked = true;
         this.chkNaoPeriodico.CheckState = System.Windows.Forms.CheckState.Checked;
         this.chkNaoPeriodico.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
         this.chkNaoPeriodico.Location = new System.Drawing.Point(114, 406);
         this.chkNaoPeriodico.Margin = new System.Windows.Forms.Padding(4);
         this.chkNaoPeriodico.Name = "chkNaoPeriodico";
         this.chkNaoPeriodico.Size = new System.Drawing.Size(123, 20);
         this.chkNaoPeriodico.TabIndex = 16;
         this.chkNaoPeriodico.Text = "Não periódico";
         this.chkNaoPeriodico.UseVisualStyleBackColor = true;
         this.chkNaoPeriodico.CheckedChanged += new System.EventHandler(this.chkNaoPeriodico_CheckedChanged);
         // 
         // chkTabela
         // 
         this.chkTabela.Checked = true;
         this.chkTabela.CheckState = System.Windows.Forms.CheckState.Checked;
         this.chkTabela.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
         this.chkTabela.Location = new System.Drawing.Point(18, 406);
         this.chkTabela.Margin = new System.Windows.Forms.Padding(4);
         this.chkTabela.Name = "chkTabela";
         this.chkTabela.Size = new System.Drawing.Size(80, 20);
         this.chkTabela.TabIndex = 15;
         this.chkTabela.Text = "Tabela";
         this.chkTabela.UseVisualStyleBackColor = true;
         this.chkTabela.CheckedChanged += new System.EventHandler(this.chkTabela_CheckedChanged);
         // 
         // chkPeriodico
         // 
         this.chkPeriodico.Checked = true;
         this.chkPeriodico.CheckState = System.Windows.Forms.CheckState.Checked;
         this.chkPeriodico.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
         this.chkPeriodico.Location = new System.Drawing.Point(250, 406);
         this.chkPeriodico.Margin = new System.Windows.Forms.Padding(4);
         this.chkPeriodico.Name = "chkPeriodico";
         this.chkPeriodico.Size = new System.Drawing.Size(120, 20);
         this.chkPeriodico.TabIndex = 14;
         this.chkPeriodico.Text = "Periódico";
         this.chkPeriodico.UseVisualStyleBackColor = true;
         this.chkPeriodico.CheckedChanged += new System.EventHandler(this.chkPeriodico_CheckedChanged);
         // 
         // cboServer
         // 
         this.cboServer.FormattingEnabled = true;
         this.cboServer.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
         this.cboServer.Location = new System.Drawing.Point(275, 372);
         this.cboServer.Margin = new System.Windows.Forms.Padding(4);
         this.cboServer.Name = "cboServer";
         this.cboServer.Size = new System.Drawing.Size(55, 24);
         this.cboServer.TabIndex = 165;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(217, 377);
         this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(54, 17);
         this.label1.TabIndex = 168;
         this.label1.Text = "Server:";
         // 
         // frmMain
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(563, 511);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.cboServer);
         this.Controls.Add(this.chkNaoPeriodico);
         this.Controls.Add(this.chkTabela);
         this.Controls.Add(this.chkPeriodico);
         this.Controls.Add(this.chkConsultar);
         this.Controls.Add(this.chkEnviar);
         this.Controls.Add(this.chkCleanLog);
         this.Controls.Add(this.mskIntervalo);
         this.Controls.Add(this.lblIntervalo_cap);
         this.Controls.Add(this.btPause);
         this.Controls.Add(this.btStart);
         this.Controls.Add(this.lblVersao);
         this.Controls.Add(this.lblStatus);
         this.Controls.Add(this.txtLog);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Margin = new System.Windows.Forms.Padding(4);
         this.Name = "frmMain";
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
         this.Text = "eSocial";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private System.Windows.Forms.TextBox txtLog;
      private System.Windows.Forms.Label lblStatus;
      private System.Windows.Forms.Label lblVersao;
      private System.Windows.Forms.Button btStart;
      private System.Windows.Forms.Button btPause;
      private System.Windows.Forms.Label lblIntervalo_cap;
      private System.Windows.Forms.MaskedTextBox mskIntervalo;
      private System.Windows.Forms.CheckBox chkCleanLog;
      private System.Windows.Forms.CheckBox chkEnviar;
      private System.Windows.Forms.CheckBox chkConsultar;
      private System.Windows.Forms.CheckBox chkNaoPeriodico;
      private System.Windows.Forms.CheckBox chkTabela;
      private System.Windows.Forms.CheckBox chkPeriodico;
      private System.Windows.Forms.ComboBox cboServer;
      private System.Windows.Forms.Label label1;
   }
}