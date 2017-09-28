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
        Task<String> confirmUserAsync(confirmUser peticion);
        Task<String> reloadmailAsync(reloadmail peticion);
        Task<String> changepasswordAsync(changepassword peticion);
        Task<List<UsuarioRespuesta>> LoginAsync(Login peticion);
        Task GetQRAsync(EntradaQR peticion);
        Task infoAlumnoAsync(AlumnoPeticion peticion);
        Task GetFAQssync();
    }
}
