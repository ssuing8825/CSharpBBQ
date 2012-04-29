using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpBbq.Business.Model
{
   public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
