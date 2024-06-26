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

    public partial class MenuMesas : Window
    {
        List<MeseroModel> meseros = new List<MeseroModel>();
        List<Reserva> reservas = new List<Reserva>();
        List<Comida> comidaPedida = new List<Comida>();

        public MenuMesas()
        {
            InitializeComponent();
            meseros = UsersControl.ListarMeseros();
            this.MeserosList.ItemsSource = meseros;
            reservas = ReservasControl.MostrarHistorial();
            this.ReservasList.ItemsSource=reservas;
        }
        private void AgregarReserva_Click(object sender, RoutedEventArgs e)
        {
            Reservas ventanaReserva = new Reservas();
            bool? result = ventanaReserva.ShowDialog();
            if (result == true)
            {
                // Obtener la nueva reserva y agregarla a la lista
                Reserva nuevaReserva = ventanaReserva.Reserva;
                reservas.Add(nuevaReserva);
                this.ReservasList.ItemsSource = null;
                this.ReservasList.ItemsSource = reservas;
            }
        }
        private void AgregarOrden_Click(object sender, RoutedEventArgs e)
        {
            MenuComidas ventanaAgregarOrden = new MenuComidas();
            ventanaAgregarOrden.ShowDialog();

            foreach(Comida c in ventanaAgregarOrden.comidaPedida)
            {
                comidaPedida.Add(c);
            }
        }

        private void VerOrden_Click(object sender, RoutedEventArgs e)
        {
            PedidoPorMesa ventanaPedidoPorMesa = new PedidoPorMesa(comidaPedida);
            ventanaPedidoPorMesa.ShowDialog();
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            //Guardar en ORDEN comidaPedida y mesero(combobox). y eso guardar en MESA junto a estado
            this.Close();
        }


        private void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            //Limpiar orden, mesero y estado en MESA
            this.Close();
        }
    }
}
