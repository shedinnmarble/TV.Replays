using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TV.Replays.Model
{
    public class LiveRoom
    {
        public TvName Name { get; set; }
        public string Url { get; set; }
        public bool IsOnline { get; set; }
    }
}
