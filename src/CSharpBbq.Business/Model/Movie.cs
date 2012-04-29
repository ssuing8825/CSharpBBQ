using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpBbq.Business.Model
{
   public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Actor { get; set; }
        public string Actorasdf { get; set; }
        public string Description { get; set; }
        public Int16 Rating { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
