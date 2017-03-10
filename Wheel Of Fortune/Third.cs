using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Wheel_Of_Fortune;
using Wheel_Of_Fortune.Enums;

namespace Wheel_Of_Fortune {
    public class Third {

        internal int Index = 1;
        internal ColorChanger TextColorChanger;
        internal ColorChanger BackColorChanger;
        internal string Text { get; set; }
        internal int Value { get; set; }
        internal ThirdType Type { get; set; }

        private void InitColorChangers(ColorSet text, ColorSet back) {
            TextColorChanger = new ColorChanger(text);
            BackColorChanger = new ColorChanger(back);

                TextColorChanger.StartColorCycling();
                BackColorChanger.StartColorCycling();
        }

        public override string ToString() {
            return "Number: " + Index + "\nTopColor: " + TextColorChanger.TopColor + "\nBottomColor: " + TextColorChanger.BottomColor + "\nText: " + Text + "\nValue: " + Value + "\nType: " + Type;
        }

        public Third DeepCopy() {
            Third toReturn = new Third();
            ColorSet text = new ColorSet(TextColorChanger.TopColor, TextColorChanger.BottomColor);
            ColorSet back = new ColorSet(BackColorChanger.TopColor, BackColorChanger.BottomColor);
            toReturn.SetColor(text, back);
            toReturn.Text = this.Text;
            toReturn.Value = this.Value;
            toReturn.Type = this.Type;
            toReturn.Index = this.Index;

            return toReturn;
        }

        public void SetColor(Color text, Color back) {
            InitColorChangers(new ColorSet(text, text), new ColorSet(back, back));
        }

        public void SetColor(ColorSet text, ColorSet back) {
            InitColorChangers(text, back);
        }

        internal ColorChanger GetTextColorChanger() {
            return TextColorChanger;
        }

        internal ColorChanger GetBackColorChanger() {
            return BackColorChanger;
        }

        public override bool Equals(object obj) {
            if (!(obj is Third)) {
                return false;
            }

            Third comingIn = (Third)obj;

            return (this.TextColorChanger.TopColor == comingIn.TextColorChanger.TopColor && this.BackColorChanger.TopColor == comingIn.BackColorChanger.TopColor && this.Text.Equals(comingIn.Text) && this.Value == comingIn.Value && this.Type == comingIn.Type);
        }
    }
}
