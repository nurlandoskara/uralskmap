namespace UralskMap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocationPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        LocationType = c.Int(nullable: false),
                        Position_Latitude = c.Double(nullable: false),
                        Position_Longitude = c.Double(nullable: false),
                        Position_Altitude = c.Double(nullable: false),
                        Position_AltitudeReference = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LocationPoints");
        }
    }
}
