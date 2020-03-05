using ModelDemo.Common;
using System.Collections.Generic;
namespace ModelDemo {
	public class Docente : AuditEntity, ISoftDeleted {
		// Propiedades Escalares
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Domicilio { get; set; }
		public int Edad { get; set; }

		public bool Deleted { get; set; }

		// Propiedades de Navegación de Colección
		public virtual ICollection<CursoDocente> CursosDocentes { get; set; }

	}
}
