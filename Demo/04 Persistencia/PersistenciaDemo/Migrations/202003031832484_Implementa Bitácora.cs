namespace PersistenciaDemo.Migrations {
	using System;
	using System.Data.Entity.Migrations;

	public partial class ImplementaBitácora : DbMigration {
		public override void Up() {
			CreateTable(
					"dbo.Bitacora",
					c => new {
						Id = c.Int(nullable: false, identity: true),
						BDOrigen = c.String(),
						EntidadOrigen = c.String(),
						Accion = c.String(),
						Propiedad = c.String(),
						TipoDato = c.String(),
						ValorAntes = c.String(),
						ValorDespues = c.String(),
						Correo = c.String(),
						Fecha = c.DateTime(),
					})
					.PrimaryKey(t => t.Id);

			AddColumn("dbo.Cursos", "Deleted", c => c.Boolean(nullable: false));
			AddColumn("dbo.Docentes", "Deleted", c => c.Boolean(nullable: false));
		}

		public override void Down() {
			DropColumn("dbo.Docentes", "Deleted");
			DropColumn("dbo.Cursos", "Deleted");
			DropTable("dbo.Bitacora");
		}
	}
}
