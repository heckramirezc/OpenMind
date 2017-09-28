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
        public ExtendedEntry Usuario;
        Button recuperar;
		StackLayout envioCodigo;		
        String user;
        public Forget(String user)
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
			
            if(!string.IsNullOrEmpty(user))
            {
                Usuario.Text = user.Trim();
            }

			


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
									envioCodigo									
								}
							}
						}

					}
				}
			};
			
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
			this.IsBusy = true;
			
            reloadmail peticion1 = new reloadmail
			{
                nocarnet = Usuario.Text.Trim()
			};
			String Session1 = String.Empty;
            Session1 = await App.ManejadorDatos.reloadmailAsync(peticion1);
			if (String.IsNullOrEmpty(Session1))
			{
				ShowToast(ToastNotificationType.Error, "Inconvenientes al recuperar la contraseña", "Inténtalo de nuevo más tarde o contáctanos.", 5);
				recuperar.IsVisible = true;
				recuperar.IsEnabled = true;
				this.IsBusy = false;
				await Navigation.PopAllPopupAsync();
				return;
			}
			else
			{
				if (Session1.Equals("Realizado"))
				{
                    ShowToast(ToastNotificationType.Success, "Recuperación de contraseña", "Se han enviado nuevos datos de acceso a tu dirección de correo electrónico registrada.", 10);
					recuperar.IsVisible = true;
					recuperar.IsEnabled = true;
					this.IsBusy = false;
					await Navigation.PopAllPopupAsync();
					return;
				}
				else
				{
					ShowToast(ToastNotificationType.Error, "Recuperación de contraseña", Session1, 5);
					recuperar.IsVisible = true;
					recuperar.IsEnabled = true;
					this.IsBusy = false;
				}
			}
			

			
            this.IsBusy = false;
			recuperar.IsVisible = true;
			recuperar.IsEnabled = true;
			
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

		
	}
}
