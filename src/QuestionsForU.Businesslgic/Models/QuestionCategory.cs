using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionForU.Businesslgic.Models
{
    public partial class QuestionCategory 
    {

        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public Question Question { get; set; }
    }
}