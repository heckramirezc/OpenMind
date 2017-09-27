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
    }
}
