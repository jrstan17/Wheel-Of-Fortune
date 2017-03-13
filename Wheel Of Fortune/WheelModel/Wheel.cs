using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using Wheel_Of_Fortune.Enums;

namespace Wheel_Of_Fortune.WheelModel {
    class Wheel {
        List<Space> Spaces;
        internal Third CurrentThird;
        internal int ThirdCount;

        public Wheel() {
            Initialize();
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

        #region Initialization
        private void Initialize() {
            Spaces = new List<Space>();
            Space space = new Space();
            Wedge wedge = new Wedge();
            Third third = new Third();

            ColorSet colorSetText = new ColorSet(Colors.Black, Colors.White);
            ColorSet colorSetBack = new ColorSet(Colors.LightGray, Colors.DarkGray);

            third.Text = "$2500";
            third.Value = 2500;
            third.Type = ThirdType.HighAmount;
            third.SetColor(colorSetText, colorSetBack);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$500";
            third.Value = 500;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.TEAL);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$900";
            third.Value = 900;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.YELLOW);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$700";
            third.Value = 700;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.RED);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$600";
            third.Value = 600;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.BLUE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$800";
            third.Value = 800;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.ORANGE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$500";
            third.Value = 500;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.PURPLE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$700";
            third.Value = 700;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.YELLOW);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            space = new Space();
            List<Third> list = new List<Third>();
            third.Text = "BANKRUPT";
            third.Value = 0;
            third.Type = ThirdType.Bankrupt;
            third.SetColor(WheelColors.WHITE, WheelColors.BLACK);
            list.Add(third.DeepCopy());
            colorSetText.TopColor = WheelColors.BLACK;
            colorSetText.BottomColor = Colors.LimeGreen;
            colorSetBack.TopColor = Colors.LimeGreen;
            colorSetBack.BottomColor = Colors.Black;
            third.Text = "ONE MILLION";
            third.Value = 500;
            third.Type = ThirdType.Million;
            third.SetColor(colorSetText.DeepCopy(), colorSetBack.DeepCopy());
            list.Add(third.DeepCopy());
            third.Text = "BANKRUPT";
            third.Value = 0;
            third.Type = ThirdType.Bankrupt;
            third.SetColor(WheelColors.WHITE, WheelColors.BLACK);
            list.Add(third.DeepCopy());
            wedge = new Wedge(list);
            space.Add(wedge.DeepCopy());            
            third.Text = "$500";
            third.Value = 500;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.PINK);
            wedge = new Wedge(third, 3);
            space.Add(wedge.DeepCopy());
            Spaces.Add(space);

            third.Text = "$600";
            third.Value = 600;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.RED);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$550";
            third.Value = 550;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.BLUE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$500";
            third.Value = 500;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.TEAL);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$900";
            third.Value = 900;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.PINK);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "BANKRUPT";
            third.Value = 0;
            third.Type = ThirdType.Bankrupt;
            third.SetColor(WheelColors.WHITE, WheelColors.BLACK);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$650";
            third.Value = 650;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.PURPLE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "FREE PLAY";
            third.Value = 500;
            third.Type = ThirdType.FreePlay;
            colorSetText.TopColor = Colors.Yellow;
            colorSetText.BottomColor = Colors.DarkBlue;
            colorSetBack.TopColor = Colors.LightBlue;
            colorSetBack.BottomColor = Colors.Yellow;
            third.SetColor(colorSetText.DeepCopy(), colorSetBack.DeepCopy());
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$700";
            third.Value = 700;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.BLUE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "LOSE A TURN";
            third.Value = 0;
            third.Type = ThirdType.LoseATurn;
            third.SetColor(WheelColors.BLACK, WheelColors.WHITE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$800";
            third.Value = 800;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.RED);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            Space s = new Space();
            third.Text = "PRIZE";
            third.Value = 500;
            third.Type = ThirdType.Prize;
            colorSetText.TopColor = WheelColors.BLACK;
            colorSetText.BottomColor = Colors.White;
            colorSetBack.TopColor = Colors.LightBlue;
            colorSetBack.BottomColor = Colors.DarkSlateBlue;
            third.SetColor(colorSetText.DeepCopy(), colorSetBack.DeepCopy());
            wedge = new Wedge(third, 3);
            s.Add(wedge.DeepCopy());
            third.Text = "$500";
            third.Value = 500;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.YELLOW);
            wedge = new Wedge(third, 3);
            s.Add(wedge.DeepCopy());
            Spaces.Add(s.DeepCopy());

            third.Text = "$650";
            third.Value = 650;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.PINK);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$500";
            third.Value = 500;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.TEAL);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "$900";
            third.Value = 900;
            third.Type = ThirdType.Regular;
            third.SetColor(WheelColors.BLACK, WheelColors.ORANGE);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            third.Text = "BANKRUPT";
            third.Value = 0;
            third.Type = ThirdType.Bankrupt;
            third.SetColor(WheelColors.WHITE, WheelColors.BLACK);
            wedge = new Wedge(third, 3);
            Spaces.Add(new Space(wedge.DeepCopy()));

            NumberThirds();

        }

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
        #endregion
    }
}
