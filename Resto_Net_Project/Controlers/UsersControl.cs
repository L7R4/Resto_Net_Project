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
        string archivoMeseros = "Meseros.json";
        string archivoManagers = "Managers.json";

        //Crear Usuario 
        public void CreateUser(MeseroModel mesero)
        {
            JsonManageServices<MeseroModel>.Create(archivoMeseros, mesero);
        }
        public void CreateUser(ManagerModel manager)
        {
            JsonManageServices<ManagerModel>.Create(archivoManagers, manager);
        }
        //Eliminar Usuario
        public void DeleteUser(MeseroModel mesero)
        {
            JsonManageServices<MeseroModel>.Delete(archivoMeseros, mesero);
        }
        public void DeleteUser(ManagerModel manager)
        {
            JsonManageServices<ManagerModel>.Delete(archivoManagers, manager);
        }
        //Actualizar Usuario
        public void UpdateUser(MeseroModel meseroParaActualizar, MeseroModel meseroActualizado)
        {
            JsonManageServices<MeseroModel>.Update(archivoMeseros, meseroParaActualizar, meseroActualizado);
        }
        public void UpdateUser(ManagerModel managerParaActualizar, ManagerModel managerActualizado)
        {
            JsonManageServices<ManagerModel>.Update(archivoManagers, managerParaActualizar, managerActualizado);
        }
        //Mostrar Usuarios
        public void ListarMeseros()
        {
            List<MeseroModel> meseros = JsonManageServices<MeseroModel>.Select(archivoMeseros);
            foreach (MeseroModel m in meseros)
            {
                Console.WriteLine(m.ToString());
            }
        }
        public void ListarManagers()
        {
            List<ManagerModel> managers = JsonManageServices<ManagerModel>.Select(archivoManagers);
            foreach (ManagerModel m in managers)
            {
                Console.WriteLine(m.ToString());
            }
        }
    }
}