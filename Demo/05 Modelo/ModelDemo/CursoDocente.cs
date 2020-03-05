using ModelDemo.Common;
using System;
namespace ModelDemo {
	public class CursoDocente: AuditEntity, ISoftDeleted {
		public int Id { get; set; }
		public DateTime? FechaInicio { get; set; }

		public bool Deleted { get; set; }

		public int IdCurso { get; set; }
		public virtual Curso Curso { get; set; }

		public int IdDocente { get; set; }
		public virtual Docente Docente { get; set; }
	}
}
