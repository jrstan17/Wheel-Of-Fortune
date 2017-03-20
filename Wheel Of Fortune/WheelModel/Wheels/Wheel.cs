using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using Wheel_Of_Fortune.Enums;

namespace Wheel_Of_Fortune.WheelModel.Wheels {
    abstract class Wheel : IDisposable{
        internal List<Space> Spaces;
        internal Third CurrentThird;
        internal int ThirdCount;

        public Wheel() {
            Initialize();
            NumberThirds();
            CurrentThird = GetThird(0);
            Spaces.Reverse();
        }

        public void MoveToNextThird() {
            int index = CurrentThird.Index;

            if (index == ThirdCount - 1) {
                index = 0;
            } else {
                index++;
            }

            CurrentThird = GetThird(index);                       
        }

        public Third GetThird(int index) {
            foreach (Space s in Spaces) {
                foreach (Third t in s.Wedges[0].Thirds) {
                    if (t.Index == index) {
                        return t;
                    }
                }
            }

            return null;
        }

        public bool MultipleWedgesInSpace(Third third) {
            Space space = SpaceWithThird(third);
            return (space.Wedges.Count > 1);
        }

        public Space SpaceWithThird(Third third) {
            foreach (Space s in Spaces) {
                if (s.Contains(third)) {
                    return s;
                }
            }

            return null;
        }

        public Wedge RemoveWedgeWith(Third third) {
            foreach (Space s in Spaces) {
                if (s.Contains(third)) {
                    return s.PickUpWedge();
                }
            }

            return null;
        }

        public List<Third> GetAllVisibleThirds() {
            List<Third> thirds = new List<Third>();

            foreach (Space s in Spaces) {
                foreach (Third t in s.Wedges[0].Thirds) {
                    thirds.Add(t);
                }
            }

            return thirds;
        }

        public override string ToString() {
            string toReturn = "";

            foreach (Space s in Spaces) {
                foreach (Wedge w in s.Wedges) {
                    foreach (Third t in w.Thirds) {
                        toReturn += t + "\n\n";
                    }
                }
            }

            return toReturn;
        }

        internal List<Wedge> GetAllVisibleWedges() {
            List<Wedge> wedges = new List<Wedge>();

            foreach (Space s in Spaces) {
                wedges.Add(s.Wedges[0]);
            }

            return wedges;
        }

        protected virtual void OnNextThird(WheelArgs e) {
            EventHandler<WheelArgs> handler = MovedToNextThird;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<WheelArgs> MovedToNextThird;

        public abstract void Initialize();

        private void NumberThirds() {
            int count = 0;

            foreach (Space s in Spaces) {
                for (int i = 0; i < s.Wedges.Count; i++) {
                    Wedge w = s.Wedges[i];

                    if (i != 0) {
                        count -= w.Thirds.Count;
                    }

                    foreach (Third t in w.Thirds) {
                        t.Index = count;
                        count++;
                    }
                }
            }

            ThirdCount = count;
        }

        public void Dispose() {
           foreach(Space s in Spaces) {
                foreach(Wedge w in s.Wedges) {
                    foreach(Third t in w.Thirds) {
                        t.Dispose();
                    }
                }
            }
        }
    }
}
