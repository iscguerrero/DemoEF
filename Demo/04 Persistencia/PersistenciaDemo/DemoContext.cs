using System.Data.Entity;
using ModelDemo;
using System.Linq;
using ModelDemo.Common;
using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
namespace PersistenciaDemo {
	public class DemoContext: DbContext, IDisposable {
		public DemoContext()
			: base("name=DemoCtx") {
				Configuration.LazyLoadingEnabled = false;
				Configuration.ProxyCreationEnabled = false;
		}

		public virtual DbSet<Curso> Curso { get; set; }
		public virtual DbSet<Docente> Docente { get; set; }
		public virtual DbSet<Estudiante> Estudiante { get; set; }
		public virtual DbSet<Bitacora> Bitacora { get; set; }
		public virtual DbSet<CursoEstudiante> CursoEstudiante { get; set; }
		public virtual DbSet<CursoDocente> CursoDocente { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			//base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Curso>().ToTable("Cursos");
			modelBuilder.Entity<Docente>().ToTable("Docentes");
			modelBuilder.Entity<Estudiante>().ToTable("Estudiantes");
			modelBuilder.Entity<Bitacora>().ToTable("Bitacora");
			modelBuilder.Entity<CursoEstudiante>().ToTable("CursosEstudiantes");
			modelBuilder.Entity<CursoDocente>().ToTable("CursosDocentes");

			// Configuración de llaves foraneas de las tablas *.*
			modelBuilder.Entity<CursoDocente>()
				.HasRequired(cd => cd.Curso)
				.WithMany(c => c.CursosDocentes)
				.HasForeignKey(cd => cd.IdCurso);

			modelBuilder.Entity<CursoDocente>()
				.HasRequired(cd => cd.Docente)
				.WithMany(d => d.CursosDocentes)
				.HasForeignKey(cd => cd.IdDocente);

			/*// Simples
			modelBuilder.Entity<CursoDocente>()
				.HasKey(x => x.Id);
			// Compuesta
			modelBuilder.Entity<CursoDocente>()
				.HasKey(x => new { x.IdCurso, x.IdDocente });*/

			modelBuilder.Entity<CursoEstudiante>()
				.HasRequired(ce => ce.Curso)
				.WithMany(c => c.CursosEstudiantes)
				.HasForeignKey(ce => ce.IdCurso);

			modelBuilder.Entity<CursoEstudiante>()
				.HasRequired(ce => ce.Estudiante)
				.WithMany(e => e.CursosEstudiantes)
				.HasForeignKey(ce => ce.IdEstudiante);

			modelBuilder.Entity<Bitacora>()
				.Property(x => x.IdRegistro).HasColumnOrder(5);

		}

		public override int SaveChanges() {
			MakeAudit();

			return base.SaveChanges();
		}

		// Funcion encargada de implementar la auditoría
		private void MakeAudit() {
			var modifiedEntries = ChangeTracker.Entries().Where(
				x => x.Entity is AuditEntity
					&& (
					x.State == EntityState.Added
					|| x.State == EntityState.Modified
					|| x.State == EntityState.Deleted
				)
			);

			foreach (var entry in modifiedEntries) {
				if (entry.Entity != null) {
					string correo = "siga@usebeq.edu.mx";

					// Obtenemos el nombre de las propiedades de la entidad
					var properties = entry.State.ToString() == "Deleted" ? entry.OriginalValues.PropertyNames : entry.CurrentValues.PropertyNames;

					// Obtenemos el valor de la llave primaria del entry que estamos recorriendo
					ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;
					ObjectSet<Curso> set = objectContext.CreateObjectSet<Curso>();
					string PKName = set.EntitySet.ElementType
															.KeyMembers
															.First().Name;

					string IdRegistro = entry.State.ToString() == "Added" ? "0" : entry.OriginalValues[PKName].ToString();

					foreach (var propName in properties) {
						
						var original = entry.State.ToString() == "Added" ? "" : entry.OriginalValues[propName];
						var current = entry.State.ToString() == "Deleted" ? "" : entry.CurrentValues[propName];

						if (propName == "Deleted" && entry.State.ToString() == "Modified")
							original = "False";

						if (Convert.ToString(current) != Convert.ToString(original)) {
							this.Bitacora.Add(new Bitacora {
								BDOrigen = this.Database.Connection.Database,
								EntidadOrigen = entry.Entity.GetType().Name,
								Accion = entry.State.ToString(),
								Propiedad = propName,
								IdRegistro = IdRegistro,
								TipoDato = Convert.ToString(current.GetType()),
								ValorAntes = Convert.ToString(original),
								ValorDespues = Convert.ToString(current),
								Fecha = DateTime.Now,
								Correo = correo
							});
						}

					}

				}
			}

		}

	}

}
