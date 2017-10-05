using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenMind.Modelos.Alumno;
using OpenMind.Modelos.Entrada;
using OpenMind.Modelos.Usuario;

namespace OpenMind.Data
{
    public class ManejadorDatos
    {
        IServicioWeb ServicioWeb;
        public ManejadorDatos(IServicioWeb servicio)
        {
            ServicioWeb = servicio;
        }

        public Task<String> confirmUserAsync(confirmUser peticion)
		{
			return ServicioWeb.confirmUserAsync(peticion);
		}

        public Task<String> reloadmailAsync(reloadmail peticion)
		{
            return ServicioWeb.reloadmailAsync(peticion);
		}

        public Task<String> changepasswordAsync(changepassword peticion)
		{
			return ServicioWeb.changepasswordAsync(peticion);
		}

        public Task<List<UsuarioRespuesta>> LoginAsync(Login peticion)
        {
            return ServicioWeb.LoginAsync(peticion);
        }

        public Task GetQRAsync(EntradaQR peticion)
        {
            return ServicioWeb.GetQRAsync(peticion);
        }

        public Task GetFAQssync()
		{
			return ServicioWeb.GetFAQssync();
		}

        public Task infoAlumnoAsync(AlumnoPeticion peticion)
        {
            return ServicioWeb.infoAlumnoAsync(peticion);
        }

		public Task getCursosCatedraticoAsync(AlumnoPeticion peticion)
		{
			return ServicioWeb.getCursosCatedraticoAsync(peticion);
		}
    }
}
