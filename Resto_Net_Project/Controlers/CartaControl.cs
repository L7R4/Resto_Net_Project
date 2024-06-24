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
    public class CartaControl
    {
        private static string _archivo = "comidas.json";

        public static void CreateItem(Comida comida)
        {
            JsonManageServices<Comida>.Create(_archivo, comida);
        }

        public void DeleteItem(Comida comida)
        {
            JsonManageServices<Comida>.Delete(_archivo, comida);
        }

        public static void UpdateItem(Comida comidaParaActualizar, Comida comidaActualizada)
        {
            JsonManageServices<Comida>.Update(_archivo, comidaParaActualizar, comidaActualizada);
        }

        public void MostrarCarta()
        {
            List<Comida> carta = JsonManageServices<Comida>.Select(_archivo);
            foreach (Comida c in carta)
            {
                Console.WriteLine(c.ToString());
            }
        }
    }
}