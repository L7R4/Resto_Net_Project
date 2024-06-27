using Microsoft.Win32;
using Resto_Net_Project.Controlers;
using Resto_Net_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Lógica de interacción para Carta.xaml
    /// </summary>
    public partial class Carta : Window
    {
        List<Comida> carta = new List<Comida>();
        public Carta()
        {
            InitializeComponent();
            carta = CartaControl.MostrarCarta();
            CartaList.ItemsSource = carta;
            NuevaComidaContainer.Visibility = Visibility.Hidden;
        }

        // Se debe abrir un mensaje emergente de confirmacion
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que desencadenó el evento
            if (CartaList.SelectedItem != null && CartaList.SelectedItem is Comida comida)
            {
                // Llamar al método para eliminar utilizando CartaControl.DeleteItem(comida)
                CartaControl.DeleteItem(comida);

                carta = CartaControl.MostrarCarta();
                CartaList.ItemsSource = carta;

            }
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            NuevaComidaContainer.Visibility = Visibility.Visible;
            
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            Comida comida = new Comida(this.NombreInput.Text, double.Parse(this.PrecioInput.Text));
            CartaControl.CreateItem(comida);
            carta = CartaControl.MostrarCarta();
            CartaList.ItemsSource = carta;
            this.NombreInput.Text = string.Empty;
            this.PrecioInput.Text = string.Empty;
            NuevaComidaContainer.Visibility = Visibility.Hidden;
        }
    }
}
