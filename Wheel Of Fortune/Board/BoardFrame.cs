using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel_Of_Fortune.Enums;

namespace Wheel_Of_Fortune.Board {
    class BoardFrame : List<Trilon>{

        public new void Add(Trilon trilon) {
            base.Add(trilon.DeepCopy());
        }
    }
}
