using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QAWebsite.Models
{
    public class Question
    {
        public Question()
        {
            Answers = new List<Answer>();
            QuestionComments = new List<QuestionComment>();
            Date = System.DateTime.Now;
            QuestionTags = new HashSet<QuestionTag>();
        }
        public int Id { get; set; }
        [Required]
        [MinLength(3),MaxLength(128)]
        public string Title { get; set; }
        [Required]
        [MinLength(3), MaxLength(512)]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int QuestionVote { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuestionComment> QuestionComments { get; set; }
        public virtual ICollection<QuestionTag> QuestionTags { get; set; }
    }
}