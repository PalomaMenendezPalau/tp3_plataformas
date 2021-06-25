using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TP2
{
    public partial class Form2 : Form
    {

        AgenciaManager miAgencia = new AgenciaManager(new Agencia(10));
        int intentos = 0;
        private string[] userLog = new string[2];

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void botonIngreso_Click(object sender, EventArgs e)
        {
            if (txtLoginIngresoDNI.Text != "")
            {
                string mensajeResultado = miAgencia.ValidarLogin(txtLoginIngresoDNI.Text, txtLoginIngresoPass.Text);
                if (mensajeResultado.Equals("True") || mensajeResultado.Equals("False"))
                {
                    //CARGAR DATOS DE LOGINVALUES
                    userLog[0] = txtLoginIngresoDNI.Text; //DNI
                    userLog[1] = mensajeResultado;//Perfil
                    intentos = 0;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(mensajeResultado);
                }
            }
            else {
                MessageBox.Show("Debe ingresar un usuario para acceder");
            }

        }

        public string[] getLoginValues() {
            return userLog;
        }

        private void ingresoPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLoginRegistrarse_Click(object sender, EventArgs e)
        {
            panelRegistrarse.Visible = true;
        }

        private void btnRegistrarseVolver_Click(object sender, EventArgs e)
        {
            panelRegistrarse.Visible = false;
        }

        private void btnRegistrarseCrearUsr_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = miAgencia.ValidarUsuarioCrea(textBoxRegistrarseDNI.Text);
                if (respuesta == "true")
                {
                    miAgencia.agregarUsuario(
                        int.Parse(textBoxRegistrarseDNI.Text),
                        textBoxRegistrarseNombre.Text,
                        textBoxRegistrarseEmail.Text,
                        textBoxRegistarsePass.Text,
                        checkBoxRegistrarseAdmin.Checked,
                        false);

                    //miAgencia.GuardarDatosUsuarios();
                    MessageBox.Show("Usuario generado con exito");
                    panelRegistrarse.Visible = false;
                    LimpiarInputs();
                }
                else {
                    MessageBox.Show(respuesta);
                }
            }
            catch {
                MessageBox.Show("No se pudo registrar el usuario");
            }

            }

        public void LimpiarInputs() {
            textBoxRegistrarseDNI.Text = "";
            textBoxRegistrarseNombre.Text = "";
            textBoxRegistrarseEmail.Text = "";
            textBoxRegistarsePass.Text = "";
            checkBoxRegistrarseAdmin.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxRegistarsePass.UseSystemPasswordChar)
                textBoxRegistarsePass.UseSystemPasswordChar = false;
            else
                textBoxRegistarsePass.UseSystemPasswordChar = true;
        }

        private void btnCloseLogin_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCloseLogin2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


    }
    }
