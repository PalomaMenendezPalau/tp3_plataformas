using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace TP2
{
    class AgenciaManager
    {
        protected Agencia miAgencia;
        protected List<Usuarios> misUsuarios = new List<Usuarios>();
        protected List<Reservas> misReservas = new List<Reservas>();
        protected List<Hotel> misHoteles = new List<Hotel>();
        protected List<Cabaña> misCabanias = new List<Cabaña>();
        private int intentos = 0;

        public AgenciaManager(Agencia agen) {
            miAgencia = agen;
            this.InicializarAtributos();
            foreach (Usuarios u in misUsuarios)
            {
                Console.WriteLine("DNI: "+u.getDni().ToString()+"Pass: "+u.getPassword()+"Nombre: "+u.getNombre());
            }
        }

        private void InicializarAtributos()
        {
            //Conexion a la bd
            string connectionString = Properties.Resources.connectionString;

            //query a realizar. 
            string queryString_hotel = "SELECT * FROM dbo.HOTELES";
            string queryString_cabanias = "SELECT * FROM dbo.CABANIAS";
            string queryString_users = "SELECT * FROM dbo.USUARIOS";
            string queryString_reserva = "SELECT * FROM dbo.RESERVAS";

            //creo la conexion SQL CON UN USING para liberar recursos.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //defino el comando a enviar al motor con consulta y conexxion
                SqlCommand command = new SqlCommand(queryString_hotel, connection);
                SqlCommand command1 = new SqlCommand(queryString_cabanias, connection);
                SqlCommand command2 = new SqlCommand(queryString_users, connection);
                SqlCommand command3 = new SqlCommand(queryString_reserva, connection);
                
                try
                {
                    //abro mi conexion
                    connection.Open();

                    Hotel aux_hotel;
                    Cabaña aux_cabanias;
                    Usuarios aux_users;
                    Reservas aux_reserva;

                    /*************************
                                 HOTELES*/
                    SqlDataReader reader_hotel = command.ExecuteReader();
                    while (reader_hotel.Read())
                    {
                        if (reader_hotel.GetByte(6).ToString().Equals("1"))
                        {
                            aux_hotel = new Hotel(reader_hotel.GetInt32(0), reader_hotel.GetString(1), reader_hotel.GetString(2), reader_hotel.GetString(3),
                                reader_hotel.GetInt32(4), reader_hotel.GetInt32(5), true, float.Parse(reader_hotel.GetDouble(7).ToString()));
                            misHoteles.Add(aux_hotel);
                        }
                        else {
                            aux_hotel = new Hotel(reader_hotel.GetInt32(0), reader_hotel.GetString(1), reader_hotel.GetString(2), reader_hotel.GetString(3),
                             reader_hotel.GetInt32(4), reader_hotel.GetInt32(5), false, float.Parse(reader_hotel.GetDouble(7).ToString()));
                            misHoteles.Add(aux_hotel);
                        }
                    }
                        reader_hotel.Close();
                    /*************************
                                 CABANIAS*/
                    SqlDataReader reader_cabanias = command1.ExecuteReader();
                    while (reader_cabanias.Read()) 
                    {
                        if (reader_cabanias.GetInt32(6).ToString().Equals("1")) { 
                           aux_cabanias = new Cabaña(
                                reader_cabanias.GetInt32(0), reader_cabanias.GetString(1), reader_cabanias.GetString(2), reader_cabanias.GetString(3),
                                reader_cabanias.GetInt32(4), reader_cabanias.GetInt32(5), true, float.Parse(reader_cabanias.GetDouble(7).ToString()),
                                reader_cabanias.GetInt32(8), reader_cabanias.GetInt32(9));
                        misCabanias.Add(aux_cabanias);
                        }
                        else
                        {
                            aux_cabanias = new Cabaña(
                                 reader_cabanias.GetInt32(0), reader_cabanias.GetString(1), reader_cabanias.GetString(2), reader_cabanias.GetString(3),
                                 reader_cabanias.GetInt32(4), reader_cabanias.GetInt32(5), false, float.Parse(reader_cabanias.GetDouble(7).ToString()),
                                 reader_cabanias.GetInt32(8), reader_cabanias.GetInt32(9));
                        misCabanias.Add(aux_cabanias);
                        }
                    }
                        reader_cabanias.Close();

                    /*************************
                                 USUARIOS*/
                    SqlDataReader reader_users = command2.ExecuteReader();
                        while (reader_users.Read())
                        {
                        if (reader_users.GetByte(4).ToString().Equals("1"))
                        { 
                            if (reader_users.GetByte(5).ToString().Equals("1"))
                            {
                                aux_users = new Usuarios(reader_users.GetInt32(0), reader_users.GetString(1), reader_users.GetString(2), reader_users.GetString(3), true, true);
                                misUsuarios.Add(aux_users);                                    
                            }
                            else
                            {
                                aux_users = new Usuarios(reader_users.GetInt32(0), reader_users.GetString(1), reader_users.GetString(2), reader_users.GetString(3), true, false);
                                misUsuarios.Add(aux_users);
                            }
                        }
                        else
                        {
                            if (reader_users.GetByte(5).ToString().Equals("1"))
                            {
                                aux_users = new Usuarios(reader_users.GetInt32(0), reader_users.GetString(1), reader_users.GetString(2), reader_users.GetString(3), false, true);
                                misUsuarios.Add(aux_users);
                            }
                            else
                            {
                                aux_users = new Usuarios(reader_users.GetInt32(0), reader_users.GetString(1), reader_users.GetString(2), reader_users.GetString(3), false, false);
                                misUsuarios.Add(aux_users);
                            }
                        }

                        }
                    reader_users.Close();
                    /*************************
                                 RESERVAS*/
                    SqlDataReader reader_reserva = command3.ExecuteReader();
                    while (reader_reserva.Read())
                    {

                        if (todosUsuarios(reader_reserva.GetInt32(4)) &&
                            todosHoteles(reader_reserva.GetInt32(3)))
                        {
                            aux_reserva = new Reservas(reader_reserva.GetInt32(0), reader_reserva.GetDateTime(1), reader_reserva.GetDateTime(2), getHotelXCodigo(reader_reserva.GetInt32(3)),
                          getUsuarioXDni(reader_reserva.GetInt32(4)), reader_reserva.GetFloat(5));
                            misReservas.Add(aux_reserva);
                        }
                        else if (todosUsuarios(reader_reserva.GetInt32(4)) &&
                          todosHoteles(reader_reserva.GetInt32(3)))
                        {
                            aux_reserva = new Reservas(reader_reserva.GetInt32(0), reader_reserva.GetDateTime(1), reader_reserva.GetDateTime(2), getCabaniaXCodigo(reader_reserva.GetInt32(3)),
                        getUsuarioXDni(reader_reserva.GetInt32(4)), reader_reserva.GetFloat(5));
                            misReservas.Add(aux_reserva);
                        }
                    }
                            reader_reserva.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Algo salio mal perritos");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }

        }

        // METODOS DE AGREGAR --

        // agregar Hotel
        public bool agregarHotel(int Codigo, string Nombre, string Ciudad, string Barrio, int Estrellas, int CantPersonas, bool Tv, float PrecioPP)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string connectionString = Properties.Resources.connectionString;
            string queryString = "INSERT INTO [dbo].[HOTELES] ([CODIGO],[NOMBRE],[CIUDAD],[BARRIO],[ESTRELLAS],[CANT_PERSONAS],[TV],[PRECIO]) " +
                "VALUES (@codigo,@nombre,@ciudad,@barrio,@estrellas,@cantpersonas,@tv,@preciopp);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@ciudad", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@barrio", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@estrellas", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@cantpersonas", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@tv", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@preciopp", SqlDbType.Float));
                command.Parameters["@codigo"].Value = Codigo;
                command.Parameters["@nombre"].Value = Nombre;
                command.Parameters["@ciudad"].Value = Ciudad;
                command.Parameters["@barrio"].Value = Barrio;
                command.Parameters["@estrellas"].Value = Estrellas;
                command.Parameters["@cantpersonas"].Value = CantPersonas;
                command.Parameters["@tv"].Value = Tv;
                command.Parameters["@preciopp"].Value = PrecioPP;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            if (resultadoQuery == 1)
            {
                //Ahora sí lo agrego en la lista
                Hotel nuevo = new Hotel(Codigo, Nombre, Ciudad, Barrio, Estrellas, CantPersonas, Tv, PrecioPP);
                misHoteles.Add(nuevo);
                return true;

            }
            else
            {
                //algo salió mal con la query porque no generó 1 registro
                return false;
            }
        }

        //FIN METODO AGREGAR HOTEL

        //METODO AGREGAR CABAÑA
        public bool agregarCabania(int Codigo, string Nombre, string Ciudad, string Barrio, int Estrellas,
            int CantPersonas, bool Tv, float PrecioDIA, int Habitaciones, int Baños)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string connectionString = Properties.Resources.connectionString;
            string queryString = "INSERT INTO [dbo].[CABANIAS] ([CODIGO],[NOMBRE],[CIUDAD],[BARRIO],[ESTRELLAS],[CANT_PERSONAS],[TV],[PRECIO_DIA],[HABITACIONES],[BAÑOS]) " +
                "VALUES (@codigo,@nombre,@ciudad,@barrio,@estrellas,@cantpersonas,@tv,@preciodia,@habitaciones,@baños);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@ciudad", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@barrio", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@estrellas", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@cantpersonas", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@tv", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@preciodia", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@habitaciones", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@baños", SqlDbType.Int));
                command.Parameters["@codigo"].Value = Codigo;
                command.Parameters["@nombre"].Value = Nombre;
                command.Parameters["@ciudad"].Value = Ciudad;
                command.Parameters["@barrio"].Value = Barrio;
                command.Parameters["@estrellas"].Value = Estrellas;
                command.Parameters["@cantpersonas"].Value = CantPersonas;
                command.Parameters["@tv"].Value = Tv;
                command.Parameters["@preciodia"].Value = PrecioDIA;
                command.Parameters["@habitaciones"].Value = Habitaciones;
                command.Parameters["@baños"].Value = Baños;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            if (resultadoQuery == 1)
            {
                //Ahora sí lo agrego en la lista
                Cabaña nuevo = new Cabaña(Codigo, Nombre, Ciudad, Barrio, Estrellas, CantPersonas, Tv, PrecioDIA, Habitaciones, Baños);
                misCabanias.Add(nuevo);
                return true;

            }
            else
            {
                //algo salió mal con la query porque no generó 1 registro
                return false;
            }
        }
        // fin metodo AGREGAR CABANIA

        //agregar USUARIO
        public bool agregarUsuario(int Dni, string Nombre, string Mail, string Password, bool EsAdmin, bool Bloqueado)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string connectionString = Properties.Resources.connectionString;
            string queryString = "INSERT INTO [dbo].[USUARIOS] ([DNI],[MAIL],[NOMBRE],[PASSWORD],[ES_ADMIN],[BLOQUEADO]) VALUES (@dni,@nombre,@mail,@password,@esadm,@bloqueado);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@mail", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@esadm", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@bloqueado", SqlDbType.Bit));
                command.Parameters["@dni"].Value = Dni;
                command.Parameters["@nombre"].Value = Nombre;
                command.Parameters["@mail"].Value = Mail;
                command.Parameters["@password"].Value = Password;
                command.Parameters["@esadm"].Value = EsAdmin;
                command.Parameters["@bloqueado"].Value = Bloqueado;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            if (resultadoQuery == 1)
            {
                //Ahora sí lo agrego en la lista
                Usuarios nuevo = new Usuarios(Dni, Nombre, Mail, Password, EsAdmin, Bloqueado);
                misUsuarios.Add(nuevo);
                return true;
            }
            else
            {
                //algo salió mal con la query porque no generó 1 registro
                return false;
            }
        }

        //fin agregar usuarios

        //agregar RESERVA

        public bool agregarReserva(int ID, DateTime FDesde, DateTime FHasta, Alojamiento Propiedad, Usuarios Persona, float Precio)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string connectionString = Properties.Resources.connectionString;
            string queryString = "INSERT INTO [dbo].[RESERVAS] ([ID],[FECHA_DESDE],[FECHA_HASTA],[DNI_USUARIO],[CODIGO_CABANIA],[CODIGO_HOTEL],[PRECIO_TOTAL])" +
                " VALUES (@id,@fdesde,@fhasta,@usuario,@cabania,@hotel,@precio);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@fdesde", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@fhasta", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@usuario", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@cabania", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@hotel", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@precio", SqlDbType.Float));
                command.Parameters["@id"].Value = ID;
                command.Parameters["@fdesde"].Value = FDesde;
                command.Parameters["@fhasta"].Value = FHasta;
                command.Parameters["@usuario"].Value = Persona.getDni();
                if (Propiedad is Hotel)
                {
                    command.Parameters["@cabania"].Value = 0;
                    command.Parameters["@hotel"].Value = Propiedad.getCodigo();
                }
                else
                {
                    command.Parameters["@cabania"].Value = Propiedad.getCodigo();
                    command.Parameters["@hotel"].Value = 0;
                }
                command.Parameters["@precio"].Value = Precio;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                    if(resultadoQuery == 1)
                        misReservas.Add(new Reservas(ID, FDesde, FHasta, Propiedad, Persona, Precio));
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

        }

        // METODOS DE OBTENER

        //obtener HOTELES
        public List<List<string>> ObtenerHoteles()
        {
            List<List<string>> salida = new List<List<string>>();
            foreach (Hotel hotel in misHoteles)
            {
                salida.Add(new List<string>() {hotel.getCodigo().ToString(), hotel.getNombre(),hotel.getCiudad(),hotel.getBarrio(),
                        hotel.getEstrellas().ToString(),hotel.getCantPersonas().ToString(),hotel.getTV().ToString(),hotel.getPrecioPorPersona().ToString()});


            }
            return salida;
        }
        // FIN METODO OBTENER HOTELES

        //METODO OBTENER CABAÑAS

        public List<List<string>> ObtenerCabanias()
        {
            List<List<string>> salida = new List<List<string>>();
            foreach (Cabaña cabania in misCabanias)
            {
                salida.Add(new List<string>() {cabania.getCodigo().ToString(),cabania.getNombre(),cabania.getCiudad(),cabania.getBarrio(),
                        cabania.getEstrellas().ToString(),cabania.getCantPersonas().ToString(),cabania.getTV().ToString(),
                    cabania.getPrecioDia().ToString(),cabania.getHabitaciones().ToString(),cabania.getBaños().ToString()});

            }
            return salida;
        }

        // fin METODO OBTENER CABANIAS

        //obtener USUARIOS
        public List<List<string>> ObtenerUsuarios()
        {
            List<List<string>> salida = new List<List<string>>();
            foreach (Usuarios u in misUsuarios)
            {
                salida.Add(new List<string>() {u.getDni().ToString(), u.getNombre(),u.getMail(),u.getPassword(),
                        u.getEsAdmin().ToString(),u.getBloqueado().ToString()});

            }
            return salida;
        }
        // FIN METODO OBTENER USUARIOS

        // METODO OBTENER RESERVAS
        public List<List<string>> ObtenerReservas()
        {
            List<List<string>> salida = new List<List<string>>();
            foreach (Reservas reserva in misReservas)
            {
                salida.Add(new List<string>() {reserva.getID().ToString(), reserva.getFDesde().ToString(),reserva.getFHasta().ToString(),reserva.getPropiedad().ToString(),
                        reserva.getPersona().ToString(),reserva.getPrecio().ToString()});


            }
            return salida;
        }
        // FIN METODO OBTENER RESERVAS
        // METODOS DE MODIFICAR 

           // modificar ussuario

        public bool modificarUsuario(int Dni, string Nombre, string Mail, string Password, bool EsAdmin, bool Bloqueado)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string connectionString = Properties.Resources.connectionString;
            string queryString = "UPDATE [dbo].[USUARIOS] SET NOMBRE=@nombre, MAIL=@mail,PASSWORD=@password, ES_ADMIN=@esadm, BLOQUEADO=@bloqueado WHERE DNI=@dni;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@mail", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@esadm", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@bloqueado", SqlDbType.Bit));
                command.Parameters["@dni"].Value = Dni;
                command.Parameters["@nombre"].Value = Nombre;
                command.Parameters["@mail"].Value = Mail;
                command.Parameters["@password"].Value = Password;
                command.Parameters["@esadm"].Value = EsAdmin;
                command.Parameters["@bloqueado"].Value = Bloqueado;
                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            if (resultadoQuery == 1)
            {
                try
                {
                    //Ahora sí lo MODIFICO en la lista
                    for (int i = 0; i < misUsuarios.Count; i++)
                        if (misUsuarios[i].getDni() == Dni)
                        {
                            misUsuarios[i].setNombre(Nombre);
                            misUsuarios[i].setMail(Mail);
                            misUsuarios[i].setPassword(Password);
                            misUsuarios[i].setEsAdmin(EsAdmin);
                            misUsuarios[i].setBloqueado(Bloqueado);
                        }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                //algo salió mal con la query porque no generó 1 registro
                return false;
            }
        }
        // fin metodo modificar Usuario

        //METODO MODIFICAR HOTELES
        public bool modificarHoteles(int Codigo, string Nombre, string Ciudad, string Barrio, int Estrellas, int CantPersonas, bool Tv, float PrecioPP)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string connectionString = Properties.Resources.connectionString;
            string queryString = "UPDATE [dbo].[HOTELES] SET NOMBRE=@nombre, CIUDAD=@ciudad,BARRIO=@barrio, ESTRELLAS=@estrellas, " +
                "CANT_PERSONAS=@cantpersonas, TV=@tv, PRECIO=@preciopp WHERE CODIGO=@codigo;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@ciudad", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@barrio", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@estrellas", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@cantpersonas", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@tv", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@preciopp", SqlDbType.Float));
                command.Parameters["@codigo"].Value = Codigo;
                command.Parameters["@nombre"].Value = Nombre;
                command.Parameters["@ciudad"].Value = Ciudad;
                command.Parameters["@barrio"].Value = Barrio;
                command.Parameters["@estrellas"].Value = Estrellas;
                command.Parameters["@cantpersonas"].Value = CantPersonas;
                command.Parameters["@tv"].Value = Tv;
                command.Parameters["@preciopp"].Value = PrecioPP;
                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            if (resultadoQuery == 1)
            {
                try
                {
                    //Ahora sí lo MODIFICO en la lista
                    for (int i = 0; i < misHoteles.Count; i++)
                        if (misHoteles[i].getCodigo() == Codigo)
                        {
                            misHoteles[i].setNombre(Nombre);
                            misHoteles[i].setCiudad(Ciudad);
                            misHoteles[i].setBarrio(Barrio);
                            misHoteles[i].setEstrellas(Estrellas);
                            misHoteles[i].setCantPersonas(CantPersonas);
                            misHoteles[i].setTV(Tv);
                            misHoteles[i].setPrecioPorPersona(PrecioPP);
                        }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                //algo salió mal con la query porque no generó 1 registro
                return false;
            }
        }
        // fin metodo modificar Hoteles

        //METODO MODIFICAR CABANIAS
        public bool modificarCabanias(int Codigo, string Nombre, string Ciudad, string Barrio, int Estrellas, int CantPersonas, bool Tv, float PrecioDIA, int Habitaciones, int Baños)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string connectionString = Properties.Resources.connectionString;
            string queryString = "UPDATE [dbo].[CABANIAS] SET NOMBRE=@nombre, CIUDAD=@ciudad,BARRIO=@barrio, ESTRELLAS=@estrellas, " +
                "CANT_PERSONAS=@cantpersonas, TV=@tv, PRECIO_DIA=@precioDia ,HABITACIONES=@habitaciones" +
                " ,BAÑOS =@banios WHERE CODIGO=@codigo;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@ciudad", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@barrio", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@estrellas", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@cantpersonas", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@tv", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@precioDia", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@habitaciones", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@banios", SqlDbType.Int));
                command.Parameters["@codigo"].Value = Codigo;
                command.Parameters["@nombre"].Value = Nombre;
                command.Parameters["@ciudad"].Value = Ciudad;
                command.Parameters["@barrio"].Value = Barrio;
                command.Parameters["@estrellas"].Value = Estrellas;
                command.Parameters["@cantpersonas"].Value = CantPersonas;
                command.Parameters["@tv"].Value = Tv;
                command.Parameters["@precioDia"].Value = PrecioDIA;
                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            if (resultadoQuery == 1)
            {
                try
                {
                    //Ahora sí lo MODIFICO en la lista
                    for (int i = 0; i < misCabanias.Count; i++)
                        if (misCabanias[i].getCodigo() == Codigo)
                        {
                            misCabanias[i].setNombre(Nombre);
                            misCabanias[i].setCiudad(Ciudad);
                            misCabanias[i].setBarrio(Barrio);
                            misCabanias[i].setEstrellas(Estrellas);
                            misCabanias[i].setCantPersonas(CantPersonas);
                            misCabanias[i].setTV(Tv);
                            misCabanias[i].setPrecioDia(PrecioDIA);
                            misCabanias[i].setHabitaciones(Habitaciones);
                            misCabanias[i].setBaños(Baños);
                        }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                //algo salió mal con la query porque no generó 1 registro
                return false;
            }
        }
        // fin metodo modificar Hoteles
        public bool modificarReserva(int ID, DateTime FDesde, DateTime FHasta, Alojamiento Propiedad, Usuarios Persona, float Precio)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string connectionString = Properties.Resources.connectionString;
            string queryString = "UPDATE [dbo].[RESERVAS] SET FECHA_DESDE=@fdesde, FECHA_HASTA=@fhasta" +
               //FGM_ FALTA EL CODIGO DE PERSONA, ES EL DNI?
               ",CODIGO_CABANIA=@cabania,CODIGO_HOTEL=@hotel, PRECIO_TOTAL = @precio WHERE ID=@id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@fdesde", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@fdesde", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@cabania", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@hotel", SqlDbType.Int));
                //FGM_ falta usuario, como figura en la base de datos?
                command.Parameters.Add(new SqlParameter("@precio", SqlDbType.Float));
                command.Parameters["@id"].Value = ID;
                command.Parameters["@fdesde"].Value = FDesde;
                command.Parameters["@fhasta"].Value = FHasta;
                if (Propiedad is Hotel) {
                    command.Parameters["@cabania"].Value = null;
                    command.Parameters["@hotel"].Value = Propiedad.getCodigo();
                        }
                else
                {
                    command.Parameters["@cabania"].Value = Propiedad.getCodigo();
                    command.Parameters["@hotel"].Value = null;
                }
                command.Parameters["@usuario"].Value = Persona.getDni();
                command.Parameters["@precio"].Value = Precio;
                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            if (resultadoQuery == 1)
            {
                try
                {
                    //Ahora sí lo MODIFICO en la lista
                    foreach(Reservas r in misReservas)
                    { 
                        if (r.getID() == ID)
                        {   
                            r.setFDesde(FDesde);
                            r.setFHasta(FHasta);
                            r.setPersona(Persona);
                            r.setPropiedad(Propiedad);
                            r.setPrecio(Precio);
                        }
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                //algo salió mal con la query porque no generó 1 registro
                return false;
            }
        }
        // fin metodo modificar Usuario

        // METODOS DE ELIMINAR 

        // -> ELIMINAR USUARIO

        public bool eliminarUsuario(int Dni)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string connectionString = Properties.Resources.connectionString;
            string queryString = "DELETE FROM [dbo].[USUARIOS] WHERE DNI=@dni;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters["@dni"].Value = Dni;

                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            if (resultadoQuery == 1)
            {
                try
                {
                    //Ahora sí lo elimino en la lista
                    for (int i = 0; i < misUsuarios.Count; i++)
                        if (misUsuarios[i].getDni() == Dni)
                            misUsuarios.RemoveAt(i);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {

                return false;
            }
        }

        // FIN DE ELIMINAR USUARIO

        // METODO ELIMINAR HOTEL
        public bool eliminarHotel(int Codigo)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string connectionString = Properties.Resources.connectionString;
            string queryString = "DELETE FROM [dbo].[HOTELES] WHERE CODIGO=@codigo;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@codgio", SqlDbType.Int));
                command.Parameters["@codigo"].Value = Codigo;

                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            if (resultadoQuery == 1)
            {
                try
                {
                    //Ahora sí lo elimino en la lista
                    for (int i = 0; i < misHoteles.Count; i++)
                        if (misHoteles[i].getCodigo() == Codigo)
                            misHoteles.RemoveAt(i);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                //algo salió mal con la query porque no generó 1 registro
                return false;
            }
        }

        //fin ELIMINAR HOTEL

        // METODO ELIMINAR Cabaña
        public bool eliminarCabania(int Codigo)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string connectionString = Properties.Resources.connectionString;
            string queryString = "DELETE FROM [dbo].[CABANIAS] WHERE CODIGO=@codigo;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@codgio", SqlDbType.Int));
                command.Parameters["@codigo"].Value = Codigo;

                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            if (resultadoQuery == 1)
            {
                try
                {
                    //Ahora sí lo elimino en la lista
                    for (int i = 0; i < misCabanias.Count; i++)
                        if (misCabanias[i].getCodigo() == Codigo)
                            misCabanias.RemoveAt(i);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                //algo salió mal con la query porque no generó 1 registro
                return false;
            }
        }
        //fin ELIMINAR CABAÑA

        public bool eliminarReserva(int ID)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string connectionString = Properties.Resources.connectionString;
            string queryString = "DELETE FROM [dbo].[RESERVAS] WHERE ID=@id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = ID;

                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            if (resultadoQuery == 1)
            {
                try
                {
                    //Ahora sí lo elimino en la lista
                    for (int i = 0; i < misReservas.Count; i++)
                        if (misReservas[i].getID() == ID)
                            misReservas.RemoveAt(i);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {

                return false;
            }
            }

            public Usuarios buscarUsuarios(int dniUsuario)
            {
                return misUsuarios.Find(user => user.getDni() == dniUsuario);
            }

        
        /*
        //agrego alojamientosss

   

        // agrego un nuevo hotel
         public bool nuevoHotel(int Codigo, string Nombre, string Ciudad, string Barrio, int Estrellas, int CantPersonas, bool Tv, float PrecioPP)
        {
            miAgencia.insertarAlojamiento(new Hotel(Codigo, Nombre, Ciudad, Barrio, Estrellas, CantPersonas, Tv, PrecioPP));
            return true;
        }

        // llamo al metodo de eliminarAlojamiento de agencia
        //TODO  -> hay que quitar las reservas hechas tambien en este metodo? // se podria hacer con un foreach que recorra la lista?
        public bool quitarAlojamiento(int cod)
        {

            try
            {
                miAgencia.eliminarAlojamiento(cod);

                foreach (Reservas r in misReservas)
                    if (misReservas != null)
                        if (r.getPropiedad().getCodigo() == cod)
                            eliminarReserva(r.getID());
                return true;

            }
            catch (Exception e)
            {
                MessageBox.Show("No existe un alojamiento con ese codigo." + e.Message);
                return false;
            }
        }

        // llamo al metodo desde agencia
        public bool modificarAlojamiento(Alojamiento viejoAloj, Alojamiento nuevoAloj)
        {
            if (quitarAlojamiento(viejoAloj.getCodigo()))
            {
                miAgencia.insertarAlojamiento(nuevoAloj);
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool GuardarDatosCabaña() {
            StreamWriter archivosCabañas = new StreamWriter("cabanas.txt", false);
            foreach (Cabaña c in SoloCabanas())
            {
                archivosCabañas.WriteLine(
                        c.getCodigo() + ","+
                        c.getNombre() + "," +
                        c.getCiudad() + "," +
                        c.getBarrio() + "," +
                        c.getEstrellas() + "," +
                        c.getCantPersonas() + "," +
                        c.getTV() + "," +
                        c.getPrecioDia() + "," +
                        c.getHabitaciones() + "," +
                        c.getBaños()
                        );

            }
            archivosCabañas.Close();
            return true;
            }

        // estos metodos son para mostrar las diferentes opciones en los comboBox del usuario
        public List<string> MostrarCiudadf()
        {
            List<string> listaCiudad = new List<string>();
            foreach (Alojamiento a in this.getMiAgencia().getAlojamientos())
                listaCiudad.Add(a.getCiudad());

            return listaCiudad.Distinct().ToList();
        }
      
        public List<string> MostrarTipoAloj()
        {
            List<string> tipoAloj = new List<String>
            {
                "Todos",
                "Hotel",
                "Cabaña"
            };

            return tipoAloj;          
        }

        public List<string> MostrarEstrellas()
        {
            List<string> listaEstrellas = new List<string>
            {
                "1",
                "2",
                "3",
                "4",
                "5"
            };
            return listaEstrellas;
        }

        public List<string> MostrarCantPersonas()
        {
            List<string> listaCantPersonas = new List<string>
            {
                "1",
                "2",
                "3",
                "4",
                "5",
                "6"
            };
            return listaCantPersonas;
        }

        public List<Alojamiento> SoloCabanas()
        {
            List<Alojamiento> cabanas = new List<Alojamiento>();
            foreach (Alojamiento a in miAgencia.getAlojamientos())
            {
                if (a is Cabaña)
                    cabanas.Add((Alojamiento)a);
            }
            return cabanas;
        }

        public List<Hotel> SoloHoteles()
        {
            List<Hotel> hoteles = new List<Hotel>();
            foreach (Alojamiento a in miAgencia.getAlojamientos())
            {
                if (a is Hotel)
                    hoteles.Add((Hotel)a);
            }
            return hoteles;
        }
        public bool GuardarDatosHoteles()
        {
            StreamWriter archivoHoteles = new StreamWriter("hoteles.txt", false);
            foreach (Hotel h in SoloHoteles())
            {
                archivoHoteles.WriteLine(
                        h.getCodigo() + "," +
                        h.getNombre() + "," +
                        h.getCiudad() + "," +
                        h.getBarrio() + "," +
                        h.getEstrellas() + "," +
                        h.getCantPersonas() + "," +
                        h.getTV() + "," +
                        h.getPrecioPorPersona());

            }
            archivoHoteles.Close();
            return true;
        }

        public bool GuardarReservas()
        {
            StreamWriter archivoReservas = new StreamWriter("reservas.txt", false);
            foreach (Reservas r in misReservas)
            {
                archivoReservas.WriteLine(
                        r.getID() + "," +
                        r.getFDesde() + "," +
                        r.getFHasta() + "," +                                         
                        r.getPropiedad().getCodigo() + "," +
                        r.getPersona().getDni() + "," +
                        r.getPrecio());
            }
            archivoReservas.Close();
            return true;
        }

        public bool NuevaReserva(int id, DateTime fechaIda, DateTime fechaVuelta, int codigoAlojamiento, int dniUsuario, float precioTotal)
        {
            Alojamiento codigoAloj = this.miAgencia.estaCodigo(codigoAlojamiento);
            Usuarios dniUser = buscarUsuarios(dniUsuario);
            
            misReservas.Add(new Reservas(id, fechaIda, fechaVuelta, codigoAloj, dniUser, precioTotal));

            return true;
        }
        public bool eliminarReserva(int id){

            try{
                foreach (Reservas r in misReservas)
                if(misReservas != null)
                    if (r.getID() == id)
                     misReservas.RemoveAt(r.getID());  
                return true;

            }catch (Exception e){
                Console.WriteLine("No existe una reserva con esa id." + e.Message);
                return false;
            }
        }

        public bool agregarUsuario(int Dni, string Nombre, string Mail, string Password, bool EsAdmin, bool Bloqueado){
            misUsuarios.Add(new Usuarios(Dni, Nombre, Mail, Password, EsAdmin, Bloqueado));
            return true;
        }
        public bool eliminarUsuario(int dni){

            try{
                foreach (Usuarios u in misUsuarios)
                if(misUsuarios != null)
                    if(u.getDni() == dni)
                    misUsuarios.Remove(u);
                return true;

            }catch (Exception e){
                Console.WriteLine("No se encontraron usuarios con ese DNI" + e.Message);
                return false;
            }
        }

        public bool GuardarDatosUsuarios()
        {
            StreamWriter archivoUsuarios = new StreamWriter("usuarios.txt", false);
            foreach (Usuarios u in misUsuarios)
            {
                archivoUsuarios.WriteLine(
                    u.getDni().ToString() +","+
                    u.getNombre() + "," +
                    u.getMail() + "," +
                    u.getPassword() + "," +
                    u.getEsAdmin() + "," +
                    u.getBloqueado());
            }
            archivoUsuarios.Close();
            return true;
        }


        //_FGM {
        public void BloquearUsuario(Usuarios usr)
        {
            foreach (Usuarios u in misUsuarios)
            {
                if (usr.getDni() == u.getDni())
                {
                    u.setBloqueado(true);
                }
            }
        }


        public string ValidarLogin(string dni, string pass)
        {
            string mensaje = "";

            foreach (Usuarios u in misUsuarios)
            {
                if (u.getDni() == int.Parse(dni))
                {
                    if (u.getPassword().Equals(pass))
                    {
                        if (!u.getBloqueado())
                        {
                            if (u.getEsAdmin())
                            {
                                return "True";
                            }
                            else
                            {
                                return "False";
                            }
                        }
                        else
                        {
                            return "Usuario Bloqueado";
                        }
                    }
                    else
                    {
                        intentos++;
                        mensaje = "Contraseña invalida";
                        
                        if (intentos == 3)
                        {
                            u.setBloqueado(true);
                            GuardarDatosUsuarios();
                            intentos = 0;
                            mensaje = "Se bloqueo el usuario";
                        }

                        return mensaje;
                    }
                }
                else
                {
                    mensaje = "Usuario invalido";
                }

            }
            return mensaje;
        }

        public string ValidarUsuarioCrea(string dni)
        {

            foreach (Usuarios u in misUsuarios)
                if (u.getDni() == int.Parse(dni))
                    return "Ese usuario ya existe, intente con otro DNI";

            return "true";

        }

        //} _FGM




        */

        // estos metodos son para mostrar las diferentes opciones en los comboBox del usuario
        public List<string> MostrarCiudad()
        {
            List<string> listaCiudad = new List<string>();
            foreach (Cabaña a in misCabanias)
                listaCiudad.Add(a.getCiudad());

            foreach (Hotel a in misHoteles)
                listaCiudad.Add(a.getCiudad());

            return listaCiudad.Distinct().ToList();
        }

        public List<string> MostrarTipoAloj()
        {
            List<string> tipoAloj = new List<String>
            {
                "Todos",
                "Hotel",
                "Cabaña"
            };

            return tipoAloj;
        }


        public List<string> MostrarEstrellas()
        {
            List<string> listaEstrellas = new List<string>
            {
                "1",
                "2",
                "3",
                "4",
                "5"
            };
            return listaEstrellas;
        }

        public List<string> MostrarCantPersonas()
        {
            List<string> listaCantPersonas = new List<string>
            {
                "1",
                "2",
                "3",
                "4",
                "5",
                "6"
            };
            return listaCantPersonas;
        }

        public string ValidarUsuarioCrea(string dni)
        {

            foreach (Usuarios u in misUsuarios)
                if (u.getDni() == int.Parse(dni))
                    return "Ese usuario ya existe, intente con otro DNI";

            return "true";

        }
        public string ValidarLogin(string dni, string pass)
        {
            string mensaje = "";

            foreach (Usuarios u in misUsuarios) {
                if (u.getDni() == int.Parse(dni) && u.getPassword().Equals(pass))
                {
                    if (!u.getBloqueado())
                    {
                        if (u.getEsAdmin())
                        {
                            return "true";
                        }
                        else
                        {
                            return "false";
                        }
                    }
                    else
                    {
                        return "Usuario Bloqueado";
                    }
                }
                else if (u.getDni() == int.Parse(dni) && !u.getPassword().Equals(pass))
                {
                    mensaje = "Contraseña invalida";
                    intentos++;
                    if (intentos == 3)
                    {
                        bloquearUsuario(u.getDni());
                        intentos = 0;
                        mensaje = "Se bloqueo el usuario";
                    }
                    return mensaje;
                }
                else
                {
                    mensaje = "Usuario invalido";
                }
            }
        return mensaje;
        }

        public bool bloquearUsuario(int Dni)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            string connectionString = Properties.Resources.connectionString;
            string queryString = "UPDATE [dbo].[USUARIOS] SET BLOQUEADO=1 WHERE DNI=@dni;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters["@dni"].Value = Dni;
                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            if (resultadoQuery == 1)
            {
                try
                {
                    //Ahora sí lo MODIFICO en la lista
                    for (int i = 0; i < misUsuarios.Count; i++)
                        if (misUsuarios[i].getDni() == Dni)
                        {
                            misUsuarios[i].setBloqueado(true);
                        }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                //algo salió mal con la query porque no generó 1 registro
                return false;
            }
        }

        public bool todosUsuarios(int dni)
        {

            foreach (Usuarios usu in misUsuarios)
            {
                if (dni == usu.getDni())
                {
                    return true;
                }

            }

            return false;
        }
        public bool todosHoteles(int Codigo)
        {

            foreach (Hotel hotel in misHoteles)
            {
                if (Codigo == hotel.getCodigo())
                {
                    return true;
                }

            }

            return false;
        }
        public bool todosCabanias(int Codigo)
        {

            foreach (Cabaña cabania in misCabanias)
            {
                if (Codigo == cabania.getCodigo())
                {
                    return true;
                }

            }

            return false;
        }
        public Usuarios getUsuarioXDni(int dni)
        {

            foreach (Usuarios usu in misUsuarios)

                if (dni == usu.getDni())

                    return usu;

            return new Usuarios(0, "NULL", "NULL", "NULL", false, false);
        }
        public Hotel getHotelXCodigo(int Codigo)
        {

            foreach (Hotel hotel in misHoteles)

                if (Codigo == hotel.getCodigo())

                    return hotel;

            return new Hotel(0, "NULL", "NULL", "NULL", 0, 0, false, 0);
        }
        public Cabaña getCabaniaXCodigo(int Codigo)
        {

            foreach (Cabaña cabania in misCabanias)

                if (Codigo == cabania.getCodigo())

                    return cabania;

            return new Cabaña(0, "NULL", "NULL", "NULL", 0, 0, false, 0, 0, 0);
        }



        // setter y getters

        public Agencia getMiAgencia() { return miAgencia; }

        public void setMisUsuarios(List<Usuarios> MisUsuarios) { misUsuarios = MisUsuarios; }
        public List<Usuarios> getMisUsuarios() { return misUsuarios; }

        public void setMisReservas(List<Reservas> MisReservas) { misReservas = MisReservas; }
        public List<Reservas> getMisReservas() { return misReservas; }

        public void setMisHoteles(List<Hotel> MisHoteles) { misHoteles = MisHoteles; }
        public List<Hotel> getMisHoteles() { return misHoteles; }

        public void setMisCabanias(List<Cabaña> MisCabanias) { misCabanias = MisCabanias; }
        public List<Cabaña> getMisCabanias() { return misCabanias; }

    }
}
