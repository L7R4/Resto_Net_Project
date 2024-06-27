using Resto_Net_Project.Models;
using Resto_Net_Project.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = UsuarioInput.Text;
            string pass = ContraseniaInput.Password;
            Debug.WriteLine(usuario);
            Debug.WriteLine(pass);

            // Usar la expresión lambda para seleccionar los managers que coincidan con el usuario y contraseña
            List<ManagerModel> manager = JsonManageServices<ManagerModel>.Select("managers.json", m => m.Email.Equals(usuario) && m.Password.Equals(pass));

            if (manager.Count != 0)
            {
                // Si son correctos, abrir la ventana principal
                 
                this.Close();
                var mainWindow = Application.Current.MainWindow as Preview;
                mainWindow.EditViewFrame.NavigationService.Navigate(new EditViewPage());
                mainWindow.EditViewFrame.Visibility = Visibility.Visible;

            }
            else
            {
                // Si no son correctos, mostrar un mensaje de error
                MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
