using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel {
    class Space {
        IList<Wedge> _Tile;
        IList<Wedge> Tiles { get { return _Tile; } set { value = _Tile; } }
    }
}
