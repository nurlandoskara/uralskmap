namespace UralskMap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LocationPoints", "Position_Altitude");
            DropColumn("dbo.LocationPoints", "Position_AltitudeReference");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LocationPoints", "Position_AltitudeReference", c => c.Int(nullable: false));
            AddColumn("dbo.LocationPoints", "Position_Altitude", c => c.Double(nullable: false));
        }
    }
}
