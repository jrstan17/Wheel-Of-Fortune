using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fortune {
    class Wedge {

        internal IList<Third> Thirds;

        public Wedge() {

        }

        public Wedge(List<Third> thirds) {
            Thirds = thirds;
        }

        public Wedge(Third third, int amount) {
            Thirds = new List<Third>();

            for (int i = 0; i < amount; i++) {
                Thirds.Add(third);
            }
        }

        public Wedge DeepCopy() {
            List<Third> thirds = new List<Third>();
            foreach(Third t in Thirds) {
                thirds.Add(t.DeepCopy());
            }

            return new Wedge(thirds);
        }

        public bool IsStandard() {
            return (Thirds[1].Equals(Thirds[0]) && Thirds[2].Equals(Thirds[0]));
        }
    }
}
