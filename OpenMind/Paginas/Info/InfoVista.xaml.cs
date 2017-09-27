﻿using System;
using System.Collections.Generic;
using OpenMind.ControlesPersonalizados;
using Xamarin.Forms;

namespace OpenMind.Paginas.Info
{
    public partial class InfoVista : ContentPage
    {
        public InfoVista()
        {
            InitializeComponent();

			Grid Info = new Grid
			{                
				//VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Center,
				//Padding = new Thickness(0, 0, 0, 20),
				RowSpacing = 5,
				ColumnSpacing = 10,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }
				},
				ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (25, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Auto) },
				}
			};


            Info.Children.Add(
                new IconView
                {
                    WidthRequest = 15,
                    HeightRequest = 15,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Foreground = Color.FromHex("3E1152"),
                    Source="calendario.png"
                } ,0,0);
			Info.Children.Add(
				new Label
				{                    
					Text = "14 - OCT - 2017 ",
					FontSize = 14,					
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center,
					FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
					TextColor = Color.FromHex("3E1152"),
				}, 1, 0);

			Info.Children.Add(
				new IconView
				{
					WidthRequest = 15,
					HeightRequest = 15,
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					Foreground = Color.FromHex("3E1152"),
					Source = "hora.png"
				}, 0, 1);
			Info.Children.Add(
				new Label
				{
                Text = "14:00 - 18:00 horas",
					FontSize = 14,					
					FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
                    VerticalOptions = LayoutOptions.Center,
					TextColor = Color.FromHex("3E1152"),
				}, 1, 1);

			Info.Children.Add(
				new IconView
				{
					WidthRequest = 15,
					HeightRequest = 15,
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					Foreground = Color.FromHex("3E1152"),
					Source = "geo.png"
				}, 0, 2);
			Info.Children.Add(
				new Label
				{
					Text = "Salón municipal de Ciudad Vieja -  Sacatepéquez GT",
					FontSize = 14,					
                    VerticalOptions = LayoutOptions.Center,
					FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
					TextColor = Color.FromHex("3E1152"),
				}, 1, 2);

			Image waze = new Image
			{
				Source = "waze.png",
				HorizontalOptions = LayoutOptions.Center
            };

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped+= (sender, e) => 
            {
                
            };

            waze.GestureRecognizers.Add(tap);
            Content = new StackLayout
            {                                
                Children = 
                {
                    new ScrollView
                    {                        
                        VerticalOptions = LayoutOptions.FillAndExpand,
						Padding = new Thickness(40,0),				
                        Content= new StackLayout
                        {
							HorizontalOptions = LayoutOptions.Center,
            				Spacing = 20,
            				Padding = new Thickness(0, 20, 0, 0),
                            Children = 
                            {
								new Image
            					{
            						Source = "logoColor.png",
            						WidthRequest = 175,
            						HeightRequest = 175,
            						HorizontalOptions = LayoutOptions.Center
            					},
            					new Label
            					{
            						Text = "Con el fin de enriquecer los conocimientos año con año se lleva a cabo el seminario de la carrera de Ingeniería en sistemas de la Universidad Mariano Gálvez. \r\n\nEn esta ocasión el seminario lleva el nombre de OPENMIND, y contara con la participación de los expositores:\r\n\n\ta.\tIngeniero Danilo Escobar: Quien impartirá la charla de Open Source Intelligence.\r\n\tb.\tIngeniero Amílcar de León: Con el tema Hacking Ético.\r\n\nEste año se contara con una visión ecológica por lo cual nosotros como curso de Sistemas Gerenciales optimizaremos los procesos de uso de papel y se empleara únicamente medios electrónicos.\r",
            						FontSize = 12,
                                    HorizontalTextAlignment = TextAlignment.Center,
            						HorizontalOptions = LayoutOptions.FillAndExpand,
            						FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
            						TextColor = Color.FromHex("262626"),
            					},
            					Info,
								new BoxView
            					{
                                    BackgroundColor = Color.Transparent,            						
            						HorizontalOptions = LayoutOptions.FillAndExpand,
            						HeightRequest = 10
            					},
            					waze
                            }
                        }

                    },
                    new BoxView 
                    {
                        BackgroundColor = Color.FromHex("3E1152"), 
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = 2
                    }
                }
            };
        }
    }
}
