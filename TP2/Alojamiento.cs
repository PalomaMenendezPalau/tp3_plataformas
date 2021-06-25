using System;
using System.Collections.Generic;
using System.Text;

namespace TP2
{
    abstract class Alojamiento
    {
        protected int codigo;
        protected string nombre;
        protected string ciudad;
        protected string barrio;
        protected int estrellas;
        protected int cantPersonas;
        protected bool tv;

        public override string ToString()
        {
            return "Cod: "+ codigo + "/ Nombre: " + nombre + " / Ciudad: " + ciudad + " / Barrio " + barrio + " / Estrellas: " + estrellas;
        }
        public bool igualCodigo(Alojamiento a)
        {
            return codigo == a.getCodigo();
        }

        public void setCodigo(int Codigo) { codigo = Codigo; }
        public void setNombre(string Nombre) { nombre = Nombre; }
        public void setCiudad(string Ciudad) { ciudad = Ciudad; }
        public void setBarrio(string Barrio) { barrio = Barrio; }
        public void setEstrellas(int Estrellas) { estrellas = Estrellas; }
        public void setCantPersonas(int CantPersonas) { cantPersonas = CantPersonas; }
        public void setTV(bool tieneTV) { tv = tieneTV; }

        public int getCodigo() { return codigo; }
        public string getNombre() { return nombre; }
        public string getCiudad() { return ciudad; }
        public string getBarrio() { return barrio; }
        public int getEstrellas() { return estrellas; }
        public int getCantPersonas() { return cantPersonas; }
        public bool getTV() { return tv; }

    }
}
