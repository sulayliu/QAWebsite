namespace QAWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuestionCommentAndAnswerComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        AnswerId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AnswerId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.QuestionComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        QuestionId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.QuestionId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AnswerComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuestionComments", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.AnswerComments", "AnswerId", "dbo.Answers");
            DropIndex("dbo.QuestionComments", new[] { "UserId" });
            DropIndex("dbo.QuestionComments", new[] { "QuestionId" });
            DropIndex("dbo.AnswerComments", new[] { "UserId" });
            DropIndex("dbo.AnswerComments", new[] { "AnswerId" });
            DropTable("dbo.QuestionComments");
            DropTable("dbo.AnswerComments");
        }
    }
}
