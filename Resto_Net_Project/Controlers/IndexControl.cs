using Resto_Net_Project.Models;
using Resto_Net_Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Resto_Net_Project.Controlers
{
    public class IndexControl
    {

        public static void CrearOrden(Mesa mesa)
        {
            if (mesa == null) throw new Exception("Mesa inválida");
            if (mesa.Estado != EstadoMesa.Atendida || mesa.Estado != EstadoMesa.Ocupada) throw new Exception("La mesa no está Ocupada ni Atendida");

            Orden nuevaOrden = new Orden();
            mesa.Orden = nuevaOrden;
        }
        public static void ActualizarOrden(Mesa mesa, MeseroModel mesero= null, List<Comida> menu=null)
        {
            if (mesa == null) throw new Exception("Mesa inválida");

            Orden orden = mesa.Orden;

            orden.Mesero = mesero;
            orden.Menu = menu;

        }

        public static void TerminarOrden(Mesa mesa)
        {
            if (mesa == null) throw new Exception("Mesa inválida");

            //Guardar la orden en un archivo con la funcion Create de jsonManageServices
            string archivo = "orden.json";
            JsonManageServices<Orden>.Create(archivo, mesa.Orden);
            mesa.Liberar();

        }

        public static void ReservarMesa(Mesa mesa, string nombre, DateTime fecha)
        {
            bool isValid = true;
            if (mesa == null) throw new Exception("Mesa inválida");
            if (fecha == null) throw new Exception("Fecha inválida");

            List<Reserva> reservas= mesa.Reservas;


            foreach (Reserva reserva in reservas)
            {
                // Caso de exito de reserva
                // fecha -> 15:00
                // fecha reserva -> 11:00 + 3 horas = 14:00
                // fecha reserva -> 11:00 - 3 horas = 08:00

                // Caso de fallo de reserva
                // fecha -> 12:00
                // fecha reserva -> 11:00 + 3 horas = 14:00
                // fecha reserva -> 11:00 - 3 horas = 08:00

                if (fecha<=(reserva.Fecha.AddHours(3)) && fecha>=(reserva.Fecha.AddHours(-3)))
                {
                    isValid = false;
                }
            }

            if (!isValid) throw new Exception("La reserva está en conflicto con una reserva previa");

            Reserva nuevaReserva = new Reserva(nombre, fecha);

            reservas.Add(nuevaReserva);

        }

       
        //Funcion que retorne una lista de elementos que esten en los archivos mesas.json, banquetas.json, puertas.json, paredes.json, barras.json
        public static List<Elemento> GetElementos()
        {
            List<Elemento> elementos = new List<Elemento>();

            elementos.AddRange(JsonManageServices<Mesa>.Select("mesas.json"));
            elementos.AddRange(JsonManageServices<Banqueta>.Select("banquetas.json"));
            elementos.AddRange(JsonManageServices<Puerta>.Select("puertas.json"));
            elementos.AddRange(JsonManageServices<Pared>.Select("paredes.json"));
            elementos.AddRange(JsonManageServices<Barra>.Select("barras.json"));

            return elementos;
        }
    }


}
