using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TV.Replays.Model
{
    public class Player
    {
        public Player()
        {
            LiveRooms = new List<LiveRoom>();
        }

        public string Name { get; set; }

        public GameName GameName { get; set; }

        public string Gender { get; set; }

        public string Description { get; set; }

        public bool IsOnline
        {
            get
            {
                IEnumerable<LiveRoom> onlineRoom = LiveRooms.Where(a => a.IsOnline == true);
                return onlineRoom.Count() != 0;
            }
        }

        public List<LiveRoom> LiveRooms { get; set; }
    }


}
