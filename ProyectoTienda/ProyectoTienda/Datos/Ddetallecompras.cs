using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;
using ProyectoTienda.Modelo;
using ProyectoTienda.Conexion;



namespace ProyectoTienda.Datos
{
    public  class Ddetallecompras
    {
        public async Task InsertarDc(MdetalleCompras parametros)
        {
            await Cconexion.firebase
                .Child("DetalleCompra")
                .PostAsync(new MdetalleCompras()
                {
                    Cantidad = parametros.Cantidad,
                    IdProducto = parametros.IdProducto,
                    PrecioCompra = parametros.PrecioCompra,
                    Total = parametros.Total
                }) ;
        }



        public async Task<List<MdetalleCompras>> MostrarVistapreviaDC()
        {
            var ListaDc = new List<MdetalleCompras>();
            var parametrosProductos = new Mproductos();
            var funcionproductos = new Dproductos();
            var data = (await Cconexion.firebase
                .Child("DetalleCompra")
                .OnceAsync<MdetalleCompras>())
                .Where(a => a.Key != "Modelo")
                .Select(item => new MdetalleCompras
                {
                    IdProducto = item.Object.IdProducto,
                    Iddetallecompra = item.Key
                })
                ;


            foreach (var hobit in data)
            {
                var parametros = new MdetalleCompras();
                parametros.IdProducto = hobit.IdProducto;
                parametrosProductos.IdProducto = hobit.IdProducto;
                var listaproductos = await funcionproductos.MostrarproductosId(parametrosProductos);
                parametros.Imagen = listaproductos[0].Icono;
                ListaDc.Add(parametros);
            }
            return ListaDc;
        }

        public async Task<List<MdetalleCompras>> MostrarDC()
        {
            var ListaDc = new List<MdetalleCompras>();
            var parametrosProductos = new Mproductos();
            var funcionproductos = new Dproductos();
            var data = (await Cconexion.firebase
                .Child("DetalleCompra")
                .OnceAsync<MdetalleCompras>())
                .Where(a => a.Key != "Modelo")
                .Select(item => new MdetalleCompras
                {
                    IdProducto = item.Object.IdProducto,
                    Iddetallecompra = item.Key,
                    Cantidad=item.Object.Cantidad,
                    Total = item.Object.Total
                })
                ;


            foreach (var hobit in data)
            {
                var parametros = new MdetalleCompras();
                parametros.IdProducto = hobit.IdProducto;
                parametrosProductos.IdProducto = hobit.IdProducto;
                var listaproductos = await funcionproductos.MostrarproductosId(parametrosProductos);
                parametros.Descripcion = listaproductos[0].Descripcion;
                parametros.Imagen = listaproductos[0].Icono;
                parametros.Cantidad = hobit.Cantidad;
                parametros.Total = hobit.Total;
                ListaDc.Add(parametros);
            }
            return ListaDc;
        }

        public async Task<List<MdetalleCompras>> MostrarDCporId(string idproducto)
        {
            return (await Cconexion.firebase
                .Child("DetalleCompra")
                .OnceAsync<MdetalleCompras>())
                .Where(a=>a.Object.IdProducto==idproducto)
                .Select(item => new MdetalleCompras
                {
                    Total = item.Object.Total
                }
                ).ToList();
        }
        
        public async Task Editardetalle(MdetalleCompras parametros)
        {
            var data = (await Cconexion.firebase
                .Child("DetalleCompra").OnceAsync<MdetalleCompras>())
                .Where(a => a.Object.IdProducto == parametros.IdProducto)
                .FirstOrDefault();

            double cantidadInicial = Convert.ToDouble(data.Object.Cantidad);
            data.Object.Cantidad = (cantidadInicial+Convert.ToDouble(parametros.Cantidad)).ToString();
            double cantidad = Convert.ToDouble(data.Object.Cantidad);
            double preciocompra = Convert.ToDouble(parametros.PrecioCompra);
            double total = 0;
            total = cantidad * preciocompra;
            data.Object.Total = total.ToString();
            await Cconexion.firebase
                .Child("DetalleCompra")
                .Child(data.Key)
                .PutAsync(data.Object);
        
        }

        public async Task<List<MdetalleCompras>> MostrarCantidades()
        {
            return (await Cconexion.firebase
                .Child("DetalleCompra")
                .OnceAsync<MdetalleCompras>())
                .Where(a=>a.Key!="Modelo")
                .Select(item => new MdetalleCompras
                {
                    Cantidad = item.Object.Cantidad
                }
                ).ToList();
        }


        public async Task <string> sumarCantidad()
        {
            var funcion = new Ddetallecompras();
            var lista = await funcion.MostrarCantidades();
            double cantidad = 0;
            foreach(var hobit in lista)
            {
                cantidad += Convert.ToDouble(hobit.Cantidad);
            }
            return cantidad.ToString();
        }
    
    }
    


}
