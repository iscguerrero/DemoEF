using System;
using System.ComponentModel;
namespace ModelDemo {
	public class Bitacora {
		public int Id { get; set; }
		public string BDOrigen { get; set; }
		public string EntidadOrigen { get; set; }
		public string Accion { get; set; }
		public string Propiedad { get; set; }
		public string IdRegistro { get; set; }
		public string TipoDato { get; set; }
		public string ValorAntes { get; set; }
		public string ValorDespues { get; set; }
		public string Correo { get; set; }
		public DateTime? Fecha { get; set; }
	}
}
