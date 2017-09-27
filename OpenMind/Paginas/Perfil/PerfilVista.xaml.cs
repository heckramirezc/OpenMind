using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace OpenMind.Paginas.Perfil
{
    public partial class PerfilVista : ContentPage
    {
        public PerfilVista()
        {
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
        }
		
        async protected override void OnAppearing()
        {
            base.OnAppearing();
            await Navigation.PushModalAsync(new MiPerfil());
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
