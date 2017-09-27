using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenMind.Modelos.Usuario
{
	public class Login
	{
		public string username { get; set; }
		public string password { get; set; }
		public string parametros
		{
			get
			{
                return
                    "grant_type=password" 
                    + "&username=" + username
                    + "&password=" + password;
			}
		}
	}

	public class UsuarioRespuesta
	{
		public string access_token { get; set; }
		public string token_type { get; set; }
		public string refresh_token { get; set; }
		public string expires_in { get; set; }
		public string scope { get; set; }		
        public string error { get; set; }       
        public string error_description { get; set; }
	}

}
