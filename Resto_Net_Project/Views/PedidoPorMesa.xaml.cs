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
    /// Lógica de interacción para PedidoPorMesa.xaml
    /// </summary>
    public partial class PedidoPorMesa : Window
    {
        public PedidoPorMesa(List<Comida> comidaPedida)
        {
            InitializeComponent();
           this.PedidosList.ItemsSource = comidaPedida;
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
