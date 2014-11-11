using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TV.Replays.Model;
using TV.Replays.Service;
using TV.Replays.Service.Model;


namespace TV.Replays.WebApi.Controllers
{
    public class PlayerController : ApiController
    {
        public LiveService Service;
        public PlayerController(LiveService playerService)
        {
            Service = playerService;
        }

        public IEnumerable<PlayerViewModel> Get()
        {
            return Service.GetPlayerViewModels(Game.Dota2);
        }
        public PlayerInformationViewModel Get(string id)
        {
            var player = Service.GetPlayerInformationViewModel(id);
            return player;
        }
    }
}
