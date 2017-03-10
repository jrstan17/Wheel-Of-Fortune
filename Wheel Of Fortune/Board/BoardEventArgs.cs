using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fortune.Board {
    class BoardEventArgs : EventArgs{
        public int TrilonsChanged { get; set; }
        public char LetterRevealed { get; set; }

        public bool AreVowelsRevealed { get; set; }

        public BoardEventArgs() {
            AreVowelsRevealed = false;
        }
    }
}
