using Resto_Net_Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Resto_Net_Project.Models
{
    
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
            this.InicioOrden = DateTime.Now.TimeOfDay;
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
            return $"Nombre: {Nombre} - ${Precio}";
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

    public enum TipoMesa
    {
        MesaCuadrada_2_Sillas,
        MesaCuadrada_4_Sillas,
        MesaCuadrada_6_Sillas,
        MesaCuadrada_8_Sillas,
        MesaCuadrada_10_Sillas,
        MesaRedonda_2_Sillas,
        MesaRedonda_4_Sillas,
        MesaRedonda_6_Sillas,
    }

    public abstract class Elemento : IIdentifiable
    {
        public int Id { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Ancho { get; set; }
        public double Alto { get; set; }
        public bool InPlane { get; set; }


        public Elemento(double x, double y, double ancho, double alto, bool inPlane)
        {
            PosX = x; PosY = y; Ancho = ancho; Alto = alto;
            this.InPlane = inPlane;
        }
    }
    public class Mesa : Elemento
    {
        public TipoMesa TipoMesa { get; set; }
        public int Sillas { get; set; }
        public Orden Orden { get; set; }
        public EstadoMesa Estado { get; set; } = EstadoMesa.Libre;

        //atributyo de lista de reservas
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();

        public Color Color { get; set; } = Colors.Black;

        // Color que retorna en el panel
        //public Brush ColorBrush => new SolidColorBrush(Color);

        public Mesa(double posX, double posY, double ancho, double alto, int sillas, bool inPlane) : base(posX, posY, ancho, alto, inPlane)
        {
            Sillas = sillas;
        }

        // Estados
        public void Liberar() => Estado = EstadoMesa.Libre;
        public void Reservar() => Estado = EstadoMesa.Reservada;
        public void Ocupado() => Estado = EstadoMesa.Ocupada;
        public void Atendido() => Estado = EstadoMesa.Atendida;
    }
    public class Silla : Elemento
    {
        //public double Radio { get; set; } = 10;
        public Brush Color { get; set; } = Brushes.Brown;

        public Silla(double posX, double posY, double ancho, double alto, bool inPlane) : base(posX, posY, ancho, alto, inPlane)
        {

        }

        // Método para crear una silla visualmente como un Ellipse
        /*public Ellipse CrearVisualmente()
        {
            return new Ellipse
            {
                Width = Radio * 2,
                Height = Radio * 2,
                Fill = Color
            };
        }*/

    }
    public class Pared : Elemento
    {
        public Color Color { get; set; } = Colors.Gray;

        public Pared(double posX, double posY, double ancho, double alto, bool inPlane) : base(posX, posY, ancho, alto, inPlane)
        {

        }

        // Color que retorna en el panel
        public Brush ColorBrush => new SolidColorBrush(Color);


    }
    public class Puerta : Elemento
    {
        public Color Color { get; set; } = Colors.Gray;

        public Puerta(double posX, double posY, double ancho, double alto, bool inPlane) : base(posX, posY, ancho, alto, inPlane){ }

        // Color que retorna en el panel
        public Brush ColorBrush => new SolidColorBrush(Color);
    }
    public class Barra : Elemento
    {
        public Color Color { get; set; } = Colors.Aqua;
        public Barra(double posX, double posY, double ancho, double alto, bool inPlane) : base(posX, posY, ancho, alto, inPlane){}
    }
    public class Divisor : Elemento
    {
        public Color Color { get; set; } = Colors.BlueViolet;
        public Divisor(double posX, double posY, double ancho, double alto, bool inPlane) : base(posX, posY, ancho, alto, inPlane){}
    }

 
    #endregion

}
