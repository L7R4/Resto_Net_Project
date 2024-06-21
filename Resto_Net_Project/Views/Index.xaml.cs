using System.Windows;

/* Hola Rossi, soy Julián. Te dejo aca las cosas que tenes q ir trabajando ;P
 * - Los elementos en la lista de objetos puedan arrastrarse al mantener el click.
 * - Lo que se arrastra de hecho debe ser un clon del objeto original.
 * - Dichos objetos deberan poder soltarse en el Mapa y permanecer ahí, si se suelta en otro lado se borra insta.
 * - Que las paredes, los divisores y las etiquetas puedan redimencionarse unicamente dentro del Mapa (las etiquetas 
 *   que tambien solo se puedan escribir ahi).
 * 
 * Por ahora enfocate en eso. El objetivo actual es lograr mapear el restaurante y guardarlo en un archivo. Luego
 *  de eso el objetivo será cargar desde ese archivo. 
 * Yo mientras me pongo a hacer la otra ventana de Estado de las Mesas. Esa ventana tendrias q hacer que se abra 
 *  cuando se haga click en alguno de los objetos que estén dentro del mapa.
 *  
 *  SUERTE CAMPEON! 💀👍          */

namespace Resto_Net_Project.Views
{
    public partial class Index : Window
    {
        public Index()
        {
            InitializeComponent();
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Editar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
