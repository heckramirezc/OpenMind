using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenMind.Modelos.SQLite;
using Xamarin.Forms;

namespace OpenMind
{
	public class FAQsAgrupacionModeloVista
	{		
		public FAQsAgrupacionModeloVista() { }
		public string Pregunta { get; set; }
		public string Respuesta { get; set; }
		

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
                    Children =
                    {
						new Label
    					{
    						Text = Respuesta,
    						FontSize = 14,
                            HorizontalTextAlignment = TextAlignment.Center,
    						FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
    						TextColor = Color.FromHex("262626"),
    					}
                    }
				};



				var fBody = new Frame
				{
					Padding = new Thickness(0, 0, 0, 0),
					OutlineColor = Color.FromHex("3E1152"),
					HorizontalOptions = LayoutOptions.CenterAndExpand,
					HasShadow = false,
					Content = Grid
				};
				StackLayout ContenidoCursos = new StackLayout
				{
					IsVisible = true,
					Spacing = 5,
					VerticalOptions = LayoutOptions.FillAndExpand,
					Children =
					{
						fBody
					}
				};

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
                            Text = Pregunta,
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
