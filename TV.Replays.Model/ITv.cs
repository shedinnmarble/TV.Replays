using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TV.Replays.Model
{
    public interface ITv
    {
        string Name { get; }
        IEnumerable<Live> GetDota2();
        IEnumerable<Live> GetLOL();

        string GetVideoHtmlCode(string liveRoom);
    }
}
