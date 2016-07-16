namespace QuestionsForU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Categoriy")]
    public partial class Categoriy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Categoriy()
        {
            BookCategories = new HashSet<BookCategory>();
            Categoriy1 = new HashSet<Categoriy>();
            QuestionCategories = new HashSet<QuestionCategory>();
        }

        public int ID { get; set; }

        [StringLength(256)]
        public string Category { get; set; }

        public int? ParentID { get; set; }

        public long HistoryID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookCategory> BookCategories { get; set; }

        public virtual History History { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Categoriy> Categoriy1 { get; set; }

        public virtual Categoriy Categoriy2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionCategory> QuestionCategories { get; set; }
    }
}
