namespace QAWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLengthLimited : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "Content", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.Questions", "Title", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Questions", "Content", c => c.String(nullable: false, maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "Content", c => c.String());
            AlterColumn("dbo.Questions", "Title", c => c.String());
            AlterColumn("dbo.Answers", "Content", c => c.String());
        }
    }
}
