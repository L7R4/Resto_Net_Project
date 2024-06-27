using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Resto_Net_Project.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Resto_Net_Project.Services;
using Resto_Net_Project.Controlers;


namespace Resto_Net_Project.Views
{
    public partial class EditViewPage : Page
    {

        List<UIElement> elementosColocados = new List<UIElement>();
        private UIElement objetoActual;
        private bool arrastrando;

        public EditViewPage()
        {
            InitializeComponent();
            List<Elemento> plano = IndexControl.GetElementos();
            foreach (Elemento item in plano)
            {
                Grid elemento = ConvertirElemento(item);
                Mapa.Children.Add(elemento);
                elementosColocados.Add(elemento);
            }
        }

        private Grid ConvertirElemento(Elemento e)
        {
            Grid newStackPanel = new Grid
            {
                Width = e.Ancho,
                Height = e.Alto,
            };
            if (e is Mesa mesa)
            {

                newStackPanel.DataContext = $"Mesa,{mesa.Sillas}";

            }
            else
            {

                newStackPanel.DataContext = $"{e.GetType().Name}";
            }

            Canvas.SetLeft(newStackPanel, e.PosX);
            Canvas.SetTop(newStackPanel, e.PosY);

            Image newImage = new Image
            {
                Height = e.Alto,
                Width = e.Ancho,
                Source = new ImageSourceConverter().ConvertFromString(e.PathImage) as ImageSource,
                Stretch = Stretch.Fill
            };

            newStackPanel.Children.Add(newImage);
            newStackPanel.MouseLeftButtonDown += Mapa_MouseLeftButtonDown;

            return newStackPanel;
        }

        #region Funciones helpers



        // Funcion para forzar la medición y organización de un elemento
        private void MeasureAndArrangeElement(UIElement element)
        {
            element.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            element.Arrange(new Rect(element.DesiredSize));
        }


        // Funcion para establecer la posición de un elemento
        private void SetPointElement(UIElement element, Canvas canvas)
        {
            Point posicion = Mouse.GetPosition(canvas);
            Canvas.SetLeft(element, posicion.X - (element.RenderSize.Width / 2));
            Canvas.SetTop(element, posicion.Y - (element.RenderSize.Height / 2));

        }


        // Funcin para verificar si un elemento esta en el mapa
        private void VerificarSiEstaEnMapa(UIElement element, Canvas canvas)
        {
            if (VisualTreeHelper.HitTest(canvas, Mouse.GetPosition(canvas)) != null)
            {
                if (!elementosColocados.Contains(element))
                {
                    this.PanelWrapper.Children.Remove(element);
                    canvas.Children.Add(element);
                    elementosColocados.Add(element);
                }

            }
            else
            {
                this.PanelWrapper.Children.Remove(element);
                this.Mapa.Children.Remove(element);
            }
        }


        // Funcion para crear un elemento con la imagen
        private Grid CreateNewElement(Grid elementoOriginal)
        {
            Grid newStackPanel = new Grid
            {
                DataContext = elementoOriginal.DataContext,
                HorizontalAlignment = elementoOriginal.HorizontalAlignment,
                Cursor = elementoOriginal.Cursor,
            };

            foreach (UIElement child in elementoOriginal.Children)
            {
                if (child is Image originalImage)
                {
                    Image newImage = new Image
                    {
                        Height = originalImage.Height,
                        Width = originalImage.Width,
                        Source = originalImage.Source,
                        Cursor = originalImage.Cursor,
                        Stretch = originalImage.Stretch,
                    };

                    newStackPanel.Children.Add(newImage);
                }
            }


            return newStackPanel;
        }


        //Funcion que se pasa por parametro el objetoActual.DataContext y retorna un array de string separado por comas
        private string[] GetArrayFromDataContext(object dataContext)
        {
            string data = dataContext.ToString();
            string[] array = data.Split(',');
            return array;
        }

       



        private List<Elemento> CargarObjetoMemoria(List<UIElement> elementosColocados)
        {
            List<Elemento> listaElementos = new List<Elemento>();
            foreach (Grid item in elementosColocados)
            {
                string[] dataContext = GetArrayFromDataContext(item.DataContext);

                string type = dataContext[0];
                Elemento nuevoElemento;

                // Obtener el ancho y el alto del elemento
                double ancho = item.RenderSize.Width;
                double alto = item.RenderSize.Height;

                // Obtener la posicion X y Y del elemento
                double posX = Canvas.GetLeft(item);
                double posY = Canvas.GetTop(item);

                string pathImage = ((Image)item.Children[0]).Source.ToString();
                if (type != "Mesa")
                {
                    nuevoElemento = ElementoFactory.CrearElemento(type, posX, posY, ancho, alto, pathImage, true);
                }
                else
                {
                    int cantidadSillas = int.Parse(dataContext[1]);
                    nuevoElemento = ElementoFactory.CrearElemento(type, posX, posY, ancho, alto, pathImage, true, cantidadSillas);
                }

                listaElementos.Add(nuevoElemento);
            }

            return listaElementos;
        }
        

        
        #endregion


        #region Eventos
        // Evento para clonar un elemento y asignar el evento de arrastrar
        private void CloneElement_click(object sender, MouseButtonEventArgs e)
        {
            Grid newStackPanel = CreateNewElement((Grid)sender);

            // Medir y organizar el nuevo StackPanel
            MeasureAndArrangeElement(newStackPanel);

            // Agregar el nuevo StackPanel al PanelWrapper
            this.PanelWrapper.Children.Add(newStackPanel);

            // Establecer la posición inicial del nuevo StackPanel
            Point mousePosition = e.GetPosition(this.PanelWrapper);
            Canvas.SetLeft(newStackPanel, mousePosition.X - (newStackPanel.RenderSize.Width / 2));
            Canvas.SetTop(newStackPanel, mousePosition.Y - (newStackPanel.RenderSize.Height / 2));

            objetoActual = newStackPanel;
            arrastrando = true;
            this.MouseMove += PanelWrapper_MouseMove;
            this.MouseLeftButtonUp += PanelWrapper_MouseLeftButtonUp;
        }


        // Evento de mover el mouse
        private void PanelWrapper_MouseMove(object sender, MouseEventArgs e)
        {
            if (objetoActual != null && arrastrando)
            {
                Point posicion;
                if (elementosColocados.Contains(objetoActual)) { 
                    posicion = e.GetPosition(this.Mapa);
                }
                else
                {
                    posicion = e.GetPosition(this.PanelWrapper);
                }
                Canvas.SetLeft(objetoActual, posicion.X - (objetoActual.RenderSize.Width / 2));
                Canvas.SetTop(objetoActual, posicion.Y - (objetoActual.RenderSize.Height / 2));
            }



        }
 

        // Evento de soltar el click izquierdo del mouse
        private void PanelWrapper_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (arrastrando)
            {
                VerificarSiEstaEnMapa(objetoActual, this.Mapa);
                SetPointElement(objetoActual, this.Mapa);
                objetoActual.MouseLeftButtonDown += Mapa_MouseLeftButtonDown; // Agregar evento para mover el objeto en el mapa

                arrastrando = false;
                this.MouseMove -= PanelWrapper_MouseMove;
                this.MouseLeftButtonUp -= PanelWrapper_MouseLeftButtonUp;

                objetoActual = null;
            }
        }


        // Evento para rediseñar el mapa y las coordenadas de los elementos si se modifica el tamaño del mapa 
        private void Mapa_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            if (e.PreviousSize.Width == 0) return;

            //Obtenemos las dimensiones anteriores 
            double old_Heigth = e.PreviousSize.Height;
            double old_Width = e.PreviousSize.Width;

            //Obtenemos las dimensiones nuevas
            double new_Heigth = e.NewSize.Height;
            double new_Width = e.NewSize.Width;

            //Calculamos el factor de escala
            double scale_Width = new_Width / old_Width;
            double scale_Heigth = new_Heigth / old_Heigth;

            foreach (FrameworkElement item in canvas.Children)
            {
                double old_Left = Canvas.GetLeft(item);
                double old_Top = Canvas.GetTop(item);

                Canvas.SetLeft(item, old_Left * scale_Width);
                Canvas.SetTop(item, old_Top * scale_Heigth);
            }


        }



        // Evento para mover los objetos que se encuentren en el canvas
        public void Mapa_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid elemento = sender as Grid;
            if (elemento != null && elementosColocados.Contains(elemento))
            {
                objetoActual = elemento;
                arrastrando = true;
                this.MouseMove += PanelWrapper_MouseMove;
                this.MouseLeftButtonUp += PanelWrapper_MouseLeftButtonUp;
            }
        }

        #endregion


        // Estos botones deben aparecer cuando esta en modo editor ↓

        // Se debe abrir un mensaje emergente de confirmacion
        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as Preview;
            if (mainWindow != null)
            {
                mainWindow.Mapa.Children.Clear();
                mainWindow.EditViewFrame.Visibility = Visibility.Collapsed;
                //NavigationService.GoBack();
                List<Elemento> plano = IndexControl.GetElementos();
                foreach (Elemento item in plano)
                {
                    UIElement elemento = mainWindow.ConvertirElemento(item);

                    mainWindow.Mapa.Children.Add(elemento);
                }

            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            EditControl.VaciarPlano();
            List<Elemento> lista= CargarObjetoMemoria(elementosColocados);

            foreach (var item in lista)
            {
                EditControl.CreateElement(item);
            }
            MessageBox.Show("Los elementos se han guardado correctamente.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);


        }
        private void Cargar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Colaboradores_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Carta_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Historial_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}




