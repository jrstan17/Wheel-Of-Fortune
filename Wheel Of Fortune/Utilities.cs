using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fortune {
    class Utilities {

        public static Random rnd = new Random();

        public static bool IsVowel(char c) {
            return (c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U');
        }
    }
}
