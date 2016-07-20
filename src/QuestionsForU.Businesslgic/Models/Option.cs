using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionForU.Businesslgic.Models
{
    public partial class Option
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsAnswer { get; set; }
        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}