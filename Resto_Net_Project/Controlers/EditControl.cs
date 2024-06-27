using Resto_Net_Project.Models;
using Resto_Net_Project.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Resto_Net_Project.Controlers
{
    public class EditControl
    {
        private static string archivo;

        public static void CreateElement(Elemento elemento)
        {

            if (elemento is Mesa mesa)
            {
                archivo = "mesas.json";
                JsonManageServices<Mesa>.Create(archivo,mesa);
            }
            else if (elemento is Banqueta banqueta)
            {
                archivo = "banquetas.json";
                JsonManageServices<Banqueta>.Create(archivo, banqueta);
            }
            else if (elemento is Puerta puerta)
            {
                archivo = "puertas.json";
                JsonManageServices<Puerta>.Create(archivo, puerta);
            }
            else if (elemento is Pared pared)
            {
                archivo = "paredes.json";
                JsonManageServices<Pared>.Create(archivo, pared);
            }
            else if (elemento is Barra barra)
            {
                archivo = "barras.json";
                JsonManageServices<Barra>.Create(archivo, barra);
            }

        }



        public static void VaciarPlano()
        {
            List<string> listaArchivos = new List<string>{ "mesas.json", "banquetas.json", "paredes.json","puertas.json","barras.json" };
            foreach (string archivo in listaArchivos)
            {
                JsonManageServices<Elemento>.DeleteAll(archivo);
            }
            
            
        }


        
    }
}