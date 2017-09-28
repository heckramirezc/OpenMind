using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenMind.ControlesPersonalizados;
using OpenMind.Modelos.SQLite;
using Xamarin.Forms;

namespace OpenMind
{
	public class CursosAgrupacionModeloVista
	{
		System.Globalization.CultureInfo globalizacion = new System.Globalization.CultureInfo("es-GT");
		public CursosAgrupacionModeloVista() { }
        public string Nombre { get; set; }
        public string Seccion { get; set; }
		public string Codigo { get; set; }
        //public string Correlativo { get; set; }

		public StackLayout Contenido
		{
			get
			{
                Grid Grid = new Grid
                {
					Padding = new Thickness(20, 10, 20, 10),
					ColumnSpacing = 5,
                    RowSpacing = 5,
					VerticalOptions = LayoutOptions.CenterAndExpand,
					HorizontalOptions = LayoutOptions.CenterAndExpand,
					RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }
				},
					ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Auto) },
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
				}
                };


                Grid.Children.Add(
					new Label
					{
                        Text = "Código:",
						FontSize = 14,
						FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
						TextColor = Color.FromHex("3E1152"),
					}, 0,0);
				Grid.Children.Add(
					new Label
					{
                        Text = Codigo,
						FontSize = 14,
						FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
						TextColor = Color.FromHex("262626"),
					}, 1, 0);

				Grid.Children.Add(
					new Label
					{
						Text = "Sección:",
						FontSize = 14,
						FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
						TextColor = Color.FromHex("3E1152"),
					}, 0, 1);
				Grid.Children.Add(
					new Label
					{
                        Text = Seccion,
						FontSize = 14,
						FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
						TextColor = Color.FromHex("262626"),
					}, 1, 1);
				/*
				Grid.Children.Add(
					new Label
					{
						Text = "Correlativo:",
						FontSize = 14,
						FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
						TextColor = Color.FromHex("3E1152"),
					}, 0, 0);
				Grid.Children.Add(
					new Label
					{
                        Text = Nombre,
						FontSize = 14,
						FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
						TextColor = Color.FromHex("CDCDCD"),
					}, 0, 0);*/
                    				
				StackLayout ContenidoCursos = new StackLayout();

				switch (Device.RuntimePlatform)
				{
					case Device.iOS:
						ContenidoCursos = new StackLayout
						{
							IsVisible = true,
							Spacing = 5,
							VerticalOptions = LayoutOptions.FillAndExpand,
							Children =
							{
								new Frame
								{
									Padding = new Thickness(0, 0, 0, 0),
									OutlineColor = Color.FromHex("3E1152"),
									HorizontalOptions = LayoutOptions.CenterAndExpand,
									Content = Grid
								}
							}
						};
						break;
					case Device.Android:
						ContenidoCursos = new StackLayout
						{
							IsVisible = true,
							Spacing = 5,
							VerticalOptions = LayoutOptions.FillAndExpand,
							Children =
							{
								new CustomFrame
								{
									Padding = new Thickness(0, 0, 0, 0),
									OutlineColor = Color.FromHex("3E1152"),
									HorizontalOptions = LayoutOptions.CenterAndExpand,
									BorderRadius = Device.OnPlatform(6, 15, 12),
									Content = Grid
								}
							}
						};
						break;
				}
                		
				return ContenidoCursos;
			}
		}
		public StackLayout Cabecera
		{
			get
			{
                Grid Header = new Grid
                {                    
                    HeightRequest = 35,
                    MinimumWidthRequest = 35,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children =
                    {
						new Button
                        {
                            Text = Nombre,
                            TextColor = Color.FromHex("3E1152"),
                            FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                            FontSize = 10,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand,
            				BackgroundColor = Color.FromHex("ffffff"),            			
            				BorderRadius = 6,
                            BorderColor = Color.FromHex("3E1152"),
                            BorderWidth = 2,
                            HeightRequest = 35
            			}
                    }
                };
				

				return new StackLayout
				{
                    IsVisible = true,
					HeightRequest = 45,
					MinimumWidthRequest = 45,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Spacing = 0,
					Children =
					{						
						Header
					}
				};
			}
		}
	}
}
