using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoTienda.Vistas;
using Plugin.SharedTransitions;

namespace ProyectoTienda
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SharedTransitionNavigationPage(new Compras());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
