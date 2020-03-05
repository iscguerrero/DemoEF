using ModelDemo;
using ModelDemo.Common;
using PersistenciaDemo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace ConsoleApplDemo {
	class Program {
		static void Main(string[] args) {
			using (var ctx = new DemoContext()) {
				using (var trans = ctx.Database.BeginTransaction()) {
					try {
						// Codigo para dar de alta un nuevo curso
						Curso ToAddCurso = new Curso {
							Nombre = "Scrum",
							Precio = 360,
							Deleted = false
						};

						var ToAddCursoState = ctx.Entry(ToAddCurso).State;

						Curso AddedCurso = AddCurso(ToAddCurso, ctx);

						ctx.SaveChanges();

						int IdAdd = AddedCurso.Id;

						var AddedCursoState = ctx.Entry(AddedCurso).State;

						// Codigo para editar un curso no trackeado
						Curso ToEditNoTrackingCurso = ctx.Curso.Where(c => c.Id == 4).AsNoTracking().SingleOrDefault();
						ToEditNoTrackingCurso.Nombre = "Lenguas";
						ToEditNoTrackingCurso.Precio = 320;

						var ToEditNoTrackingCursoState = ctx.Entry(ToEditNoTrackingCurso).State;

						var UpdatedNoTRackingCurso = EditNoTrackingCurso(ToEditNoTrackingCurso, ctx);

						var UpdatedNoTrackingCursoState = ctx.Entry(UpdatedNoTRackingCurso).State;

						// Codigo para editar un curso trackeado
						Curso ToEditTrackingCurso = ctx.Curso.Where(c => c.Id == 5).SingleOrDefault();
						ToEditTrackingCurso.Nombre = "Filosofía";
						ToEditTrackingCurso.Precio = 210;

						var ToEditTrackingCursoState = ctx.Entry(ToEditTrackingCurso).State;

						Curso UpdatedTrackingCurso = EditTrackingCurso(ToEditTrackingCurso, ctx);

						var UpdatedTrackingCursoState = ctx.Entry(UpdatedTrackingCurso).State;

						// Codigo para borrado lógico de un registro
						Curso ToSoftDeletedCurso = SoftDeleteCurso(7, ctx);

						bool DeleteValue = ToSoftDeletedCurso.Deleted;

						var ToSoftDeletedCursoState = ctx.Entry(ToSoftDeletedCurso).State;

						// Codigo para borrado en bruto de un registro
						Curso ToHardDeleteCurso = ctx.Curso.Where(x => x.Id == 6).SingleOrDefault();

						bool state = HardDeleteCurso(ToHardDeleteCurso, ctx);

						ctx.SaveChanges();

						trans.Commit();
					} catch (Exception e) {
						trans.Rollback();
					}

				}
			}
		}

		private static Curso AddCurso(Curso curso, DemoContext ctx) {
			try {
				ctx.Curso.Add(curso);
				return curso;
			} catch (Exception e) {
				return null;
			}
		}

		private static Curso EditNoTrackingCurso(Curso curso, DemoContext ctx) {
			try {
				Curso existing = ctx.Set<Curso>().Find(curso.Id);
				if (existing == null)
					return null;

				ctx.Entry(existing).CurrentValues.SetValues(curso);
				return existing;
			} catch (Exception e) {
				return null;
			}
		}

		private static Curso EditTrackingCurso(Curso curso, DemoContext ctx) {
			try {
				ctx.Entry(curso).State = EntityState.Modified;
				return curso;
			} catch (Exception e) {
				return null;
			}
		}

		private static Curso SoftDeleteCurso(int Id, DemoContext ctx) {
			try {
				var curso = ctx.Set<Curso>().Find(Id);
				if (curso == null)
					return null;

				((ISoftDeleted)curso).Deleted = true;
				ctx.Set<Curso>().Attach(curso); // agregarlo al changestracker
				ctx.Entry(curso).State = EntityState.Modified;
				return curso;

			} catch (Exception) {
				return null;
			}
		}

		private static bool HardDeleteCurso(Curso curso, DemoContext ctx) {
			try {
				ctx.Curso.Remove(curso);
				return true;
			} catch (Exception) {
				return false;
			}
		}

	}
}
