namespace MangaForm
{
    partial class MangaConfiguration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MangaConfiguration));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pcbVol = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumeroVol = new System.Windows.Forms.TextBox();
            this.btnSearchFolder = new System.Windows.Forms.Button();
            this.pcbLocal = new System.Windows.Forms.PictureBox();
            this.pcbChrome = new System.Windows.Forms.PictureBox();
            this.pcbCap = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cbkLocal = new System.Windows.Forms.CheckBox();
            this.txtVol = new System.Windows.Forms.TextBox();
            this.txtCap = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.cbkChrome = new System.Windows.Forms.CheckBox();
            this.cbkCap = new System.Windows.Forms.CheckBox();
            this.cbkVol = new System.Windows.Forms.CheckBox();
            this.fbdPath = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbVol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLocal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbChrome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbCap)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pcbVol);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtNumeroVol);
            this.panel1.Controls.Add(this.btnSearchFolder);
            this.panel1.Controls.Add(this.pcbLocal);
            this.panel1.Controls.Add(this.pcbChrome);
            this.panel1.Controls.Add(this.pcbCap);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnVoltar);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Controls.Add(this.cbkLocal);
            this.panel1.Controls.Add(this.txtVol);
            this.panel1.Controls.Add(this.txtCap);
            this.panel1.Controls.Add(this.btnSalvar);
            this.panel1.Controls.Add(this.cbkChrome);
            this.panel1.Controls.Add(this.cbkCap);
            this.panel1.Controls.Add(this.cbkVol);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(488, 369);
            this.panel1.TabIndex = 0;
            // 
            // pcbVol
            // 
            this.pcbVol.Cursor = System.Windows.Forms.Cursors.Help;
            this.pcbVol.Image = global::MangaForm.Properties.Resources.round_help_button;
            this.pcbVol.Location = new System.Drawing.Point(178, 162);
            this.pcbVol.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pcbVol.Name = "pcbVol";
            this.pcbVol.Size = new System.Drawing.Size(12, 12);
            this.pcbVol.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbVol.TabIndex = 22;
            this.pcbVol.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.label4.Location = new System.Drawing.Point(39, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 19);
            this.label4.TabIndex = 21;
            this.label4.Text = "Qdt";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.label3.Location = new System.Drawing.Point(230, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 19);
            this.label3.TabIndex = 20;
            this.label3.Text = "Nº";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.label2.Location = new System.Drawing.Point(39, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 19);
            this.label2.TabIndex = 19;
            this.label2.Text = "Nº";
            // 
            // txtNumeroVol
            // 
            this.txtNumeroVol.Enabled = false;
            this.txtNumeroVol.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.txtNumeroVol.Location = new System.Drawing.Point(263, 187);
            this.txtNumeroVol.Name = "txtNumeroVol";
            this.txtNumeroVol.Size = new System.Drawing.Size(100, 26);
            this.txtNumeroVol.TabIndex = 18;
            this.txtNumeroVol.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumeroVol_KeyPress);
            // 
            // btnSearchFolder
            // 
            this.btnSearchFolder.Enabled = false;
            this.btnSearchFolder.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.btnSearchFolder.Location = new System.Drawing.Point(393, 249);
            this.btnSearchFolder.Name = "btnSearchFolder";
            this.btnSearchFolder.Size = new System.Drawing.Size(84, 26);
            this.btnSearchFolder.TabIndex = 17;
            this.btnSearchFolder.Text = "Procurar";
            this.btnSearchFolder.UseVisualStyleBackColor = true;
            this.btnSearchFolder.Click += new System.EventHandler(this.BtnSearchFolder_Click);
            // 
            // pcbLocal
            // 
            this.pcbLocal.Cursor = System.Windows.Forms.Cursors.Help;
            this.pcbLocal.Image = global::MangaForm.Properties.Resources.round_help_button;
            this.pcbLocal.Location = new System.Drawing.Point(206, 225);
            this.pcbLocal.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pcbLocal.Name = "pcbLocal";
            this.pcbLocal.Size = new System.Drawing.Size(12, 12);
            this.pcbLocal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbLocal.TabIndex = 16;
            this.pcbLocal.TabStop = false;
            // 
            // pcbChrome
            // 
            this.pcbChrome.Cursor = System.Windows.Forms.Cursors.Help;
            this.pcbChrome.Image = global::MangaForm.Properties.Resources.round_help_button;
            this.pcbChrome.Location = new System.Drawing.Point(126, 74);
            this.pcbChrome.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pcbChrome.Name = "pcbChrome";
            this.pcbChrome.Size = new System.Drawing.Size(12, 12);
            this.pcbChrome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbChrome.TabIndex = 15;
            this.pcbChrome.TabStop = false;
            // 
            // pcbCap
            // 
            this.pcbCap.Cursor = System.Windows.Forms.Cursors.Help;
            this.pcbCap.Image = global::MangaForm.Properties.Resources.round_help_button;
            this.pcbCap.Location = new System.Drawing.Point(234, 103);
            this.pcbCap.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pcbCap.Name = "pcbCap";
            this.pcbCap.Size = new System.Drawing.Size(12, 12);
            this.pcbCap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbCap.TabIndex = 14;
            this.pcbCap.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 33);
            this.label1.TabIndex = 13;
            this.label1.Text = "Configurações";
            // 
            // btnVoltar
            // 
            this.btnVoltar.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Location = new System.Drawing.Point(370, 309);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(95, 40);
            this.btnVoltar.TabIndex = 12;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.BtnVoltar_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(175, 309);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(175, 40);
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "Resetar Configurações";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // txtPath
            // 
            this.txtPath.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtPath.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(18, 249);
            this.txtPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(368, 26);
            this.txtPath.TabIndex = 10;
            // 
            // cbkLocal
            // 
            this.cbkLocal.AutoSize = true;
            this.cbkLocal.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.cbkLocal.Location = new System.Drawing.Point(18, 220);
            this.cbkLocal.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbkLocal.Name = "cbkLocal";
            this.cbkLocal.Size = new System.Drawing.Size(190, 23);
            this.cbkLocal.TabIndex = 9;
            this.cbkLocal.Text = "Mudar Local de Download";
            this.cbkLocal.UseVisualStyleBackColor = true;
            this.cbkLocal.CheckedChanged += new System.EventHandler(this.CbkLocal_CheckedChanged);
            // 
            // txtVol
            // 
            this.txtVol.Enabled = false;
            this.txtVol.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVol.Location = new System.Drawing.Point(80, 187);
            this.txtVol.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtVol.Name = "txtVol";
            this.txtVol.Size = new System.Drawing.Size(137, 26);
            this.txtVol.TabIndex = 8;
            this.txtVol.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtVol_KeyPress);
            // 
            // txtCap
            // 
            this.txtCap.Enabled = false;
            this.txtCap.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCap.Location = new System.Drawing.Point(71, 127);
            this.txtCap.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtCap.Name = "txtCap";
            this.txtCap.Size = new System.Drawing.Size(137, 26);
            this.txtCap.TabIndex = 7;
            this.txtCap.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCap_KeyPress);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.Location = new System.Drawing.Point(20, 309);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(135, 40);
            this.btnSalvar.TabIndex = 6;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // cbkChrome
            // 
            this.cbkChrome.AutoSize = true;
            this.cbkChrome.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.cbkChrome.Location = new System.Drawing.Point(18, 69);
            this.cbkChrome.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbkChrome.Name = "cbkChrome";
            this.cbkChrome.Size = new System.Drawing.Size(111, 23);
            this.cbkChrome.TabIndex = 1;
            this.cbkChrome.Text = "Usar Chrome";
            this.cbkChrome.UseVisualStyleBackColor = true;
            // 
            // cbkCap
            // 
            this.cbkCap.AutoSize = true;
            this.cbkCap.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.cbkCap.Location = new System.Drawing.Point(18, 98);
            this.cbkCap.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbkCap.Name = "cbkCap";
            this.cbkCap.Size = new System.Drawing.Size(220, 23);
            this.cbkCap.TabIndex = 2;
            this.cbkCap.Text = "Baixar a partir de um capítulo";
            this.cbkCap.UseVisualStyleBackColor = true;
            this.cbkCap.CheckedChanged += new System.EventHandler(this.CbkCap_CheckedChanged);
            // 
            // cbkVol
            // 
            this.cbkVol.AutoSize = true;
            this.cbkVol.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.cbkVol.Location = new System.Drawing.Point(18, 158);
            this.cbkVol.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbkVol.Name = "cbkVol";
            this.cbkVol.Size = new System.Drawing.Size(163, 23);
            this.cbkVol.TabIndex = 3;
            this.cbkVol.Text = "Compilar em Volumes";
            this.cbkVol.UseVisualStyleBackColor = true;
            this.cbkVol.CheckedChanged += new System.EventHandler(this.CbkVol_CheckedChanged);
            // 
            // MangaConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 369);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MangaConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbVol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLocal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbChrome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbCap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbkChrome;
        private System.Windows.Forms.CheckBox cbkCap;
        private System.Windows.Forms.CheckBox cbkVol;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.TextBox txtVol;
        private System.Windows.Forms.TextBox txtCap;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.CheckBox cbkLocal;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.FolderBrowserDialog fbdPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pcbLocal;
        private System.Windows.Forms.PictureBox pcbChrome;
        private System.Windows.Forms.PictureBox pcbCap;
        private System.Windows.Forms.Button btnSearchFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumeroVol;
        private System.Windows.Forms.PictureBox pcbVol;
    }
}