
namespace TP2
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelRegistrarse = new System.Windows.Forms.Panel();
            this.btnCloseLogin = new System.Windows.Forms.Button();
            this.btnVisiblePass = new System.Windows.Forms.Button();
            this.textBoxRegistarsePass = new System.Windows.Forms.TextBox();
            this.textBoxRegistrarseEmail = new System.Windows.Forms.TextBox();
            this.textBoxRegistrarseDNI = new System.Windows.Forms.TextBox();
            this.textBoxRegistrarseNombre = new System.Windows.Forms.TextBox();
            this.btnRegistrarseVolver = new System.Windows.Forms.Button();
            this.btnRegistrarseCrearUsr = new System.Windows.Forms.Button();
            this.checkBoxRegistrarseAdmin = new System.Windows.Forms.CheckBox();
            this.labelRegistrarsePass = new System.Windows.Forms.Label();
            this.labelRegistrarseEMail = new System.Windows.Forms.Label();
            this.labelRegistrarseDNI = new System.Windows.Forms.Label();
            this.labelRegistrarseNombre = new System.Windows.Forms.Label();
            this.labelRegistrarseTitle = new System.Windows.Forms.Label();
            this.labelLoginIniciarSesion = new System.Windows.Forms.Label();
            this.labelLoginTitle = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnLoginRegistrarse = new System.Windows.Forms.Button();
            this.botonIngreso = new System.Windows.Forms.Button();
            this.txtLoginIngresoPass = new System.Windows.Forms.TextBox();
            this.txtLoginIngresoDNI = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLoginDNI = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnCloseLogin2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelRegistrarse.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.panelRegistrarse);
            this.panel1.Controls.Add(this.btnCloseLogin2);
            this.panel1.Controls.Add(this.labelLoginIniciarSesion);
            this.panel1.Controls.Add(this.labelLoginTitle);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnLoginRegistrarse);
            this.panel1.Controls.Add(this.botonIngreso);
            this.panel1.Controls.Add(this.txtLoginIngresoPass);
            this.panel1.Controls.Add(this.txtLoginIngresoDNI);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.labelLoginDNI);
            this.panel1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Location = new System.Drawing.Point(509, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 450);
            this.panel1.TabIndex = 0;
            // 
            // panelRegistrarse
            // 
            this.panelRegistrarse.BackColor = System.Drawing.Color.Black;
            this.panelRegistrarse.Controls.Add(this.btnCloseLogin);
            this.panelRegistrarse.Controls.Add(this.btnVisiblePass);
            this.panelRegistrarse.Controls.Add(this.textBoxRegistarsePass);
            this.panelRegistrarse.Controls.Add(this.textBoxRegistrarseEmail);
            this.panelRegistrarse.Controls.Add(this.textBoxRegistrarseDNI);
            this.panelRegistrarse.Controls.Add(this.textBoxRegistrarseNombre);
            this.panelRegistrarse.Controls.Add(this.btnRegistrarseVolver);
            this.panelRegistrarse.Controls.Add(this.btnRegistrarseCrearUsr);
            this.panelRegistrarse.Controls.Add(this.checkBoxRegistrarseAdmin);
            this.panelRegistrarse.Controls.Add(this.labelRegistrarsePass);
            this.panelRegistrarse.Controls.Add(this.labelRegistrarseEMail);
            this.panelRegistrarse.Controls.Add(this.labelRegistrarseDNI);
            this.panelRegistrarse.Controls.Add(this.labelRegistrarseNombre);
            this.panelRegistrarse.Controls.Add(this.labelRegistrarseTitle);
            this.panelRegistrarse.Location = new System.Drawing.Point(0, 0);
            this.panelRegistrarse.Name = "panelRegistrarse";
            this.panelRegistrarse.Size = new System.Drawing.Size(292, 450);
            this.panelRegistrarse.TabIndex = 6;
            this.panelRegistrarse.Visible = false;
            // 
            // btnCloseLogin
            // 
            this.btnCloseLogin.BackColor = System.Drawing.Color.Black;
            this.btnCloseLogin.Location = new System.Drawing.Point(262, 0);
            this.btnCloseLogin.Name = "btnCloseLogin";
            this.btnCloseLogin.Size = new System.Drawing.Size(30, 32);
            this.btnCloseLogin.TabIndex = 13;
            this.btnCloseLogin.Text = "X";
            this.btnCloseLogin.UseVisualStyleBackColor = false;
            this.btnCloseLogin.Click += new System.EventHandler(this.btnCloseLogin_Click);
            // 
            // btnVisiblePass
            // 
            this.btnVisiblePass.BackColor = System.Drawing.Color.Transparent;
            this.btnVisiblePass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVisiblePass.BackgroundImage")));
            this.btnVisiblePass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnVisiblePass.Location = new System.Drawing.Point(234, 291);
            this.btnVisiblePass.Name = "btnVisiblePass";
            this.btnVisiblePass.Size = new System.Drawing.Size(29, 21);
            this.btnVisiblePass.TabIndex = 12;
            this.btnVisiblePass.UseVisualStyleBackColor = false;
            this.btnVisiblePass.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxRegistarsePass
            // 
            this.textBoxRegistarsePass.Location = new System.Drawing.Point(72, 287);
            this.textBoxRegistarsePass.Name = "textBoxRegistarsePass";
            this.textBoxRegistarsePass.Size = new System.Drawing.Size(156, 30);
            this.textBoxRegistarsePass.TabIndex = 11;
            this.textBoxRegistarsePass.UseSystemPasswordChar = true;
            // 
            // textBoxRegistrarseEmail
            // 
            this.textBoxRegistrarseEmail.Location = new System.Drawing.Point(72, 229);
            this.textBoxRegistrarseEmail.Name = "textBoxRegistrarseEmail";
            this.textBoxRegistrarseEmail.Size = new System.Drawing.Size(156, 30);
            this.textBoxRegistrarseEmail.TabIndex = 10;
            // 
            // textBoxRegistrarseDNI
            // 
            this.textBoxRegistrarseDNI.Location = new System.Drawing.Point(72, 170);
            this.textBoxRegistrarseDNI.Name = "textBoxRegistrarseDNI";
            this.textBoxRegistrarseDNI.Size = new System.Drawing.Size(156, 30);
            this.textBoxRegistrarseDNI.TabIndex = 9;
            // 
            // textBoxRegistrarseNombre
            // 
            this.textBoxRegistrarseNombre.Location = new System.Drawing.Point(72, 112);
            this.textBoxRegistrarseNombre.Name = "textBoxRegistrarseNombre";
            this.textBoxRegistrarseNombre.Size = new System.Drawing.Size(156, 30);
            this.textBoxRegistrarseNombre.TabIndex = 8;
            // 
            // btnRegistrarseVolver
            // 
            this.btnRegistrarseVolver.BackColor = System.Drawing.Color.Black;
            this.btnRegistrarseVolver.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRegistrarseVolver.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRegistrarseVolver.Location = new System.Drawing.Point(203, 408);
            this.btnRegistrarseVolver.Name = "btnRegistrarseVolver";
            this.btnRegistrarseVolver.Size = new System.Drawing.Size(86, 39);
            this.btnRegistrarseVolver.TabIndex = 7;
            this.btnRegistrarseVolver.Text = "Volver";
            this.btnRegistrarseVolver.UseVisualStyleBackColor = false;
            this.btnRegistrarseVolver.Click += new System.EventHandler(this.btnRegistrarseVolver_Click);
            // 
            // btnRegistrarseCrearUsr
            // 
            this.btnRegistrarseCrearUsr.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRegistrarseCrearUsr.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRegistrarseCrearUsr.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRegistrarseCrearUsr.Location = new System.Drawing.Point(95, 354);
            this.btnRegistrarseCrearUsr.Name = "btnRegistrarseCrearUsr";
            this.btnRegistrarseCrearUsr.Size = new System.Drawing.Size(121, 39);
            this.btnRegistrarseCrearUsr.TabIndex = 6;
            this.btnRegistrarseCrearUsr.Text = "Registrarse";
            this.btnRegistrarseCrearUsr.UseVisualStyleBackColor = false;
            this.btnRegistrarseCrearUsr.Click += new System.EventHandler(this.btnRegistrarseCrearUsr_Click);
            // 
            // checkBoxRegistrarseAdmin
            // 
            this.checkBoxRegistrarseAdmin.AutoSize = true;
            this.checkBoxRegistrarseAdmin.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxRegistrarseAdmin.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.checkBoxRegistrarseAdmin.Location = new System.Drawing.Point(72, 325);
            this.checkBoxRegistrarseAdmin.Name = "checkBoxRegistrarseAdmin";
            this.checkBoxRegistrarseAdmin.Size = new System.Drawing.Size(129, 23);
            this.checkBoxRegistrarseAdmin.TabIndex = 5;
            this.checkBoxRegistrarseAdmin.Text = "Administrador";
            this.checkBoxRegistrarseAdmin.UseVisualStyleBackColor = true;
            // 
            // labelRegistrarsePass
            // 
            this.labelRegistrarsePass.AutoSize = true;
            this.labelRegistrarsePass.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelRegistrarsePass.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelRegistrarsePass.Location = new System.Drawing.Point(72, 261);
            this.labelRegistrarsePass.Name = "labelRegistrarsePass";
            this.labelRegistrarsePass.Size = new System.Drawing.Size(104, 23);
            this.labelRegistrarsePass.TabIndex = 4;
            this.labelRegistrarsePass.Text = "Contraseña";
            // 
            // labelRegistrarseEMail
            // 
            this.labelRegistrarseEMail.AutoSize = true;
            this.labelRegistrarseEMail.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelRegistrarseEMail.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelRegistrarseEMail.Location = new System.Drawing.Point(72, 203);
            this.labelRegistrarseEMail.Name = "labelRegistrarseEMail";
            this.labelRegistrarseEMail.Size = new System.Drawing.Size(61, 23);
            this.labelRegistrarseEMail.TabIndex = 3;
            this.labelRegistrarseEMail.Text = "E-Mail";
            // 
            // labelRegistrarseDNI
            // 
            this.labelRegistrarseDNI.AutoSize = true;
            this.labelRegistrarseDNI.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelRegistrarseDNI.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelRegistrarseDNI.Location = new System.Drawing.Point(72, 144);
            this.labelRegistrarseDNI.Name = "labelRegistrarseDNI";
            this.labelRegistrarseDNI.Size = new System.Drawing.Size(43, 23);
            this.labelRegistrarseDNI.TabIndex = 2;
            this.labelRegistrarseDNI.Text = "DNI";
            // 
            // labelRegistrarseNombre
            // 
            this.labelRegistrarseNombre.AutoSize = true;
            this.labelRegistrarseNombre.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelRegistrarseNombre.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelRegistrarseNombre.Location = new System.Drawing.Point(72, 86);
            this.labelRegistrarseNombre.Name = "labelRegistrarseNombre";
            this.labelRegistrarseNombre.Size = new System.Drawing.Size(77, 23);
            this.labelRegistrarseNombre.TabIndex = 1;
            this.labelRegistrarseNombre.Text = "Nombre";
            // 
            // labelRegistrarseTitle
            // 
            this.labelRegistrarseTitle.AutoSize = true;
            this.labelRegistrarseTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelRegistrarseTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelRegistrarseTitle.Location = new System.Drawing.Point(95, 32);
            this.labelRegistrarseTitle.Name = "labelRegistrarseTitle";
            this.labelRegistrarseTitle.Size = new System.Drawing.Size(133, 29);
            this.labelRegistrarseTitle.TabIndex = 0;
            this.labelRegistrarseTitle.Text = "Registrarse";
            // 
            // labelLoginIniciarSesion
            // 
            this.labelLoginIniciarSesion.AutoSize = true;
            this.labelLoginIniciarSesion.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelLoginIniciarSesion.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelLoginIniciarSesion.Location = new System.Drawing.Point(90, 96);
            this.labelLoginIniciarSesion.Name = "labelLoginIniciarSesion";
            this.labelLoginIniciarSesion.Size = new System.Drawing.Size(152, 29);
            this.labelLoginIniciarSesion.TabIndex = 13;
            this.labelLoginIniciarSesion.Text = "Iniciar sesión";
            // 
            // labelLoginTitle
            // 
            this.labelLoginTitle.AutoSize = true;
            this.labelLoginTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelLoginTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelLoginTitle.Location = new System.Drawing.Point(13, 32);
            this.labelLoginTitle.Name = "labelLoginTitle";
            this.labelLoginTitle.Size = new System.Drawing.Size(266, 29);
            this.labelLoginTitle.TabIndex = 12;
            this.labelLoginTitle.Text = "¡Hola, que bueno verte!";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.CausesValidation = false;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.button2.ForeColor = System.Drawing.Color.Blue;
            this.button2.Location = new System.Drawing.Point(58, 272);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(196, 36);
            this.button2.TabIndex = 8;
            this.button2.Text = "¿Olvidó su contraseña?";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.button2.UseMnemonic = false;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnLoginRegistrarse
            // 
            this.btnLoginRegistrarse.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLoginRegistrarse.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLoginRegistrarse.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnLoginRegistrarse.Location = new System.Drawing.Point(95, 359);
            this.btnLoginRegistrarse.Name = "btnLoginRegistrarse";
            this.btnLoginRegistrarse.Size = new System.Drawing.Size(122, 40);
            this.btnLoginRegistrarse.TabIndex = 6;
            this.btnLoginRegistrarse.Text = "Registrarse";
            this.btnLoginRegistrarse.UseVisualStyleBackColor = false;
            this.btnLoginRegistrarse.Click += new System.EventHandler(this.btnLoginRegistrarse_Click);
            // 
            // botonIngreso
            // 
            this.botonIngreso.BackColor = System.Drawing.Color.SteelBlue;
            this.botonIngreso.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.botonIngreso.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.botonIngreso.Location = new System.Drawing.Point(95, 313);
            this.botonIngreso.Name = "botonIngreso";
            this.botonIngreso.Size = new System.Drawing.Size(122, 40);
            this.botonIngreso.TabIndex = 3;
            this.botonIngreso.Text = "Ingresar";
            this.botonIngreso.UseVisualStyleBackColor = false;
            this.botonIngreso.Click += new System.EventHandler(this.botonIngreso_Click);
            // 
            // txtLoginIngresoPass
            // 
            this.txtLoginIngresoPass.Location = new System.Drawing.Point(74, 239);
            this.txtLoginIngresoPass.Name = "txtLoginIngresoPass";
            this.txtLoginIngresoPass.PasswordChar = '*';
            this.txtLoginIngresoPass.Size = new System.Drawing.Size(168, 30);
            this.txtLoginIngresoPass.TabIndex = 1;
            this.txtLoginIngresoPass.UseSystemPasswordChar = true;
            this.txtLoginIngresoPass.TextChanged += new System.EventHandler(this.ingresoPass_TextChanged);
            // 
            // txtLoginIngresoDNI
            // 
            this.txtLoginIngresoDNI.Location = new System.Drawing.Point(74, 172);
            this.txtLoginIngresoDNI.Name = "txtLoginIngresoDNI";
            this.txtLoginIngresoDNI.Size = new System.Drawing.Size(168, 30);
            this.txtLoginIngresoDNI.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(74, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Contraseña";
            // 
            // labelLoginDNI
            // 
            this.labelLoginDNI.AutoSize = true;
            this.labelLoginDNI.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelLoginDNI.Location = new System.Drawing.Point(74, 146);
            this.labelLoginDNI.Name = "labelLoginDNI";
            this.labelLoginDNI.Size = new System.Drawing.Size(43, 23);
            this.labelLoginDNI.TabIndex = 4;
            this.labelLoginDNI.Text = "DNI";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnCloseLogin2
            // 
            this.btnCloseLogin2.BackColor = System.Drawing.Color.Black;
            this.btnCloseLogin2.Location = new System.Drawing.Point(262, 0);
            this.btnCloseLogin2.Name = "btnCloseLogin2";
            this.btnCloseLogin2.Size = new System.Drawing.Size(30, 32);
            this.btnCloseLogin2.TabIndex = 14;
            this.btnCloseLogin2.Text = "X";
            this.btnCloseLogin2.UseVisualStyleBackColor = false;
            this.btnCloseLogin2.Click += new System.EventHandler(this.btnCloseLogin2_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log in";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelRegistrarse.ResumeLayout(false);
            this.panelRegistrarse.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button botonIngreso;
        private System.Windows.Forms.TextBox txtLoginIngresoPass;
        private System.Windows.Forms.TextBox txtLoginIngresoDNI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLoginDNI;
        private System.Windows.Forms.Panel panelRegistrarse;
        private System.Windows.Forms.TextBox textBoxRegistarsePass;
        private System.Windows.Forms.TextBox textBoxRegistrarseEmail;
        private System.Windows.Forms.TextBox textBoxRegistrarseDNI;
        private System.Windows.Forms.TextBox textBoxRegistrarseNombre;
        private System.Windows.Forms.Button btnRegistrarseVolver;
        private System.Windows.Forms.Button btnRegistrarseCrearUsr;
        private System.Windows.Forms.CheckBox checkBoxRegistrarseAdmin;
        private System.Windows.Forms.Label labelRegistrarsePass;
        private System.Windows.Forms.Label labelRegistrarseEMail;
        private System.Windows.Forms.Label labelRegistrarseDNI;
        private System.Windows.Forms.Label labelRegistrarseNombre;
        private System.Windows.Forms.Label labelRegistrarseTitle;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnLoginRegistrarse;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label labelLoginIniciarSesion;
        private System.Windows.Forms.Label labelLoginTitle;
        private System.Windows.Forms.Button btnVisiblePass;
        private System.Windows.Forms.Button btnCloseLogin;
        private System.Windows.Forms.Button btnCloseLogin2;
    }
}