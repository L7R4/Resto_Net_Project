﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;

namespace Resto_Net_Project.Services
{
    public interface IIdentifiable
    {
        int Id { get; set; }
    }
    
    public class JsonManageServices<T> where T : IIdentifiable
    {
        // Path para el ServiceFiles folder
        private static string solutionFolder = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\ServicesFiles";

        public static List<T> Select(string archivoJSON, Func<T, bool> filtro = null)
        {
            FileInfo file = new FileInfo(solutionFolder + $"\\{archivoJSON}");

            string jsonContent = File.ReadAllText(file.FullName); // Lee todo el texto, lo guarda, y luego lo cierra
            List<T> lista = JsonSerializer.Deserialize<List<T>>(jsonContent); // Se deserializa el archivo JSON a elementos de una clase 

            if (filtro != null)
            {
                lista = lista.Where(filtro).ToList(); // Se aplica el filtro, es decir una funcion lambda pasada por parametro
            }

            return lista;
        }

        public static void Create(string archivoJSON, T nuevoElemento)
        {
            FileInfo file = new FileInfo(solutionFolder + $"\\{archivoJSON}");
            List<T> lista = Select(file.Name); // Deserializamos
            nuevoElemento.Id = SetIdElement(lista); // Seteamos el ID del elemento con SetIdElement para que sea autoincremental
            lista.Add(nuevoElemento);
            GuardarCambios(file, lista); // Funcion para guardar el nuevo elemento y luego volver a serializar la lista
        }

        public static void Update(string archivoJSON, T elementoParaActualizar, T elementoActualizado)
        {
            FileInfo file = new FileInfo(solutionFolder + $"\\{archivoJSON}");
            List<T> lista = Select(file.Name); // Deserializamos

            int index = lista.FindIndex(e => e.Id == elementoParaActualizar.Id); // Obtiene el indice en el que se encuentra dicho ID, sino encuentra -1

            if (index != -1)
            {
                lista[index] = elementoActualizado;
                elementoActualizado.Id = elementoParaActualizar.Id; // Colocamos el mismo ID
                GuardarCambios(file, lista);
            }
            else
            {
                throw new Exception("Elemento no encontrado");
            }
        }

        public static void Delete(string archivoJSON, T element)
        {
            FileInfo file = new FileInfo(solutionFolder + $"\\{archivoJSON}");

            List<T> lista = Select(file.Name);

            int index = lista.FindIndex(e => e.Id == element.Id); // Obtiene el indice en el que se encuentra dicho ID, sino encuentra -1

            if (index != -1)
            {
                lista.RemoveAt(index);
                GuardarCambios(file, lista);
            }
            else
            {
                throw new Exception("Elemento no encontrado");
            }
            
        }

        private static void GuardarCambios(FileInfo archivoJSON, List<T> lista)
        {

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonContent = JsonSerializer.Serialize(lista, options);
            File.WriteAllText(archivoJSON.FullName, jsonContent);
        }

        private static int SetIdElement(List<T> lista)
        {
            // Recorremos el array y obtenemos el maximo id que encuentre
            int lastId = lista.Max(e => e.Id);
            return lastId + 1; // Le sumamos 1 para que sea autoincrementable
        }


    }
}



