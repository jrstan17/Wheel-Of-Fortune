using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel {
    class Wheel {
        IList<Space> _Spaces;
        IList<Space> Spaces { get { return _Spaces; } set { value = _Spaces; } }
    }
}
