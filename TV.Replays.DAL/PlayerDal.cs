using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TV.Replays.IDAL;
using TV.Replays.Model;

namespace TV.Replays.DAL
{
    public class PlayerDal : BaseDal<Player>, IPlayerDal
    {
        public override void SetCollectionName()
        {
            this.CollectionName = "Players";
        }
    }
}
