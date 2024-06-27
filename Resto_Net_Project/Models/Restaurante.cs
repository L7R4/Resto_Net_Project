using Resto_Net_Project.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Resto_Net_Project.Models
{
    public static class ResourceFolder
    {
        public static string GetResourceFolder() => new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\ServicesFiles";

    }
    public class Orden : IIdentifiable
    {
        public int Id { get; set; }
        public MeseroModel Mesero { get; set; }
        public List<Comida> Menu { get; set; }

        public DateTime Fecha { get; set; }
        
        public TimeSpan InicioOrden { get; private set; }
        
        public TimeSpan FinalOrden { get; set; }

        public TimeSpan Permanencia => FinalOrden != TimeSpan.Zero ? FinalOrden - InicioOrden : TimeSpan.Zero;


        
        public Orden()
        {
            this.Fecha = DateTime.Today;
        }


        //Metodo que al pasarle un TimeSpan Formatee en horas minutos y segundos
        public string FormatearTiempo(TimeSpan tiempo) => $"{tiempo.Hours}:{tiempo.Minutes}:{tiempo.Seconds}";

        // Tostring de la clase en formato de texto
        public override string ToString()
        {
            return $"Mesero: {Mesero}\n" +
                $"Fecha: {Fecha.Day}-{Fecha.Month}-{Fecha.Year} \n" +
                $"Inicio de la orden: {FormatearTiempo(InicioOrden)}\n" +
                $"Final de la orden: {FormatearTiempo(FinalOrden)}\n" +
                $"Permanencia: {FormatearTiempo(Permanencia)}";
        }
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
            return $"{Nombre} --- ${Precio}";
        }
    }

    public class Reserva : IIdentifiable
    {
        public int Id { get; set; }
        public string Nombre { get; set;}
        public DateTime Fecha { get; set; }

        public Reserva(string nombre, DateTime fecha)
        {
            this.Nombre= nombre;
            this.Fecha = fecha;
        }
        public override string ToString()
        {
            return Nombre+" - "+Fecha;
        }
    }

 

    #region Codigo de astrid ELEMENTOS

    public enum EstadoMesa
    {
        Libre,
        Ocupada,
        Reservada,
        Atendida
    }


    
    public abstract class Elemento : IIdentifiable
    {
        private string _pathImage;
        public string PathImage
        {
            get { return _pathImage; }

            set
            {
                _pathImage = ResourceFolder.GetResourceFolder() + "\\Resto_Net_Project\\" + value;
            }

        }
        public int Id { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Ancho { get; set; }
        public double Alto { get; set; }
        public bool InPlane { get; set; }


        public Elemento(double x, double y, double ancho, double alto, string PathImage , bool inPlane)
        {
            PosX = x; 
            PosY = y; 
            Ancho = ancho; 
            Alto = alto;
            _pathImage = PathImage;
            this.InPlane = inPlane;
        }
    }

    public class Mesa : Elemento
    {
        
        public int Sillas { get; set; }
        public Orden Orden { get; set; }
        
        public EstadoMesa Estado { get; set; } = EstadoMesa.Libre;

        public List<Reserva> Reservas { get; set; } = new List<Reserva>();

        
        public Mesa(double posX, double posY, double ancho, double alto, int sillas, string pathImage ,bool inPlane) : base(posX, posY, ancho, alto, pathImage, inPlane)
        {
            Sillas = sillas;
        }



        // Estados
        public void Liberar() => Estado = EstadoMesa.Libre;
        public void Reservar() => Estado = EstadoMesa.Reservada;
        public void Ocupado() => Estado = EstadoMesa.Ocupada;
        public void Atendido() => Estado = EstadoMesa.Atendida;
    }
    public class Banqueta : Elemento
    {

        public Banqueta(double posX, double posY, double ancho, double alto, string PathImage, bool inPlane) : base(posX, posY, ancho, alto,PathImage, inPlane)
        {

        }

    }
    public class Pared : Elemento
    {
        

        public Pared(double posX, double posY, double ancho, double alto, string pathImage, bool inPlane) : base(posX, posY, ancho, alto, pathImage ,inPlane)
        {

        }

    }
    public class Puerta : Elemento
    {
       

        public Puerta(double posX, double posY, double ancho, double alto, string pathImage, bool inPlane) : base(posX, posY, ancho, alto, pathImage,inPlane){ }

    }
    public class Barra : Elemento
    {
        

        public Barra(double posX, double posY, double ancho, double alto, string pathImage, bool inPlane) : base(posX, posY, ancho, alto,pathImage,inPlane){}
    }
    

    public static class ElementoFactory
    {
        public static Elemento CrearElemento(string tipo, double posX, double posY, double ancho, double alto, string pathImage,bool inPlane, params object[] parametros)
        {
            switch (tipo)
            {
                case "Mesa":
                    int sillas = (int)parametros[0]; 
                    return new Mesa(posX, posY, ancho, alto, sillas, pathImage, inPlane);
                case "Banqueta":
                    return new Banqueta(posX, posY, ancho, alto, pathImage, inPlane);
                case "Pared":
                    return new Pared(posX, posY, ancho, alto, pathImage,inPlane);
                case "Puerta":
                    return new Puerta(posX, posY, ancho, alto, pathImage, inPlane);
                case "Barra":
                    return new Barra(posX, posY, ancho, alto, pathImage, inPlane);
                default:
                    throw new ArgumentException($"Tipo de elemento desconocido: {tipo}");
            }
        }
    }

    #endregion

}
