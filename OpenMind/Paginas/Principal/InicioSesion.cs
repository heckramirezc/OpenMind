using System;
using Xamarin.Forms;
using OpenMind.Helpers;
using OpenMind.Modelos.Usuario;
using System.Collections.Generic;
using System.Linq;
using Plugin.Toasts;
using Rg.Plugins.Popup.Extensions;

namespace OpenMind.Paginas.Principal
{
    public class InicioSesion : ContentPage
    {
        ExtendedEntry Usuario, Contrasenia;
		Label forget, contactenos;
		Button login;
        public InicioSesion()
        {
			Usuario = new ExtendedEntry()
			{
				AutomationId = "LoginUser",
                Keyboard = Keyboard.Telephone,
				Placeholder = "USUARIO",
				PlaceholderColor = Color.FromHex("91a5af"),
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("CDCDCD"),
                BackgroundColor = Color.Transparent,
				Text = string.Empty,
                FontSize = Device.OnPlatform(16, 22, 12),
				HeightRequest = Device.OnPlatform(40, 50, 12),
				HasBorder = false,
                Margin = new Thickness(15,0),
                VerticalOptions = LayoutOptions.Center,
                ReturnKeyType = ReturnKeyTypes.Next
			};
            Usuario.Completed+= (sender, e) => { /*Usuario.Unfocus();*/  Contrasenia.Focus();};
            Contrasenia = new ExtendedEntry()
            {
                AutomationId = "LoginPass",
                Placeholder = "CONTRASEÑA",
                PlaceholderColor = Color.FromHex("91a5af"),
                FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                TextColor = Color.FromHex("CDCDCD"),
                BackgroundColor = Color.Transparent,
                Text = string.Empty,
				FontSize = Device.OnPlatform(16,22, 12),
				HeightRequest = Device.OnPlatform(40, 50, 12),
				HasBorder = false,
				Margin = new Thickness(15, 0),
                VerticalOptions = LayoutOptions.Center,
                IsPassword = true,
                ReturnKeyType = ReturnKeyTypes.Go
			};
            Contrasenia.Completed += (sender, e) => { Contrasenia.Unfocus(); Login(); };

				var ingreso = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
                Padding = new Thickness(20,0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
				Spacing = 10,
				Children =
				{
					//Usuario,
                    new Grid
                    {
                        HeightRequest = Device.OnPlatform(40,50, 12),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
							new RoundedBoxView.Forms.Plugin.Abstractions.RoundedBoxView
                			{
                				BackgroundColor = Color.Transparent,
                				BorderThickness = 2,
                				CornerRadius = 20,
                				BorderColor = Color.FromHex("94a8c1"),
                				HeightRequest = Device.OnPlatform(40,50, 12),
                				HorizontalOptions = LayoutOptions.FillAndExpand,
                			},
                            Usuario
                        },
                    },
					new Grid
					{
                        HeightRequest = Device.OnPlatform(40,50, 12),
						Children =
						{
							new RoundedBoxView.Forms.Plugin.Abstractions.RoundedBoxView
							{
								BackgroundColor = Color.Transparent,
								BorderThickness = 2,
								CornerRadius = 20,
                                BorderColor = Color.FromHex("94a8c1"),
								HeightRequest = Device.OnPlatform(40,50, 12),
								//WidthRequest = 128,
							},
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
                Margin = new Thickness(50,0)
			};
            
			var content_ingreso = new StackLayout
			{   
                VerticalOptions = LayoutOptions.CenterAndExpand,
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
				Text = "Recuperar contraseña.",
				TextColor = Color.FromHex("f7efd9"),
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				FontSize = 14,
				//Margin = new Thickness(0, 0, 0, 40),
				//VerticalOptions = LayoutOptions.EndAndExpand,
				HorizontalOptions = LayoutOptions.Center
			};

            contactenos = new Label
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
				Spacing = 15,
                Children = {new Grid{Children ={indicador, login }},forget, contactenos }
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

        private async void Forget_Clicked()
        {
            await Navigation.PushPopupAsync(new CambioContrasenia(Usuario.Text.Trim(), Contrasenia.Text.Trim()));
            //await Navigation.PushPopupAsync(new Forget());
        }

        private void Login_Clicked(object sender, EventArgs e)
        {			
            Login();
        }

        public async void Login()
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
				password = Contrasenia.Text.Trim().Replace("&", "%26")
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
                confirmUser peticion1 = new confirmUser
				{
					username = Usuario.Text.Trim()
				};
                String Session1 = String.Empty;
                Session1 = await App.ManejadorDatos.confirmUserAsync(peticion1);				
                if (String.IsNullOrEmpty(Session1))
                {
                    ShowToast(ToastNotificationType.Error, "Tenemos problemas en verificar tu cuenta.", "Inténtalo de nuevo más tarde o contáctanos.", 5);
                    login.IsVisible = true;
                    login.IsEnabled = true;
                    this.IsBusy = false;
                    return;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("llego aca");
                    if (Session1.Equals("1"))
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
                    else
                    {
                        await Navigation.PushPopupAsync(new CambioContrasenia(Usuario.Text.Trim(), Contrasenia.Text.Trim()));      
                    }
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
