using UralskMap.Models;

namespace UralskMap
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ModelContainer : DbContext
    {
        // Your context has been configured to use a 'ModelContainer' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'UralskMap.ModelContainer' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ModelContainer' 
        // connection string in the application configuration file.
        public ModelContainer()
            : base("name=ModelContainer")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<LocationPoint> Locations { get; set; }
    }
}