using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenMind.Modelos.Alumno;
using OpenMind.Modelos.Entrada;
using OpenMind.Modelos.Usuario;


namespace OpenMind.Data
{
	public interface IServicioWeb
	{
		Task<List<UsuarioRespuesta>> LoginAsync(Login peticion);
        Task GetQRAsync(EntradaQR peticion);
        Task infoAlumnoAsync(AlumnoPeticion peticion);
        Task GetFAQssync();
	}
}
