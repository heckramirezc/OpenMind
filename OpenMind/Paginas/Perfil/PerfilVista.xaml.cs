using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace OpenMind.Paginas.Perfil
{
    public partial class PerfilVista : ContentPage
    {
        bool mostrar = true;
        public PerfilVista()
        {
            MessagingCenter.Subscribe<Principal.PrincipalTP>(this, "Desactivar", (sender) =>
			{
                mostrar = false;
			});
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
        }
		
        async protected override void OnAppearing()
        {
            base.OnAppearing();
			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					await Navigation.PushModalAsync(new MiPerfil());
					break;
                case Device.Android:
                    if(!Data.Constantes.PerfilAbierto && mostrar)
                    {
                        await Navigation.PushModalAsync(new MiPerfil());
                    }
                    break;
			}
        }

        public void MostrarEntrada()
		{            
			((Principal.PrincipalTP)this.Parent).MostrarEntrada();
		}

		public void MostrarInfo()
		{
			((Principal.PrincipalTP)this.Parent).MostrarInfo();
		}
    }

	
}
