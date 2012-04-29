using System;
using System.Data.Entity;
using System.Linq;
using CSharpBbq.Data;
using CSharpBbq.Data.Model.Ladder;

namespace Movies
{
    class Program
    {
        static void Main(string[] args)
        {
            // DbDatabase.SetInitializer<MovieDbContext>(new MovieDbContext.Initializer());
           Database.SetInitializer<LadderDbContext>(new LadderDbContext.Initializer());

            GoToLadderRepository();

            // GoToLadder();
            //GoToDB();

        }
        private static void GoToLadderRepository()
        {
            LadderRepository r = new LadderRepository();
            foreach (var s in r.LadderWeeks())
            {
                Console.WriteLine(" - {0}", s.WeekNumber);
            }

         
            Console.WriteLine(" - {0}", r.CurrentWeek().WeekNumber);
        }


        private static void GoToLadder()
        {
            using (var db = new LadderDbContext())
            {
                Console.WriteLine("All People in database:");
                foreach (var item in db.LadderWeeks)
                {
                    Console.WriteLine(" - {0}", item.WeekNumber);
                }
            }

            Console.WriteLine("Done.");
            Console.ReadLine();

        }

        private static void GoToDB()
        {
            using (var db = new MovieDbContext())
            {
                var allComedies = from m in db.Movies
                                  where m.Tags.Count(c => c.TagName == "Comedy") > 0
                                  select m;

                Console.WriteLine("All comedies in database:");
                foreach (var item in allComedies)
                {
                    Console.WriteLine(" - {0}", item.Title);
                }
            }

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
