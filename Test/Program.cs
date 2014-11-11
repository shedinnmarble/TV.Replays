using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TV.Replays.Model;
using TV.Replays.XmlDAL;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var tvList = TvFactory.CreateTvList();


            foreach (var tv in tvList)
            {
                int i = tv.GetDota2().Count();
                Console.WriteLine(tv.Name + " " + i);
            }

            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
