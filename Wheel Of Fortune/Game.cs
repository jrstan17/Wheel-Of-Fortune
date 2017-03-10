using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel_Of_Fortune.Enums;

namespace Wheel_Of_Fortune {
    class Game {
        private PlayerChoice playerChoice;
        public PlayerChoice PlayerChoice {
            get { return playerChoice; }
            set {
                playerChoice = value;
                PlayerChoiceChange(EventArgs.Empty);
            }
        }

        protected virtual void PlayerChoiceChange(EventArgs e) {
            EventHandler handler = OnPlayerChoiceChange;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler OnPlayerChoiceChange;
    }
}
