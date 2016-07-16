namespace QuestionsForU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RecentComment
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2500)]
        public string Comment { get; set; }

        public short? Rating { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastModifiedDate { get; set; }

        public long? CreatedUser { get; set; }

        public long? LastModifiedUser { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool IsDeleted { get; set; }

        [StringLength(256)]
        public string name { get; set; }
    }
}
