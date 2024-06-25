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
    public class UsersControl
    {
        private static string archivoMeseros = "meseros.json";
        private static string archivoManagers = "managers.json";

        //Crear Usuario 
        public static void CreateUser(MeseroModel mesero)
        {
            JsonManageServices<MeseroModel>.Create(archivoMeseros, mesero);
        }
        public static void CreateUser(ManagerModel manager)
        {
            JsonManageServices<ManagerModel>.Create(archivoManagers, manager);
        }
        //Eliminar Usuario
        public static void DeleteUser(MeseroModel mesero)
        {
            JsonManageServices<MeseroModel>.Delete(archivoMeseros, mesero);
        }
        public static void DeleteUser(ManagerModel manager)
        {
            JsonManageServices<ManagerModel>.Delete(archivoManagers, manager);
        }
        //Actualizar Usuario
        public static void UpdateUser(MeseroModel meseroParaActualizar, MeseroModel meseroActualizado)
        {
            JsonManageServices<MeseroModel>.Update(archivoMeseros, meseroParaActualizar, meseroActualizado);
        }
        public static void UpdateUser(ManagerModel managerParaActualizar, ManagerModel managerActualizado)
        {
            JsonManageServices<ManagerModel>.Update(archivoManagers, managerParaActualizar, managerActualizado);
        }
        //Mostrar Usuarios
        public static List<MeseroModel> ListarMeseros()
        {
            List<MeseroModel> meseros = JsonManageServices<MeseroModel>.Select(archivoMeseros);
            return meseros;
        }
        public static void ListarManagers()
        {
            List<ManagerModel> managers = JsonManageServices<ManagerModel>.Select(archivoManagers);
            foreach (ManagerModel m in managers)
            {
                Console.WriteLine(m.ToString());
            }
        }
    }
}