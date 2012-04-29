using System.ComponentModel.DataAnnotations;
using System;


namespace CSharpBbq.Data.Model.Ladder
{
    public class Standing
    {
        public int Id { get; set; }

        [Required]
        public int LadderWeekId { get; set; }
        public virtual LadderWeek LadderWeek { get; set; }

        [Required]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public Int32 Position { get; set; }

        [NotMapped]
        public int TotalPoints { get; set; }
    }
}
