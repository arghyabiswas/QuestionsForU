namespace QuestionsForU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuestionOption")]
    public partial class QuestionOption
    {
        public int ID { get; set; }

        public int QuestionID { get; set; }

        [StringLength(500)]
        public string Option { get; set; }

        public bool IsAnswer { get; set; }

        public virtual Question Question { get; set; }
    }
}
