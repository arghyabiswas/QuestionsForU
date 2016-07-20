using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionForU.Businesslgic.Models
{
    public partial class Question
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string Notes { get; set; }
        public string SeoTag { get; set; }
        public decimal Rating { get; set; }
        public int Status { get; set; }
        public int TypeId { get; set; }
        public long ImageId { get; set; }
        public int ChapterId { get; set; }

        public Chapter Chapter { get; set; }
        public Type Type { get; set; }
        public Image Image { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Option> Options { get; set; }
    }
}