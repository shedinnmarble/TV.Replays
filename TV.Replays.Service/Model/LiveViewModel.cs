﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TV.Replays.Service.Model
{
    public class LiveViewModel
    {
        public LiveViewModel()
        {
            Categories = new string[] { };
        }
        public string Id { get; set; }
        public string VideoImage { get; set; }
        public string Title { get; set; }
        public string PlayerName { get; set; }
        public int ViewSum { get; set; }
        public bool IsOnline { get; set; }
        public string[] Categories { get; set; }
    }
}
