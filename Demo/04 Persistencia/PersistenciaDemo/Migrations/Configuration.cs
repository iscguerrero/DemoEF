namespace PersistenciaDemo.Migrations {
	using ModelDemo;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<PersistenciaDemo.DemoContext> {
		public Configuration() {
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(PersistenciaDemo.DemoContext context) {
			// Insertar en la tabla de cursos
			/*List<Curso> cursos = new List<Curso>();
			List<Docente> docentes = new List<Docente>();
			List<Estudiante> estudiantes = new List<Estudiante>();

			context.Curso.AddOrUpdate(new Curso {
				Id = 1,
				Nombre = "Matematicas II",
				Precio = 100
			});

			cursos.Add(new Curso {
				Id = 7,
				Nombre = "Español",
				Precio = 150
			});
			cursos.Add(new Curso {
				Id = 8,
				Nombre = "Ciencias Sociales",
				Precio = 120
			});

			docentes.Add(new Docente {
				Id = 6,
				Nombre = "Domingo",
				Domicilio = "Bien lejos",
				Edad = 26,
				Cursos = cursos.Where(c => new[] { "Español", "Matemáticas" }.Contains(c.Nombre)).ToList()
			});
			docentes.Add(new Docente {
				Id = 7,
				Nombre = "Hugo",
				Domicilio = "Bien cercas",
				Edad = 30,
				Cursos = cursos
			});

			estudiantes.Add(new Estudiante {
				Id = 6,
				Nombre = "Alma",
				Edad = 18,
				Domicilio = "La fuente",
				Cursos = cursos
			});
			estudiantes.Add(new Estudiante {
				Id = 7,
				Nombre = "Alejandra",
				Edad = 18,
				Domicilio = "El Tepe",
				CursosEstudiantes = cursos
			});

			context.Curso.AddRange(cursos);
			context.Docente.AddRange(docentes);
			context.Estudiante.AddRange(estudiantes);

			context.SaveChanges();*/

			base.Seed(context);
		}
	}
}
