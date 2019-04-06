namespace HRTimesheetApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUserRoles : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4bdfddfb-0f06-448a-a839-4e7912db7a65', N'Admin')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2e3515dd-746d-4631-8a91-c81b066fe4ed', N'4bdfddfb-0f06-448a-a839-4e7912db7a65')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ac63bc74-4762-428c-ba8c-4d3eaf016fe0', N'4bdfddfb-0f06-448a-a839-4e7912db7a65')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
