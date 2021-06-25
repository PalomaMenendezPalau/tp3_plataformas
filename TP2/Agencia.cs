using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
//TODO 
//LLAMAR A TODOS LOS METODOS DESDE FORMS
namespace TP2
{
    class Agencia
    {
        private List<Alojamiento> misAlojamientos;
        private int cantAlojamientos;

        public Agencia(int CantidadAlojamientos)
        {
            misAlojamientos = new List<Alojamiento>();
            cantAlojamientos = CantidadAlojamientos;
        }


        //reviso que no haya otro con el mismo código, si ya hay devuelvo false
        public bool insertarAlojamiento(Alojamiento aloj)
        {
            foreach (Alojamiento a in misAlojamientos)
                if (a.igualCodigo(aloj))
                    return false;
            //si llegó hasta acá es porque no está ese código
            misAlojamientos.Add(aloj);
            return true;
        }
        //modifico el alojamiento cuando encuentra el mismo codigo
        //TODO llamar al metodo desde forms
        public bool modificarAlojamiento(Alojamiento aloj)
        {
            foreach(Alojamiento a in misAlojamientos)
                if(misAlojamientos != null)
                    if(a.igualCodigo(aloj))       
                         return true;
            // si me retorna a falso es porque no encontro el codigo             
            return false;
        }
        // elimino el alojamiento cuando encuentra el mismo codigo que se ingresa
        //TODO llamar al metodo desde forms
        public bool eliminarAlojamiento(int codAloj)
        {

            if (misAlojamientos == null)
            {
                return false;
            }
            else
            {
                foreach (Alojamiento a in misAlojamientos)
                {
                    if (a.getCodigo() == codAloj)
                    {
                        misAlojamientos.Remove(a);
                        return true;
                    }
                }
                return false;
            }

        }
    
        public bool estaAlojamiento(int codigo)
        {
            foreach (Alojamiento a in misAlojamientos)
            if (misAlojamientos != null)
                if (a.getCodigo() == codigo)
                    return true;
            //si llegó hasta acá es porque no está ese código
            return false;
        }

        public bool estaLlena() { return cantAlojamientos <= misAlojamientos.Count; }
        public bool HayAlojamientos() { return misAlojamientos.Count > 0; }

        public Agencia AlojamientosPorCantidadDePersonas(int cantPersonas)
        {
            Agencia salidaPersonas = new Agencia(this.cantAlojamientos);
            foreach (Alojamiento a in misAlojamientos) { 
                if (a.getCantPersonas() == cantPersonas)
                    salidaPersonas.insertarAlojamiento(a);
            }
            return salidaPersonas;              
        }

        //chequeo si el codigo esta o no, con metodo de tipo alojamiento, ya que reservas lo pide
        public Alojamiento estaCodigo(int codigoAlojamiento)
        {
            return this.getAlojamientos().Find(al => al.getCodigo() == codigoAlojamiento);
        }

        public Agencia CiudadesDeAlojamientos(String ciudades)
        {
            Agencia ciudadesAlojs = new Agencia(this.cantAlojamientos);
            foreach (Alojamiento a in misAlojamientos) { 
                if (a.getCiudad() == ciudades)
                    ciudadesAlojs.insertarAlojamiento(a);
            }
            return ciudadesAlojs;
        }

        public Agencia MasEstrellas(int cant)
        {
            Agencia Salida = new Agencia(this.cantAlojamientos);
            foreach (Alojamiento a in misAlojamientos)
                if (a.getEstrellas() >= cant)
                    Salida.insertarAlojamiento(a);
            return Salida;
        }

        public Agencia AlojamientosEntrePrecios(float precioMin, float precioMax)
        {
            Agencia Salida = new Agencia(this.cantAlojamientos);
            foreach (Alojamiento a in misAlojamientos)

                if (a is Cabaña)
                {
                    Cabaña c = (Cabaña)a;
                    if (c.getPrecioDia() >= precioMin && c.getPrecioDia() <= precioMax) { 
                        Salida.insertarAlojamiento(c);                      
                    }

                }
                else if(a is Hotel)
                {
                    Hotel h = (Hotel)a;
                    if (h.getPrecioPorPersona() >= precioMin && h.getPrecioPorPersona() <= precioMax) { 
                        Salida.insertarAlojamiento(h);
                    }
       
                }
            return Salida;
        }

        public int getCantidad() { return cantAlojamientos; }
        public void setCantidad(int CantAlojamientos) { cantAlojamientos = CantAlojamientos; }

        public List<Alojamiento> getAlojamientos()
        {
            return misAlojamientos.OrderBy(a => a.getEstrellas()).ThenBy(a => a.getCantPersonas()).ThenBy(a => a.getCodigo()).ToList();
        }
    }
}