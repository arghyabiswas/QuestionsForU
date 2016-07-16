namespace QuestionsForU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            Comments = new HashSet<Comment>();
            QuestionCategories = new HashSet<QuestionCategory>();
            QuestionOptions = new HashSet<QuestionOption>();
        }

        public int ID { get; set; }

        [Column("Question")]
        [Required]
        [StringLength(2500)]
        public string Question1 { get; set; }

        [Required]
        [StringLength(2500)]
        public string Notes { get; set; }

        public int? QuestionTypeID { get; set; }

        [Required]
        [StringLength(1000)]
        public string SeoTag { get; set; }

        public decimal Rating { get; set; }

        public int QuestionStatus { get; set; }

        public long? ImageID { get; set; }

        public int? ChapterID { get; set; }

        public long HistoryID { get; set; }

        public virtual Chapter Chapter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual History History { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionCategory> QuestionCategories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }

        public virtual QuestionType QuestionType { get; set; }
    }
}
