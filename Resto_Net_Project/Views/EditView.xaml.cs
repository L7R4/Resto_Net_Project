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
    public partial class EditView : Window
    {

        List<UIElement> elementosColocados = new List<UIElement>();
        private UIElement objetoActual;
        private bool arrastrando;
        //public ObservableCollection<Mesa> Mesas { get; set; } = new ObservableCollection<Mesa>();
        //public ObservableCollection<Mesa> Plano { get; set; } = new ObservableCollection<Mesa>(); 
        //public ObservableCollection<Pared> Paredes { get; set; } = new ObservableCollection<Pared>();

        public EditView()
        {
            InitializeComponent();
        }

        #region Codigo de Lauti
        
        // Funcion para forzar la medición y organización de un elemento
        private void MeasureAndArrangeElement(UIElement element)
        {
            //element.Measure fuerza una medición del tamaño deseado del elemento.
            //element.Arrange Organiza el elemento en su tamaño deseado.

            element.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            element.Arrange(new Rect(element.DesiredSize));
        }
        
        // Evento para clonar un elemento y asignar el evento de arrastrar
        private void CloneElement_click(object sender, MouseEventArgs e)
        {
            string gridXaml = XamlWriter.Save(sender); // Guardar el objeto en un string
            StringReader stringReader = new StringReader(gridXaml); // Crear un StringReader
            XmlReader xmlReader = XmlReader.Create(stringReader);  // Crear un XmlReader

            UIElement clonedElement = (UIElement)XamlReader.Load(xmlReader); // Cargar el objeto desde el XmlReader

            MeasureAndArrangeElement(clonedElement); // Medir y organizar el objeto clonado
 
            this.PanelWrapper.Children.Add(clonedElement); // Agregar el objeto clonado al Canvas

            // Establecer la posición inicial del objeto clonado
            Point mousePosition = e.GetPosition(this.PanelWrapper);
            Canvas.SetLeft(clonedElement, mousePosition.X - (clonedElement.RenderSize.Width / 2));
            Canvas.SetTop(clonedElement, mousePosition.Y - (clonedElement.RenderSize.Height / 2));
           
            
            objetoActual = clonedElement;
            arrastrando = true;
            this.MouseMove += PanelWrapper_MouseMove;
            this.MouseLeftButtonUp += PanelWrapper_MouseLeftButtonUp;



        }
        
        // Evento de mover el mouse
        private void PanelWrapper_MouseMove(object sender, MouseEventArgs e)
        {
            if (objetoActual != null && arrastrando)
            {
                // Actualizar la posición del objeto clonado mientras se arrastra
                Point posicion = e.GetPosition(this.PanelWrapper);

                Canvas.SetLeft(objetoActual, posicion.X - (objetoActual.RenderSize.Width / 2));
                Canvas.SetTop(objetoActual, posicion.Y - (objetoActual.RenderSize.Height / 2));
            }
        }

        // Evento de soltar el click izquierdo del mouse mouse
        private void PanelWrapper_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (arrastrando)
            {
                arrastrando = false;
                this.MouseMove -= PanelWrapper_MouseMove;
                this.MouseLeftButtonUp -= PanelWrapper_MouseLeftButtonUp;
                objetoActual = null;
            }
        }
        #endregion

        #region Eventos de Astrid
        // ---------------------- EVENTOS ---------------------- //

        // se activa cuando se hace click sobre un elemento
        private void ManejarClicElemento(object sender, MouseButtonEventArgs e)
        {
            var imagen = sender as Image;
            if (imagen != null)
            {
                DragDrop.DoDragDrop(imagen, imagen.DataContext, DragDropEffects.Move);
            }
        }

        // se activa cuando se está presionando el elemento y realiza la operación de arrastrar y soltar
        private void ManejarMovimientoElemento(object sender, MouseEventArgs e)
        {
            var imagen = sender as Image;
            if (e.LeftButton == MouseButtonState.Pressed && imagen != null)
            {
                DragDrop.DoDragDrop(imagen, imagen.DataContext, DragDropEffects.Move);
            }
        }

        
        // durante una operación de arrastrar y soltar. Si el efecto de la operación es DragDropEffects.Move, cambia el cursor a una mano
        private void RetroalimentacionElemento(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effects == DragDropEffects.Move)
            {
                Mouse.SetCursor(Cursors.Hand);
            }
            e.Handled = true;
        }
        

        //Evento MouseDown para iniciar el arrastre de una pared
        private void ManejarClickPared(object sender, MouseButtonEventArgs e)
        {
            var rectangle = sender as Rectangle;
            if (rectangle != null)
            {
                var pared = rectangle.DataContext as Pared;
                if (pared != null)
                {
                    DragDrop.DoDragDrop(rectangle, pared, DragDropEffects.Move);
                }
            }
        }

        private void ManejarRetroalimentacionGrid(object sender, GiveFeedbackEventArgs e)
        {
            Grid grid = sender as Grid;
            if (grid == null)
                return;

            // Obtener la posición del cursor relativa al Grid
            Point cursorPos = Mouse.GetPosition(grid);

            // Realizar un HitTest para detectar si el ratón está sobre una Image (mesa)
            HitTestResult hitTestResult = VisualTreeHelper.HitTest(grid, cursorPos);
            if (hitTestResult != null && hitTestResult.VisualHit is Image)
            {
                Mouse.SetCursor(Cursors.Hand);
            }
            else
            {
                Mouse.SetCursor(Cursors.Arrow);
            }

            e.Handled = true; // Indicar que hemos manejado el evento
        }
       

        // Método para verificar si la posición está ocupada
        private bool PosicionOcupada(UIElement elementoNuevo)
        {
            foreach (UIElement elementoExistente in elementosColocados)
            {
                if (elementoExistente is Image imagenExistente || elementoExistente is Rectangle rectanguloExistente)
                {
                    // Obtén las posiciones de las esquinas del elemento existente
                    double leftExistente = Canvas.GetLeft(elementoExistente);
                    double topExistente = Canvas.GetTop(elementoExistente);
                    double rightExistente = leftExistente + elementoExistente.RenderSize.Width;
                    double bottomExistente = topExistente + elementoExistente.RenderSize.Height;

                    // Obtén las posiciones de las esquinas del elemento nuevo
                    double leftNuevo = Canvas.GetLeft(elementoNuevo);
                    double topNuevo = Canvas.GetTop(elementoNuevo);
                    double rightNuevo = leftNuevo + elementoNuevo.RenderSize.Width;
                    double bottomNuevo = topNuevo + elementoNuevo.RenderSize.Height;

                    // Verifica si hay alguna superposición
                    if (leftNuevo < rightExistente && rightNuevo > leftExistente &&
                        topNuevo < bottomExistente && bottomNuevo > topExistente)
                    {
                        return true; // Hay superposición
                    }
                }
            }
            return false; // No hay superposición
        }


        // Elimina elementos puestos en el mapa
        private void EliminarElementos(object sender, MouseButtonEventArgs e)
        {
            var canvas = sender as Canvas;
            if (canvas != null)
            {
                // Obtén los elementos que están bajo el punto de clic
                var elementsToRemove = new List<UIElement>();
                var position = e.GetPosition(canvas);
                VisualTreeHelper.HitTest(canvas, null, result =>
                {
                    if (result.VisualHit is UIElement element && element != canvas)
                    {
                        elementsToRemove.Add(element);
                    }
                    return HitTestResultBehavior.Continue;
                }, new PointHitTestParameters(position));

                // Elimina los elementos del Canvas
                foreach (var element in elementsToRemove)
                {
                    canvas.Children.Remove(element);
                    elementosColocados.Remove(element);
                    // También puedes eliminarlos de tu colección de elementos colocados si es necesario
                }
            }
        }

        // actua cuando se suelta un elemento arrastrado en el mapa
        private void SoltarElementoEnMapa(object sender, DragEventArgs e)
        {
            var elemento = e.Data.GetData(typeof(Elemento)) as Elemento;
            if (elemento != null)
            {
                Point dropPosition = e.GetPosition(Mapa);

                // Crea una nueva instancia de Image o Rectangle según corresponda
                Image nuevaImagen = new Image
                {
                    //Source = elemento.ImagenSource,
                    Width = elemento.Ancho,
                    Height = elemento.Alto
                };

                // Establece la posición de la nueva imagen
                Canvas.SetLeft(nuevaImagen, dropPosition.X - (elemento.Ancho / 2));
                Canvas.SetTop(nuevaImagen, dropPosition.Y - (elemento.Alto / 2));

                if (!PosicionOcupada(nuevaImagen))
                {
                    (sender as Canvas).Children.Add(nuevaImagen);
                    elementosColocados.Add(nuevaImagen);

                    elemento.PosX = dropPosition.X;
                    elemento.PosY = dropPosition.Y;

                    // Lógica para guardar estas coordenadas en tu modelo de datos o base de datos
                }
                else
                {
                    // La posición está ocupada, maneja este caso como prefieras
                }
            }
        }

        #endregion

    }
}




