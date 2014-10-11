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

        public string TvName { get; set; }

        public string Room { get; set; }

        public string Title { get; set; }

        public string PlayerName { get; set; }

        public string PlayerImg { get; set; }

        public string LiveImg { get; set; }

        public string ViewSum { get; set; }

        public string GetVideoHtmlCode()
        {
            return _tv.GetVideoHtmlCode(this.Room);
        }
    }
}
