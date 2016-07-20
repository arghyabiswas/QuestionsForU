using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionForU.Businesslgic.Models
{
    public partial class Book
    {

        public int Id { get; set; }
        public string Text { get; set; }
       
        public List<Chapter> Chapters { get; set; }
        public List<BookCtegory> BookCategories { get; set; }

    }
}