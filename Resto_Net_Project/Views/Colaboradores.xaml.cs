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
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Colaboradores : Window
    {
        List<MeseroModel> meseros = new List<MeseroModel>();
        public Colaboradores()
        {
            InitializeComponent();
            DataContext = this;
            meseros = UsersControl.ListarMeseros();
            this.MeserosList.ItemsSource = meseros;
            AgregarMeseroContainer.Visibility = Visibility.Hidden;
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MeserosList.SelectedItem is MeseroModel meseroSelected)
            {
                // Limpiar la selección del ComboBox
                MeserosList.Text = string.Empty;
                UsersControl.DeleteUser(meseroSelected);
                meseros = UsersControl.ListarMeseros();
                this.MeserosList.ItemsSource = meseros;
                MessageBox.Show("Mesero eliminado exitosamente!");
            }
            else
            {
                MessageBox.Show("No hay ningún mesero seleccionado.");
            }

        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            AgregarMeseroContainer.Visibility = Visibility.Visible;
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            MeseroModel meseroNuevo = new MeseroModel(this.NombreInput.Text, this.DNIInput.Text, this.EmailInput.Text, this.TelefonoInput.Text);
            UsersControl.CreateUser(meseroNuevo);
            meseros = UsersControl.ListarMeseros();
            this.MeserosList.ItemsSource = meseros;
            AgregarMeseroContainer.Visibility = Visibility.Hidden;
            LimpiarInputs();
            MessageBox.Show("Mesero agregado exitosamente!");
        }
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            AgregarMeseroContainer.Visibility = Visibility.Hidden;
            LimpiarInputs();
        }
        public void LimpiarInputs() {
            NombreInput.Text = string.Empty;
            TelefonoInput.Text = string.Empty;
            DNIInput.Text = string.Empty;
            EmailInput.Text = string.Empty;
        }

    }
}
