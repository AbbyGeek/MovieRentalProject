namespace MovieRentalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class seedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8000067f-c589-4dde-9210-fd9632341b33', N'guest@movierentalproject.com', 0, N'AK2qr6BlbyJHb3ZpcIbBGKnwJ5aEOLrzwE2n4wWNXoMaDvvHoynpT6pXulWhP8wbdQ==', N'8abe6d9d-9f46-49ec-82fa-ab543d0e6158', NULL, 0, 0, NULL, 1, 0, N'guest@movierentalproject.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9004a1a6-a99d-4b0b-857d-d618263e4b44', N'admin@movierentalproject.com', 0, N'APPL0SBAb9YTmXXAGZ4djhFW7vtyZmnJdcAJkAtN1NRqGUdyKYl0opBlUBEx0jq4hA==', N'464d1839-685f-4194-9253-8afc1cfb3c79', NULL, 0, 0, NULL, 1, 0, N'admin@movierentalproject.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c0dfab5f-8c9b-4ee8-8a61-dbfa58b7a988', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9004a1a6-a99d-4b0b-857d-d618263e4b44', N'c0dfab5f-8c9b-4ee8-8a61-dbfa58b7a988')
");
        }

        public override void Down()
        {
        }
    }
}
