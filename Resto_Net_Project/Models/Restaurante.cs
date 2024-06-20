using Resto_Net_Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resto_Net_Project.Models
{
    public class Orden : IIdentifiable
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public List<Comida> Menu { get; set; }
        public Mesa Mesa { get; set; }

        public DateTime Fecha { get; set; }

        private TimeSpan _inicioOrden;
        public TimeSpan InicioOrden
        {
            get => _inicioOrden;
            set
            {
                // Solo si el estado de la mesa es ocupada se puede asignar la hora de inicio de la permanencia
                if (Mesa != null && Mesa.Estado == EstadoMesa.Ocupada)
                {
                    _inicioOrden = DateTime.Now.TimeOfDay;
                }
                else
                {
                    _inicioOrden = TimeSpan.Zero;
                }
            }
        }

        private TimeSpan _finalOrden;
        public TimeSpan FinalOrden
        {
            get => _finalOrden;
            set => _finalOrden = DateTime.Now.TimeOfDay;
        }

        public TimeSpan Permanencia => FinalOrden != TimeSpan.Zero ? FinalOrden - InicioOrden : TimeSpan.Zero;


        
        public Orden(Mesa mesa)
        {
            this.Mesa = mesa;
            this.Fecha = DateTime.Today;
        }


        //Metodo que al pasarle un TimeSpan Formatee en horas minutos y segundos
        public string FormatearTiempo(TimeSpan tiempo) => $"{tiempo.Hours}:{tiempo.Minutes}:{tiempo.Seconds}";

        // Tostring de la clase en formato de texto
        public override string ToString()
        {
            return $"Cliente: {Cliente}\n" +
                $"Mesa: {Mesa.Nombre}\n" +
                $"Fecha: {Fecha.Day}-{Fecha.Month}-{Fecha.Year} \n" +
                $"Inicio de la orden: {FormatearTiempo(InicioOrden)}\n" +
                $"Final de la orden: {FormatearTiempo(FinalOrden)}\n" +
                $"Permanencia: {FormatearTiempo(Permanencia)}";
        }
    }

    public enum EstadoMesa
    {
        Libre,
        Ocupada,
        Reservada,
        Atendida
    }

    public class Comida : IIdentifiable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        private double _precio;
        public double Precio
        {
            get => _precio;

            set => _precio = value > 0 ? value : throw new Exception("Precio no válido");

        }
        

        public Comida(string nombre, double precio)
        {
            this.Nombre = nombre;
            this.Precio = Math.Round(precio,2);
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre} - ${Precio}";
        }
    }
}

    

