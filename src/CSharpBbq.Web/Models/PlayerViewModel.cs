using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSharpBbq.Data.Model.Ladder;

namespace CSharpBbq.Web.Models
{
    public class PlayerViewModel
    {
        public Player Player { get; set; }
        public List<Match> Matches { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public string LastMatch { get; set; }
        public int CurrentRank { get; set; }
        public int Points { get; set; }
    }
}