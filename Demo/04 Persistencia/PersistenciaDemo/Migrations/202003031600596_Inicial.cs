namespace PersistenciaDemo.Migrations {
	using System;
	using System.Data.Entity.Migrations;

	public partial class Inicial : DbMigration {
		public override void Up() {
			CreateTable(
					"dbo.Cursos",
					c => new {
						Id = c.Int(nullable: false, identity: true),
						Nombre = c.String(),
						Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
					})
					.PrimaryKey(t => t.Id);

			CreateTable(
					"dbo.Docentes",
					c => new {
						Id = c.Int(nullable: false, identity: true),
						Nombre = c.String(),
						Domicilio = c.String(),
						Edad = c.Int(nullable: false),
					})
					.PrimaryKey(t => t.Id);

			CreateTable(
					"dbo.Estudiantes",
					c => new {
						Id = c.Int(nullable: false, identity: true),
						Nombre = c.String(),
						Domicilio = c.String(),
						Edad = c.Int(nullable: false),
						deleted = c.Boolean(nullable: false),
					})
					.PrimaryKey(t => t.Id);

			CreateTable(
					"dbo.DocenteCursoes",
					c => new {
						Docente_Id = c.Int(nullable: false),
						Curso_Id = c.Int(nullable: false),
					})
					.PrimaryKey(t => new { t.Docente_Id, t.Curso_Id })
					.ForeignKey("dbo.Docentes", t => t.Docente_Id, cascadeDelete: true)
					.ForeignKey("dbo.Cursos", t => t.Curso_Id, cascadeDelete: true)
					.Index(t => t.Docente_Id)
					.Index(t => t.Curso_Id);

			CreateTable(
					"dbo.EstudianteCursoes",
					c => new {
						Estudiante_Id = c.Int(nullable: false),
						Curso_Id = c.Int(nullable: false),
					})
					.PrimaryKey(t => new { t.Estudiante_Id, t.Curso_Id })
					.ForeignKey("dbo.Estudiantes", t => t.Estudiante_Id, cascadeDelete: true)
					.ForeignKey("dbo.Cursos", t => t.Curso_Id, cascadeDelete: true)
					.Index(t => t.Estudiante_Id)
					.Index(t => t.Curso_Id);

		}

		public override void Down() {
			DropForeignKey("dbo.EstudianteCursoes", "Curso_Id", "dbo.Cursos");
			DropForeignKey("dbo.EstudianteCursoes", "Estudiante_Id", "dbo.Estudiantes");
			DropForeignKey("dbo.DocenteCursoes", "Curso_Id", "dbo.Cursos");
			DropForeignKey("dbo.DocenteCursoes", "Docente_Id", "dbo.Docentes");
			DropIndex("dbo.EstudianteCursoes", new[] { "Curso_Id" });
			DropIndex("dbo.EstudianteCursoes", new[] { "Estudiante_Id" });
			DropIndex("dbo.DocenteCursoes", new[] { "Curso_Id" });
			DropIndex("dbo.DocenteCursoes", new[] { "Docente_Id" });
			DropTable("dbo.EstudianteCursoes");
			DropTable("dbo.DocenteCursoes");
			DropTable("dbo.Estudiantes");
			DropTable("dbo.Docentes");
			DropTable("dbo.Cursos");
		}
	}
}
