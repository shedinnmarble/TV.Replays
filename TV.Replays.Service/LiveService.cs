using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using TV.Replays.IDAL;
using TV.Replays.Model;
using TV.Replays.Service.Model;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace TV.Replays.Service
{
    public class LiveService
    {
        public IPlayerDal PlayerDal;
        public LiveService(IPlayerDal playerDal)
        {
            PlayerDal = playerDal;
            if (UpdateLiveCacheTimer == null)
                UpdateLiveCacheTimer = new Timer(state =>
                {
                    ReloadLiveCache();
                }, null, 0, 30000);
        }

        private static ConcurrentDictionary<string, Live> LiveCache = new ConcurrentDictionary<string, Live>();
        private static Timer UpdateLiveCacheTimer;

        public IEnumerable<Live> GetLives()
        {
            if (LiveCache == null)
            {
                ReloadLiveCache();
            }
            return LiveCache.Values;
        }

        public IEnumerable<LiveViewModel> GetLiveViewModels(Game game)
        {
            var playerList = GetPlayers()
                .Where(a => a.Game == game.ToString())
                .Where(a => a.Recommend == false)
                .OrderByDescending(a => a.Level);
            foreach (var player in playerList)
            {
                LiveViewModel liveVM = new LiveViewModel();
                liveVM.Id = player.Id;
                liveVM.PlayerName = player.Name;
                if (player.Live != null)
                {
                    liveVM.ViewSum = player.Live.ViewSumToNumber();
                    liveVM.Title = player.Live.Title;
                    liveVM.VideoImage = player.Live.VideoIcon;
                }
                liveVM.Categories = player.Categories;
                liveVM.IsOnline = player.IsOnline();

                yield return liveVM;
            }
        }
        public IEnumerable<PlayerViewModel> GetPlayerViewModels(Game game)
        {
            var players = GetPlayers()
                .Where(a => a.Game == game.ToString())
                .Where(a => a.Recommend == true)
                .OrderByDescending(a => a.Level);
            foreach (var player in players)
            {
                PlayerViewModel playerVM = new PlayerViewModel();
                playerVM.Id = player.Id;
                playerVM.Name = player.Name;
                playerVM.Icon = player.Icon;
                playerVM.IsOnline = player.IsOnline();
                yield return playerVM;
            }
        }
        public IEnumerable<VideoViewModel> GetVideoViewModels(Game game)
        {
            var lives = GetLives();
            foreach (var live in lives)
            {
                VideoViewModel videoVM = new VideoViewModel();
                videoVM.Id = CreateVideoViewModelId(live);
                videoVM.PlayerName = live.PlayerName;
                videoVM.Title = live.Title;
                videoVM.VideoImage = live.VideoIcon;
                videoVM.ViewSum = live.ViewSumToNumber();

                yield return videoVM;
            }
        }
        public PlayerInformationViewModel GetPlayerInformationViewModel(string id)
        {
            Player player = GetPlayers().FirstOrDefault(p => p.Id == id);

            if (player == null)
                return null;

            PlayerInformationViewModel vm = new PlayerInformationViewModel();

            if (player.Live != null)
            {
                vm.Title = player.Live.Title;
                vm.ViewSum = player.Live.ViewSumToNumber();
            }
            vm.PlayerName = player.Name;
            vm.Icon = player.Icon;
            vm.Description = player.Description;
            vm.Categories = player.Categories;
            return vm;
        }
        public string GetLinkByVideoViewModelId(string id)
        {
            if (LiveCache.ContainsKey(id))
                return LiveCache[id].GetVideoLink();
            return "";
        }

        private string CreateVideoViewModelId(Live live)
        {
            switch (live.TvName)
            {
                case TvName.斗鱼Tv:
                    return "dy_" + live.RoomId;
                case TvName.战旗Tv:
                    return "zq_" + live.RoomId;
                case TvName.火猫Tv:
                    return "hm_" + live.RoomId;
                case TvName._17173:
                    return live.RoomId;
                case TvName.YY:
                    return "YY_" + live.RoomId;
                default:
                    return "";
            }
        }
        private void ReloadLiveCache()
        {
            var tvList = TvFactory.CreateTvList();
            foreach (var tv in tvList)
            {
                foreach (var live in tv.GetAll())
                {
                    string key = CreateVideoViewModelId(live);
                    LiveCache.AddOrUpdate(key, live, (k, v) => live);
                }
            }
        }
        public IEnumerable<Player> GetPlayers()
        {
            var liveList = GetLives();
            var playerList = PlayerDal.Get();

            foreach (var player in playerList)
            {
                liveList.IsOnline(player);
            }
            return playerList;
        }
    }
}
