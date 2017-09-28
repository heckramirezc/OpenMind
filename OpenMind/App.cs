using System;
using Xamarin.Forms;
using OpenMind.Data;
using OpenMind.Paginas.Principal;
using System.Threading.Tasks;
using System.Text;
using OpenMind.Helpers;
using Medicloud.Data;
using OpenMind.Paginas.Perfil;
using OpenMind.Modelos.Alumno;
using System.Collections.Generic;
using OpenMind.Modelos.Entrada;

namespace OpenMind
{
    public partial class App : Application
    {			
		public static OpenMindDatabase database;
		public static ManejadorDatos ManejadorDatos { get; set; }
        public static OpenMindDatabase Database
		{
			get
			{
				if (database == null)
				{
					database = new OpenMindDatabase();
				}
				return database;
			}
		}

        public App()
        {
			MessagingCenter.Subscribe<InicioSesion>(this, "Autenticado", (sender) =>
			{

                Sesion();
			});

            MessagingCenter.Subscribe<CambioContrasenia>(this, "Autenticado", (sender) =>
			{
				Sesion();
			});

			MessagingCenter.Subscribe<MiPerfil>(this, "NoAutenticado", (sender) =>
			{
                Settings.session_access_token = String.Empty;
                Settings.session_token_type = String.Empty;
				Settings.session_refresh_token = String.Empty;
				Settings.session_expires_in = String.Empty;
				Settings.session_scope = String.Empty;
				Settings.session_carne = String.Empty;
                Settings.session_nombre = String.Empty;
				MainPage = new InicioSesion();
			});

            ManejadorDatos = new ManejadorDatos(new ServicioWeb());
            if (string.IsNullOrEmpty((Settings.session_access_token)))
			{				
                MainPage = new InicioSesion();
			}
			else
			{	
                
				MainPage = new PrincipalTP();

			}
		}
        async public void Sesion()
        {
            await Sincronizar();
            MainPage = new PrincipalTP();
        }

        async public Task Sincronizar()
        {
            if (!String.IsNullOrEmpty(Settings.session_carne) || !String.IsNullOrEmpty(Settings.session_access_token))
            {
				AlumnoPeticion peticion = new AlumnoPeticion
				{
					nocarnet = Settings.session_carne
				};
				List<AlumnoRespuesta> Alumno = new List<AlumnoRespuesta>();
				await App.ManejadorDatos.infoAlumnoAsync(peticion);

				EntradaQR peticion2 = new EntradaQR
				{
					nocarnet = Settings.session_carne
				};

				await App.ManejadorDatos.GetQRAsync(peticion2);
            }
            			
			await App.ManejadorDatos.GetFAQssync();
        }

		async protected override void OnStart()
		{
			await Sincronizar();

		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
    }
}
