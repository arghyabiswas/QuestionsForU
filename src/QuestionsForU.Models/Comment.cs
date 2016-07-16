namespace QuestionsForU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public int ID { get; set; }

        [Column("Comment")]
        [Required]
        [StringLength(2500)]
        public string Comment1 { get; set; }

        public short? Rating { get; set; }

        public int QuestionID { get; set; }

        public long HistoryID { get; set; }

        public virtual History History { get; set; }

        public virtual Question Question { get; set; }
    }
}
