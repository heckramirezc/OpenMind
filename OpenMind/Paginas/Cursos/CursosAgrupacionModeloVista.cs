using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenMind.ControlesPersonalizados;
using OpenMind.Modelos.SQLite;
using OpenMind.Paginas.Cursos;
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
                    				
                StackLayout ContenidoCursos = new StackLayout
                {
                    Children = 
                    {
                        new ContenidoCursosDT(Codigo, Seccion)
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
