using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TV.Replays.Common;
using TV.Replays.IDAL;
using TV.Replays.Model;
using TV.Replays.Service;

namespace TV.Replays.WebApi.Controllers
{
    public class PlayerEditController : Controller
    {
        public IPlayerDal _playerDal;
        public LiveService _service;
        public PlayerEditController(IPlayerDal playerDal)
        {
            _playerDal = playerDal;
            _service = new LiveService(playerDal);
        }

        public ActionResult Index(int page = 1, string category = "", string tv = "", bool isOnline = false, bool recommend = false, bool isDesc = true)
        {
            int pageIndex = page;
            int pageSize = 15;
            int count = 0;
            IEnumerable<Player> players;

            if (!String.IsNullOrEmpty(category) || !String.IsNullOrEmpty(tv) || recommend || isOnline)
            {
                players = _playerDal.Get();

                if (recommend)
                    players = players.Where(a => a.Recommend);

                if (!String.IsNullOrEmpty(category.Trim()))
                    players = players.Where(a => a.Categories != null)
                        .Where(a => a.Categories.Contains(category));

                if (!String.IsNullOrEmpty(tv.Trim()))
                    players = players.Where(a => a.LiveRooms != null)
                        .Where(a => a.LiveRooms.Select(p => p.Name).Contains(tv));

                foreach (var player in players)
                {
                    _service.GetLives().IsOnline(player);
                }

                if (isOnline)
                    players = players.Where(a => a.IsOnline());

                count = pageSize;
            }
            else
            {
                players = _playerDal.Get(pageIndex, pageSize, out count);

                foreach (var player in players)
                {
                    _service.GetLives().IsOnline(player);
                }
            }

            if (isDesc)
                players = players.OrderByDescending(a => a.Level);
            else
                players = players.OrderBy(a => a.Level);

            ViewBag.CurrentIndex = pageIndex;
            ViewBag.TotalPages = (count + pageSize - 1) / pageSize;
            ViewBag.NumberOfPages = count;
            return View(players);
        }

        public ActionResult Create()
        {
            return View(new Player());
        }

        [HttpPost]
        public ActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                if (player.Categories == null)
                    player.Categories = new string[0];
                _playerDal.Insert(player);
                return RedirectToAction("Index");
            }
            else
            {
                return View(player);
            }
        }

        public ActionResult Edit(string id)
        {
            var player = _playerDal.Get(id);
            return View(player);
        }

        [HttpPost]
        public ActionResult Edit(Player player)
        {
            if (ModelState.IsValid)
            {
                _playerDal.Update(player);
                return RedirectToAction("Index");
            }
            else
            {
                return View(player);
            }
        }

        public ActionResult Delete(string id)
        {
            _playerDal.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult EditLevel(string id, int level)
        {
            var player = _playerDal.Get(id);
            player.Level = level;
            _playerDal.Update(player);
            return RedirectToAction("Index");
        }

        public ActionResult Recommend(string id)
        {
            var player = _playerDal.Get(id);
            player.Recommend = true;
            _playerDal.Update(player);
            return RedirectToAction("Index");
        }

        public ActionResult CancelRecommend(string id)
        {
            var player = _playerDal.Get(id);
            player.Recommend = false;
            _playerDal.Update(player);
            return RedirectToAction("Index");
        }

        private string UploadImage(HttpPostedFile file)
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PlayerIcon");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string md5 = Md5Helper.MD5FromFile(file.InputStream);
            string fileName = Path.Combine(dir, md5 + ".jpg");

            if (!System.IO.File.Exists(fileName))
                System.IO.File.Create(fileName);

            file.SaveAs(fileName);

            return fileName;
        }
    }
}
