﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TV.Replays.Service.Model
{
    public class PlayerViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool IsOnline { get; set; }
    }
}
