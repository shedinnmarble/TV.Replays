using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TV.Replays.Model
{
    public static class LiveExtension
    {
        public static bool IsOnline(this IEnumerable<Live> lives, Player player)
        {
            if (player.LiveRooms.Select(a => a.Name).Contains(TvName.DouYu))
            {
                LiveRoom liveRoom = player.LiveRooms.Where(a => a.Name == TvName.DouYu).SingleOrDefault();
                string url = liveRoom.Url;
                if (!url.StartsWith("http://"))
                {
                    url = "http://" + url;
                }
                foreach (var live in lives)
                {
                    if (string.Equals(live.RoomUrl, url, StringComparison.OrdinalIgnoreCase))
                    {
                        liveRoom.IsOnline = true;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
