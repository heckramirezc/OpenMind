using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Diagnostics;
using Xamarin.Forms;
using OpenMind.Data;
using OpenMind.Modelos.Usuario;
using OpenMind.Modelos.Entrada;
using OpenMind.Modelos.Alumno;
using OpenMind.Modelos.SQLite;
using OpenMind;
using OpenMind.Helpers;
using OpenMind.Modelos.FAQs;

namespace Medicloud.Data
{
    public class ServicioWeb : IServicioWeb
    {
        HttpClient cliente;
        public List<UsuarioRespuesta> Usuario { get; private set; }
        public List<EntradaQRRespuesta> QR { get; private set; }
        public List<AlumnoRespuesta> Alumno { get; private set; }
        public List<FAQsRespuesta> FAQs { get; private set; }

		public ServicioWeb()
		{
			cliente = new HttpClient();
			cliente.MaxResponseContentBufferSize = 256000;
			cliente.Timeout = TimeSpan.FromSeconds(60000);

		}

		public async Task<List<UsuarioRespuesta>> LoginAsync(Login peticion)
		{
            Uri uri = new Uri(Constantes.URL_Users_Login + peticion.parametros);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,uri);
            request.Headers.TransferEncodingChunked = true;
			Usuario = new List<UsuarioRespuesta>();			
			try
			{                
				System.Diagnostics.Debug.WriteLine("PARAMETROS: " + uri);

                request.Headers.Clear();
                //request.Headers.Add("client_id", "3a607fffc0fa47ec80054948009d0c43");
                //request.Headers.Add("client_secret", "23cb101140c9d4cecb6fbe1571028961e");
                //request.Headers.Add("grant_type", "authorization_code");
                //request.Headers.Add("redirect_uri", "http://192.168.1.132:8081/appsocial/instagram/token.xhtml");
                //request.Headers.Add("code", "59409ad994ca4c5dab60ad2aa9b2bf84");
                request.Headers.Add("Authorization", "Basic dW1nOnNlY3JldA==");

                var solicitud = await cliente.SendAsync(request);
				Debug.WriteLine("SOLICITUD: " + solicitud);
				solicitud.EnsureSuccessStatusCode();

				string respuesta = await solicitud.Content.ReadAsStringAsync();
				Debug.WriteLine("RESPUESTA: " + respuesta);
                Usuario.Add(JsonConvert.DeserializeObject<UsuarioRespuesta>(respuesta));
			}
			catch (Exception e)
			{
                
			    Debug.WriteLine("ERROR: " + e.Message);
			}
            return Usuario;
		}

        public async Task GetQRAsync(EntradaQR peticion)
		{
			Uri uri = new Uri(Constantes.URL_Users_GetQR);			
            QR = new List<EntradaQRRespuesta>();
			try
			{
				System.Diagnostics.Debug.WriteLine("PARAMETROS: " + uri + peticion.parametros);
				var respuesta = await cliente.GetStringAsync(uri + peticion.parametros);
				System.Diagnostics.Debug.WriteLine("RESPUESTA: " + respuesta);
                QR = JsonConvert.DeserializeObject<List<EntradaQRRespuesta>>(respuesta);
                foreach (var qr in QR)
                {
                    Settings.session_url = qr.url;
                }
			}
			catch (Exception e)
			{

				Debug.WriteLine("ERROR: " + e.Message);
			}
		}

        public async Task infoAlumnoAsync(AlumnoPeticion peticion)
		{
            App.Database.limpiarCursos();
            Uri uri = new Uri(Constantes.URL_Users_infoAlumno);
            Alumno = new List<AlumnoRespuesta>();
			try
			{
				System.Diagnostics.Debug.WriteLine("PARAMETROS: " + uri + peticion.parametros);
				var respuesta = await cliente.GetStringAsync(uri + peticion.parametros);
				System.Diagnostics.Debug.WriteLine("RESPUESTA: " + respuesta);
				Alumno = JsonConvert.DeserializeObject<List<AlumnoRespuesta>>(respuesta);
                foreach (var alumno in Alumno)
                {
                    Settings.session_nombre = alumno.nombres + " " + alumno.apellidos;
                    foreach (var asignacion in alumno.asignaciones)
                    {
						cursos curso = new cursos
						{
                            idCurso = asignacion.curso.idCurso,
                            nombre = asignacion.curso.nombre,
                            seccion = asignacion.curso.seccion
						};
						try
						{
							await Task.Run(() =>
							{
                                if (App.database.InsertCursos(curso) == 1)
									System.Diagnostics.Debug.WriteLine("CORRECTO RECETAS: ¡Se ha realizado correctamente la insersion de datos!");
								else
									System.Diagnostics.Debug.WriteLine("ERROR: ¡Ha ocurrido un error inesperado al insercion de datos!");
							});
						}
						catch (Exception e)
						{
							System.Diagnostics.Debug.WriteLine("ERROR: " + e.Message);
						}
                    }									
                }            				
			}
			catch (Exception e)
			{
				Debug.WriteLine("ERROR: " + e.Message);
			}			
		}

		public async Task GetFAQssync()
		{
            App.Database.limpiarFAQs();
			Uri uri = new Uri(Constantes.URL_Users_allQuestion);
            FAQs = new List<FAQsRespuesta>();
			try
			{
				System.Diagnostics.Debug.WriteLine("PARAMETROS: " + uri);
				var respuesta = await cliente.GetStringAsync(uri);
				System.Diagnostics.Debug.WriteLine("RESPUESTA: " + respuesta);
                FAQs = JsonConvert.DeserializeObject<List<FAQsRespuesta>>(respuesta);

                foreach (var preguntaFrecuente in FAQs)
				{
                    faqs faq = new faqs
					{
                        pregunta = preguntaFrecuente.pregunta,
                        respuesta = preguntaFrecuente.respuesta
					};
					try
					{
						await Task.Run(() =>
						{
                            if (App.database.InsertFAQ(faq) == 1)
								System.Diagnostics.Debug.WriteLine("CORRECTO RECETAS: ¡Se ha realizado correctamente la insersion de datos!");
							else
								System.Diagnostics.Debug.WriteLine("ERROR: ¡Ha ocurrido un error inesperado al insercion de datos!");
						});
					}
					catch (Exception e)
					{
						System.Diagnostics.Debug.WriteLine("ERROR: " + e.Message);
					}					
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine("ERROR: " + e.Message);
			}
		}

    }
    
}