using System;
using Xamarin.Forms;
using OpenMind.Helpers;
using OpenMind.Modelos.Usuario;
using System.Collections.Generic;
using System.Linq;
using Plugin.Toasts;

namespace OpenMind.Paginas.Principal
{
    public class InicioSesion : ContentPage
    {
        ExtendedEntry Usuario, Contrasenia;
		Label forget;
		Button login;
        public InicioSesion()
        {
			Usuario = new ExtendedEntry()
			{
				AutomationId = "LoginUser",
				Keyboard = Keyboard.Email,
				Placeholder = "USUARIO",
				PlaceholderColor = Color.FromHex("91a5af"),
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("CDCDCD"),
                BackgroundColor = Color.Transparent,
				Text = string.Empty,
                FontSize = 18,
				HeightRequest = 55,
				HasBorder = false,
                Margin = 3,
                VerticalOptions = LayoutOptions.Center
			};

            Contrasenia = new ExtendedEntry()
            {
                AutomationId = "LoginPass",
                Placeholder = "CONTRASEÑA",
                PlaceholderColor = Color.FromHex("91a5af"),
                FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                TextColor = Color.FromHex("91a5af"),
                FontSize = 18,
                BackgroundColor = Color.Transparent,
                Text = string.Empty,
                HeightRequest = 55,
                HasBorder = false,
                Margin = 3,
                VerticalOptions = LayoutOptions.Center,
                IsPassword = true
			};

			//var rond = ;

			var ingreso = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Padding = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
				Spacing = 10,
				Children =
				{
					//Usuario,
                    new Grid
                    {
                        HeightRequest = 45,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
							/*new RoundedBoxView.Forms.Plugin.Abstractions.RoundedBoxView
                			{
                				BackgroundColor = Color.Transparent,
                				BorderThickness = 2,
                				CornerRadius = 20,
                				BorderColor = Color.White,
                				HeightRequest = 45,
                				HorizontalOptions = LayoutOptions.FillAndExpand,
                			},*/
                            Usuario
                        },
                    },
					new Grid
					{
                        HeightRequest = 45,
						Children =
						{
							/*new RoundedBoxView.Forms.Plugin.Abstractions.RoundedBoxView
							{
								BackgroundColor = Color.Transparent,
								BorderThickness = 2,
								CornerRadius = 20,
								BorderColor = Color.White,
								HeightRequest = 45,
								WidthRequest = 128,
							},*/
							Contrasenia
						},
					}

				}
			};
			var fIngreso = new Frame
			{
				WidthRequest = 300,
				Padding = new Thickness(0, 0, 0, 0),
                BackgroundColor = Color.Transparent,
                OutlineColor = Color.Transparent,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				HasShadow = false,
				Content = ingreso
			};

			var logo = new Image
			{                
				Source = "logo.png",
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 250
			};
            
			var content_ingreso = new StackLayout
			{                
				Spacing = 25,				
				Children = { logo, fIngreso }
			};

			login = new Button
			{
				AutomationId = "Login",
				Text = "INGRESAR",
				TextColor = Color.FromHex("3E1152"),
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				FontSize = 15,
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("ffffff"),
				WidthRequest = 200,
				HeightRequest = 35,
                BorderRadius = 20
			};
			login.Clicked += Login_Clicked;


			var indicador = new ActivityIndicator
			{
				IsVisible = true,
				IsRunning = true,
				BindingContext = this,
				Color = Color.FromHex("f7efd9"),
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
			};
			indicador.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");

			forget = new Label
			{                
				Text = "¿Tienes dudas? contáctanos.",
				TextColor = Color.FromHex("f7efd9"),
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				FontSize = 14,
                Margin = new Thickness(0,0,0,40),
                VerticalOptions = LayoutOptions.EndAndExpand,
				HorizontalOptions = LayoutOptions.Center
			};
			var tap = new TapGestureRecognizer();
			tap.Tapped += (s, e) => Forget_Clicked();
			forget.GestureRecognizers.Add(tap);

            var acceso = new StackLayout
            {                
                VerticalOptions = LayoutOptions.FillAndExpand,
				Spacing = 40,
				Children = { login, indicador, forget }
			};
            BackgroundImage = "fondo.png";
            Content = new ScrollView
            {                
                Content=
					new StackLayout
					{                    
                        VerticalOptions = LayoutOptions.FillAndExpand,
						Spacing = 20,
						Padding = new Thickness(0, 20, 0, 0),
						Children = { content_ingreso, acceso }
					}
            };
		}

        private void Forget_Clicked()
        {
            
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
			if (String.IsNullOrEmpty(Usuario.Text))
			{
				await DisplayAlert("", "Por favor, indique su usuario/carné", "Aceptar");
				Usuario.Focus();
				return;
			}
			
			if (String.IsNullOrEmpty(Contrasenia.Text))
			{
				await DisplayAlert("", "Por favor, indique su contraseña", "Aceptar");
				Contrasenia.Focus();
				return;
			}
			this.IsBusy = true;
			login.IsEnabled = false;
			login.IsVisible = false;

			Login peticion = new Login
			{
                username = Usuario.Text.Trim(),
                password = Contrasenia.Text.Trim().Replace("&","%26")
			};
			List<UsuarioRespuesta> Session = new List<UsuarioRespuesta>();
			Session = await App.ManejadorDatos.LoginAsync(peticion);
			bool isEmpty = !Session.Any();
			if (isEmpty)
			{
				ShowToast(ToastNotificationType.Error, "Verifique sus datos de inicio de sesión", "Los datos de acceso proporcionados son erroneos.", 5);
				login.IsVisible = true;
				login.IsEnabled = true;
				this.IsBusy = false;
			}
			else
			{
				foreach (var session in Session)
				{                    
                    ShowToast(ToastNotificationType.Success, "¡Bienvenido!", "Has iniciado sesión correctamente.", 7);
					Settings.session_access_token = session.access_token;
                    Settings.session_token_type = session.token_type;
                    Settings.session_refresh_token = session.refresh_token;
                    Settings.session_expires_in = session.expires_in;
                    Settings.session_scope = session.scope;
                    Settings.session_carne = Usuario.Text.Trim();
		            MessagingCenter.Send<InicioSesion>(this, "Autenticado");
				}
			}

        }

		private async void ShowToast(ToastNotificationType type, string titulo, string descripcion, int tiempo)
		{
			var notificator = DependencyService.Get<IToastNotificator>();
			bool tapped = await notificator.Notify(type, titulo, descripcion, TimeSpan.FromSeconds(tiempo));
		}
    }
}
