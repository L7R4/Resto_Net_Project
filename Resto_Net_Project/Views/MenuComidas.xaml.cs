using Resto_Net_Project.Models;
using System;
using System.Collections.Generic;
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
using Resto_Net_Project.Controlers;


namespace Resto_Net_Project.Views
{

    public partial class MenuComidas : Window
    {
        public List<Comida> comidaPedida = new List<Comida>();
        List<Comida> carta = new List<Comida>();
       

        public MenuComidas() {
            InitializeComponent();
            carta = CartaControl.MostrarCarta();
            CartaList.ItemsSource = carta;
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CartaList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CartaList.SelectedItem != null)
            {
                Comida selectedItem = (Comida)CartaList.SelectedItem;
                if (!comidaPedida.Contains(selectedItem))
                {
                    comidaPedida.Add(selectedItem);
                    AgregarOrdenUI(selectedItem);
                }
            }
        }

        private void AgregarOrdenUI(Comida comida)
        {
            StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };
            Label nombreLabel = new Label { Content = comida.Nombre, Foreground = System.Windows.Media.Brushes.White, Width = 100 };
            Button restarButton = new Button { Content = "-", Width = 20, Height = 20, Margin = new Thickness(5), Background = System.Windows.Media.Brushes.Gray, Foreground = System.Windows.Media.Brushes.White, Style = (Style)Resources["RoundedButton"] };
            Label cantidadLabel = new Label { Content = "1", Width = 20, Foreground = System.Windows.Media.Brushes.White, HorizontalContentAlignment = HorizontalAlignment.Center };
            Button sumarButton = new Button { Content = "+", Width = 20, Height = 20, Margin = new Thickness(5), Background = System.Windows.Media.Brushes.Gray, Foreground = System.Windows.Media.Brushes.White, Style = (Style)Resources["RoundedButton"] };

            int cantidad = 1;

            restarButton.Click += (s, e) =>
            {
                if (cantidad > 0)
                {
                    cantidad--;
                    cantidadLabel.Content = cantidad.ToString();
                    comidaPedida.Remove(comida);
                }
            };

            sumarButton.Click += (s, e) =>
            {
                cantidad++;
                cantidadLabel.Content = cantidad.ToString();
                comidaPedida.Add(comida);
            };

            stackPanel.Children.Add(nombreLabel);
            stackPanel.Children.Add(restarButton);
            stackPanel.Children.Add(cantidadLabel);
            stackPanel.Children.Add(sumarButton);

            OrdenList.Children.Add(stackPanel);
        }
    }
}

