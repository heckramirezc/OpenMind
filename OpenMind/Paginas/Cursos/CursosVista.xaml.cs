using System;
using System.Collections.Generic;
using System.Linq;
using OpenMind.Helpers;
using OpenMind.Modelos.Alumno;
using OpenMind.Modelos.SQLite;
using Xamarin.Forms;

namespace OpenMind.Paginas.Cursos
{
    public partial class CursosVista : ContentPage
    {
        Accordion CursosAccordion;
        ScrollView Contenido;
        List<CursosAgrupacionModeloVista> CursosListado;

        public CursosVista()
        {
            InitializeComponent();
            Contenido = new ScrollView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(40, 0)
            };

			CursosListado = new List<CursosAgrupacionModeloVista>();
			try
			{
				foreach (var curso in App.Database.GetCursos().ToList())
				{
					CursosListado.Add(new CursosAgrupacionModeloVista()
					{
						Nombre = curso.nombre,
						Codigo = curso.idCurso,
						Seccion = curso.seccion
					});
				}
				CursosAccordion = new Accordion()
				{
					FirstExpaned = true,
					DataSource = Cursos()
				};
				CursosAccordion.DataBind();
				Contenido.Content = CursosAccordion;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine((ex.StackTrace));
			}

            Label contactar = new Label
            {
                Text = "¿Tienes dudas? contáctanos.",
                TextColor = Color.FromHex("3E1152"),
                FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 20),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Center
            };
			var tap2 = new TapGestureRecognizer();
			tap2.Tapped += (s, e) =>
			{
				Device.OpenUri(new Uri("https://goo.gl/F2LzXD"));
			};
			contactar.GestureRecognizers.Add(tap2);
			Content = new StackLayout
			{
				Children =
				{                    
                    new Grid
                    {
                        HeightRequest = 165,   
                        Children =
                        {
							new Image
        					{
                                
        						Source = "header.png",
        						HorizontalOptions = LayoutOptions.FillAndExpand,
        						VerticalOptions = LayoutOptions.Start,
                                Margin = new Thickness(-25,0),
                                Aspect = Aspect.AspectFill
        					},
							new Image
        					{
        						Source = "misCursos.png",   
                                WidthRequest = 220,
        						VerticalOptions = LayoutOptions.End,
                                Margin = new Thickness(0,0,0,10)
        					}
                        }
                    },
                    Contenido,
					contactar,
					new BoxView
					{
						BackgroundColor = Color.FromHex("3E1152"),
						VerticalOptions = LayoutOptions.End,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						HeightRequest = 2
					}
				}
			};
        }
		public List<AccordionSource> Cursos()
		{			
			List<AccordionSource> ItemsCursos = new List<AccordionSource>();
			foreach (var curso in CursosListado)
			{
				if (curso.Cabecera.IsVisible && curso.Contenido.IsVisible)
				{
					ItemsCursos.Add(new AccordionSource
					{
						Cabecera = curso.Cabecera,
						Contenido = curso.Contenido
					});					
				}
			}
			return ItemsCursos;
		}

        async protected override void OnAppearing()
        {
            base.OnAppearing();						
		}
    }
}
