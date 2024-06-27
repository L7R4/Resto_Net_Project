using Resto_Net_Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Resto_Net_Project.Views
{
    /// <summary>
    /// Lógica de interacción para HistorialOrdenes.xaml
    /// </summary>
    public partial class HistorialOrdenes : Window
    {
        public HistorialOrdenes()
        {
            InitializeComponent();
            List<Orden> historial = Controlers.HistorialOrdenControl.MostrarHistorial();
            DisplayOrders(historial);

        }

        private void DisplayOrders(List<Orden> historial)
        {
            foreach (Orden orden in historial)
            {
                Debug.WriteLine(orden.ToString());
                StackPanel ordenPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };

                TextBlock numeroOrden = new TextBlock
                {
                    Text = $"Orden {orden.Id}",
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(10, 5, 0, 0)
                };

                TextBlock fechaOrden = new TextBlock
                {
                    Text = $"Fecha: {orden.Fecha.ToShortDateString()}",
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 14,
                    Margin = new Thickness(10, 5, 0, 0)
                };

                TextBlock permanenciaOrden = new TextBlock
                {
                    Text = $"Permanencia: {orden.Permanencia.ToString(@"hh\:mm")}",
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 14,
                    Margin = new Thickness(7, 5, 0, 0)
                };

                TextBlock meseroOrden = new TextBlock
                {
                    Text = $"Mesero: {orden.Mesero.Nombre}",
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 14,
                    Margin = new Thickness(7, 5, 0, 0)
                };

                Button verMenuButton = new Button
                {
                    Content = "Ver Menú",
                    Margin = new Thickness(0, 0, 0, 0),
                    Foreground = new SolidColorBrush(Colors.White),
                    Background = null,
                    BorderBrush = null
                };
                verMenuButton.Click += VerMenu_Click;

                Border buttonBorder = new Border
                {
                    CornerRadius = new CornerRadius(8),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5BB8FF")),
                    BorderThickness = new Thickness(2),
                    Width = 67,
                    Height = 27,
                    Margin = new Thickness(7, 1, 0, 0),
                    Child = verMenuButton
                };

                ordenPanel.Children.Add(numeroOrden);
                ordenPanel.Children.Add(fechaOrden);
                ordenPanel.Children.Add(permanenciaOrden);
                ordenPanel.Children.Add(meseroOrden);
                ordenPanel.Children.Add(buttonBorder);

                

                OrdenesStackPanel.Children.Add(ordenPanel);
            }
        }

        private void VerMenu_Click(object sender, RoutedEventArgs e)
        {
            // Recupera la orden desde el Tag del botón
            Button button = sender as Button;
            Orden orden = button?.Tag as Orden;

            if (orden != null)
            {
                // Lógica para manejar la orden
                PedidoPorMesa pedidoPorMesa = new PedidoPorMesa(orden.Menu);
                pedidoPorMesa.ShowDialog();
                // Aquí puedes agregar la lógica adicional que necesites para mostrar el menú de la orden
            }
        }

    }
}
