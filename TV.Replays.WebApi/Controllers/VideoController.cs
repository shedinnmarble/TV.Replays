using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TV.Replays.Service;
using TV.Replays.Service.Model;

namespace TV.Replays.WebApi.Controllers
{
    public class VideoController : ApiController
    {
        private LiveService Service;
        public VideoController(LiveService liveService)
        {
            Service = liveService;
        }

        public IEnumerable<VideoViewModel> Get()
        {
            return Service.GetVideoViewModels(Model.Game.Dota2)
                .OrderByDescending(a => a.ViewSum);
        }

        public string Get(string id)
        {
            return Service.GetLinkByVideoViewModelId(id);
        }
    }
}
