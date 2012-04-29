using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpBbq.Data.Model.Ladder
{
    public class LadderWeek
    {
        public int Id { get; set; }
        public int WeekNumber { get; set; }
        public DateTime StertDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public virtual ICollection<Standing> Standings { get; set; }
        public virtual ICollection<Match> Matches { get; set; }

    }
}
