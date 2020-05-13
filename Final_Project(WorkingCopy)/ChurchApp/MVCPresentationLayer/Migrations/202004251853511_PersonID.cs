namespace MVCPresentationLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PersonID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PersonID");
        }
    }
}
