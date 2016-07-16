namespace QuestionsForU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UploadedImage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UploadedImage()
        {
            UploadedImages1 = new HashSet<UploadedImage>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(100)]
        public string ImageName { get; set; }

        [Required]
        [StringLength(10)]
        public string ImageExtention { get; set; }

        public long ImageSize { get; set; }

        public int ChapterID { get; set; }

        public long? ParentID { get; set; }

        public int? State { get; set; }

        public string ProcessValue { get; set; }

        public long HistoryID { get; set; }

        public virtual Chapter Chapter { get; set; }

        public virtual History History { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UploadedImage> UploadedImages1 { get; set; }

        public virtual UploadedImage UploadedImage1 { get; set; }
    }
}
