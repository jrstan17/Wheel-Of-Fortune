using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fortune {
    class Space {
        internal List<Wedge> Wedges;

        public Space() {
            Wedges = new List<Wedge>();
        }

        public Space(Wedge wedge) {
            Wedges = new List<Wedge>();
            Wedges.Add(wedge);
        }

        public Space(List<Wedge> wedges) {
            Wedges = wedges;
        }

        public void Add(Wedge wedge) {
            Wedges.Add(wedge);
        }

        public Wedge PickUpWedge() {
            Wedge w = null;

            if (Wedges.Count > 1) {
                w = Wedges[0].DeepCopy();
                Wedges.RemoveAt(0);
            }

            return w;
        }

        public bool Contains(Third third) {
            foreach (Wedge w in Wedges) {
                foreach(Third t in w.Thirds) {
                    if (third.Equals(t)) {
                        return true;
                    }
                }
            }

            return false;
        }

        public Space DeepCopy() {
            List<Wedge> wedges = new List<Wedge>();
            foreach (Wedge w in Wedges) {
                wedges.Add(w.DeepCopy());
            }

            return new Space(wedges);
        }
    }
}
