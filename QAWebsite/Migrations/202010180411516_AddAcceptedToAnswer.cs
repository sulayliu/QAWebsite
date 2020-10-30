namespace QAWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAcceptedToAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Accepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "Accepted");
        }
    }
}
