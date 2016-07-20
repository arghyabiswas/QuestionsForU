using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionForU.Businesslgic.Models
{

    public partial class Category
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public Category Parent { get; set; }
        public List<Category> Chields { get; set; }
                
        public List<BookCtegory> BookCategories { get; set; }
        public List<QuestionCategory> QuestionCategories { get; set; }
    }
}