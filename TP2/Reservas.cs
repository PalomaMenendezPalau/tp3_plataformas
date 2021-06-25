using System;
using System.Collections.Generic;
using System.Text;

namespace TP2
{ 
    class Reservas 
    {
        protected int id;
        protected DateTime fDesde;
        protected DateTime fHasta;
        protected Alojamiento propiedad;
        protected Usuarios persona;
        protected float precio;

        public Reservas (int ID, DateTime FDesde, DateTime FHasta, Alojamiento Propiedad, Usuarios Persona , float Precio)
        {
            id = ID;
            fDesde = FDesde;
            fHasta = FHasta;
            propiedad = Propiedad;
            persona = Persona;
            precio = Precio;

        }
        public void setID(int ID) { id = ID; }
        public int getID() { return id; }

        public void setFDesde (DateTime FDesde) { fDesde = FDesde; }
        public DateTime getFDesde() { return fDesde; }

        public void setFHasta(DateTime FHasta) { fHasta = FHasta; }
        public DateTime getFHasta() { return fHasta; }

        public void setPropiedad(Alojamiento Propiedad) { propiedad = Propiedad; }
        public Alojamiento getPropiedad() { return propiedad; }
        public void setPersona(Usuarios Persona) { persona = Persona; }
        public Usuarios getPersona() { return persona; }

        public void setPrecio(float Precio) { precio = Precio; }
        public float getPrecio() { return precio; }

        public override string ToString()
        {
            return "ID: " + id + "/ Fecha Desde: " + fDesde + " / Fecha Hasta: " + fHasta + " / Alojamiento: " + propiedad + " / Usuario: " + persona + " / Precio: " + precio;
        }
    }

}

