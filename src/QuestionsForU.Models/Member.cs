namespace QuestionsForU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            Histories = new HashSet<History>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [StringLength(256)]
        public string first_name { get; set; }

        [StringLength(256)]
        public string last_name { get; set; }

        [StringLength(256)]
        public string name { get; set; }

        [StringLength(256)]
        public string email { get; set; }

        public int? Roles { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLoginDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<History> Histories { get; set; }
    }
}
