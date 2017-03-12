using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fortune.Countdown {
    abstract class TimerMechanic {

        public int Duration = 15000; // in millis
        public long Tick { get; set; }        

        public TimerMechanic() {
            Tick = 0;
            Reset();
        }

        public void SetDuration(int duration) {
            Duration = duration;
            Tick = duration;
        }

        public abstract void UpdateTick(int updateInterval);
        public abstract string GetText();

        public bool IsUp() {
            return (Tick == 0);
        }

        public void Reset() {
            Tick = Duration;
        }
    }
}
