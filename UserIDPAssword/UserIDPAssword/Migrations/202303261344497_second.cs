namespace UserIDPAssword.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "GmailConfirm", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "GmailConfirm", c => c.Int(nullable: false));
        }
    }
}
