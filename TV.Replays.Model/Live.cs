using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TV.Replays.Model
{
    public class Live
    {
        public Live(ITv tv)
        {
            this._tv = tv;
            this.TvName = tv.Name;
        }

        private ITv _tv;

        public TvName TvName { get; set; }

        public string RoomUrl { get; set; }

        public string Title { get; set; }

        public string PlayerName { get; set; }

        public string PlayerImg { get; set; }

        public string LiveImg { get; set; }

        public string ViewSum { get; set; }

        public string GetVideoHtmlCode()
        {
            return _tv.GetVideoUrl(this.RoomUrl);
        }
    }
}
