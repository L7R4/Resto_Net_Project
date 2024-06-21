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
 */
namespace Resto_Net_Project.Views
{
    /// <summary>
    /// Lógica de interacción para MenuMesas.xaml
    /// </summary>
    public partial class MenuMesas : Window
    {
        public MenuMesas()
        {
            InitializeComponent();
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reservado_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
