using ModelDemo.Common;
using System;
namespace ModelDemo {
	public class CursoEstudiante: AuditEntity, ISoftDeleted {
		public int Id { get; set; }
		public DateTime? FechaIncripcion { get; set; }

		public bool Deleted { get; set; }

		public int IdCurso { get; set; }
		public virtual Curso Curso { get; set; }

		public int IdEstudiante { get; set; }
		public virtual Estudiante Estudiante { get; set; }
	}
}
