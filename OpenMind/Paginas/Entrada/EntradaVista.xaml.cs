using System;
using System.Collections.Generic;
using System.Linq;
using OpenMind.Helpers;
using OpenMind.Modelos.Entrada;
using Xamarin.Forms;

namespace OpenMind.Paginas.Entrada
{
    public partial class EntradaVista : ContentPage
    {
        Image imageQR;
        public EntradaVista()
        {            
            InitializeComponent();
            imageQR = new Image
            {                
                WidthRequest = 300,
                HeightRequest = 300,
				HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,				
                Aspect = Aspect.Fill
            };
            if(!String.IsNullOrEmpty(Settings.session_url))
            {
                imageQR.Source = new Uri(Settings.session_url);
            }
			StackLayout Contenido = new StackLayout
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
								Source = "miEntrada.png",
								WidthRequest = 220,
								VerticalOptions = LayoutOptions.End,
								Margin = new Thickness(0,0,0,10)
							}
						}
					},
					new ScrollView
					{
						VerticalOptions = LayoutOptions.FillAndExpand,
						Content= new StackLayout
						{
							HorizontalOptions = LayoutOptions.CenterAndExpand,
							VerticalOptions = LayoutOptions.CenterAndExpand,
							Children =
							{
								imageQR
							}
						}

					},
					new BoxView
					{
						BackgroundColor = Color.FromHex("3E1152"),
						VerticalOptions = LayoutOptions.End,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						HeightRequest = 2
					}
				}
			};
            Content = Contenido;

            TapGestureRecognizer tap = new TapGestureRecognizer();
			tap.Tapped += (sender, e) => {
				if (!String.IsNullOrEmpty(Settings.session_url))
				{
					imageQR.Source = new Uri(Settings.session_url);
				}
			};
            Contenido.GestureRecognizers.Add(tap);
        }
        async protected override void OnAppearing()
        {
            base.OnAppearing();
			if (!String.IsNullOrEmpty(Settings.session_url))
			{
				imageQR.Source = new Uri(Settings.session_url);
			}
        }
    }
}
