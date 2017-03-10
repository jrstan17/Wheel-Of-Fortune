using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fortune.WheelModel {
    class WheelStoppedArgs : EventArgs{
        public Third CurrentThird { get; set; }
    }
}
