using System;
using System.Collections.Generic;
using Resto_Net_Project.Controlers;
using Resto_Net_Project.Models;
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

    public partial class Reservas : Window
    {
        public Reserva Reserva { get; private set; }

        public Reservas()
        {
            InitializeComponent();
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (FechaInput.SelectedDate.HasValue)
            {
                DateTime fecha = FechaInput.SelectedDate.Value;
                string horaSeleccionada = (HoraInput.SelectedItem as ComboBoxItem).Content.ToString();
                DateTime fechaHora = DateTime.Parse(fecha.ToString("yyyy-MM-dd") + " " + horaSeleccionada);

                Reserva = new Reserva(NombreInput.Text, fechaHora);
                ReservasControl.GuardarReserva(Reserva);
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fecha válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
