using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;

using OpenMind.Modelos.SQLite;
using OpenMind.Helpers;
using OpenMind.ControlesPersonalizados;
using OpenMind.Modelos.Usuario;
using System.Text.RegularExpressions;
using Plugin.Toasts;


namespace OpenMind.Paginas.Principal
{
	public partial class Forget : PopupPage
	{
		String PwdReset_ID;
		public ExtendedEntry Usuario, contrasenia;
		ExtendedEntry codigo, confirmacionContrasenia;
		Button recuperar, continuar, cambiar;
		StackLayout envioCodigo, confirmacionCodigo, cambioContrasenia;
		public bool Logeado;
		RelativeLayout gridBotonRecuperar;

		public Forget()
		{
			Usuario = new ExtendedEntry()
			{
				Keyboard = Keyboard.Email,				
				HasBorder = false,
				IsPassword = false,
				Placeholder = "Toca para ingresar",
				PlaceholderColor = Color.FromHex("91a5af"),
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("CDCDCD"),
				XAlign = TextAlignment.End,
				FontSize = 14,
				Margin = 0,
                ReturnKeyType = ReturnKeyTypes.Go
			};
            Usuario.Completed += (sender, e) => { Usuario.Unfocus(); recuperacion(); };
			codigo = new ExtendedEntry
			{
				Keyboard = Keyboard.Numeric,
				TextColor = Color.FromHex("3F3F3F"),
				HasBorder = false,
				IsPassword = false,
				Placeholder = "Toca para ingresar",
				PlaceholderColor = Color.FromHex("B2B2B2"),
				XAlign = TextAlignment.End,
				FontFamily = Device.OnPlatform("OpenSans-Bold", "OpenSans-Bold", null),
				FontSize = 14,
				Margin = new Thickness(0, 0, 35, 0)
			};
			codigo.TextChanged += (sender, e) =>
			{
				if (!codigo.Text.Equals(PwdReset_ID))
				{
					codigo.TextColor = Color.FromHex("E9242A");
				}
				else
				{
					codigo.TextColor = Color.FromHex("53A946");
				}
			};

			IconView emailView = new IconView
			{
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center,
				Source = "email.png",
				Foreground = Color.FromHex("B2B2B2"),
				WidthRequest = 20
			};
			IconView codigoView = new IconView
			{
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center,
				Source = "iSeguridad.png",
				Foreground = Color.FromHex("B2B2B2"),
				WidthRequest = 15
			};
			IconView contraseniaView = new IconView
			{
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center,
				Source = "iPassView.png",
				Foreground = Color.FromHex("B2B2B2"),
				WidthRequest = 25
			};
			IconView contraseniaConfirmacionView = new IconView
			{
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center,
				Source = "iPassView.png",
				Foreground = Color.FromHex("B2B2B2"),
				WidthRequest = 25
			};


			TapGestureRecognizer contraseniaViewTAP = new TapGestureRecognizer();
			TapGestureRecognizer contraseniaConfirmacionViewTAP = new TapGestureRecognizer();
			contraseniaViewTAP.Tapped += (sender, e) =>
			{
				if (contrasenia.IsPassword)
				{
					contrasenia.IsPassword = false;
					contraseniaView.Foreground = Color.FromHex("007D8C");

				}
				else
				{
					contrasenia.IsPassword = true;
					contraseniaView.Foreground = Color.FromHex("B2B2B2");
				}
			};

			contraseniaConfirmacionViewTAP.Tapped += (sender, e) =>
			{
				if (confirmacionContrasenia.IsPassword)
				{
					confirmacionContrasenia.IsPassword = false;
					contraseniaConfirmacionView.Foreground = Color.FromHex("007D8C");
				}
				else
				{
					confirmacionContrasenia.IsPassword = true;
					contraseniaConfirmacionView.Foreground = Color.FromHex("B2B2B2");
				}
			};

			contraseniaView.GestureRecognizers.Add(contraseniaViewTAP);
			contraseniaConfirmacionView.GestureRecognizers.Add(contraseniaConfirmacionViewTAP);


			recuperar = new Button
			{
				Text = "RECUPERAR",
				VerticalOptions = LayoutOptions.Start,
				TextColor = Color.FromHex("ffffff"),
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				FontSize = 15,
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("3E1152"),
				WidthRequest = 200,
				HeightRequest = 35,
				BorderRadius = 20,
			};
			recuperar.Clicked += Recuperar_Clicked;



			continuar = new Button
			{
				Text = "VALIDAR",
				FontSize = 18,
				TextColor = Color.White,
				FontAttributes = FontAttributes.Bold,
				FontFamily = Device.OnPlatform("OpenSans-Bold", "OpenSans-Bold", null),
				VerticalOptions = LayoutOptions.Start,
				BackgroundColor = Color.FromHex("53A946"),
				WidthRequest = 128,
				HeightRequest = 38,
			};
			continuar.Clicked += Continuar_Clicked;

			cambiar = new Button
			{
				Text = "CONFIRMAR",
				FontSize = 18,
				TextColor = Color.White,
				FontAttributes = FontAttributes.Bold,
				FontFamily = Device.OnPlatform("OpenSans-Bold", "OpenSans-Bold", null),
				VerticalOptions = LayoutOptions.Start,
				BackgroundColor = Color.FromHex("53A946"),
				WidthRequest = 138,
				HeightRequest = 38,
			};
			cambiar.Clicked += Cambiar_Clicked;

			gridBotonRecuperar = new RelativeLayout
			{
				WidthRequest = 130,
				HeightRequest = 42,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.End,
			};
			gridBotonRecuperar.Children.Add(
			new RoundedBoxView.Forms.Plugin.Abstractions.RoundedBoxView
			{
				BackgroundColor = Color.FromHex("B2B2B2"),
				CornerRadius = 6,
				HeightRequest = 40,
				WidthRequest = 128,
			}, Constraint.Constant(2), Constraint.Constant(2));
			gridBotonRecuperar.Children.Add(recuperar, Constraint.Constant(0), Constraint.Constant(0));


			RelativeLayout gridBotonValidar = new RelativeLayout
			{
				WidthRequest = 130,
				HeightRequest = 42,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.End,
			};
			gridBotonValidar.Children.Add(
			new RoundedBoxView.Forms.Plugin.Abstractions.RoundedBoxView
			{
				BackgroundColor = Color.FromHex("B2B2B2"),
				CornerRadius = 6,
				HeightRequest = 40,
				WidthRequest = 128,
			}, Constraint.Constant(2), Constraint.Constant(2));
			gridBotonValidar.Children.Add(continuar, Constraint.Constant(0), Constraint.Constant(0));

			RelativeLayout gridBoton = new RelativeLayout
			{
				WidthRequest = 140,
				HeightRequest = 42,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.End,
			};
			gridBoton.Children.Add(
			new RoundedBoxView.Forms.Plugin.Abstractions.RoundedBoxView
			{
				BackgroundColor = Color.FromHex("B2B2B2"),
				CornerRadius = 6,
				HeightRequest = 40,
				WidthRequest = 138,
			}, Constraint.Constant(2), Constraint.Constant(2));
			gridBoton.Children.Add(cambiar, Constraint.Constant(0), Constraint.Constant(0));


			IconView cerrar = new IconView
			{
				//Margin = new Thickness(0, 0, 10, 0),
				HorizontalOptions = LayoutOptions.End,
				//Source = EstilosCita.Iconos.Cancelar,
				Foreground = Color.FromHex("a2b3bb"),
				WidthRequest = 20,
				HeightRequest = 20
			};
			TapGestureRecognizer cerrarTAP = new TapGestureRecognizer();
			cerrarTAP.Tapped += async (sender, e) =>
			{
				bool accion = await DisplayAlert("", "¿Desea cancelar la recuperación?", "Cancelar", "Regresar");
				if (accion)
					await Navigation.PopPopupAsync();
			};
			cerrar.GestureRecognizers.Add(cerrarTAP);


			var indicador = new ActivityIndicator
			{
				IsVisible = true,
				IsRunning = true,
				BindingContext = this,
				Color = Color.FromHex("2AB4EE"),
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
			};
			indicador.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");


			envioCodigo = new StackLayout
			{
				Padding = new Thickness(20, 5),
				Spacing = 25,
				Children =
				{
					new Label
					{
						Text = "RECUPERACIÓN DE CONTRASEÑA",
						HorizontalTextAlignment = TextAlignment.Center,
						TextColor = Color.FromHex("3E1152"),
                        FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
						FontSize = 18,
						VerticalOptions = LayoutOptions.Center
					},
					new StackLayout
					{
						Spacing = 0,
						Children =
						{
							new Label
							{
								Text ="USUARIO: *",
								FontSize = 13,
								TextColor = Color.FromHex("3E1152"),
								FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
							},
							new StackLayout
							{                                
								Spacing=1,
								Children=
								{
                                    
									new Grid
									{
										Children=
										{
											Usuario,
											emailView
										}
									},
									new BoxView {BackgroundColor= Color.FromHex("3E1152"), HeightRequest=2 },
									new BoxView {HeightRequest=0 }
								}
							}
						}
					},
					indicador,
					recuperar
				}
			};

			confirmacionCodigo = new StackLayout
			{
				IsVisible = false,
				Padding = new Thickness(20, 5),
				Spacing = 25,
				Children =
				{
					new Label
					{
						Text = "Ingresa el código \r\nque has recibido \r\nen tu badeja de entrada",
						HorizontalTextAlignment = TextAlignment.Center,
						TextColor = Color.FromHex("007D8C"),
						FontFamily = Device.OnPlatform("OpenSans-Bold", "OpenSans-Bold", null),
						FontSize = 18,
						VerticalOptions = LayoutOptions.Center
					},
					new StackLayout
					{
						Spacing = 0,
						Children =
						{
							new Label
							{
								Text ="CÓDIGO DE SEGURIDAD: *",
								FontSize = 13,
								TextColor = Color.FromHex("007D8C"),
								FontAttributes = FontAttributes.Bold,
								FontFamily = Device.OnPlatform("OpenSans-Bold", "OpenSans-Bold", null)
							},
							new StackLayout
							{
								Spacing=1,
								Children=
								{
									new Grid
									{
										Children=
										{
											codigo,
											codigoView
										}
									},
									new BoxView {BackgroundColor= Color.FromHex("007D8C"), HeightRequest=2 },
									new BoxView {HeightRequest=0 }
								}
							}
						}
					},
					gridBotonValidar
				}
			};


			contrasenia = new ExtendedEntry
			{
				TextColor = Color.FromHex("3F3F3F"),
				HasBorder = false,
				IsPassword = false,
				Placeholder = "Toca para ingresar",
				PlaceholderColor = Color.FromHex("B2B2B2"),
				XAlign = TextAlignment.End,
				FontFamily = Device.OnPlatform("OpenSans-Bold", "OpenSans-Bold", null),
				FontSize = 14,
				Margin = new Thickness(0, 0, 35, 0)
			};

			confirmacionContrasenia = new ExtendedEntry
			{
				TextColor = Color.FromHex("3F3F3F"),
				HasBorder = false,
				IsPassword = false,
				Placeholder = "Toca para confirmar contraseña",
				PlaceholderColor = Color.FromHex("B2B2B2"),
				XAlign = TextAlignment.End,
				FontFamily = Device.OnPlatform("OpenSans-Bold", "OpenSans-Bold", null),
				FontSize = 14,
				Margin = new Thickness(0, 0, 35, 0)
			};

			confirmacionContrasenia.TextChanged += (sender, e) =>
			{
				if (!confirmacionContrasenia.Text.Equals(contrasenia.Text))
				{
					confirmacionContrasenia.TextColor = Color.FromHex("E9242A");
					contrasenia.TextColor = Color.FromHex("3F3F3F");
				}
				else
				{
					contrasenia.TextColor = Color.FromHex("53A946");
					confirmacionContrasenia.TextColor = Color.FromHex("53A946");
				}
			};


			cambioContrasenia = new StackLayout
			{
				IsVisible = false,
				Padding = new Thickness(20, 5),
				Spacing = 25,
				Children =
				{
					new StackLayout
					{
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						Children =
						{
							new IconView
							{
								Source = "iSeguridad.png",
								WidthRequest = 15,
								HeightRequest = 15,
								Foreground = Color.FromHex("007D8C"),
								VerticalOptions = LayoutOptions.Center
							},
							new Label
							{
								Text = "Ingresa y confirma\r\ntu nueva contraseña",
								HorizontalTextAlignment = TextAlignment.Center,
								TextColor = Color.FromHex("007D8C"),
								FontFamily = Device.OnPlatform("OpenSans-ExtraBold", "OpenSans-ExtraBold", null),
								FontSize = 18,
								VerticalOptions = LayoutOptions.Center
							}
						}
					},
					new StackLayout
					{
						Spacing = 0,
						Children =
						{
							new Label
							{
								Text ="NUEVA CONTRASEÑA:*",
								FontSize = 13,
								TextColor = Color.FromHex("007D8C"),
								FontAttributes = FontAttributes.Bold,
								FontFamily = Device.OnPlatform("OpenSans-Bold", "OpenSans-Bold", null)
							},
							new StackLayout
							{
								Spacing=1,
								Children=
								{
									new Grid
									{
										Children=
										{
											contrasenia,
											contraseniaView
										}
									},
									new BoxView {BackgroundColor= Color.FromHex("007D8C"), HeightRequest=2 },
									new BoxView {HeightRequest=0 }
								}
							}
						}
					},
					new StackLayout
					{
						Spacing = 0,
						Children =
						{
							new Label
							{
								Text ="CONFIRMAR CONTRASEÑA:*",
								FontSize = 13,
								TextColor = Color.FromHex("007D8C"),
								FontAttributes = FontAttributes.Bold,
								FontFamily = Device.OnPlatform("OpenSans-Bold", "OpenSans-Bold", null)
							},
							new StackLayout
							{
								Spacing=1,
								Children=
								{
									new Grid
									{
										Children=
										{
											confirmacionContrasenia,
											contraseniaConfirmacionView
										}
									},
									new BoxView {BackgroundColor= Color.FromHex("007D8C"), HeightRequest=2 },
									new BoxView {HeightRequest=0 }
								}
							}
						}
					},
					gridBoton
				}
			};

			Padding = new Thickness(15, 0); ;
			Content = new Frame
            {
                VerticalOptions = LayoutOptions.Center,
                Padding = 5,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                OutlineColor = Color.White,
				HasShadow = true,
                BackgroundColor = Color.White,
				Content = new StackLayout
				{
					Padding = 5,
					Spacing = 0,
					Children = {
						cerrar,
						new ScrollView
						{
							Padding= 0,
							HorizontalOptions= LayoutOptions.FillAndExpand,
							VerticalOptions = LayoutOptions.FillAndExpand,
							Content = new StackLayout
							{
								Children =
								{
									envioCodigo,
									confirmacionCodigo,
									cambioContrasenia
								}
							}
						}

					}
				}
			};
			contrasenia.IsPassword = true;
			confirmacionContrasenia.IsPassword = true;
		}

		async void Continuar_Clicked(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(codigo.Text))
			{
				await DisplayAlert("", "Por favor, ingrese el código enviado.", "Aceptar");
				codigo.Focus();
				return;
			}
			else
			{
				if (!String.IsNullOrEmpty(PwdReset_ID))
				{
					if (codigo.Text.Equals(PwdReset_ID))
					{
						confirmacionCodigo.IsVisible = false;
						cambioContrasenia.IsVisible = true;
					}
					else
					{
						await DisplayAlert("", "El código ingresado no coincide con el enviado.", "Aceptar");
						codigo.Focus();
						return;
					}
				}
				else
				{
					ShowToast(ToastNotificationType.Error, "Recuperación de contraseña", "Servicio no disponible, intente más tarde.", 7);
					await Navigation.PopPopupAsync();
				}
			}
		}

		

		async void Cambiar_Clicked(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(contrasenia.Text))
			{
				await DisplayAlert("", "Por favor, indique su nueva contraseña", "Aceptar");
				contrasenia.Focus();
				return;
			}
			else
			{
				if (!Regex.Match(contrasenia.Text, @"^\S{6,15}$").Success)
				{
					await DisplayAlert("", "La contraseña debe tener entre 6 y 15 caracteres.", "Aceptar");
					contrasenia.Focus();
					return;
				}
			}
			if (String.IsNullOrEmpty(confirmacionContrasenia.Text))
			{
				await DisplayAlert("", "Por favor, confirme su nueva contraseña.", "Aceptar");
				confirmacionContrasenia.Focus();
				return;
			}

			if ((!String.IsNullOrEmpty(contrasenia.Text) && !String.IsNullOrEmpty(confirmacionContrasenia.Text)) && !confirmacionContrasenia.Text.Equals(contrasenia.Text))
			{
				await DisplayAlert("", "Las contraseñas no coinciden", "Aceptar");
				confirmacionContrasenia.Focus();
				return;
			}
			
			
		}

        async void recuperacion()
        {
			Usuario.IsEnabled = false;
			if (String.IsNullOrEmpty(Usuario.Text))
			{
				await DisplayAlert("", "Por favor, indique su Usuario/Carné ", "Aceptar");
				Usuario.IsEnabled = true;
				Usuario.Focus();
				return;
			}
			this.IsBusy = true;
			recuperar.IsEnabled = false;
			recuperar.IsVisible = false;
			gridBotonRecuperar.IsVisible = false;

            this.IsBusy = false;
			recuperar.IsVisible = true;
			recuperar.IsEnabled = true;
			gridBotonRecuperar.IsVisible = true;
        }

		void Recuperar_Clicked(object sender, EventArgs e)
		{
            recuperacion();
		}

		private async void ShowToast(ToastNotificationType type, string titulo, string descripcion, int tiempo)
		{
			var notificator = DependencyService.Get<IToastNotificator>();
			bool tapped = await notificator.Notify(type, titulo, descripcion, TimeSpan.FromSeconds(tiempo));
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
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
			Device.BeginInvokeOnMainThread(async () =>
			{
				var result = await this.DisplayAlert("", "¿Desea cancelar la recuperación?", "Cancelar", "Regresar");
				if (result) await this.Navigation.PopPopupAsync();
			});
			return true;
		}

		protected override bool OnBackgroundClicked()
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
				var result = await this.DisplayAlert("", "¿Desea cancelar la recuperación?", "Cancelar", "Regresar");
				if (result) await this.Navigation.PopPopupAsync();
			});
			return false;
		}

		private async void Login(string email, string password)
		{
						
		}
	}
}
