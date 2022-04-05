﻿using ProyectoTienda.Modelo;
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
    public partial class AgregarCompras : ContentPage
    {
        public AgregarCompras(Mproductos parametrosTrae)
        {
            InitializeComponent();
            BindingContext = new VMAgregarcompra(Navigation,parametrosTrae);
        }
    }
}