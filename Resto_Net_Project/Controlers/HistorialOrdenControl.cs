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
    public class HistorialOrdenControl
    {
        private string _archivo = "archivo.json";

        public void GuardarOrden(Orden orden)
        {
            JsonManageServices<Orden>.Create(_archivo, orden);
        }

        public void MostrarHistorial()
        {
            List<Orden> historial = JsonManageServices<Orden>.Select(_archivo);
            foreach (Orden o in historial)
            {
                Console.WriteLine(o.ToString());
            }
        }
    }
}