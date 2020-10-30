using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QAWebsite.Models
{
    public class AnswerComment
    {
        public AnswerComment()
        {
            Date = System.DateTime.Now;
        }
        public int Id { get; set; }
        [Required]
        [MinLength(3),MaxLength(512)]
        public string Comment { get; set; }
        public DateTime Date { get; set; } 
        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}