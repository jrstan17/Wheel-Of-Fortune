using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fortune.Enums {
    public class WheelArgs : EventArgs {
        internal Third CurrentThird { get; set; }
    }
}
