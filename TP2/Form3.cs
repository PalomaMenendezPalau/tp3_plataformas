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
            
            if (userLog[1].Equals("false"))
                {
                    this.mainTabControl.TabPages.Remove(tabAdmin);
                }
            CargarDataGridAdmin(manager.getMisHoteles(),manager.getMisCabanias());
            CargarDataGridUser(manager.getMisHoteles(), manager.getMisCabanias());
            CargarDataGridABMUsuarios();
            CargarDataGridReservas();
            ComboBoxUsuario();
        }

        public void ComboBoxUsuario()
        {
            foreach (string ciudad in manager.MostrarCiudad())
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
                 dataGridAdmin.Rows.Clear();
                 CargarDataGridAdmin(manager.getMisHoteles(),manager.getMisCabanias());
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
//                        GuardarDatosHoteles();
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
                        CargarDataGridAdmin(manager.getMisHoteles(), manager.getMisCabanias());
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
            dataGridABMUsuarios.Rows.Clear();
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

        private void CargarDTGAdminCabañas(List<Cabaña> cabañas) {
            foreach (Cabaña c in cabañas)
            {
                string aTv;
                int n = dataGridAdmin.Rows.Add();
                if (c.getTV()) { aTv = "Si"; } else { aTv = "No"; }

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
        private void CargarDTGAdminHoteles(List<Hotel> hoteles)
        {
            
            foreach (Hotel h in hoteles)
            {
                string aTv;
                int n = dataGridAdmin.Rows.Add();
                if (h.getTV()) { aTv = "Si"; } else { aTv = "No"; }

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
        }

        private void CargarDataGridAdmin(List<Hotel> hoteles, List<Cabaña> cabañas) {
            CargarDTGAdminCabañas(cabañas);
            CargarDTGAdminHoteles(hoteles);
        }
        private void CargarDataGridUser(List<Hotel> hoteles, List<Cabaña> cabañas)
        {
            dataGridUser.Rows.Clear();
            CargarDTGUserCabañas(cabañas);
            CargarDTGUserHoteles(hoteles);
        }
        private void CargarDataGridUserFiltro(List<Alojamiento> alojs)
        {
            List<Cabaña> cabañas = new List<Cabaña>();
            List<Hotel> hoteles = new List<Hotel>();
            foreach (Alojamiento a in alojs)
            {
                if (a is Cabaña)
                {
                    Cabaña c = (Cabaña)a;
                    cabañas.Add(new Cabaña(c.getCodigo(),
                        c.getNombre(),
                        c.getCiudad(),
                        c.getBarrio(),
                        c.getEstrellas(),
                        c.getCantPersonas(),
                        c.getTV(),
                        c.getPrecioDia(),
                        c.getHabitaciones(),
                        c.getBaños()));
                }
                else 
                {
                    Hotel h = (Hotel)a;
                    hoteles.Add(new Hotel(h.getCodigo(),
                        h.getNombre(),
                        h.getCiudad(),
                        h.getBarrio(),
                        h.getEstrellas(),
                        h.getCantPersonas(),
                        h.getTV(),
                        h.getPrecioPorPersona()));
                }
            }
            dataGridUser.Rows.Clear();
            CargarDTGUserCabañas(cabañas);
            CargarDTGUserHoteles(hoteles);
        }

        private void CargarDataGridUserFiltroHoteles(List<Alojamiento> alojs)
        {
            List<Hotel> hoteles = new List<Hotel>();
            foreach (Alojamiento a in alojs)
            {
                if (a is Hotel)
                {
                    Hotel h = (Hotel)a;
                    hoteles.Add(new Hotel(h.getCodigo(),
                        h.getNombre(),
                        h.getCiudad(),
                        h.getBarrio(),
                        h.getEstrellas(),
                        h.getCantPersonas(),
                        h.getTV(),
                        h.getPrecioPorPersona()));
                }
            }
            dataGridUser.Rows.Clear();
            CargarDTGUserHoteles(hoteles);
        }

        private void CargarDataGridUserFiltroCabañas(List<Alojamiento> alojs)
        {
            List<Cabaña> cabañas = new List<Cabaña>();
            foreach (Alojamiento a in alojs)
            {
                if (a is Cabaña)
                {
                    Cabaña c = (Cabaña)a;
                    cabañas.Add(new Cabaña(c.getCodigo(),
                        c.getNombre(),
                        c.getCiudad(),
                        c.getBarrio(),
                        c.getEstrellas(),
                        c.getCantPersonas(),
                        c.getTV(),
                        c.getPrecioDia(),
                        c.getHabitaciones(),
                        c.getBaños()));
                }
            }
            dataGridUser.Rows.Clear();
            CargarDTGUserCabañas(cabañas);
        }

        //_FGM PRUEBA DE CARGA DATAGRIDUSER
        private void CargarDTGUserCabañas(List<Cabaña> cabañas) {
            foreach (Cabaña c in cabañas)
            {
                int n = dataGridUser.Rows.Add();
                string aTv;
                if (c.getTV()) { aTv = "Si"; } else { aTv = "No"; }
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
        private void CargarDTGUserHoteles(List<Hotel> hoteles)
        {
            foreach (Hotel h in hoteles)
            {
                int n = dataGridUser.Rows.Add();
                string aTv;
                if (h.getTV()) { aTv = "Si"; } else { aTv = "No"; }
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
            
            float precio = float.Parse(dataGridUser.Rows[dataGridUser.CurrentCell.RowIndex].Cells["Precio"].Value.ToString());

            if (dataGridUser.Columns[e.ColumnIndex].Name == "Reservar")
            {
                int filaSeleccionada = dataGridUser.CurrentCell.RowIndex;
                int codigoSeleccionado = int.Parse(dataGridUser.Rows[filaSeleccionada].Cells[0].Value.ToString()); 
                string tipoAloj = dataGridUser.Rows[filaSeleccionada].Cells[1].Value.ToString();

                if (tipoAloj.Equals("Hotel"))
                {
                    foreach (Hotel h in manager.getMisHoteles())
                        if (codigoSeleccionado == h.getCodigo())
                            manager.agregarReserva(
                                manager.getMisReservas().Count + 1,
                                dateTimeIda.Value,
                                dateTimeVuelta.Value,
                                (Alojamiento)h,
                                manager.buscarUsuarios(int.Parse(userLog[0])),
                                (precio * int.Parse(labelDiasTotales.Text))
                                );
                }
                else
                {
                    foreach (Cabaña c in manager.getMisCabanias())
                        if (codigoSeleccionado == c.getCodigo())
                            manager.agregarReserva(
                                manager.getMisReservas().Count + 1,
                                dateTimeIda.Value,
                                dateTimeVuelta.Value,
                                (Alojamiento)c,
                                manager.buscarUsuarios(int.Parse(userLog[0])),
                                (precio * int.Parse(labelDiasTotales.Text))
                                );
                }
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
                    CargarDTGUserCabañas(manager.getMisCabanias());

                    if (textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals("0"))
                    {
                        MessageBox.Show("No hay alojamientos en ese rango de precios.");
                        button3Filtrar.Enabled = true;
                        dataGridUser.Rows.Clear();
                        CargarDataGridUser(manager.getMisHoteles(), manager.getMisCabanias());
                    }
                    else if (!textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals("0"))
                    {
                        MessageBox.Show("El precio maximo tiene que ser mayor al minimo.");
                        button3Filtrar.Enabled = true;
                        dataGridUser.Rows.Clear();
                        CargarDataGridUser(manager.getMisHoteles(), manager.getMisCabanias());
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

                        CargarDataGridUserFiltroCabañas(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString())).getAlojamientos());

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
                        CargarDataGridUserFiltroCabañas(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .getAlojamientos());

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
                        CargarDataGridUserFiltroCabañas(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .getAlojamientos());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //SIN PONER PRECIOS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridUserFiltroCabañas(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .getAlojamientos());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SIN PONER PRECIOS, Y TAMPOCO CIUDAD
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridUserFiltroCabañas(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .MasEstrellas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .getAlojamientos());

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CANTIDAD DE PERSONAS
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridUserFiltroCabañas(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .getAlojamientos());

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CANTIDAD DE ESTRELLAS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridUserFiltroCabañas(manager.getMiAgencia()
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .getAlojamientos());

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //CIUDADES Y CANT PERSONAS
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridUserFiltroCabañas(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .getAlojamientos());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //CIUDADES Y CANTESTRELLAS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridUserFiltroCabañas(manager.getMiAgencia()
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .getAlojamientos());

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
                        CargarDataGridUserFiltroCabañas(manager.getMiAgencia()
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .getAlojamientos());

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
                        CargarDataGridUserFiltroCabañas(manager.getMiAgencia()
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .getAlojamientos());

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
                        CargarDataGridUserFiltroCabañas(manager.getMiAgencia()
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .getAlojamientos());
                 
                }
            }
                /*  ************************************************************************************************************************************************
                    ************************************************************************************************************************************************
                    ************************************************************************************************************************************************
                                    SI FILTRAMOS POR HOTEL          */
                else if (selectedItemAloj.Equals("Hotel"))
                {
                    CargarDTGUserHoteles(manager.getMisHoteles());
                    if (textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals("0"))
                    {
                        MessageBox.Show("No hay alojamientos en ese rango de precios.");
                        button3Filtrar.Enabled = true;
                        dataGridUser.Rows.Clear();
                        CargarDataGridUser(manager.getMisHoteles(), manager.getMisCabanias());
                    }
                    else if (!textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals("0"))
                    {
                        MessageBox.Show("El precio maximo tiene que ser mayor al minimo.");
                        button3Filtrar.Enabled = true;
                        dataGridUser.Rows.Clear();
                        CargarDataGridUser(manager.getMisHoteles(), manager.getMisCabanias());
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
                        CargarDataGridUserFiltroHoteles(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .getAlojamientos());

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
                        CargarDataGridUserFiltroHoteles(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .getAlojamientos());

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
                        CargarDataGridUserFiltroHoteles(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .getAlojamientos());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //SIN PONER PRECIOS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridUserFiltroHoteles(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .getAlojamientos());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SIN PONER PRECIOS, Y TAMPOCO CIUDAD
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridUserFiltroHoteles(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .MasEstrellas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .getAlojamientos());

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CANTIDAD DE PERSONAS
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridUserFiltroHoteles(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .getAlojamientos());

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CANTIDAD DE ESTRELLAS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridUserFiltroHoteles(manager.getMiAgencia()
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .getAlojamientos());

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //CIUDADES Y CANT PERSONAS
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridUserFiltroHoteles(manager.getMiAgencia()
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .getAlojamientos());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //CIUDADES Y CANTESTRELLAS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridUserFiltroHoteles(manager.getMiAgencia()
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .getAlojamientos());

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
                        CargarDataGridUserFiltroHoteles(manager.getMiAgencia()
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .getAlojamientos());

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
                        CargarDataGridUserFiltroHoteles(manager.getMiAgencia()
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .getAlojamientos());

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
                        CargarDataGridUserFiltroHoteles(manager.getMiAgencia()
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .getAlojamientos());
                    }
                }
                /*  ************************************************************************************************************************************************
                    ************************************************************************************************************************************************
                    ************************************************************************************************************************************************
                                    SI FILTRAMOS POR TODOS          */

                else if (selectedItemAloj.Equals("Todos"))
                {
                    CargarDataGridUser(manager.getMisHoteles(), manager.getMisCabanias());
                    if (textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals("0"))
                    {
                        MessageBox.Show("No hay alojamientos en ese rango de precios.");
                        button3Filtrar.Enabled = true;
                        dataGridUser.Rows.Clear();
                        CargarDataGridUser(manager.getMisHoteles(), manager.getMisCabanias());
                    }
                    else if(!textBox1PrecioMin.Text.Trim().Equals("0")
                        && textBox2PrecioMax.Text.Trim().Equals("0"))
                    {
                        MessageBox.Show("El precio maximo tiene que ser mayor al minimo.");
                        button3Filtrar.Enabled = true;
                        dataGridUser.Rows.Clear();
                        CargarDataGridUser(manager.getMisHoteles(), manager.getMisCabanias());
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
                        CargarDataGridUserFiltro(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .getAlojamientos());

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
                        CargarDataGridUserFiltro(manager.getMiAgencia()
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .getAlojamientos());

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
                        CargarDataGridUserFiltro(manager.getMiAgencia()
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .getAlojamientos());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //SIN PONER PRECIOS
                    else if(comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridUserFiltro(manager.getMiAgencia()
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .getAlojamientos());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SIN PONER PRECIOS, Y TAMPOCO CIUDAD
                    else if(comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridUserFiltro(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .getAlojamientos());

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                        comboBox4CantEstrellas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CANTIDAD DE PERSONAS
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridUserFiltro(manager.getMiAgencia()
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .getAlojamientos());

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //SOLAMENTE CANTIDAD DE ESTRELLAS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem == null)
                    {
                        CargarDataGridUserFiltro(manager.getMiAgencia()
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .getAlojamientos());

                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()));
                    }
                    //CIUDADES Y CANT PERSONAS
                    else if (comboBox4CantEstrellas.SelectedItem == null
                        && comboBox3CantPersonas.SelectedItem != null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridUserFiltro(manager.getMiAgencia()
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .AlojamientosPorCantidadDePersonas(int.Parse(comboBox3CantPersonas.SelectedItem.ToString()))
                            .getAlojamientos());

                        comboBox2City.SelectedItem = comboBox2City.SelectedItem.ToString();
                        comboBox3CantPersonas.SelectedItem = (int.Parse(comboBox3CantPersonas.SelectedItem.ToString()));
                    }
                    //CIUDADES Y CANTESTRELLAS
                    else if (comboBox4CantEstrellas.SelectedItem != null
                        && comboBox3CantPersonas.SelectedItem == null
                        && comboBox2City.SelectedItem != null)
                    {
                        CargarDataGridUserFiltro(manager.getMiAgencia()
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .MasEstrellas(int.Parse(comboBox4CantEstrellas.SelectedItem.ToString()))
                            .getAlojamientos());

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
                        CargarDataGridUserFiltro(manager.getMiAgencia()
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .getAlojamientos());

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
                        CargarDataGridUserFiltro(manager.getMiAgencia()
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .CiudadesDeAlojamientos(comboBox2City.SelectedItem.ToString())
                            .getAlojamientos());

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
                        CargarDataGridUserFiltro(manager.getMiAgencia()
                            .AlojamientosEntrePrecios(float.Parse(textBox2PrecioMax.Text.Trim().ToString()), float.Parse(textBox1PrecioMin.Text.Trim().ToString()))
                            .getAlojamientos());
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
            CargarDataGridUser(manager.getMisHoteles(), manager.getMisCabanias());
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

                MessageBox.Show("Se ha editado la cabaña con éxito");
                dataGridAdmin.Rows.Clear();
                CargarDataGridAdmin(manager.getMisHoteles(), manager.getMisCabanias());
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
                dataGridAdmin.Rows.Clear();
                CargarDataGridAdmin(manager.getMisHoteles(), manager.getMisCabanias());
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
                MessageBox.Show("Se ha borrado el alojamiento con éxito");
                dataGridAdmin.Rows.Clear();
                CargarDataGridAdmin(manager.getMisHoteles(), manager.getMisCabanias());
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

                MessageBox.Show("Se ha editado el hotel con éxito");
                dataGridAdmin.Rows.Clear();
                CargarDataGridAdmin(manager.getMisHoteles(), manager.getMisCabanias());
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