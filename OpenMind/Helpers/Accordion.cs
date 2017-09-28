using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace OpenMind.Helpers
{
	public class Accordion : ContentView
	{
		#region Private Variables
		List<AccordionSource> mDataSource;
		bool mFirstExpaned = false;
		StackLayout mMainLayout;
		#endregion

		public Accordion()
		{
			var mMainLayout = new StackLayout();
			Content = mMainLayout;
		}

		public Accordion(List<AccordionSource> aSource)
		{
			mDataSource = aSource;
			DataBind();
		}

		#region Properties
		public List<AccordionSource> DataSource
		{
			get { return mDataSource; }
			set { mDataSource = value; }
		}
		public bool FirstExpaned
		{
			get { return mFirstExpaned; }
			set { mFirstExpaned = value; }
		}
		#endregion

		public void DataBind()
		{
			var vMainLayout = new StackLayout() { Spacing = 0 };
			var vFirst = true;
			if (mDataSource != null)
			{
				foreach (var vSingleItem in mDataSource)
				{

					var vHeaderButton = new AccordionButton();
					var vAccordionContent = new ContentView()
					{
						Content = vSingleItem.Contenido,
						IsVisible = false,
                        Padding = new Thickness(0,-13,0,5)
					};
					if (vFirst)
					{
						vHeaderButton.Expand = mFirstExpaned;
						vAccordionContent.IsVisible = mFirstExpaned;
						vFirst = false;
                        foreach (var itemStack in vSingleItem.Cabecera.Children)
                        {
							if (itemStack.GetType() == typeof(Grid))
							{
								var vsGrid = (Grid)itemStack;
								foreach (var itemSGrid in vsGrid.Children)
								{
									if (itemSGrid.GetType() == typeof(Button))
									{
										var btn = (Button)itemSGrid;
										btn.BackgroundColor = Color.FromHex("3E1152");
										btn.TextColor = Color.FromHex("ffffff");
										btn.FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null);
									}
								}
							}
                        }
					}
					vHeaderButton.AssosiatedContent = vAccordionContent;
					vHeaderButton.Clicked += OnAccordionButtonClicked;
					vHeaderButton.IsVisible = vSingleItem.Cabecera.IsVisible;
					vMainLayout.Children.Add(
						new Grid
						{                            
							IsVisible = vSingleItem.Cabecera.IsVisible,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							HeightRequest = 40,
							MinimumHeightRequest = 40,
							Children =
							{
								vSingleItem.Cabecera,
								vHeaderButton
							}
						});
					vMainLayout.Children.Add(vAccordionContent);

				}
			}
			mMainLayout = vMainLayout;
			Content = mMainLayout;
		}

		void OnAccordionButtonClicked(object sender, EventArgs args)
		{
			var vSenderButton = (AccordionButton)sender;
            Button boton = new Button();
			var isVisible = vSenderButton.Expand;
			foreach (var vChildItem in mMainLayout.Children)
			{
				if (vChildItem.GetType() == typeof(ContentView)) vChildItem.IsVisible = false;
				if (vChildItem.GetType() == typeof(Grid))
				{
					var vGrid = (Grid)vChildItem;
                    bool asociar = false;
                    StackLayout vStack = new StackLayout();
					foreach (var itemGrid in vGrid.Children)
					{
						if (itemGrid.GetType() == typeof(AccordionButton))
						{
							var vButton = (AccordionButton)itemGrid;
                            if (vSenderButton == vButton)
                                asociar = true;
                            else
                                asociar = false;
							vButton.Expand = false;
							if (vStack != null)
							{
								foreach (var itemStack in vStack.Children)
								{
									if (itemStack.GetType() == typeof(Grid))
									{
										var vsGrid = (Grid)itemStack;
										foreach (var itemSGrid in vsGrid.Children)
										{
											if (itemSGrid.GetType() == typeof(Button))
											{
                                                var btn = (Button)itemSGrid;
                                                if (asociar)
												    boton = btn;
                                                else
                                                {
													btn.BackgroundColor = Color.FromHex("ffffff");
													btn.TextColor = Color.FromHex("3E1152");
                                                    btn.FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null);
                                                }												
											}
										}
									}
								}
							}
						}
						if (itemGrid.GetType() == typeof(StackLayout))
						{
							vStack = (StackLayout)itemGrid;							
						}                        
					}
				}
			}


			if (isVisible)
			{
				vSenderButton.Expand = false;
                boton.BackgroundColor = Color.FromHex("ffffff");
                boton.TextColor = Color.FromHex("3E1152");
                boton.FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null);
				double position = 0;
				vSenderButton.AssosiatedContent.Animate("collapse",
					x =>
					{
						position = vSenderButton.AssosiatedContent.Y * x;
					}, 16, 0, Easing.SpringOut, (d, b) =>
					{
						System.Diagnostics.Debug.WriteLine("altura: " + vSenderButton.AssosiatedContent.Height);
						System.Diagnostics.Debug.WriteLine("posicion: " + position);
					});
			}
			else
			{
				vSenderButton.Expand = true;
				boton.BackgroundColor = Color.FromHex("3E1152");
				boton.TextColor = Color.FromHex("ffffff");
                boton.FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null);
				double position = 0;
				vSenderButton.AssosiatedContent.Animate("expand",
					x =>
					{
						position = vSenderButton.AssosiatedContent.Y * x;
					}, 16, 0, Easing.SpringOut, (d, b) =>
					{
						System.Diagnostics.Debug.WriteLine("altura: " + vSenderButton.AssosiatedContent.Height);
						System.Diagnostics.Debug.WriteLine("posicion: " + position);
					});
			}
			vSenderButton.AssosiatedContent.IsVisible = vSenderButton.Expand;
		}

	}

	public class AccordionButton : Button
	{
		#region Private Variables
		bool mExpand = false;
		#endregion
		public AccordionButton()
		{
			HorizontalOptions = LayoutOptions.FillAndExpand;
			BackgroundColor = Color.Transparent;
			BorderColor = Color.Transparent;
			MinimumHeightRequest = 30;
			BorderWidth = 0;
		}
		#region Properties
		public bool Expand
		{
			get { return mExpand; }
			set { mExpand = value; }
		}
		public ContentView AssosiatedContent
		{ get; set; }
		#endregion
	}

	public class AccordionSource
	{
		public StackLayout Cabecera { get; set; }
		public StackLayout Contenido { get; set; }
	}
}


