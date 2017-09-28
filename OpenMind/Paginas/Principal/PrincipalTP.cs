using System;
using OpenMind.Paginas.Perfil;
using OpenMind.Paginas.Info;
using OpenMind.Paginas.Cursos;
using OpenMind.Paginas.Entrada;
using OpenMind.Paginas.FAQ;
using Xamarin.Forms;
using Plugin.Toasts;
using System.Linq;
using OpenMind.Modelos.SQLite;
using System.Collections.Generic;
using OpenMind.Data;

namespace OpenMind.Paginas.Principal
{
    public class PrincipalTP : TabbedPage
    {
        Page info, cursos, entrada, faq;		
        NavigationPage perfil;
        public PrincipalTP()
        {
            perfil = new NavigationPage(new PerfilVista());
            perfil.Icon = "perfil.png";
            perfil.Title = "Mi perfil";
            info = new InfoVista();		
            cursos = new CursosVista();		
            entrada = new EntradaVista();
            faq = new FAQVista();
			BarBackgroundColor = Color.White;
			BarTextColor = Color.Gray;
            Children.Add(perfil);
            Children.Add(info);
            Children.Add(cursos);
            Children.Add(entrada);
            Children.Add(faq);
		}
		async public void MostrarInfo()
		{
            CurrentPage = info;
            await Navigation.PopModalAsync();
		}

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Perfill();
        }

        public async void Perfill()
        {
            if (CurrentPage == perfil)
                await Navigation.PushModalAsync(new MiPerfil());                           
        }

		async public void MostrarEntrada()
		{
            CurrentPage = entrada;
            await Navigation.PopModalAsync();
		}

        async protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
