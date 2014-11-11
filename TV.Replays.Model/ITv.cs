﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TV.Replays.Model
{
    public interface ITv
    {
        TvName Name { get; }

        IEnumerable<Live> GetDota2();
        IEnumerable<Live> GetLoL();
        IEnumerable<Live> GetWar3();

        IEnumerable<Live> GetAll();

        string GetVideoLink(string liveRoomId);
    }
}
