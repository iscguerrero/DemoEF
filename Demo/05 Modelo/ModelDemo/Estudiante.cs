using ModelDemo.Common;
using System.Collections.Generic;
namespace ModelDemo {
	public class Estudiante : AuditEntity, ISoftDeleted {
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Domicilio { get; set; }
		public int Edad { get; set; }

		public bool Deleted { get; set; }

		public virtual ICollection<CursoEstudiante> CursosEstudiantes { get; set; }
	}
}
