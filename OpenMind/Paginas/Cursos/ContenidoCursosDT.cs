using System;
using System.Collections.Generic;
using OpenMind.ControlesPersonalizados;
using OpenMind.Data;
using OpenMind.Helpers;
using OpenMind.Modelos.Alumno;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace OpenMind.Paginas.Cursos
{
    public class ContenidoCursosDT : ContentView
    {
        public ContenidoCursosDT(String Codigo, String Seccion)
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
				}, 0, 0);
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
			TapGestureRecognizer tap = new TapGestureRecognizer();
			tap.Tapped += async (sender, e) =>
			{
                if (Settings.session_role.Equals("E"))
                    return;

                if (!Constantes.ModalAbierto && ContenidoCursos.IsEnabled)
				{
					ContenidoCursos.IsEnabled = false;
					Constantes.ModalAbierto = true;
                    await Navigation.PushPopupAsync(new Indicador("Actualizando asistencia", Color.White));
                    AsistenciaPeticion peticion = new AsistenciaPeticion
					{
                        idCurso = Codigo.Trim()
					};
                    List<AsistenciaRespuesta> respuesta =  await App.ManejadorDatos.getAlumnosAsync(peticion);
                    await Navigation.PopAllPopupAsync();
					await Navigation.PushPopupAsync(new CursoAsistentesDT(respuesta));
					ContenidoCursos.IsEnabled = true;
				}
				else
					System.Diagnostics.Debug.WriteLine("Modal abierto actualmente");				
			};
			ContenidoCursos.GestureRecognizers.Add(tap);

            Content = ContenidoCursos;
        }
    }
}
