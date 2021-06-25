using System;
using System.Collections.Generic;
using System.Text;

namespace TP2
{
    class Hotel : Alojamiento
    {
        protected float precioPorPersona;

        public Hotel(int Codigo, string Nombre, string Ciudad, string Barrio, int Estrellas, int CantPersonas, bool Tv, float PrecioPP)
        {
            codigo = Codigo;
            nombre = Nombre;
            ciudad = Ciudad;
            barrio = Barrio;
            estrellas = Estrellas;
            cantPersonas = CantPersonas;
            tv = Tv;
            precioPorPersona = PrecioPP;
        }

        public void setPrecioPorPersona(float nuevoPrecioPP) { precioPorPersona = nuevoPrecioPP; }

        public float getPrecioPorPersona() { return precioPorPersona; }

        public override string ToString()
        {
            return base.ToString() + " Precio: " + precioPorPersona;
        }

    }
}
