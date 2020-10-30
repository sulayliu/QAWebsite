using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QAWebsite.Models
{
    public class Answer
    {
        public Answer()
        {
            AnswerComments = new List<AnswerComment>();
            AnswerDate = System.DateTime.Now;
        }
        public int Id { get; set; }
        [Required]
        [MinLength(3),MaxLength(512)]
        public string Content { get; set; }
        public DateTime AnswerDate { get; set; }
        public bool Accepted { get; set; }
        public int AnswerVote { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<AnswerComment> AnswerComments { get; set; }
    }
}