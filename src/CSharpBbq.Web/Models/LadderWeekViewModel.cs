using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSharpBbq.Data.Model.Ladder;

namespace CSharpBbq.Web.Models
{
    public class LadderWeekViewModel
    {
        public LadderWeek LadderWeek { get; set; }
        public List<Match> Matches { get; set; }
        public List<PlayerViewModel> Standings { get; set; }
    }
}