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


/*
 - En este lograr darle funcionalidad a los botones, el combobox y el reset.
 - En el codigo Xaml encontraras entre los checks un textbox colapsado. Este se debe hacer visible cuando 
   el check reservado este tildado.
 - Si te fijas en el codigo xaml, el label "Mesa" esta dentro de un grid, junto a una imagen, esta 
 representa la mesa seleccionada y el color de la misma dependiendo de su estado, los cuales aparecen en 
 leyenda en la ventana principal.
 - Que al apretar en el boton menu abra la una ventana de MenuComidas


<<<<<<< HEAD
=======

>>>>>>> 97a65d5abc0707bc346f1fc47e13ad7fdd93f43d
 */
namespace Resto_Net_Project.Views
{
    /// <summary>
    /// Lógica de interacción para MenuMesas.xaml
    /// </summary>
    public partial class MenuMesas : Window
    {
        List<MeseroModel> meseros = new List<MeseroModel>();
        public MenuMesas()
        {
            InitializeComponent();
            ReservaInput.Visibility = Visibility.Hidden;
            DataContext = this;
            meseros = UsersControl.ListarMeseros();
            this.MeserosList.ItemsSource = meseros;
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {

        }

        // Que salga una ventana emergente de confirmacion
        private void Reset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reservado_Checked(object sender, RoutedEventArgs e)
        {
            ReservaInput.Visibility = Visibility.Visible;
            
        }

    }
}
