﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel_Of_Fortune.Prizes;

namespace Wheel_Of_Fortune {
    class Player {
        public string Name { get; set; }
        private int roundWinnigns;
        public int RoundWinnings {
            get { return roundWinnigns; }
            set {
                roundWinnigns = value;
                OnRoundWinningChanged(EventArgs.Empty);
            }
        }
        public int TotalWinnings { get; set; }
        public List<Prize> WonPrizes { get; set; }
        public int FreePlays { get; set; }
        public bool HasMillionWedge { get; set; }
        public bool IsStartingPlayer { get; set; }

        public Player(string name) {
            Name = name;
            RoundWinnings = 0;
            TotalWinnings = 0;
            WonPrizes = new List<Prize>();
            HasMillionWedge = false;
            FreePlays = 0;
            IsStartingPlayer = false;
        }

        public event EventHandler RoundWinningChanged;

        protected virtual void OnRoundWinningChanged(EventArgs e) {
            EventHandler handler = RoundWinningChanged;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}
