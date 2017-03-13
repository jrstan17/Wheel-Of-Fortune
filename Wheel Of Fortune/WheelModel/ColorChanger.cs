using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Wheel_Of_Fortune.WheelModel {
    public class ColorChanger {

        private int colorChangeTime = 0;
        
        public object Dispatcher { get; private set; }

        Random rnd = new Random();

        double rChangeToBottom;
        double gChangeToBottom;
        double bChangeToBottom;
        double rChangeToTop;
        double gChangeToTop;
        double bChangeToTop;

        DispatcherTimer Timer;
        double CurrentR;
        double CurrentG;
        double CurrentB;
        bool onItsWayUp = true;

        internal Color TopColor { get; set; }
        internal Color BottomColor { get; set; }

        public ColorChanger(ColorSet set) {
            TopColor = set.TopColor;
            BottomColor = set.BottomColor;
            CurrentR = BottomColor.R;
            CurrentG = BottomColor.G;
            CurrentB = BottomColor.B;

            Timer = new DispatcherTimer();
            SetColorChangeTime();
            Timer.Tick += Timer_Elapsed;
            SetChangePerTick();
        }

        private void SetColorChangeTime() {
            colorChangeTime = rnd.Next(37, 44);
            Timer.Interval = TimeSpan.FromMilliseconds(colorChangeTime);
        }

        public Color GetColor() {
            return TopColor;
        }

        public void StartColorCycling() {
                Timer.Start();
        }

        public void StopColorCycling() {
            Timer.Stop();
        }

        private void Timer_Elapsed(object sender, EventArgs e) {
            Color CurrentColor = Color.FromRgb((byte)Math.Round(CurrentR), (byte)Math.Round(CurrentG), (byte)Math.Round(CurrentB));

            if (CurrentColor.Equals(BottomColor)) {
                onItsWayUp = true;
                SetColorChangeTime();
            } else if (CurrentColor.Equals(TopColor)) {
                onItsWayUp = false;
            }

            if (onItsWayUp) {
                CurrentR += rChangeToTop;
                CurrentG += gChangeToTop;
                CurrentB += bChangeToTop;
            } else {
                CurrentR += rChangeToBottom;
                CurrentG += gChangeToBottom;
                CurrentB += bChangeToBottom;
            }

            ColorArgs args = new ColorArgs();
            args.Color = CurrentColor;
            OnColorChanged(args);
        }

        protected virtual void OnColorChanged(ColorArgs e) {
            EventHandler<ColorArgs> handler = ColorChanged;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<ColorArgs> ColorChanged;

        #region Color Line Equation Methods
        private void SetChangePerTick() {
            Point p1 = new Point(0, TopColor.R);
            Point p2 = new Point(colorChangeTime, BottomColor.R);
            rChangeToBottom = m(p1, p2);

            p1 = new Point(0, TopColor.G);
            p2 = new Point(colorChangeTime, BottomColor.G);
            gChangeToBottom = m(p1, p2);

            p1 = new Point(0, TopColor.B);
            p2 = new Point(colorChangeTime, BottomColor.B);
            bChangeToBottom = m(p1, p2);

            p1 = new Point(0, BottomColor.R);
            p2 = new Point(colorChangeTime, TopColor.R);
            rChangeToTop = m(p1, p2);

            p1 = new Point(0, BottomColor.G);
            p2 = new Point(colorChangeTime, TopColor.G);
            gChangeToTop = m(p1, p2);

            p1 = new Point(0, BottomColor.B);
            p2 = new Point(colorChangeTime, TopColor.B);
            bChangeToTop = m(p1, p2);
        }

        private double m(Point p1, Point p2) {
            return (p2.Y - p1.Y) / (p2.X - p1.X);
        }
        #endregion
    }
}
