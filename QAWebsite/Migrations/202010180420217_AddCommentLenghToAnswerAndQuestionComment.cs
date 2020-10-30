namespace QAWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentLenghToAnswerAndQuestionComment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AnswerComments", "Comment", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.QuestionComments", "Comment", c => c.String(nullable: false, maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuestionComments", "Comment", c => c.String());
            AlterColumn("dbo.AnswerComments", "Comment", c => c.String());
        }
    }
}
