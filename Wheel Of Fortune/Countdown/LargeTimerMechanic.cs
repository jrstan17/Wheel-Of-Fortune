using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fortune.Countdown {
    class LargeTimerMechanic : TimerMechanic {

        public LargeTimerMechanic(int duration) {
            SetDuration(duration);
        }

        public override void UpdateTick(int updateInterval) {
            if (Tick > 0) {
                Tick -= updateInterval;
            } else {
                Tick = 0;
            }
        }

        public override string GetText() {
            int deciseconds = (int)(Tick / 100);

            StringBuilder sb = new StringBuilder();
            int minutes = 0;

            if (deciseconds >= 600) {
                minutes = deciseconds / 600;
                deciseconds %= 600;
            }

            if (minutes < 100) {
                sb.Append("0" + minutes + ":");
            } else {
                sb.Append(minutes + ":");
            }

            int seconds = 0;
            if (deciseconds >= 10) {
                seconds = deciseconds / 10;
                deciseconds %= 10;
            }

            if (seconds < 10) {
                sb.Append("0" + seconds + ".");
            } else {
                sb.Append(seconds + ".");
            }

            sb.Append(deciseconds);

            return sb.ToString();
        }
    }
}
