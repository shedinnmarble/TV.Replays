using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace TV.Replays.Model
{
    public class DouYuTv : ITv
    {
        public TvName Name { get { return TvName.DouYu; } }

        public IEnumerable<Live> GetDota2()
        {
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync("http://www.douyutv.com/directory/game/DOTA2");
            string html = response.Result;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var ulElement = doc.GetElementbyId("item_data");
            var liElements = ulElement.FirstChild.SelectNodes("li");

            List<Live> lives = new List<Live>();
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
                    live.PlayerImg = a_element.FirstChild.FirstChild.GetAttributeValue("src", "");
                    live.PlayerName = a_element.SelectNodes("div")[0].SelectSingleNode("p").SelectNodes("span")[1].InnerText;
                    live.ViewSum = a_element.SelectNodes("div")[0].SelectSingleNode("p").SelectNodes("span")[0].InnerText;

                    lives.Add(live);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return lives;
        }

        public IEnumerable<Live> GetLOL()
        {
            throw new NotImplementedException();
        }

        public string GetVideoUrl(string liveRoom)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = client.GetStringAsync(liveRoom);
                string html = response.Result;

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                var divElement = doc.GetElementbyId("js_share_more");
                var divs = divElement.SelectNodes("div")[1].SelectNodes("div");

                string result = divs[2].SelectSingleNode("input").GetAttributeValue("value", "");
                return result;
            }
            catch (Exception)
            {
                throw new Exception("获取播放地址失败");
            }
        }
    }
}
