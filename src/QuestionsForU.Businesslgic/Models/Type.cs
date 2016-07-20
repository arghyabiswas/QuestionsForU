using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionForU.Businesslgic.Models
{
    public partial class Type
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public List<Question> Questions { get; set; }
    }
}