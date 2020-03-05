namespace PersistenciaDemo.Migrations {
	using System;
	using System.Data.Entity.Migrations;

	public partial class Agregaentidadesnn : DbMigration {
		public override void Up() {
			DropForeignKey("dbo.DocenteCursoes", "Docente_Id", "dbo.Docentes");
			DropForeignKey("dbo.DocenteCursoes", "Curso_Id", "dbo.Cursos");
			DropForeignKey("dbo.EstudianteCursoes", "Estudiante_Id", "dbo.Estudiantes");
			DropForeignKey("dbo.EstudianteCursoes", "Curso_Id", "dbo.Cursos");
			DropIndex("dbo.DocenteCursoes", new[] { "Docente_Id" });
			DropIndex("dbo.DocenteCursoes", new[] { "Curso_Id" });
			DropIndex("dbo.EstudianteCursoes", new[] { "Estudiante_Id" });
			DropIndex("dbo.EstudianteCursoes", new[] { "Curso_Id" });
			CreateTable(
					"dbo.CursosDocentes",
					c => new {
						Id = c.Int(nullable: false, identity: true),
						FechaInicio = c.DateTime(),
						Deleted = c.Boolean(nullable: false),
						IdCurso = c.Int(nullable: false),
						IdDocente = c.Int(nullable: false),
					})
					.PrimaryKey(t => t.Id)
					.ForeignKey("dbo.Cursos", t => t.IdCurso, cascadeDelete: true)
					.ForeignKey("dbo.Docentes", t => t.IdDocente, cascadeDelete: true)
					.Index(t => t.IdCurso)
					.Index(t => t.IdDocente);

			CreateTable(
					"dbo.CursosEstudiantes",
					c => new {
						Id = c.Int(nullable: false, identity: true),
						FechaIncripcion = c.DateTime(),
						Deleted = c.Boolean(nullable: false),
						IdCurso = c.Int(nullable: false),
						IdEstudiante = c.Int(nullable: false),
					})
					.PrimaryKey(t => t.Id)
					.ForeignKey("dbo.Cursos", t => t.IdCurso, cascadeDelete: true)
					.ForeignKey("dbo.Estudiantes", t => t.IdEstudiante, cascadeDelete: true)
					.Index(t => t.IdCurso)
					.Index(t => t.IdEstudiante);

			DropTable("dbo.DocenteCursoes");
			DropTable("dbo.EstudianteCursoes");
		}

		public override void Down() {
			CreateTable(
					"dbo.EstudianteCursoes",
					c => new {
						Estudiante_Id = c.Int(nullable: false),
						Curso_Id = c.Int(nullable: false),
					})
					.PrimaryKey(t => new { t.Estudiante_Id, t.Curso_Id });

			CreateTable(
					"dbo.DocenteCursoes",
					c => new {
						Docente_Id = c.Int(nullable: false),
						Curso_Id = c.Int(nullable: false),
					})
					.PrimaryKey(t => new { t.Docente_Id, t.Curso_Id });

			DropForeignKey("dbo.CursosEstudiantes", "IdEstudiante", "dbo.Estudiantes");
			DropForeignKey("dbo.CursosEstudiantes", "IdCurso", "dbo.Cursos");
			DropForeignKey("dbo.CursosDocentes", "IdDocente", "dbo.Docentes");
			DropForeignKey("dbo.CursosDocentes", "IdCurso", "dbo.Cursos");
			DropIndex("dbo.CursosEstudiantes", new[] { "IdEstudiante" });
			DropIndex("dbo.CursosEstudiantes", new[] { "IdCurso" });
			DropIndex("dbo.CursosDocentes", new[] { "IdDocente" });
			DropIndex("dbo.CursosDocentes", new[] { "IdCurso" });
			DropTable("dbo.CursosEstudiantes");
			DropTable("dbo.CursosDocentes");
			CreateIndex("dbo.EstudianteCursoes", "Curso_Id");
			CreateIndex("dbo.EstudianteCursoes", "Estudiante_Id");
			CreateIndex("dbo.DocenteCursoes", "Curso_Id");
			CreateIndex("dbo.DocenteCursoes", "Docente_Id");
			AddForeignKey("dbo.EstudianteCursoes", "Curso_Id", "dbo.Cursos", "Id", cascadeDelete: true);
			AddForeignKey("dbo.EstudianteCursoes", "Estudiante_Id", "dbo.Estudiantes", "Id", cascadeDelete: true);
			AddForeignKey("dbo.DocenteCursoes", "Curso_Id", "dbo.Cursos", "Id", cascadeDelete: true);
			AddForeignKey("dbo.DocenteCursoes", "Docente_Id", "dbo.Docentes", "Id", cascadeDelete: true);
		}
	}
}
