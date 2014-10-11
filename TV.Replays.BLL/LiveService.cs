using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TV.Replays.IDAL;
using TV.Replays.Model;

namespace TV.Replays.BLL
{
    public class LiveService
    {
        public ILiveRepository _liveRepository;
        public LiveService(ILiveRepository liveRepository)
        {
            this._liveRepository = liveRepository;
        }


    }
}