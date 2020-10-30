using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QAWebsite.Models
{
    public class QuestionComment
    {
        public QuestionComment()
        {
            Date = System.DateTime.Now;
        }
        public int Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(512)]
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}