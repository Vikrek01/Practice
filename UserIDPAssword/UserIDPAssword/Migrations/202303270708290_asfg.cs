namespace UserIDPAssword.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asfg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "Leave", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "Leave");
        }
    }
}
