using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenMind.ControlesPersonalizados;
using OpenMind.Data;
using OpenMind.Modelos.Alumno;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace OpenMind.Paginas.Cursos
{
    public class CursoAsistentesDT : PopupPage
    {
        public CursoAsistentesDT(List<AsistenciaRespuesta> asistentes)
        {		

			IconView cerrar = new IconView
			{
				//Margin = new Thickness(0, 0, 10, 0),
				HorizontalOptions = LayoutOptions.End,
				Source = "iCancelarEdicion.png",
				Foreground = Color.FromHex("a2b3bb"),
				WidthRequest = 20,
				HeightRequest = 20
			};
			TapGestureRecognizer cerrarTAP = new TapGestureRecognizer();
			cerrarTAP.Tapped += async (sender, e) =>
			{
				await Navigation.PopPopupAsync();
			};
			cerrar.GestureRecognizers.Add(cerrarTAP);
			Padding = 15;



            ListView Listado = new ListView
            {
                IsPullToRefreshEnabled = false,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HasUnevenRows = true,
                HorizontalOptions = LayoutOptions.Center,
                ItemTemplate = new DataTemplate(typeof(AlumnoDT)),
                ItemsSource = asistentes,
                SeparatorColor = Color.White
            };


			Content = new Frame
			{
				VerticalOptions = LayoutOptions.Center,
				Padding = 5,
                HorizontalOptions = LayoutOptions.Center,
				OutlineColor = Color.White,
				HasShadow = true,
				BackgroundColor = Color.White,
				Content = new StackLayout
				{
					Padding = 5,
					Spacing = 10,
					Children =
					{
						cerrar,
                        new StackLayout
							{
                                Padding=new Thickness(0,10),
								Spacing = 10,
								Children=
								{
									new StackLayout
									{
										Spacing = 0,
										Children=
										{
											 new StackLayout
											{
												Spacing = 0,
												HorizontalOptions = LayoutOptions.Center,
												Children =
												{
													new Label
													{
														Text = "ESTUDIANTES CONFIRMADOS",														
                                                        TextColor = Color.FromHex("3E1152"),
														FontSize = 16,
                                                        FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
													},
													new Label
													{
														FontFamily = Device.OnPlatform("OpenSans", "OpenSans-Regular", null),
														TextColor = Color.FromHex("B2B2B2"),
														HorizontalTextAlignment = TextAlignment.Center,
														HorizontalOptions = LayoutOptions.Center,
														FontSize = 13,
                                                        Text = asistentes.Count.ToString()
													}
												}
											}
										}
									},
									Listado
								}
							},						
						new BoxView { HeightRequest = 3 },
                        new StackLayout
                        {
                            Spacing = 0,
                            Children = 
                            {
								new Label
        						{
        							FontFamily = Device.OnPlatform("OpenSans", "OpenSans-Regular", null),
        							TextColor = Color.FromHex("B2B2B2"),
        							HorizontalTextAlignment = TextAlignment.Center,
        							HorizontalOptions = LayoutOptions.Center,
        							FontSize = 8,
        							Text = "Los alumnos estudiantes listados ya han realizado el pago del evento."
        						},
        						new Label
        						{
        							FontFamily = Device.OnPlatform("OpenSans", "OpenSans-Regular", null),
        							TextColor = Color.FromHex("B2B2B2"),
        							HorizontalTextAlignment = TextAlignment.Center,
        							HorizontalOptions = LayoutOptions.Center,
        							FontSize = 7,
        							Text = "A partir del 14/10 se mostrarán los estudiantes asistentes al evento."
        						},
                            }
                        },						
                        new BoxView { HeightRequest = 3 },

					}
				}
			};
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			//((medicamentos)this.BindingContext).isSelected = false;
			Constantes.ModalAbierto = false;
		}

		protected virtual Task OnAppearingAnimationEnd()
		{
			return Content.FadeTo(0.5);
		}

		protected virtual Task OnDisappearingAnimationBegin()
		{
			return Content.FadeTo(1); ;
		}

		protected override bool OnBackButtonPressed()
		{
			return false;
		}

		protected override bool OnBackgroundClicked()
		{
			return true;
		}
	}

    internal class AlumnoDT : ViewCell
    {
        public AlumnoDT()
        {
            Label lb = new Label
            {
                HeightRequest = 25,
				FontSize = 11,
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("262626"),
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            lb.SetBinding(Label.TextProperty,"nombre");
            View = lb;
        }

    }
}
