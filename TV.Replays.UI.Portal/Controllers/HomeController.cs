using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TV.Replays.BLL;
using TV.Replays.Model;

namespace TV.Replays.UI.Portal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
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

            players.Add(igCuojue);
            players.Add(tf);

            DouYuTv dyTv = new DouYuTv();
            var lives = dyTv.GetDota2();
            foreach (var player in players)
            {
                lives.IsOnline(player);
            }

            return View(players);
        }

        public ActionResult Video(string liveRoom)
        {
            //string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "VideoPage.html");
            //string html = System.IO.File.ReadAllText(path);
            DouYuTv dyTv = new DouYuTv();
            string videoCode = dyTv.GetVideoUrl(liveRoom);

            videoCode = Server.HtmlDecode(videoCode);


            //html = html.Replace("#Video", videoCode);
            return View(new MvcHtmlString(videoCode));
        }
    }
}