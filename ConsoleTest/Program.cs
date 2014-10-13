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
            List<Player> players = new List<Player>();
            Player igCuojue = new Player();
            igCuojue.Name = "iG错觉";
            igCuojue.LiveRooms.Add(new LiveRoom { Name = TvName.DouYu, Url = "http://www.douyutv.com/CuoJue" });
            igCuojue.LiveRooms.Add(new LiveRoom { Name = TvName._17173, Url = "http://www.tv.17173.com/Cuojue" });
            igCuojue.GameName = GameName.Dota2;
            igCuojue.Description = "ig前职业";
            igCuojue.Gender = "男";

            Player tf = new Player();
            tf.Name = "早起的帕吉";
            tf.GameName = GameName.Dota2;
            tf.Gender = "男";
            tf.Description = "起得早";
            tf.LiveRooms.Add(new LiveRoom { Name = TvName.DouYu, Url = "http://www.douyutv.com/pudge" });
            tf.LiveRooms.Add(new LiveRoom { Name = TvName._17173, Url = "http://www.douyutv.com/pudge" });
            tf.LiveRooms.Add(new LiveRoom { Name = TvName.YY, Url = "http://www.douyutv.com/pudge" });

            DouYuTv dyTV = new DouYuTv();
            var lives = dyTV.GetDota2();
            lives.IsOnline(igCuojue);

            Console.WriteLine(igCuojue.IsOnline);

            Console.ReadKey();
        }
    }
}
