namespace MovieRentalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingtypo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "isSubscribedToNewsletter", c => c.Boolean(nullable: false));
            DropColumn("dbo.Customers", "idSubscribedToNewsletter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "idSubscribedToNewsletter", c => c.Boolean(nullable: false));
            DropColumn("dbo.Customers", "isSubscribedToNewsletter");
        }
    }
}
