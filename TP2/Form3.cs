using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace TP2
{
    public partial class Form3 : Form
    {
        int codigo;
        string dniUserSelect;
        private string[] userLog;
        AgenciaManager manager = new AgenciaManager(new Agencia(10));
        public Form3(string[] usrLogin)
        {
            userLog = usrLogin;
            //userLog[0] = DNI usuario logueado
            //userLog[1] = Perfil usuario logueado (true = ADMIN)

            InitializeComponent();
            
            if (userLog[1].Equals("False"))
                {
                    this.mainTabControl.TabPages.Remove(tabAdmin);
                }
            
            CargarDataGridAdmin();
            CargarDataGridUser();
            CargarDataGridABMUsuarios();
            CargarDataGridReservas();
            ComboBoxUsuario();
        }

        public void ComboBoxUsuario()
        {
            foreach (String ciudad in manager.MostrarCiudad())
               comboBox2City.Items.Add(ciudad);

            foreach (String tipoAloj in manager.MostrarTipoAloj())              
                comboBox1TypeOfAloj.Items.Add(tipoAloj);
            comboBox1TypeOfAloj.SelectedItem = "Todos";

            foreach (String estrellas in manager.MostrarEstrellas()) 
                comboBox4CantEstrellas.Items.Add(estrellas);

            foreach (string cantPersonas in manager.MostrarCantPersonas())
                comboBox3CantPersonas.Items.Add(cantPersonas);

        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panelCabana_Paint(object sender, PaintEventArgs e)
        {

        }


        //NUEVA CABAÑA
        private void button3_Click(object sender, EventArgs e)
        {
            if(!manager.getMiAgencia().estaLlena())
            {
                if (!manager.getMiAgencia().estaAlojamiento(int.Parse(txtCabañaCodigo.Text))) { 

                manager.agregarCabania(
                    int.Parse(txtCabañaCodigo.Text),
                    txtCabañaNombre.Text,
                    txtCabañaCiudad.Text,
                    txtCabañaBarrio.Text,
                    int.Parse(numCabañaEstrellas.Value.ToString()),
                    int.Parse(txtCabañaPersonas.Text),
                    checkCabañaTv.Checked,
                    int.Parse(txtCabañaPrecioDia.Text),
                    int.Parse(txtCabañasHabitaciones.Text),
                    int.Parse(txtCabañasBaños.Text));
                //FGM_ Lo ve palo esto?
                 manager.GuardarDatosCabaña();
                 dataGridAdmin.Rows.Clear();
                 CargarDataGridAdmin();
                 LimpiarInputs();
                }
                else
                {
                    MessageBox.Show("Ya existe un alojamiento con ese codigo, por favor ingresa uno distinto.");
                    txtCabañaCodigo.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Se alcanzo el limite de 10 alojamientos ingresados.");
            }
        }

        //NUEVO HOTEL
        private void btnAplicarHotel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!manager.getMiAgencia().estaLlena())
                {
                    if (!manager.getMiAgencia().estaAlojamiento(int.Parse(txtHotelCodigo.Text)))
                    {
                        GuardarDatosHoteles();
                        manager.agregarHotel(
                            int.Parse(txtHotelCodigo.Text),
                            txtHotelNombre.Text,
                            txtHotelCiudad.Text,
                            txtHotelBarrio.Text,
                            int.Parse(numHotelEstrellas.Value.ToString()),
                            int.Parse(txtHotelCantPersonas.Text),
                            checkHotelTv.Checked,
                            int.Parse(txtHotelPrecioPersona.Text));
                        LimpiarInputs();
                        dataGridAdmin.Rows.Clear();
                        CargarDataGridAdmin();
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un alojamiento con ese codigo, por favor ingresa uno distinto.");
                        txtHotelCodigo.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Se alcanzo el limite de 10 alojamientos ingresados.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error, intente nuevamente por favor.");
            }

        }

        private void GuardarDatosHoteles()
        {
            try
            {
                manager.GuardarDatosHoteles();
            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error, por favor intenta cargando los datos nuevamente.");
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

       


        private void btnAplicarUsr_Click(object sender, EventArgs e)
        {

            try
            {
                manager.agregarUsuario(
                    int.Parse(txtABMUsuariosDNI.Text),
                    txtABMUsuariosNombre.Text,
                    txtABMUsuariosMail.Text,
                    txtABMUsuariosPass.Text,
                    checkABMUsuariosAdmin.Checked,
                    checkABMUsuariosBloqueado.Checked);
                //FGM_ Lo ve palo a caso?
                manager.GuardarDatosUsuarios();
                MessageBox.Show("Usuario generado con exito");
                LimpiarInputsABMUsuarios();
                btnCrearUsr.Visible = false;
            }
            catch
            {
                MessageBox.Show("No se pudo registrar el usuario");
            }    

            CargarDataGridABMUsuarios();
        }

        private void CargarDataGridABMUsuarios() {
            //LIMPIO EL dataGridABMUsuarios
            dataGridABMUsuarios.Rows.Clear();
            //COMPLETO EL dataGridABMUsuarios CON LOS DATOS DE LA LISTA misUsuarios
            foreach (Usuarios u in manager.getMisUsuarios())
            {
                int n = dataGridABMUsuarios.Rows.Add();
                dataGridABMUsuarios.Rows[n].Cells[0].Value = u.getDni();
                dataGridABMUsuarios.Rows[n].Cells[1].Value = u.getNombre();
                dataGridABMUsuarios.Rows[n].Cells[2].Value = u.getMail();
                dataGridABMUsuarios.Rows[n].Cells[3].Value = u.getPassword();
                dataGridABMUsuarios.Rows[n].Cells[4].Value = u.getEsAdmin();
                dataGridABMUsuarios.Rows[n].Cells[5].Value = u.getBloqueado();
            }
        }
        private void LimpiarInputsABMUsuarios() {
            txtABMUsuariosDNI.Text = "";
            txtABMUsuariosNombre.Text = "";
            txtABMUsuariosMail.Text = "";
            txtABMUsuariosPass.Text = "";
            checkABMUsuariosAdmin.Checked = false;
            checkABMUsuariosBloqueado.Checked = false;
        }

        private void CargarDataGridAdmin() {
            
            List<Alojamiento> alojs = manager.getMiAgencia().getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                int n = dataGridAdmin.Rows.Add();
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }
                if (a is Hotel)
                {
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID ADMIN
                    dataGridAdmin.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridAdmin.Rows[n].Cells[1].Value = "Hotel";
                    dataGridAdmin.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridAdmin.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridAdmin.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridAdmin.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridAdmin.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridAdmin.Rows[n].Cells[7].Value = aTv;
                    dataGridAdmin.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridAdmin.Rows[n].Cells[9].Value = "1";
                    dataGridAdmin.Rows[n].Cells[10].Value = "1";   
                }
                else if (a is Cabaña)
                {

                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID ADMIN
                    dataGridAdmin.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridAdmin.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridAdmin.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridAdmin.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridAdmin.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridAdmin.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridAdmin.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridAdmin.Rows[n].Cells[7].Value = aTv;
                    dataGridAdmin.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridAdmin.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridAdmin.Rows[n].Cells[10].Value = c.getBaños();
                }

            }
        }

        private void CargarDataGridUser()
        {
            dataGridUser.Rows.Clear();
            CargarDataGridUserSoloCabanas();
            CargarDataGridUserSolohoteles();
        }

        private void CargarDataGridUserSoloCabanas()
        {
            List<Alojamiento> alojs = manager.getMiAgencia().getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Cabaña)
                {
                int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();
                    dataGridUser.Rows[n].Cells[11].Value = "Reservar";
                }
            }
        }       
        private void CargarDataGridCantEstrellasCabana(int cantEstrellas)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().MasEstrellas(cantEstrellas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();
                }
            }
        }
        private void CargarDataGridCiudadesYCantPersonasCabana(int cantPersonas, String ciudades)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).CiudadesDeAlojamientos(ciudades).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();
                }
            }
        }
        private void CargarDataGridCiudadesYCantEstrellasCabana(String ciudades, int cantEstrellas)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().MasEstrellas(cantEstrellas).CiudadesDeAlojamientos(ciudades).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();
                }
            }
        }
        private void CargarDataGridCiudadesCabana(String ciudades)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().CiudadesDeAlojamientos(ciudades).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();
                }
            }
        }
        private void CargarDataGridCiudadesYPreciosCabana(String ciudades, float precioMax, float precioMin)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().CiudadesDeAlojamientos(ciudades).AlojamientosEntrePrecios(precioMax, precioMin).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();
                }
            }
        }
        private void CargarDataGridPreciosCabana(float precioMax, float precioMin)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosEntrePrecios(precioMax, precioMin).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();
                }
            }
        }
        private void CargarDataGridCabana(int cantPersonas, float precioMax, float precioMin, String ciudades, int cantEstrellas)
        {
            dataGridUser.Rows.Clear();

            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).AlojamientosEntrePrecios(precioMax, precioMin)
                .CiudadesDeAlojamientos(ciudades).
                MasEstrellas(cantEstrellas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }
        private void CargarDataGridCityNullCabana(int cantPersonas, float precioMax, float precioMin, int cantEstrellas)
        {
            dataGridUser.Rows.Clear();

            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).AlojamientosEntrePrecios(precioMax, precioMin)
                .MasEstrellas(cantEstrellas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }
        private void CargarDataGridCantEstrellasNullCabana(int cantPersonas, float precioMax, float precioMin, String ciudades)
        {
            dataGridUser.Rows.Clear();

            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).AlojamientosEntrePrecios(precioMax, precioMin)
                .CiudadesDeAlojamientos(ciudades).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }
        private void CargarDataGridSinPreciosCabana(int cantPersonas, String ciudades, int cantEstrellas)
        {
            dataGridUser.Rows.Clear();

            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).MasEstrellas(cantEstrellas)
                .CiudadesDeAlojamientos(ciudades).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }
        private void CargarDataGridSinPreciosYCiudadCabana(int cantPersonas, int cantEstrellas)
        {
            dataGridUser.Rows.Clear();

            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).MasEstrellas(cantEstrellas)
                .getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }
        private void CargarDataGridCantPersonasCabana(int cantPersonas)
        {
            dataGridUser.Rows.Clear();

            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }        
        private void CargarDataGridCantPersonasHotel(int cantPersonas)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }
        private void CargarDataGridCityNullHotel(int cantPersonas, float precioMax, float precioMin, int cantEstrellas)
        {
            dataGridUser.Rows.Clear();

            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).AlojamientosEntrePrecios(precioMax, precioMin)
                .MasEstrellas(cantEstrellas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }
        
        private void CargarDataGridCantEstrellasNullHotel(int cantPersonas, float precioMax, float precioMin, String ciudades)
        {
            dataGridUser.Rows.Clear();

            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).AlojamientosEntrePrecios(precioMax, precioMin)
                .CiudadesDeAlojamientos(ciudades).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }
        private void CargarDataGridSinPreciosHotel(int cantPersonas, int cantEstrellas, String ciudades)
        {
            dataGridUser.Rows.Clear();

            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas)
                .CiudadesDeAlojamientos(ciudades).MasEstrellas(cantEstrellas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }
        private void CargarDataGridSinPreciosYCiudadHotel(int cantPersonas, int cantEstrellas)
        {
            dataGridUser.Rows.Clear();

            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas)
                .MasEstrellas(cantEstrellas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }

        private void CargarDataGridHotel(int cantPersonas, float precioMax, float precioMin, String ciudades, int cantEstrellas)
        {
            dataGridUser.Rows.Clear();

            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).AlojamientosEntrePrecios(precioMax, precioMin)
                .CiudadesDeAlojamientos(ciudades).
                MasEstrellas(cantEstrellas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }

        private void CargarDataGridTodos(int cantPersonas, float precioMax, float precioMin, String ciudades, int cantEstrellas)
        {
            dataGridUser.Rows.Clear();

            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).AlojamientosEntrePrecios(precioMax, precioMin)
                .CiudadesDeAlojamientos(ciudades).
                MasEstrellas(cantEstrellas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                    dataGridUser.Rows[n].Cells[11].Value = "Reservar";
                }
                else if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();
                    dataGridUser.Rows[n].Cells[11].Value = "Reservar";
                }
            }
        }
        private void CargarDataGridUserSolohoteles()
        {
            List<Alojamiento> alojs = manager.getMiAgencia().getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                    dataGridUser.Rows[n].Cells[11].Value = "Reservar";
                }

            }
        }
        private void CargarDataGridCantEstrellasHotel(int cantEstrellas)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().MasEstrellas(cantEstrellas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }
        
        private void CargarDataGridCiudadesYCantPersonasHotel(String ciudades, int cantPersonas)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().CiudadesDeAlojamientos(ciudades).AlojamientosPorCantidadDePersonas(cantPersonas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }  
        private void CargarDataGridCiudadesYCantEstrellasHotel(String ciudades, int cantEstrellas)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().CiudadesDeAlojamientos(ciudades).MasEstrellas(cantEstrellas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }
        private void CargarDataGridCiudadesHotel(String ciudades)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().CiudadesDeAlojamientos(ciudades).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }
        private void CargarDataGridCiudadesYPreciosHotel(String ciudades, float precioMax, float precioMin)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().CiudadesDeAlojamientos(ciudades).AlojamientosEntrePrecios(precioMax, precioMin).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }
        private void CargarDataGridPreciosHotel(float precioMax, float precioMin)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosEntrePrecios(precioMax, precioMin).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
            }
        }     
        private void CargarDataGridCityNullTodos(int cantEstrellas, int cantPersonas, float precioMax, float precioMin)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().MasEstrellas(cantEstrellas)
                .AlojamientosPorCantidadDePersonas(cantPersonas)
                .AlojamientosEntrePrecios(precioMax, precioMin).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
                else if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();

                }
            }
        }

        private void CargarDataGridSinPrecios(int cantEstrellas, int cantPersonas, string ciudades)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().MasEstrellas(cantEstrellas)
                .AlojamientosPorCantidadDePersonas(cantPersonas)
                .CiudadesDeAlojamientos(ciudades).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
                else if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();

                }
            }
        }

        private void CargarDataGridCantEstrellasNullTodos(float precioMax, float precioMin, String ciudades, int cantPersonas)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosEntrePrecios(precioMax, precioMin)
                .AlojamientosPorCantidadDePersonas(cantPersonas)
                .CiudadesDeAlojamientos(ciudades).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
                else if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();

                }
            }
        }

        private void CargarDataGridSinPreciosYCiudad(int cantEstrellas, int cantPersonas)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).MasEstrellas(cantEstrellas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
                else if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();

                }
            }
        }

        private void CargarDataGridCantPersonasTodos(int cantPersonas)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosPorCantidadDePersonas(cantPersonas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
                else if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();

                }
            }
        }

        private void CargarDataGridCantEstrellasTodos(int cantEstrellas)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().MasEstrellas(cantEstrellas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
                else if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();

                }
            }
        }

        private void CargarDataGridCiudadesTodos(String ciudades)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().CiudadesDeAlojamientos(ciudades).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
                else if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();

                }
            }
        }

        private void CargarDataGridCiudadesYCantPersonasTodos(String ciudades, int cantPersonas)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().CiudadesDeAlojamientos(ciudades).AlojamientosPorCantidadDePersonas(cantPersonas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
                else if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();

                }
            }
        }

        private void CargarDataGridCiudadesYCantEstrellasTodos(String ciudades, int cantEstrellas)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().CiudadesDeAlojamientos(ciudades).MasEstrellas(cantEstrellas).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
                else if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();

                }
            }
        }

        private void CargarDataGridCiudadesYPreciosTodos(float precioMax, float precioMin, String ciudades)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosEntrePrecios(precioMax, precioMin).CiudadesDeAlojamientos(ciudades).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
                else if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();

                }
            }
        }
        private void CargarDataGridCPreciosTodos(float precioMax, float precioMin)
        {
            dataGridUser.Rows.Clear();
            List<Alojamiento> alojs = manager.getMiAgencia().AlojamientosEntrePrecios(precioMax, precioMin).getAlojamientos();
            foreach (Alojamiento a in alojs)
            {
                string aTv;
                if (a.getTV()) { aTv = "Si"; } else { aTv = "No"; }

                if (a is Hotel)
                {
                    int n = dataGridUser.Rows.Add();
                    Hotel h = (Hotel)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = h.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Hotel";
                    dataGridUser.Rows[n].Cells[2].Value = h.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = h.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = h.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = h.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = h.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = h.getPrecioPorPersona();
                    dataGridUser.Rows[n].Cells[9].Value = "1";
                    dataGridUser.Rows[n].Cells[10].Value = "1";
                }
                else if (a is Cabaña)
                {
                    int n = dataGridUser.Rows.Add();
                    Cabaña c = (Cabaña)a;
                    //CARGAR VALORES EN GRID USER
                    dataGridUser.Rows[n].Cells[0].Value = c.getCodigo();
                    dataGridUser.Rows[n].Cells[1].Value = "Cabaña";
                    dataGridUser.Rows[n].Cells[2].Value = c.getEstrellas();
                    dataGridUser.Rows[n].Cells[3].Value = c.getNombre();
                    dataGridUser.Rows[n].Cells[4].Value = c.getCiudad();
                    dataGridUser.Rows[n].Cells[5].Value = c.getBarrio();
                    dataGridUser.Rows[n].Cells[6].Value = c.getCantPersonas();
                    dataGridUser.Rows[n].Cells[7].Value = aTv;
                    dataGridUser.Rows[n].Cells[8].Value = c.getPrecioDia();
                    dataGridUser.Rows[n].Cells[9].Value = c.getHabitaciones();
                    dataGridUser.Rows[n].Cells[10].Value = c.getBaños();

                }
            }
        }

        private void CargarDataGridReservas()
        {
            List<Reservas> reservas = manager.getMisReservas();
            foreach(Reservas r in reservas)
            {           
                    int res = dataGridReservas.Rows.Add();                    
                    dataGridReservas.Rows[res].Cells[0].Value = "TESTING";
                    dataGridReservas.Rows[res].Cells[1].Value = r.getFDesde();
                    dataGridReservas.Rows[res].Cells[2].Value = r.getFHasta();
                    dataGridReservas.Rows[res].Cells[3].Value = r.getPrecio();

            }
        }
        private void txtCabañaNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int codigoAloj = manager.getMiAgencia().getAlojamientos()[dataGridUser.CurrentCell.RowIndex].getCodigo();
            int precio = int.Parse(dataGridUser.Rows[dataGridUser.CurrentCell.RowIndex].Cells["Precio"].Value.ToString());

            if (dataGridUser.Columns[e.ColumnIndex].Name == "Reservar")
            {
                
                manager.agregarReserva(
                    manager.getMisReservas().Count+1,
                    dateTimeIda.Value,
                    dateTimeVuelta.Value,
                    manager.getMiAgencia().getAlojamientos()[dataGridUser.CurrentCell.RowIndex],
                    manager.buscarUsuarios(int.Parse(userLog[0])),
                    (precio * int.Parse(labelDiasTotales.Text))
                    );
                //FGM_ Este metodo lo revisa palo?
                manager.GuardarReservas();

                dataGridReservas.Rows.Clear();
                CargarDataGridReservas();
            }
            else { MessageBox.Show("Ocurrio un error al cargar la reserva, por favor intente nuevamente"); }
        }

        private void dataGridAdmin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        
        }

        private void dataGridAdmin_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridABMUsuarios_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {       
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabAdmin_Click(object sender, EventArgs e)
        {

        }

        private void LimpiarInputs()
        {
            txtCabañaCodigo.Text = "";
            txtCabañaNombre.Text = "";
            txtCabañaCiudad.Text = "";
            txtCabañaBarrio.Text = "";
            txtCabañaPersonas.Text = "";
            txtCabañasHabitaciones.Text = "";
            txtCabañasBaños.Text = "";
            txtCabañaPrecioDia.Text = "";
            checkCabañaTv.Checked = false;
            numCabañaEstrellas.Value = 1;

            txtHotelCodigo.Text = "";
            txtHotelNombre.Text = "";
            txtHotelCiudad.Text = "";
            txtHotelBarrio.Text = "";
            txtHotelCantPersonas.Text = "";
            checkHotelTv.Checked = false;
            numHotelEstrellas.Value = 1;
            txtHotelPrecioPersona.Text = "";

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void txtCabañasHabitaciones_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3Filtrar_Click(object sender, EventArgs e)
        {
            dataGridUser.Rows.Clear();
            BotonFiltrar();
        }


        private void BotonFiltrar()
        {

            /*  ************************************************************************************************************************************************
                    ************************************************************************************************************************************************
                    ************************************************************************************************************************************************
                                    SI FILTRAMOS POR CABANA          */
            string selectedItemAloj = comboBox1TypeOfAloj.SelectedItem.ToString();
            try
            {
                if (selectedItemAloj.Equals("Cabaña"))
                {
                    CargarDataGridUserSoloCabanas();

                    if (textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals("0"))
                    {
                        MessageBox.Show("No hay alojamientos en ese rango de precios.");
                        button3Filtrar.Enabled = true;
                        dataGridUser.Rows.Clear();
                        CargarDataGridUser();
                    }
                    else if (!textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals("0"))
                    {
                        MessageBox.Show("El precio maximo tiene que ser mayor al minimo.");
                        button3Filtrar.Enabled = true;
                        dataGridUser.Rows.Clear();
                        CargarDataGridUser();
                    }
                    //TODOS LOS FILTROS AL MISMO TIEMPO
                    else if (comboBox3CantPersonas.SelectedItem != null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        && comboBox4CantEstrellas.SelectedItem != null
                        || comboBox3CantPersonas.SelectedItem != null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        && comboBox4CantEstrellas.SelectedItem != null)
                    {
                        CargarDataGridCabana(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()),
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()),
                            comboBox2City.SelectedItem.ToString(),
                            int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //CIUDAD NULL
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem == null
                        || comboBox3CantPersonas.SelectedItem != null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem == null
                        && comboBox4CantEstrellas.SelectedItem != null)
                    {
                        CargarDataGridCityNullCabana(
                            int.Parse(comboBox3CantPersonas.SelectedItem.ToString()),
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()),
                            int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //CANTIDAD DE ESTRELLAS NULL
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        || comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        )
                    {
                        CargarDataGridCantEstrellasNullCabana(
                            int.Parse(comboBox3CantPersonas.SelectedItem.ToString()),
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()),
                            comboBox2City.SelectedItem.ToString());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //SIN PONER PRECIOS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridSinPreciosCabana(
                            int.Parse(comboBox3CantPersonas.SelectedItem.ToString()),
                            comboBox2City.SelectedItem.ToString(),
                            int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SIN PONER PRECIOS, Y TAMPOCO CIUDAD
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridSinPreciosYCiudadCabana(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()),
                            int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CANTIDAD DE PERSONAS
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridCantPersonasCabana(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CANTIDAD DE ESTRELLAS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridCantEstrellasCabana(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //CIUDADES Y CANT PERSONAS
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridCiudadesYCantPersonasCabana(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()), comboBox2City.SelectedItem.ToString());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //CIUDADES Y CANTESTRELLAS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridCiudadesYCantEstrellasCabana(comboBox2City.SelectedItem.ToString(), int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();

                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CIUDADES
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem != null
                        && textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals(""))
                    {
                        CargarDataGridCiudadesCabana(comboBox2City.SelectedItem.ToString());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                    }
                    //CIUDADES Y ENTRE PRECIOS
                    else if (comboBox3CantPersonas.SelectedItem == null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        && comboBox4CantEstrellas.SelectedItem == null
                        || comboBox3CantPersonas.SelectedItem == null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        && comboBox4CantEstrellas.SelectedItem == null
                        )
                    {
                        CargarDataGridCiudadesYPreciosCabana(
                            comboBox2City.SelectedItem.ToString(),
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()));

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                    }
                
                //ENTRE PRECIOS
                else if (comboBox3CantPersonas.SelectedItem == null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem == null
                        && comboBox4CantEstrellas.SelectedItem == null
                        || comboBox3CantPersonas.SelectedItem == null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem == null
                        && comboBox4CantEstrellas.SelectedItem == null
                        )
                {
                    CargarDataGridPreciosCabana(
                        float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                        float.Parse(textBox2PrecioMax.Text.Trim().ToString()));
                }
            }
                /*  ************************************************************************************************************************************************
                    ************************************************************************************************************************************************
                    ************************************************************************************************************************************************
                                    SI FILTRAMOS POR HOTEL          */
                else if (selectedItemAloj.Equals("Hotel"))
                {
                    CargarDataGridUserSolohoteles();
                    if (textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals("0"))
                    {
                        MessageBox.Show("No hay alojamientos en ese rango de precios.");
                        button3Filtrar.Enabled = true;
                        dataGridUser.Rows.Clear();
                        CargarDataGridUser();
                    }
                    else if (!textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals("0"))
                    {
                        MessageBox.Show("El precio maximo tiene que ser mayor al minimo.");
                        button3Filtrar.Enabled = true;
                        dataGridUser.Rows.Clear();
                        CargarDataGridUser();
                    }
                    //TODOS LOS FILTROS AL MISMO TIEMPO
                    else if (comboBox3CantPersonas.SelectedItem != null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        && comboBox4CantEstrellas.SelectedItem != null
                        || comboBox3CantPersonas.SelectedItem != null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        && comboBox4CantEstrellas.SelectedItem != null)
                    {
                        CargarDataGridHotel(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()),
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()),
                            comboBox2City.SelectedItem.ToString(),
                            int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //CIUDAD NULL
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem == null
                        || comboBox3CantPersonas.SelectedItem != null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem == null
                        && comboBox4CantEstrellas.SelectedItem != null)
                    {
                        CargarDataGridCityNullHotel(
                            int.Parse(comboBox3CantPersonas.SelectedItem.ToString()),
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()),
                            int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //CANTIDAD DE ESTRELLAS NULL
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        || comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        )
                    {
                        CargarDataGridCantEstrellasNullHotel(
                            int.Parse(comboBox3CantPersonas.SelectedItem.ToString()),
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()),
                            comboBox2City.SelectedItem.ToString());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //SIN PONER PRECIOS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridSinPreciosHotel(
                            int.Parse(comboBox3CantPersonas.SelectedItem.ToString()),
                            int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()),
                            comboBox2City.SelectedItem.ToString());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SIN PONER PRECIOS, Y TAMPOCO CIUDAD
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridSinPreciosYCiudadHotel(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()),
                            int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CANTIDAD DE PERSONAS
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridCantPersonasHotel(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CANTIDAD DE ESTRELLAS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridCantEstrellasHotel(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //CIUDADES Y CANT PERSONAS
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridCiudadesYCantPersonasHotel(comboBox2City.SelectedItem.ToString(), int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //CIUDADES Y CANTESTRELLAS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridCiudadesYCantEstrellasHotel(comboBox2City.SelectedItem.ToString(), int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();

                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CIUDADES
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem != null
                        && textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals(""))
                    {
                        CargarDataGridCiudadesHotel(comboBox2City.SelectedItem.ToString());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                    }
                    //CIUDADES Y ENTRE PRECIOS
                    else if (comboBox3CantPersonas.SelectedItem == null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        && comboBox4CantEstrellas.SelectedItem == null
                        || comboBox3CantPersonas.SelectedItem == null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        && comboBox4CantEstrellas.SelectedItem == null
                        )
                    {
                        CargarDataGridCiudadesYPreciosHotel(
                            comboBox2City.SelectedItem.ToString(),
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()));

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                    }
                    //ENTRE PRECIOS
                    else if (comboBox3CantPersonas.SelectedItem == null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem == null
                        && comboBox4CantEstrellas.SelectedItem == null
                        || comboBox3CantPersonas.SelectedItem == null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem == null
                        && comboBox4CantEstrellas.SelectedItem == null
                        )
                    {
                        CargarDataGridPreciosHotel(
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()));
                    }
                }
                /*  ************************************************************************************************************************************************
                    ************************************************************************************************************************************************
                    ************************************************************************************************************************************************
                                    SI FILTRAMOS POR TODOS          */

                else if (selectedItemAloj.Equals("Todos"))
                {
                    CargarDataGridUser();
                    if(textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals("0"))
                    {
                        MessageBox.Show("No hay alojamientos en ese rango de precios.");
                        button3Filtrar.Enabled = true;
                        dataGridUser.Rows.Clear();
                        CargarDataGridUser();
                    }
                    else if(!textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals("0"))
                    {
                        MessageBox.Show("El precio maximo tiene que ser mayor al minimo.");
                        button3Filtrar.Enabled = true;
                        dataGridUser.Rows.Clear();
                        CargarDataGridUser();
                    }
                    //TODOS LOS FILTROS AL MISMO TIEMPO
                    else if (comboBox3CantPersonas.SelectedItem != null
                        && !textBox1PrecioMin.Text.Trim().Equals("") 
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        && comboBox4CantEstrellas.SelectedItem != null
                        || comboBox3CantPersonas.SelectedItem != null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        && comboBox4CantEstrellas.SelectedItem != null)
                    {
                        CargarDataGridTodos(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()),
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()), 
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()),
                            comboBox2City.SelectedItem.ToString(),
                            int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //CIUDAD NULL
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem == null
                        || comboBox3CantPersonas.SelectedItem != null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem == null
                        && comboBox4CantEstrellas.SelectedItem != null)
                    {
                        CargarDataGridCityNullTodos(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()),
                            int.Parse(comboBox3CantPersonas.SelectedItem.ToString()),
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()));
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    } 
                    //CANTIDAD DE ESTRELLAS NULL
                    else if(comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        || comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        )
                    {
                        CargarDataGridCantEstrellasNullTodos(
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()),
                            comboBox2City.SelectedItem.ToString(),
                            int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //SIN PONER PRECIOS
                    else if(comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridSinPrecios(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()),
                            int.Parse(comboBox3CantPersonas.SelectedItem.ToString()),
                            comboBox2City.SelectedItem.ToString());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SIN PONER PRECIOS, Y TAMPOCO CIUDAD
                    else if(comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridSinPreciosYCiudad(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()),
                            int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CANTIDAD DE PERSONAS
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridCantPersonasTodos(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CANTIDAD DE ESTRELLAS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridCantEstrellasTodos(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //CIUDADES Y CANT PERSONAS
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridCiudadesYCantPersonasTodos(comboBox2City.SelectedItem.ToString(), int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //CIUDADES Y CANTESTRELLAS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridCiudadesYCantEstrellasTodos(comboBox2City.SelectedItem.ToString(), int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();

                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CIUDADES
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem != null
                        && textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals(""))
                    {
                        CargarDataGridCiudadesTodos(comboBox2City.SelectedItem.ToString());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                    }
                    //CIUDADES Y ENTRE PRECIOS
                    else if (comboBox3CantPersonas.SelectedItem == null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        && comboBox4CantEstrellas.SelectedItem == null
                        || comboBox3CantPersonas.SelectedItem == null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem != null
                        && comboBox4CantEstrellas.SelectedItem == null
                        )
                    {
                        CargarDataGridCiudadesYPreciosTodos(
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()),
                            comboBox2City.SelectedItem.ToString());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                    }
                    //ENTRE PRECIOS
                    else if (comboBox3CantPersonas.SelectedItem == null
                        && !textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem == null
                        && comboBox4CantEstrellas.SelectedItem == null
                        || comboBox3CantPersonas.SelectedItem == null
                        && textBox1PrecioMin.Text.Trim().Equals("")
                        && !textBox2PrecioMax.Text.Trim().Equals("")
                        && comboBox2City.SelectedItem == null
                        && comboBox4CantEstrellas.SelectedItem == null
                        )
                    {
                        CargarDataGridCPreciosTodos(
                            float.Parse(textBox1PrecioMin.Text.Trim().ToString()),
                            float.Parse(textBox2PrecioMax.Text.Trim().ToString()));
                    }

                }
                else if (selectedItemAloj.Equals(""))
                {
                                    MessageBox.Show("Hubo un error. Intente nuevamente");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al intentar filtrar, por favor intente nuevamente");
            }
        }
            
        private void BorrarFiltros()
        {
            dataGridUser.Rows.Clear();
            CargarDataGridUser();
            button3Filtrar.Enabled = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            BorrarFiltros();
            comboBox1TypeOfAloj.SelectedItem = "Todos";
            comboBox2City.SelectedItem = null;
            comboBox3CantPersonas.SelectedItem = null;
            comboBox4CantEstrellas.SelectedItem = null;
            textBox1PrecioMin.Text = "0";
            textBox2PrecioMax.Text = "";
        }

        private void comboBox1TypeOfAloj_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLimpiarIngresoCabaña_Click(object sender, EventArgs e)
        {
            LimpiarInputsCabaña();
        }

        private void LimpiarInputsCabaña() {
            txtCabañaCodigo.Text = "";
            txtCabañaNombre.Text = "";
            txtCabañaCiudad.Text = "";
            txtCabañaBarrio.Text = "";
            txtCabañaPersonas.Text = "";
            txtCabañaPrecioDia.Text = "";
            txtCabañasBaños.Text = "";
            txtCabañasHabitaciones.Text = "";
            numCabañaEstrellas.Value = 1;

        }
        private void LimpiarInputsHotel()
        {
            this.tabControl2.SelectedIndex = 1;
            txtHotelNombre.Text = "";
            txtHotelCiudad.Text = "";
            txtHotelBarrio.Text = "";
            txtHotelCantPersonas.Text = "";
            checkHotelTv.Checked = false;
            numHotelEstrellas.Value = 1;
            txtHotelPrecioPersona.Text = "";
            txtHotelCodigo.Text = "";
        }
        private void btnEditarCabaña_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Alojamiento a in manager.getMiAgencia().getAlojamientos())
                {
                    if (this.codigo == a.getCodigo())

                        manager.modificarCabanias(codigo,
                        txtCabañaNombre.Text,
                        txtCabañaCiudad.Text,
                        txtCabañaBarrio.Text,
                        int.Parse(numCabañaEstrellas.Value.ToString()),
                        int.Parse(txtCabañaPersonas.Text),
                        checkCabañaTv.Checked,
                        int.Parse(txtCabañaPrecioDia.Text),
                        int.Parse(txtCabañasHabitaciones.Text),
                        int.Parse(txtCabañasBaños.Text));
                }
                //FGM_ PALOMA VES ESTO?
                manager.GuardarDatosCabaña();
                MessageBox.Show("Se ha editado la cabaña con éxito");
                dataGridAdmin.Rows.Clear();
                CargarDataGridAdmin();
            }
            catch
            {
                MessageBox.Show("No se pudo editar la cabaña, pruebe nuevamente.");
            }
        }
        private void dataGridAdmin_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridAdmin.Rows[e.RowIndex].Cells[1].Value != null)
            {
                if (dataGridAdmin.Rows[e.RowIndex].Cells[1].Value.ToString() == "Cabaña")
                {
                    this.tabControl2.SelectedIndex = 0;
                    //RELLENA LOS VALORES DEL FORMULARIO DE CABAÑAS
                    this.codigo = int.Parse(dataGridAdmin.Rows[e.RowIndex].Cells[0].Value.ToString());
                    txtCabañaCodigo.Text = codigo.ToString();
                    numCabañaEstrellas.Value = int.Parse(dataGridAdmin.Rows[e.RowIndex].Cells[2].Value.ToString());
                    txtCabañaNombre.Text = dataGridAdmin.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtCabañaCiudad.Text = dataGridAdmin.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtCabañaBarrio.Text = dataGridAdmin.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtCabañaPersonas.Text = dataGridAdmin.Rows[e.RowIndex].Cells[6].Value.ToString();
                    if (dataGridAdmin.Rows[e.RowIndex].Cells[7].Value.ToString() == "Si")
                    {
                        checkCabañaTv.Checked = true;
                    }
                    else
                    {
                        checkCabañaTv.Checked = false;
                    }
                    txtCabañaPrecioDia.Text = dataGridAdmin.Rows[e.RowIndex].Cells[8].Value.ToString();
                    txtCabañasHabitaciones.Text = dataGridAdmin.Rows[e.RowIndex].Cells[9].Value.ToString();
                    txtCabañasBaños.Text = dataGridAdmin.Rows[e.RowIndex].Cells[10].Value.ToString();
                }
                else if (dataGridAdmin.Rows[e.RowIndex].Cells[1].Value.ToString() == "Hotel")
                {
                    this.tabControl2.SelectedIndex = 1;
                    //RELLENA LOS VALORES DEL FORMULARIO DE HOTELES
                    this.codigo = int.Parse(dataGridAdmin.Rows[e.RowIndex].Cells[0].Value.ToString());
                    txtHotelCodigo.Text = codigo.ToString();
                    numHotelEstrellas.Value = int.Parse(dataGridAdmin.Rows[e.RowIndex].Cells[2].Value.ToString());
                    txtHotelNombre.Text = dataGridAdmin.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtHotelCiudad.Text = dataGridAdmin.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtHotelBarrio.Text = dataGridAdmin.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtHotelCantPersonas.Text = dataGridAdmin.Rows[e.RowIndex].Cells[6].Value.ToString();
                    if (dataGridAdmin.Rows[e.RowIndex].Cells[7].Value.ToString() == "Si")
                    {
                        checkHotelTv.Checked = true;
                    }
                    else
                    {
                        checkHotelTv.Checked = false;
                    }
                    txtHotelPrecioPersona.Text = dataGridAdmin.Rows[e.RowIndex].Cells[8].Value.ToString();
                    dataGridAdmin.Rows[e.RowIndex].Cells[9].Value = "1";
                    dataGridAdmin.Rows[e.RowIndex].Cells[10].Value = "1";
                }
            }
            else
            {
                LimpiarInputsHotel();
                LimpiarInputsCabaña();
            }
        }

        private void btnBorrarCabaña_Click(object sender, EventArgs e)
        {
            try
            {
                manager.eliminarCabania(this.codigo);
                manager.GuardarDatosCabaña();
                dataGridAdmin.Rows.Clear();
                CargarDataGridAdmin();
                MessageBox.Show("Se ha borrado el alojamiento con éxito");
            }
            catch
            {
                MessageBox.Show("No se pudo borrar el alojamiento, pruebe nuevamente.");
            }
        }

        private void btnLimpiarHotel_Click(object sender, EventArgs e)
        {
            LimpiarInputsHotel();
        }

        private void btnBorrarHotel_Click(object sender, EventArgs e)
        {
            try
            {
                manager.eliminarHotel(this.codigo);
                manager.GuardarDatosHoteles();
                MessageBox.Show("Se ha borrado el alojamiento con éxito");
                dataGridAdmin.Rows.Clear();
                CargarDataGridAdmin();
            }
            catch
            {
                MessageBox.Show("No se pudo borrar el alojamiento, pruebe nuevamente.");
            }
        }

        private void btnEditarHotel_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Alojamiento a in manager.getMiAgencia().getAlojamientos())
                {
                    if (this.codigo == a.getCodigo())
                        manager.modificarHoteles(codigo,
                            txtHotelNombre.Text,
                            txtHotelCiudad.Text,
                            txtHotelBarrio.Text,
                            int.Parse(numHotelEstrellas.Value.ToString()),
                            int.Parse(txtHotelCantPersonas.Text),
                            checkHotelTv.Checked,
                            int.Parse(txtHotelPrecioPersona.Text));
                }
                //FGM_ PALOMA PODES VER ESTO POR FAVOR?
                manager.GuardarDatosHoteles();
                MessageBox.Show("Se ha editado el hotel con éxito");
                dataGridAdmin.Rows.Clear();
                CargarDataGridAdmin();
            }
            catch
            {
                MessageBox.Show("No se pudo editar el hotel, pruebe nuevamente.");
            }
        }

        private void textBox2PrecioMax_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridABMUsuarios_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridABMUsuarios.Rows[e.RowIndex].Cells[0].Value != null)
            {
                //RELLENA LOS VALORES DEL FORMULARIO DE USUARIOS
                txtABMUsuariosDNI.Text = dataGridABMUsuarios.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtABMUsuariosNombre.Text = dataGridABMUsuarios.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtABMUsuariosMail.Text = dataGridABMUsuarios.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtABMUsuariosPass.Text = dataGridABMUsuarios.Rows[e.RowIndex].Cells[3].Value.ToString();
                checkABMUsuariosAdmin.Checked = bool.Parse(dataGridABMUsuarios.Rows[e.RowIndex].Cells[4].Value.ToString());
                checkABMUsuariosBloqueado.Checked = bool.Parse(dataGridABMUsuarios.Rows[e.RowIndex].Cells[5].Value.ToString());
                btnCrearUsr.Visible = false;
                this.dniUserSelect = dataGridABMUsuarios.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            else {
                LimpiarInputsABMUsuarios();
            }
        }

        private void btnBorrarUser_Click(object sender, EventArgs e)
        {
            if (manager.eliminarUsuario(int.Parse(dniUserSelect)))
            {
                MessageBox.Show("Usuario borrado correctamente");
            }
            LimpiarInputsABMUsuarios();
            CargarDataGridABMUsuarios();
        }

        private void btnEditarUser_Click(object sender, EventArgs e)
        {
            manager.eliminarUsuario(int.Parse(dniUserSelect));
            manager.agregarUsuario(
                int.Parse(txtABMUsuariosDNI.Text),
                txtABMUsuariosNombre.Text,
                txtABMUsuariosMail.Text,
                txtABMUsuariosPass.Text,
                checkABMUsuariosAdmin.Checked,
                checkABMUsuariosBloqueado.Checked);

            MessageBox.Show("Usuario editado correctamente");
            //FGM_ PALOMA, VES ESTE METODO? O TE TIRO VINAGRE?
            manager.GuardarDatosUsuarios();
            LimpiarInputsABMUsuarios();
            CargarDataGridABMUsuarios();
        }

        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            LimpiarInputsABMUsuarios();
            btnCrearUsr.Visible = true;
            btnVolverUser.Visible = true;
            btnBorrarUser.Visible = false;
            btnEditarUser.Visible = false;
            btnNuevoUsuario.Visible = false;
        }

        private void btnVolverUser_Click(object sender, EventArgs e)
        {
            btnCrearUsr.Visible = false;
            btnVolverUser.Visible = false;
            btnBorrarUser.Visible = true;
            btnEditarUser.Visible = true;
            btnNuevoUsuario.Visible = true;
        }

        private void MostrarDiasTotales()
        {
            DateTime fechaDeIda = dateTimeIda.Value;
            DateTime fechaDeVuelta = dateTimeVuelta.Value;

            int diasDeDiferencia = (fechaDeVuelta - fechaDeIda).Days;

            if (diasDeDiferencia <= 0)
            {
                labelDiasTotales.Text = "-";
                return;
            }
            labelDiasTotales.Text = diasDeDiferencia.ToString();
        }

        private void dateTimeVuelta_ValueChanged(object sender, EventArgs e)
        {
            MostrarDiasTotales();
        }

        private void dateTimeIda_ValueChanged(object sender, EventArgs e)
        {
            MostrarDiasTotales();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

            
}
    

