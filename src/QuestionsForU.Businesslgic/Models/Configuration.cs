using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionForU.Businesslgic.Models
{
    public partial class Configuration
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public bool ApplicationType { get; set; }
        public long? MemberId { get; set; }
    }
}