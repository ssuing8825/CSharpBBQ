using System;
using System.ComponentModel.DataAnnotations;

namespace CSharpBbq.Data.Model.Ladder
{
    public class Match
    {
        public int Id { get; set; }

        [Required]
        public int WinnerId { get; set; }
        public Player Winner { get; set; }

        [Required]
        public int LooserId { get; set; }
        public Player Looser { get; set; }

        [Required]
        public int ChallengerId { get; set; }
        [ForeignKey("ChallengerId")]
        public Player Challenger { get; set; }


        [Required]
        public int LadderWeekId { get; set; }
        public LadderWeek LadderWeek { get; set; }

        public DateTime? DateOfMatch { get; set; }

        public Int16 G1W { get; set; }
        public Int16 G1L { get; set; }

        public Int16 G2W { get; set; }
        public Int16 G2L { get; set; }

        public Int16? G3W { get; set; }
        public Int16? G3L { get; set; }

        public Int16 WinnerRank { get; set; }
        public Int16 LooserRank { get; set; }

     


        public string GetWinnerName()
        {
            string name = Winner.Name;
            if (WinnerId == ChallengerId)
                name += "*";

            return name;
        }

        public string GetLooserName()
        {
            string name = Looser.Name;
            if (LooserId == ChallengerId)
                name += "*";

            return name;
        }


        public int GetWinnerPoints()
        {

            //Points from the match
            var total = G1W + G2W + (G3W ?? 0);

            //Points FTW
            var winnerIsRankedLower = Winner.Rank > Looser.Rank;
            if (winnerIsRankedLower)
            {
                total += Properties.Settings.Default.LowerRankFactor;
            }
            else
                total += Properties.Settings.Default.HigherRankFactor;

            //Point if challenger
            if (ChallengerId == WinnerId)
                total += Properties.Settings.Default.ChallengeFactor;

            return total;

        }

        public int GetLooserPoints()
        {
            //Points from the match
            var total = G1L + G2L + (G3L ?? 0);

            //Point if challenger
            if (ChallengerId == LooserId)
                total += Properties.Settings.Default.ChallengeFactor;

            return total;

        }

    }
}
