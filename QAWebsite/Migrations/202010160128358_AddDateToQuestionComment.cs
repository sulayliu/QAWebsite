namespace QAWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateToQuestionComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionComments", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionComments", "Date");
        }
    }
}
