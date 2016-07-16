namespace QuestionsForU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuestionCategory")]
    public partial class QuestionCategory
    {
        public int ID { get; set; }

        public int? QuestionID { get; set; }

        public int? CategoryID { get; set; }

        public virtual Categoriy Categoriy { get; set; }

        public virtual Question Question { get; set; }
    }
}
