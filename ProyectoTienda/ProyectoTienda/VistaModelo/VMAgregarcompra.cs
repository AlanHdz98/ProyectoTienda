using ProyectoTienda.Datos;
using ProyectoTienda.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace ProyectoTienda.VistaModelo
{
    public class VMAgregarcompra : BaseViewModel
    {

        #region VARIABLES

        int _Cantidad;
        string _precioTexto;
        public Mproductos parametrosrecibe { get; set; }

        #endregion

        #region CONSTRUCTOR
        public VMAgregarcompra(INavigation navigation, Mproductos parametrosTrae)
        {
            Navigation = navigation;
            parametrosrecibe = parametrosTrae;
            PrecioTexto = "$" + parametrosrecibe.Precio;
        }

        #endregion

        #region OBJETOS
        public string PrecioTexto
        {
            get { return _precioTexto; }
            set { SetValue(ref _precioTexto, value); }
        }

        public int Cantidad
        {
            get { return _Cantidad; }
            set { SetValue(ref _Cantidad, value); }
        }

        #endregion

        #region PROCESOS

        public async Task validarCompra()
        {
            var funcion = new Ddetallecompras();
            var listaXidproducto = await funcion.MostrarDCporId(parametrosrecibe.IdProducto);
            if (listaXidproducto.Count > 0)
            {
                await EditarDc();            }
            else
            {
                await InsertarDc();           
            }
        }

        public async Task EditarDc()
        {
            if (Cantidad < 1)
            {
                Cantidad = 1;
            }
            var funcion = new Ddetallecompras();
            var parametros = new MdetalleCompras();
            parametros.Cantidad = Cantidad.ToString();
            parametros.IdProducto = parametrosrecibe.IdProducto;
            parametros.PrecioCompra = parametrosrecibe.Precio;
            await funcion.Editardetalle(parametros);
            await Volver();


        }
        public async Task Volver()
        {
            await Navigation.PopAsync();
        }


        public async Task InsertarDc()
        {
            if(Cantidad == 0)
            {
                Cantidad = 1;
            }
            var funcion = new Ddetallecompras();
            var parametros = new MdetalleCompras();

            parametros.Cantidad = Cantidad.ToString();
            parametros.IdProducto = parametrosrecibe.IdProducto;
            parametros.PrecioCompra = parametrosrecibe.Precio;
            double total = 0;
            double preciocompra = Convert.ToDouble(parametrosrecibe.Precio);
            double cantidadprod = Convert.ToDouble(Cantidad);
            total = cantidadprod * preciocompra;

            parametros.Total = total.ToString();

            await funcion.InsertarDc(parametros);
            await Volver();
        }

        public void Aumentar()
        {


            Cantidad += 1;

        }
        public void Disminuir()
        {
            if (Cantidad > 0)
            {
                Cantidad -= 1;
            }
        }

        #endregion


        #region COMANDOS
        public ICommand Volvercommand => new Command(async () => await Volver());
        public ICommand Aumentarcommand => new Command(Aumentar);
        public ICommand Disminuircommand => new Command(Disminuir);
        public ICommand Insertarcommand => new Command(async()=> await validarCompra());
        #endregion
    }
}
