using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoTienda.Modelo
{
    public class MdetalleCompras
    {
        public string Cantidad { get; set; }
        public string PrecioCompra { get; set; }
        public string IdProducto { get; set; }
        public string Total { get; set; }

        public string Iddetallecompra { get; set; }

        public string Imagen { get; set; }
        public string Descripcion { get; set; }
    }
}
