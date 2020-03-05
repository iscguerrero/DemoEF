namespace PersistenciaDemo.Migrations {
	using System;
	using System.Data.Entity.Migrations;

	public partial class AgregaIdRegistroaBitacora : DbMigration {
		public override void Up() {
			AddColumn("dbo.Bitacora", "IdRegistro", c => c.String());
		}

		public override void Down() {
			DropColumn("dbo.Bitacora", "IdRegistro");
		}
	}
}
