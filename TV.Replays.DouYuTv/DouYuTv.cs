using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TV.Replays.Model;

namespace TV.Replays.DouYuTv
{
    public class DouYuTv : ITv
    {
        public DouYuTv() { }

        public TvName Name
        {
            get { return TvName.斗鱼Tv; }
        }

        public IEnumerable<Live> GetDota2()
        {
        start:
            List<Live> lives = new List<Live>();
            try
            {
                HttpClient client = new HttpClient();
                var response = client.GetStringAsync("http://www.douyutv.com/directory/game/DOTA2");
                string html = response.Result;

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                var ulElement = doc.GetElementbyId("item_data");
                var liElements = ulElement.FirstChild.SelectNodes("li");


                foreach (var li in liElements)
                {
                    try
                    {
                        Live live = new Live(this);
                        var a_element = li.SelectSingleNode("a");
                        string liveRoom = a_element.GetAttributeValue("href", "");
                        if (!liveRoom.StartsWith("http://www.douyutv.com"))
                        {
                            liveRoom = "http://www.douyutv.com" + liveRoom;
                        }

                        live.RoomUrl = liveRoom;
                        live.Title = a_element.GetAttributeValue("title", "");
                        live.VideoIcon = a_element.FirstChild.FirstChild.GetAttributeValue("data-original", "");
                        live.PlayerName = a_element.SelectNodes("div")[0].SelectSingleNode("p").SelectNodes("span")[1].InnerText;
                        live.ViewSum = a_element.SelectNodes("div")[0].SelectSingleNode("p").SelectNodes("span")[0].InnerText;
                        live.Game = Game.Dota2;
                        //string[] sprits = live.VideoIcon.Split('/');
                        //string spritNode = sprits[sprits.Length - 1];
                        //live.RoomId = spritNode.Split('_')[0];

                        string[] sprits = liveRoom.Split('/');
                        live.RoomId = sprits[sprits.Length - 1];

                        lives.Add(live);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            catch (Exception)
            {
                goto start;
            }
            return lives;
        }

        public string GetVideoLink(string liveRoomId)
        {
            return "http://staticlive.douyutv.com/common/share/play.html?room_id=" + liveRoomId;
        }


        public IEnumerable<Live> GetLoL()
        {
            return Enumerable.Empty<Live>();
        }

        public IEnumerable<Live> GetWar3()
        {
            return Enumerable.Empty<Live>();
        }

        public IEnumerable<Live> GetAll()
        {
            var dota2 = Task.Factory.StartNew(() => GetDota2());
            var lol = Task.Factory.StartNew(() => GetLoL());
            var war3 = Task.Factory.StartNew(() => GetWar3());
            List<Live> lives = new List<Live>();
            lives.AddRange(dota2.Result);
            lives.AddRange(lol.Result);
            lives.AddRange(war3.Result);

            return lives;
        }
    }
}
