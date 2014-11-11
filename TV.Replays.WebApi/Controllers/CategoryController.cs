using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TV.Replays.Model;
using TV.Replays.Service;
using TV.Replays.Service.Model;

namespace TV.Replays.WebApi.Controllers
{
    public class CategoryController : ApiController
    {
        private LiveService Service;
        public CategoryController(LiveService liveService)
        {
            Service = liveService;
        }

        public IEnumerable<LiveViewModel> Get()
        {
            return Service.GetLiveViewModels(Game.Dota2).OrderByDescending(a => a.ViewSum);
        }

        public IEnumerable<LiveViewModel> Get(string id)
        {
            IEnumerable<LiveViewModel> result = Get();
            if (result != null)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    result = result.Where(a => a.Categories.Contains(id));
                }
            }
            return result;
        }
    }
}