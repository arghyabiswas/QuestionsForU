namespace QuestionsForU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BookCategory")]
    public partial class BookCategory
    {
        public int ID { get; set; }

        public int BookID { get; set; }

        public int CategoryID { get; set; }

        public virtual Book Book { get; set; }

        public virtual Categoriy Categoriy { get; set; }
    }
}
