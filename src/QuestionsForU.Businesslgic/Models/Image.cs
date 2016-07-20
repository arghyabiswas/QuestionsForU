using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionForU.Businesslgic.Models
{
    public partial class Image
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string Extention { get; set; }
        public long ImageSize { get; set; }
        public int ChapterId { get; set; }
        public long ParentId { get; set; }
        public int? State { get; set; }
        public string ProcessValue { get; set; }

        
        public Image Parent { get; set; }
        public List<Image> Chields { get; set; }
        
        public Chapter Chapter { get; set; }
        
    }
}