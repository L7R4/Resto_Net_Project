using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Resto_Net_Project.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Resto_Net_Project.Controlers;
using Resto_Net_Project.Services;


namespace Resto_Net_Project.Views
{
    public partial class Preview : Window
    {

        List<UIElement> elementosColocados = new List<UIElement>();
        private UIElement objetoActual;
        private bool arrastrando;

        public Preview()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;

        }

        // Evento para cuando la ventana se carga
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Elemento> plano = IndexControl.GetElementos();
            foreach (Elemento item in plano)
            {
                UIElement elemento = ConvertirElemento(item);

                Mapa.Children.Add(elemento);
            }
        }

        // Funcion para generar un elemento StackPanel
        public Grid ConvertirElemento(Elemento e)
        {
               Grid newStackPanel = new Grid
               {
                   Width = e.Ancho,
                   Height = e.Alto,
                   Tag = e.Id
               };

               Canvas.SetLeft(newStackPanel, e.PosX);
               Canvas.SetTop(newStackPanel, e.PosY);

               Image newImage = new Image
               {
                   Height = e.Alto,
                   Width = e.Ancho,
                   Source = new ImageSourceConverter().ConvertFromString(e.PathImage) as ImageSource,
                   Stretch = Stretch.Fill
               };
               newStackPanel.Children.Add(newImage);
            if (e is Mesa mesa)
            {
               newStackPanel.Children.Add(CrearTextBlockSegunEstado(mesa.Estado));
               newStackPanel.MouseRightButtonDown += Elemento_MouseRightButtonDown;

            }

            return newStackPanel;
        }

        public TextBlock CrearTextBlockSegunEstado(EstadoMesa estado)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 25;
            textBlock.FontWeight = FontWeights.Bold;
            textBlock.Text = "◉";
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;

            switch (estado)
            {
                case EstadoMesa.Libre:
                    textBlock.Foreground = Brushes.Green;
                    break;
                case EstadoMesa.Ocupada:
                    textBlock.Foreground = Brushes.Red;
                    break;
                case EstadoMesa.Reservada:
                    textBlock.Foreground = Brushes.Blue;
                    break;
                case EstadoMesa.Atendida:
                    textBlock.Foreground = Brushes.Yellow;
                    break;
                default:
                    textBlock.Foreground = Brushes.Black;
                    break;
            }

            return textBlock;
        }

        // Evento para rediseñar el mapa y las coordenadas de los elementos si se modifica el tamaño del mapa 
        private void Mapa_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            if(e.PreviousSize.Width == 0) return;

            //Obtenemos las dimensiones anteriores 
            double old_Heigth = e.PreviousSize.Height;
            double old_Width = e.PreviousSize.Width;

            //Obtenemos las dimensiones nuevas
            double new_Heigth = e.NewSize.Height;
            double new_Width = e.NewSize.Width;

            //Calculamos el factor de escala
            double scale_Width = new_Width / old_Width;
            double scale_Heigth = new_Heigth / old_Heigth;

            foreach (FrameworkElement item in canvas.Children)
            {
                double old_Left = Canvas.GetLeft(item);
                double old_Top = Canvas.GetTop(item);

                Canvas.SetLeft(item, old_Left * scale_Width);
                Canvas.SetTop(item, old_Top * scale_Heigth);
            }


        }



        

        private void Ingresar_Click(object sender, RoutedEventArgs e)
        {
           
            Login LoginManager = new Login();
            LoginManager.Owner = this;
            LoginManager.ShowDialog();
        }

        private void Elemento_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid mesa = sender as Grid;
            int id = (int)mesa.Tag;
            List<Mesa> mesaSeleccionada = JsonManageServices<Mesa>.Select("mesas.json", m => m.Id == id);

            Debug.WriteLine(mesaSeleccionada[0].Sillas);

            MenuMesas menuOptions = new MenuMesas(mesaSeleccionada[0]);
            menuOptions.Owner = this;
            menuOptions.ShowDialog();
        }

        
        
        private void Historial_Click(object sender, RoutedEventArgs e)
        {
             HistorialOrdenes historial = new HistorialOrdenes();
            historial.Owner = this;
            historial.ShowDialog();
        }
    }
}




