using ProyectoTienda.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoTienda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Compras : ContentPage
    {

        VMCompras vm;
        public Compras()
        {
            InitializeComponent();
            vm = new VMCompras(Navigation, CarrilDerecha, CarrilIzquierda);
            BindingContext = vm;

            this.Appearing += Compras_Appearing;
        }

        private  async void Compras_Appearing(object sender, EventArgs e)
        {
           await vm.MostrarVstaPreviDC();
            await vm.mostrarDetalleC();
            await vm.sumarcantidades()
;        }

        private async void DeslizarPanelContador_Swiped(object sender, SwipedEventArgs e)
        {
            await vm.MostrarpanelDc(gridproductos,Paneldetallecompra,Paneldetallecontador);
        }

        private async void DeslizarPanelDetalleCompra_Swiped(object sender, SwipedEventArgs e)
        {
            await vm.MostrarGridProductos(gridproductos, Paneldetallecompra, Paneldetallecontador);
        }
    }
}