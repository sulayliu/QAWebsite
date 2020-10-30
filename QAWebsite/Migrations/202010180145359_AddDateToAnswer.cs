namespace QAWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateToAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnswerComments", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Answers", "AnswerDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Answers", "AnswerVote", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "QuestionVote", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "QuestionVote");
            DropColumn("dbo.Answers", "AnswerVote");
            DropColumn("dbo.Answers", "AnswerDate");
            DropColumn("dbo.AnswerComments", "Date");
        }
    }
}
