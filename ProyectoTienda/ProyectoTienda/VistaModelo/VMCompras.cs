using ProyectoTienda.Datos;
using ProyectoTienda.Modelo;
using ProyectoTienda.Vistas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.SharedTransitions;

namespace ProyectoTienda.VistaModelo
{
    public class VMCompras : BaseViewModel
    {

        #region VARIABLES
        string _Texto;
        List<Mproductos> _ListaProductos;
        List<MdetalleCompras> _ListaVistapreviaDc;
        List<MdetalleCompras> _ListaDc;
        string _CantidadTotal;
        int _index;

        bool _isVisiblePanelCompras;

        #endregion

        #region CONSTRUCTOR
        public VMCompras(INavigation navigation, StackLayout Carrilderecha, StackLayout Carrilizquierda)
        {
            Navigation = navigation;
            Mostrarproductos(Carrilderecha,Carrilizquierda);
            isVisiblePanelCompras = false;
        }

        #endregion

        #region OBJETOS
        public bool isVisiblePanelCompras
        {
            get { return _isVisiblePanelCompras; }
            set { SetValue(ref _isVisiblePanelCompras, value); }
            
        }


        public string CantidadTotal
        {
            get { return _CantidadTotal; }
            set { SetValue(ref _CantidadTotal, value); }

        }
        public List<MdetalleCompras> ListaVistapreviaDc
        {
            get { return _ListaVistapreviaDc; }
            set { SetValue(ref _ListaVistapreviaDc, value); }
        }

        public List<MdetalleCompras> ListaDc
        {
            get { return _ListaDc; }
            set { SetValue(ref _ListaDc, value); }
        }

        public List<Mproductos>ListaProductos
        {
            get { return _ListaProductos; }
            set { SetValue(ref _ListaProductos, value); }
        }


        #endregion

        #region PROCESOS

        public async Task Mostrarproductos(StackLayout Carrilderecha, StackLayout Carrilizquierda)
        {
            var funcion = new Dproductos();
            ListaProductos = await funcion.Mostrarproductos();
            var box = new BoxView
            {
                HeightRequest = 60
            };
            Carrilderecha.Children.Clear();
            Carrilizquierda.Children.Clear();
            Carrilderecha.Children.Add(box);
            

            foreach (var item in ListaProductos)
            {
                Dibujarproductos(item,_index,Carrilderecha,Carrilizquierda);
                _index++;
            }

        }

        public void Dibujarproductos(Mproductos item, int index, StackLayout CarriDerecha, StackLayout carriIzquierda)
        {
            var _ubicacion = Convert.ToBoolean(index % 2);
            var carril = _ubicacion ? CarriDerecha : carriIzquierda;

            var frame = new Frame
            {
                HeightRequest = 300,
                CornerRadius = 10,
                Margin = 8,
                HasShadow = false,
                BackgroundColor = Color.White,
                Padding = 22
            };

            var stack = new StackLayout
            {

            };

            var image = new Image
            {
                Source = item.Icono,
                HeightRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 10)
            };

            var labelprecio = new Label
            {
                Text = "$" + item.Precio,
                FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                Margin = new Thickness(0, 10),
                TextColor = Color.FromHex("#333333")
            };

            var labelDescripcion = new Label
            {
                Text = item.Descripcion,
                FontSize = 16,
                TextColor = Color.Black,
                CharacterSpacing = 1
            };

            var labelpeso = new Label
            {
                Text = item.Peso,
                FontSize = 13,
                TextColor = Color.FromHex("#CCCCCC"),
                CharacterSpacing = 1
                ,
                Margin = new Thickness(0, 0, 0, 5)
            };

            stack.Children.Add(image);
            stack.Children.Add(labelprecio);
            stack.Children.Add(labelDescripcion);
            stack.Children.Add(labelpeso);
            frame.Content = stack;
            var tap = new TapGestureRecognizer();
            tap.Tapped += async (object sender, EventArgs e) =>
            {
                var page = (App.Current.MainPage as SharedTransitionNavigationPage).CurrentPage;
                SharedTransitionNavigationPage.SetBackgroundAnimation(page, BackgroundAnimation.SlideFromRight);
                SharedTransitionNavigationPage.SetTransitionDuration(page, 1000);
                SharedTransitionNavigationPage.SetTransitionSelectedGroup(page, item.IdProducto);
                await Navigation.PushAsync(new AgregarCompras(item));
            };


            carril.Children.Add(frame);
            stack.GestureRecognizers.Add(tap);
        }

        public async Task MostrarVstaPreviDC()
        {
            var funcion = new Ddetallecompras();
            ListaVistapreviaDc = await funcion.MostrarVistapreviaDC();


        }

        public async Task MostrarpanelDc(Grid gridproductos, StackLayout panelcompras, StackLayout panelcontador)
        {
            uint duracion = 700;
            await Task.WhenAll(
                panelcontador.FadeTo(0, 500),
                gridproductos.TranslateTo(0, -200, duracion+200, Easing.CubicIn),
                panelcompras.TranslateTo(0, -300, duracion, Easing.CubicOut)
                );
            isVisiblePanelCompras = true;
        }


        public async Task MostrarGridProductos(Grid gridproductos, StackLayout panelcompras, StackLayout panelcontador)
        {
            uint duracion = 700;
            await Task.WhenAll(
                panelcontador.FadeTo(1, 500),
                gridproductos.TranslateTo(0, 0, duracion + 200, Easing.CubicIn),
                panelcompras.TranslateTo(0, 1000, duracion, Easing.CubicOut)
                );
            isVisiblePanelCompras = false;
        }


        public async Task sumarcantidades()
        {
            var funcion = new Ddetallecompras();
            CantidadTotal = await funcion.sumarCantidad();
        }
        public async Task mostrarDetalleC()
        {
            var funcion = new Ddetallecompras();
            ListaDc = await funcion.MostrarDC();
            
        }

        public async Task ProcesoAsyncrono()
        {
            
        
        }

        public void ProcesoSimple()
        {

        }

        #endregion


        #region COMANDOS
        public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        public ICommand procesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}
