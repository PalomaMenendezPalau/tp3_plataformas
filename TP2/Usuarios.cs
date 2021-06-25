using System;
using System.Collections.Generic;
using System.Text;

namespace TP2
{
    class Usuarios
    {
        protected int dni;
        protected string nombre;
        protected string mail;
        protected string password;
        protected bool esAdmin;
        protected bool bloqueado;

        public Usuarios(int Dni, string Nombre, string Mail , string Password , bool EsAdmin , bool Bloqueado)
        { 
            dni = Dni;
            nombre = Nombre;
            mail = Mail;
            password = Password;
            esAdmin = EsAdmin;
            bloqueado = Bloqueado;
        }

        public void setDni(int Dni) { dni = Dni; }
        public int getDni() { return dni; }

        public void setNombre(string Nombre) { nombre = Nombre; }
        public string getNombre() { return nombre; }

        public void setMail(string Mail) { mail = Mail; }
        public string getMail() { return mail; }

        public void setPassword(string Password) { password = Password; }
        public string getPassword() { return password; }

        public void setEsAdmin(bool EsAdmin) { esAdmin = EsAdmin; }
        public bool getEsAdmin() { return esAdmin; }

        public void setBloqueado(bool Bloqueado) { bloqueado = Bloqueado; }
        public bool getBloqueado() { return bloqueado; }

        public override string ToString()
        {
            return "DNI: " + dni + "/ Nombre: " + nombre + " / Mail: " + mail + " / Password: " + password + " / Es Administrador: " + esAdmin + " / Bloqueado: " + bloqueado;
        }
    }
}
