using System;
using System.Collections.Generic;
using System.Linq;
using OpenMind.Helpers;
using Xamarin.Forms;

namespace OpenMind.Paginas.FAQ
{
    public partial class FAQVista : ContentPage
    {
        Accordion FAQsAccordion;
        ScrollView Contenido;
        List<FAQsAgrupacionModeloVista> FAQsListado;

        public FAQVista()
        {
            InitializeComponent();
            Contenido = new ScrollView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(40, 0)
            };
			FAQsListado = new List<FAQsAgrupacionModeloVista>();
			try
			{
				foreach (var faq in App.Database.GetFAQs().ToList())
				{
					FAQsListado.Add(new FAQsAgrupacionModeloVista()
					{
						Pregunta = faq.pregunta,
						Respuesta = faq.respuesta
					});
				}
				FAQsAccordion = new Accordion()
				{
					FirstExpaned = true,
					DataSource = Cursos()
				};
				FAQsAccordion.DataBind();
				Contenido.Content = FAQsAccordion;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine((ex.StackTrace));
			}
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
                                Source = "preguntasFrecuentes.png",
                                WidthRequest = 220,
                                VerticalOptions = LayoutOptions.End,
                                Margin = new Thickness(0,0,0,10)
                            }
                        }
                    },
                    Contenido,
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
            foreach (var curso in FAQsListado)
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
