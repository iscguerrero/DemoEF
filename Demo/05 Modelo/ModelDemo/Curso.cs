using ModelDemo.Common;
using System.Collections.Generic;
namespace ModelDemo {
	public class Curso : AuditEntity, ISoftDeleted {
		public int Id { get; set; }
		public string Nombre { get; set; }
		public decimal Precio { get; set; }

		public bool Deleted { get; set; }

		public virtual ICollection<CursoDocente> CursosDocentes { get; set; }
		public virtual ICollection<CursoEstudiante> CursosEstudiantes { get; set; }
	}
}
