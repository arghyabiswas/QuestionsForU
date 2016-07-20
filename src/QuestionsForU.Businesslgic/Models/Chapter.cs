using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionForU.Businesslgic.Models
{
    public partial class Chapter 
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SeoTag { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
        public List<Book> BookReference { get; set; }
        public List<Question> Questions { get; set; }
        public List<Image> Images { get; set; }

    
    }
}