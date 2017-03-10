using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Wheel_Of_Fortune {
    public class ColorSet {
        internal Color TopColor;
        internal Color BottomColor;

        public ColorSet(Color one, Color two) {
            TopColor = one;
            BottomColor = two;
        }

        public ColorSet DeepCopy() {
            ColorSet toReturn = new ColorSet(TopColor, BottomColor);
            return toReturn;
        }
    }
}
