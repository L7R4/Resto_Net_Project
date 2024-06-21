using Resto_Net_Project.Models;
using Resto_Net_Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resto_Net_Project.Controlers
{
    internal class LoginControl
    {
        public static bool PermitirAcceso(string email, string password)
        {
            string ruta_archivo = "ServiceFiles\\usuarios.json";

            //Busca los usuarios cuyo email coincida con el ingresado
            List<ManagerModelo> managers = JsonManageServices<ManagerModelo>.Select(ruta_archivo, manager => manager.Email == email);

            if (managers.Count > 0 )
            {
                //Retorna true si la lista no está vacía y la contraseña coincide con la ingresada
                return managers[0].Password == password;
            }
            return false;
        }
    }
}
