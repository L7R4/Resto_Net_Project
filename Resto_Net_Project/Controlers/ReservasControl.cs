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
    public class ReservasControl
    {
        private static string _archivo = "reservas.json";

        public static void GuardarReserva(Reserva reserva)
        {
            JsonManageServices<Reserva>.Create(_archivo, reserva);
        }

        public static List<Reserva> MostrarHistorial()
        {
            List<Reserva> historial = JsonManageServices<Reserva>.Select(_archivo);
            return historial;
        }
    }
}