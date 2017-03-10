using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using Wheel_Of_Fortune;

namespace Wheel {
    public class Third {

        private const int COLOR_CHANGE_TURNOVER_TIME = 3000;

        MainWindow Window;
        Timer Timer;
        double CurrentR;
        double CurrentG;
        double CurrentB;
        int Tick;

        Color TopColor { get; set; }
        Color BottomColor { get; set; }

        double rChangeToBottom;
        double gChangeToBottom;
        double bChangeToBottom;
        double rChangeToTop;
        double gChangeToTop;
        double bChangeToTop;

        public Third(MainWindow window, Color c1, Color c2) {
            Window = window;
            TopColor = c1;
            BottomColor = c2;
            Timer = new Timer();
            Timer.AutoReset = true;
            Timer.Interval = 100;
            Timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            SetChangePerTick();
            Tick = 0;
            Timer.Start();
        }

        public Third(Color color) {
            TopColor = color;
        }

        public void SetWindow(MainWindow window) {
            Window = window;
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

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            Tick++;

            Color CurrentColor = Color.FromRgb((byte)Math.Round(CurrentR), (byte)Math.Round(CurrentG), (byte)Math.Round(CurrentB));

            if (CurrentColor.Equals(BottomColor)) {
                CurrentR += rChangeToTop;
                CurrentG += gChangeToTop;
                CurrentB += bChangeToTop;
                Tick = 0;
            } else if (CurrentColor.Equals(TopColor)) {
                CurrentR += rChangeToBottom;
                CurrentG += gChangeToBottom;
                CurrentB += bChangeToBottom;
                Tick = 0;
            }

            Window.label.Background = new SolidColorBrush(CurrentColor);
        }

        #region Color Line Equation Methods
        private void SetChangePerTick() {
            Point p1 = new Point(0, TopColor.R);
            Point p2 = new Point(COLOR_CHANGE_TURNOVER_TIME, BottomColor.R);
            rChangeToBottom = m(p1, p2);

            p1 = new Point(0, TopColor.G);
            p2 = new Point(COLOR_CHANGE_TURNOVER_TIME, BottomColor.G);
            gChangeToBottom = m(p1, p2);

            p1 = new Point(0, TopColor.B);
            p2 = new Point(COLOR_CHANGE_TURNOVER_TIME, BottomColor.B);
            bChangeToBottom = m(p1, p2);

            p1 = new Point(0, BottomColor.R);
            p2 = new Point(COLOR_CHANGE_TURNOVER_TIME, TopColor.R);
            rChangeToTop = m(p1, p2);

            p1 = new Point(0, BottomColor.G);
            p2 = new Point(COLOR_CHANGE_TURNOVER_TIME, TopColor.G);
            gChangeToTop = m(p1, p2);

            p1 = new Point(0, BottomColor.B);
            p2 = new Point(COLOR_CHANGE_TURNOVER_TIME, TopColor.B);
            bChangeToTop = m(p1, p2);
        }

        private double m(Point p1, Point p2) {
            return (p2.Y - p1.Y) / (p2.X - p1.X);
        }
        #endregion

        public enum ThirdType {
            Regular,
            HighAmount,
            Bankrupt,
            LoseATurn,
            Prize,
            FreePlay
        }
    }
}
