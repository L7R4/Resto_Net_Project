using Microsoft.Win32;
using Resto_Net_Project.Controlers;
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

namespace Resto_Net_Project.Views
{
    /// <summary>
    /// Lógica de interacción para Carta.xaml
    /// </summary>
    public partial class Carta : Window
    {
        public Carta()
        {
            InitializeComponent();
            NuevaComidaContainer.Visibility = Visibility.Hidden;
        }

        // Se debe abrir un mensaje emergente de confirmacion
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            //seleccionar elemento
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void CargarImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Todos los archivos (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                imgCargada.Source = bitmap;
            }
        }


        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
