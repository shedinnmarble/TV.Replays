using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TV.Replays.BLL;
using TV.Replays.Model;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DouYuTv douyuTv = new DouYuTv();
            var lives = douyuTv.GetDota2();

            foreach (var live in lives)
            {
                Console.WriteLine(live.GetVideoHtmlCode());
            }

            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
