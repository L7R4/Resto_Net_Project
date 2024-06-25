using Resto_Net_Project.Models;
using Resto_Net_Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Resto_Net_Project.Controlers
{
    public class EditControl
    {
        private static string archivo = "elementos.json";

        public static void CreateElement(Elemento elemento)
        {
            if (elemento.InPlane) throw new Exception("El elemento ya se encuentra en el Plano"); 
            JsonManageServices<Elemento>.Create(archivo, elemento);
        }

        public static void DeleteElement(Elemento elemento)
        {
            JsonManageServices<Elemento>.Delete(archivo, elemento);
        }

        public static void UpdateElement(Elemento elemento, Elemento elementoNuevo)
        {
            if (!elemento.InPlane) throw new Exception("El elemento no se encuentra en el Plano");
            JsonManageServices<Elemento>.Update(archivo, elemento, elementoNuevo);
        }

        
    }
}