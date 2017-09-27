using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenMind.Modelos.Entrada
{
	public class EntradaQR
	{
		public string nocarnet { get; set; }		
		public string parametros
		{
			get
			{
				return
					"nocarnet=" + nocarnet;
			}
		}
	}

	public class EntradaQRRespuesta
	{
		public string url { get; set; }		
	}

}
