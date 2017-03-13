using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel_Of_Fortune.Enums;

namespace Wheel_Of_Fortune.WheelModel {
    public class WedgeClickedEventArgs : EventArgs{
        public ThirdType Type { get; set; }
    }
}
