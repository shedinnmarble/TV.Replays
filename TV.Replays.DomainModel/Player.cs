using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TV.Replays.DomainModel
{
    public class Player
    {
        public string Name { get; set; }

        public GameName GameName { get; set; }

        public string Gender { get; set; }

        public string Description { get; set; }

        public bool IsOnline { get; set; }

        public string LiveRoomUrl { get; set; }
    }


}
