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
	public partial class CambioContrasenia : PopupPage
	{		
		public ExtendedEntry contrasenia;
		ExtendedEntry confirmacionContrasenia;
		Button  cambiar;
		StackLayout cambioContrasenia;
		public bool Logeado;		
        public String user, pass;

		public CambioContrasenia(String user, String pass)
		{
            this.user = user;
            this.pass = pass;
			
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


			

			cambiar = new Button
			{
				Text = "CONFIRMAR",				
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
			cambiar.Clicked += Cambiar_Clicked;

			
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

			contrasenia = new ExtendedEntry
			{
				
				HasBorder = false,
				IsPassword = false,
				Placeholder = "Toca para ingresar",
				
				XAlign = TextAlignment.End,
				PlaceholderColor = Color.FromHex("91a5af"),
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("CDCDCD"),
				FontSize = Device.OnPlatform(16, 18, 12),
                Margin = new Thickness(0,0,35,0),
                ReturnKeyType = ReturnKeyTypes.Next
			};
            contrasenia.Completed += (sender, e) => { confirmacionContrasenia.Focus(); };

			confirmacionContrasenia = new ExtendedEntry
			{
				
				HasBorder = false,
				IsPassword = false,
				Placeholder = "Toca para confirmar contraseña",
				
				XAlign = TextAlignment.End,
				PlaceholderColor = Color.FromHex("91a5af"),
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("CDCDCD"),
				FontSize = Device.OnPlatform(16, 18, 12),
				Margin = new Thickness(0, 0, 35, 0),
                ReturnKeyType = ReturnKeyTypes.Go
			};
            confirmacionContrasenia.Completed += (sender, e) => { recuperacion(); };

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
				IsVisible = true,
				Padding = new Thickness(20, 5),
				Spacing = 25,
				Children =
				{
					new StackLayout
					{
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						Children =
						{
							new Label
							{
								Text = "CAMBIO DE CONTRASEÑA",
								HorizontalTextAlignment = TextAlignment.Center,
								TextColor = Color.FromHex("3E1152"),
								FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
								FontSize = 18,
								VerticalOptions = LayoutOptions.Center
							},
							new Label
							{
								Text = "Actualiza tu contraseña para continuar",
								HorizontalTextAlignment = TextAlignment.Center,
								TextColor = Color.FromHex("3E1152"),
								FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
								FontSize = 14,
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
											contrasenia,
											contraseniaView
										}
									},
									new BoxView {BackgroundColor= Color.FromHex("3E1152"), HeightRequest=2 },
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
											confirmacionContrasenia,
											contraseniaConfirmacionView
										}
									},
									new BoxView {BackgroundColor= Color.FromHex("3E1152"), HeightRequest=2 },
									new BoxView {HeightRequest=0 }
								}
							}
						}
					},
                    indicador,
                    cambiar
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
						new ScrollView
						{
							Padding= 0,
							HorizontalOptions= LayoutOptions.FillAndExpand,
							VerticalOptions = LayoutOptions.FillAndExpand,
							Content = new StackLayout
							{
								Children =
								{									
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

		


		async void Cambiar_Clicked(object sender, EventArgs e)
		{
            recuperacion();

		}

		async void recuperacion()
		{
			if (String.IsNullOrEmpty(contrasenia.Text))
			{
				await DisplayAlert("", "Por favor, indique su nueva contraseña", "Aceptar");
				contrasenia.Focus();
				return;
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
			this.IsBusy = true;
            cambiar.IsEnabled = false;
			cambiar.IsVisible = false;			
            changepassword peticion1 = new changepassword
			{
                actualpass = pass,
                newpass = contrasenia.Text.Trim(),
                confirmpass = confirmacionContrasenia.Text.Trim(),
                user = user

			};
			String Session1 = String.Empty;
            Session1 = await App.ManejadorDatos.changepasswordAsync(peticion1);
            if (String.IsNullOrEmpty(Session1))
            {
                ShowToast(ToastNotificationType.Error, "Inconvenientes al cambiar la contraseña", "Inténtalo de nuevo más tarde o contáctanos.", 5);
                cambiar.IsVisible = true;
                cambiar.IsEnabled = true;
                this.IsBusy = false;
                await Navigation.PopAllPopupAsync();
                return;
            }
            else
            {
                if(Session1.Equals("Password Actualizado correctamente"))
                {
                    Login(user, contrasenia.Text);
                }
                else
                {
					ShowToast(ToastNotificationType.Error, "Cambio de contraseña",Session1, 5);
					cambiar.IsVisible = true;
					cambiar.IsEnabled = true;
					this.IsBusy = false;   
                }
            }
			this.IsBusy = false;
			cambiar.IsVisible = true;
			cambiar.IsEnabled = true;			
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
			
            return true;
		}

		protected override bool OnBackgroundClicked()
		{
            return false;
		}

		private async void Login(string email, string password)
		{
			Login peticion = new Login
			{
				username = email,
                password = password
			};
			List<UsuarioRespuesta> Session = new List<UsuarioRespuesta>();
			Session = await App.ManejadorDatos.LoginAsync(peticion);
			bool isEmpty = !Session.Any();
			if (isEmpty)
			{
				ShowToast(ToastNotificationType.Error, "Verifique sus datos de inicio de sesión", "Los datos de acceso proporcionados son erroneos.", 5);				
				this.IsBusy = false;
                MessagingCenter.Send<CambioContrasenia>(this, "Cambio");
			}
			else
			{				
				foreach (var session in Session)
				{
					ShowToast(ToastNotificationType.Success, "¡Genial!", "Tu contraseña ha sido actualizada y has iniciado sesión correctamente.", 7);
					Settings.session_access_token = session.access_token;
					Settings.session_token_type = session.token_type;
					Settings.session_refresh_token = session.refresh_token;
					Settings.session_expires_in = session.expires_in;
					Settings.session_scope = session.scope;
                    Settings.session_carne = user.Trim();
                    await Navigation.PopAllPopupAsync();
                    MessagingCenter.Send<CambioContrasenia>(this, "Autenticado");
				}
			}
		}
	}
}
