using Resto_Net_Project.Controlers;
using Resto_Net_Project.Models;
using Resto_Net_Project.Services;
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
        Orden OrdenActual = new Orden();

        Mesa mesaActual;
        Mesa mesaUpdate;
        public MenuMesas(Mesa mesa)
        {
            mesaActual = mesa;
            mesaUpdate = mesa;
            OrdenActual = mesaActual.Orden ;
            comidaPedida = OrdenActual.Menu;
            

            InitializeComponent();
            meseros = UsersControl.ListarMeseros();
            this.MeserosList.ItemsSource = meseros;
            this.MeserosList.SelectedItem = OrdenActual.Mesero;
            CargarEstado();
            reservas = mesa.Reservas;
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
            ActualizarOrden();
            this.Close();
        }

        private void ActualizarOrden()
        {
            Orden ordenUpdate = OrdenActual;
            if (MeserosList.SelectedItem != null)
            {
                if (MeserosList.SelectedItem is MeseroModel meseroSelected)
                {
                    ordenUpdate.Mesero = meseroSelected;
                }
            }

            if (comidaPedida.Count != 0)
            {
                OrdenActual.Menu = comidaPedida;
            }

            SeleccionarEstadoActual();

            List<Orden> OrdenList = JsonManageServices<Orden>.Select("orden.json",o => o.Id ==OrdenActual.Id);
            if (OrdenList.Count != 0)
            {
                JsonManageServices<Orden>.Update("orden.json", OrdenActual, ordenUpdate);

            }
            else
            {

               JsonManageServices<Orden>.Create("orden.json", OrdenActual);
            }
        }

        
        
        private void CargarEstado()
        {
            switch (mesaActual.Estado)
            {
                case EstadoMesa.Ocupada:
                    Ocupado.IsChecked = true;
                    break;

                case EstadoMesa.Atendida:
                    Atendido.IsChecked = true;
                    break;
                default:
                    break;
            }
        }

       
        public void SeleccionarEstadoActual()
        {
            if (Ocupado.IsChecked == true)
            {
                mesaUpdate.Estado = EstadoMesa.Ocupada;
                JsonManageServices<Mesa>.Update("mesas.json",mesaActual, mesaUpdate);
            }
            else if (Atendido.IsChecked == true)
            {
                mesaUpdate.Estado = EstadoMesa.Atendida;
                JsonManageServices<Mesa>.Update("mesas.json", mesaActual, mesaUpdate);
            }
            else
            {
                mesaUpdate.Estado = EstadoMesa.Libre;
                JsonManageServices<Mesa>.Update("mesas.json", mesaActual, mesaUpdate);
            }      
        }

        private void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            //Limpiar orden, mesero y estado en MESA y guardar orden en HISTORIAL
            // orden = null;
            this.Close();
        }
    }
}
