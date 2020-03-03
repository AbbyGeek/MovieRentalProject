namespace MovieRentalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stocknumberlabelchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "NumInStock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "NumInStock", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "NumberInStock");
        }
    }
}
