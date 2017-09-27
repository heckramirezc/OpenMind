using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenMind.Helpers;
using PCLThinCanvas.Core;
using PCLThinCanvas.Views;
using Xamarin.Forms;

namespace OpenMind.Paginas.Perfil
{
    public partial class MiPerfil : ContentPage
    {
        public MiPerfil()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundImage = "fondo.png";


            LineView linea = new LineView
            {
                LineColor = Color.White,
                LineWidth = 2,
                //LineCap = LineCap.Butt,
                LineDirection = LineDirection.RightToLeft
            };
            LineView linea2 = new LineView
            {
                LineColor = Color.White,
                LineWidth = 2,
                //LineCap = LineCap.Round,
                LineDirection = LineDirection.RightToLeft
            };
			LineView linea3 = new LineView
			{
				LineColor = Color.White,
				LineWidth = 2,
                //LineCap = LineCap.Square,
				LineDirection = LineDirection.LeftToRight,
			};
			RelativeLayout header = new RelativeLayout();
			header.Children.Add(linea2,
								xConstraint: Constraint.RelativeToParent((parent) => { return ((parent.Width/1.8)); }),
								yConstraint: Constraint.Constant(0),
                                widthConstraint: Constraint.Constant(40),
                                heightConstraint: Constraint.Constant(90));

			
			header.Children.Add(linea,
                                xConstraint: Constraint.Constant(0),
								yConstraint: Constraint.Constant(120),
								widthConstraint: Constraint.RelativeToParent((parent) => { return ((parent.Width / 2)); }),
								heightConstraint: Constraint.Constant(100));
            
			header.Children.Add(linea3,
								xConstraint: Constraint.RelativeToParent((parent) => { return (parent.Width/2); }),
								yConstraint: Constraint.Constant(100),
								widthConstraint: Constraint.RelativeToParent((parent) => { return ((parent.Width / 2)); }),
								heightConstraint: Constraint.Constant(80));

            Label nombre = new Label
            {
                Text = Settings.session_nombre,
                WidthRequest = 200,
                FontSize = 18,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                TextColor = Color.FromHex("CDCDCD"),
            };
            header.Children.Add(nombre,
                                xConstraint: Constraint.RelativeToParent((parent) => { return (parent.Width / 2-100); }),
								yConstraint: Constraint.Constant(190),
                                widthConstraint: Constraint.Constant(200));


            Grid headerContenido = new Grid
            {
                Children = 
                {
                    header,
                    new Image 
                    {
                        Source = "avatar.png",
                        WidthRequest = 125,
                        HeightRequest = 125,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    }
                }
            };

			Button entrada = new Button
			{
				
				Text = "MI ENTRADA",
				TextColor = Color.FromHex("3E1152"),
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				FontSize = 15,
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("ffffff"),
				WidthRequest = 200,
				HeightRequest = 40,
				BorderRadius = 20
			};
            entrada.Clicked += Entrada_Clicked;
			Button info = new Button
			{
				
				Text = "INFO DEL EVENTO",
				TextColor = Color.FromHex("3E1152"),
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				FontSize = 15,
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex("ffffff"),
				WidthRequest = 200,
				HeightRequest = 40,
				BorderRadius = 20
			};
            info.Clicked+= Info_Clicked;

			Button cerrarSesion = new Button
			{
				
				Text = "Cerrar sesión",
                TextColor = Color.White,
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				FontSize = 14,
				HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
				WidthRequest = 125,
				HeightRequest = 30,
				BorderRadius = 12,
                BorderColor = Color.White,
                BorderWidth = 2
			};
            cerrarSesion.Clicked+= CerrarSesion_Clicked;
            Content = new ScrollView
            {
                Content =
					new StackLayout
					{
						Spacing = 45,
						Children =
        				{
        					headerContenido,
        					entrada,
        					info,
        					new StackLayout
        					{
        						VerticalOptions = LayoutOptions.EndAndExpand,
        						Padding = new Thickness(0,0,0,40),
        						Children =
        						{
        							cerrarSesion
        						}
        					}

        				}
					}                    
            };
        }

        async void CerrarSesion_Clicked(object sender, EventArgs e)
        {                        
            MessagingCenter.Send<MiPerfil>(this, "NoAutenticado");
        }

        void Entrada_Clicked(object sender, EventArgs e)
        {
            ((Principal.PrincipalTP)((App)this.Parent).MainPage).MostrarEntrada();
        }

        void Info_Clicked(object sender, EventArgs e)
        {            
            ((Principal.PrincipalTP)((App)this.Parent).MainPage).MostrarInfo();
        }

        async void Boton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


    }
}
