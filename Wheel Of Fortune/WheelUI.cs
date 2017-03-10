using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;
using Wheel_Of_Fortune.Enums;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Windows.Shapes;
using System.Windows.Input;

namespace Wheel_Of_Fortune {
    class WheelUI {
        Wheel Wheel;
        Third CurrentThird;
        Canvas WheelCanvas;
        Grid MainWheelGrid;
        Storyboard story;
        DispatcherTimer StopTimer;
        internal bool IsPaused = false;
        double Speed = 0;
        double Strength = 0;
        double Angle = 0;

        Random rnd;

        DispatcherTimer FrameTimer;
        bool alreadyCheckedFlag = false;

        MainWindow Window;

        public WheelUI(MainWindow Window) {
            Wheel = new Wheel();
            story = new Storyboard();
            FrameTimer = new DispatcherTimer();
            MainWheelGrid = new Grid();
            rnd = new Random();

            StopTimer = new DispatcherTimer();
            StopTimer.Interval = TimeSpan.FromMilliseconds(50);
            StopTimer.Tick += StopTimer_Tick;

            FrameTimer = new DispatcherTimer(DispatcherPriority.Render);
            FrameTimer.Interval = TimeSpan.FromMilliseconds(10);
            FrameTimer.Tick += FrameTimer_Tick;

            this.Window = Window;
            WheelCanvas = Window.WheelCanvas;

            //WheelCanvas.MouseWheel += delegate (object sender, MouseWheelEventArgs e) {
            //    double angle = ((RotateTransform)MainWheelGrid.RenderTransform).Angle;
            //    MainWheelGrid.RenderTransform = new RotateTransform(angle - e.Delta * 0.025);
            //    MainWheelGrid.RenderTransformOrigin = new Point(0.5, 0.5);
            //    FrameTimer_Tick(null, null);
            //};

            AddToCanvas();
            SetupMainWheelGrid();
            SetupArrow();

            story.Begin();
            story.Pause();

            FrameTimer.IsEnabled = true;
        }

        private void WheelCanvas_MouseWheel(object sender, MouseWheelEventArgs e) {
            throw new NotImplementedException();
        }

        private void FrameTimer_Tick(object sender, EventArgs e) {
            Angle = ((RotateTransform)MainWheelGrid.RenderTransform).Angle;

            Window.textBlock.Text = Angle.ToString();

            if ((int)Angle % 5 == 0) {
                if (!alreadyCheckedFlag) {
                    if (Angle >= 360) {
                        Angle -= 360;
                    }
                    int index = (int)Angle / 5;
                    CurrentThird = Wheel.GetThird(index);
                    Window.CurrentText.Text = CurrentThird.Text;
                    alreadyCheckedFlag = true;
                }
            } else {
                alreadyCheckedFlag = false;
            }

        }

        private void StopTimer_Tick(object sender, EventArgs e) {

            Speed -= Strength;

            if (Speed > 0.01) {
                story.SetSpeedRatio(Speed);
            } else {
                Speed = 0;
                SetupArrow();
                story.Pause();
                StopTimer.IsEnabled = false;
            }
        }

        public void SetupMainWheelGrid() {
            for (int i = 0; i < WheelCanvas.Children.Count; i++) {
                if (WheelCanvas.Children[i] is Grid) {
                    Grid grid = (Grid)WheelCanvas.Children[i];
                    MainWheelGrid.Children.Add(grid);
                }
            }

            RotateTransform rt = new RotateTransform();
            rt.Angle = Angle;
            MainWheelGrid.RenderTransform = rt;
            MainWheelGrid.RenderTransformOrigin = new Point(0.5, 0.5);

            RotateTransform rotate = (RotateTransform)MainWheelGrid.RenderTransform;
            DoubleAnimation da = new DoubleAnimation(rotate.Angle, rotate.Angle + 360, new Duration(TimeSpan.FromSeconds(60)));
            da.RepeatBehavior = RepeatBehavior.Forever;
            story.Children.Add(da);
            Storyboard.SetTarget(da, MainWheelGrid);
            Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.Angle"));
            SetupMiddlePartOfWheel();
            WheelCanvas.Children.Add(MainWheelGrid);
        }

        public void StartAnimation() {
            Speed = 30;
            Strength = 0;

            while (Strength < 0.1 || Strength > 0.3) {
                Strength = rnd.NextDouble();
            }

            FrameTimer.IsEnabled = true;
            story.SetSpeedRatio(30);

            story.Resume();

            IsPaused = false;
        }

        public void PauseAnimation() {
            StopTimer.IsEnabled = true;
            IsPaused = true;
        }

        public void AddToCanvas() {
            Random rnd = new Random();
            List<Third> Thirds = Wheel.GetAllVisibleThirds();
            List<Wedge> Wedges = Wheel.GetAllVisibleWedges();

            double startAngle = 15;
            double size = 0;
            int currentWedge = 0;
            for (int i = 0; i < Wheel.ThirdCount; i++) {
                Grid Grid = new Grid();
                RotateTransform rt = new RotateTransform();
                rt.Angle = startAngle;
                Grid.RenderTransform = rt;
                Grid.RenderTransformOrigin = new Point(0.5, 0.5);

                ThirdUI thirdUI = new ThirdUI(Thirds[i]);
                thirdUI.Height = 750;
                thirdUI.Width = 750;
                thirdUI.Margin = new Thickness(0);
                thirdUI.StartAngle = 0;
                thirdUI.EndAngle = 15;

                if (Wheel.MultipleWedgesInSpace(thirdUI.Third)) {
                    thirdUI.MouseLeftButtonUp += delegate (object sender, MouseButtonEventArgs e) {
                        Wheel.RemoveWedgeWith(thirdUI.Third);

                        Clear();
                        AddToCanvas();
                        SetupMainWheelGrid();
                        SetupArrow();

                        story.Begin();
                        story.Pause();
                    };
                }

                Grid.Children.Add(thirdUI);

                if (Wedges[currentWedge].IsStandard()) {
                    i += 2;
                    startAngle += (360.0 / Wheel.ThirdCount) * 3;
                    size = (360.0 / Wheel.ThirdCount) * 3;
                } else {
                    startAngle += (360.0 / Wheel.ThirdCount);
                    size = 360.0 / Wheel.ThirdCount;
                }

                SetupTextBlock(Grid, size);
                MainWheelGrid.Children.Add(Grid);

                if (i % 3 == 2) {
                    currentWedge++;
                }
            }
        }

        private void SetupArrow() {
            Image arrow = SetNewArrowImage("arrow_enabled.png");

            arrow.MouseLeftButtonUp += delegate (object sender, MouseButtonEventArgs e) {
                StartAnimation();
                PauseAnimation();

                WheelCanvas.Children.Remove(arrow);
                SetNewArrowImage("arrow_disabled.png");
            };
        }

        public Image SetNewArrowImage(string fileName) {
            BitmapImage arrow = new BitmapImage(new Uri(fileName, UriKind.Relative));
            Image arrowImage = new Image();
            arrowImage.Source = arrow;
            Canvas.SetLeft(arrowImage, 730);
            Canvas.SetTop(arrowImage, 310);
            Canvas.SetZIndex(arrowImage, 99);
            WheelCanvas.Children.Add(arrowImage);
            return arrowImage;
        }

        private void SetupMiddlePartOfWheel() {
            Ellipse e = new Ellipse();
            e.Height = 300;
            e.Width = 300;
            Brush b = new SolidColorBrush(Colors.Teal);
            e.Fill = b;
            Canvas.SetLeft(e, 0);
            Canvas.SetTop(e, 0);
            Canvas.SetZIndex(e, 99);
            MainWheelGrid.Children.Add(e);
        }

        public void Clear() {
            WheelCanvas.Children.Clear();
            MainWheelGrid.Children.Clear();
        }

        private void SetupTextBlock(Grid grid, double size) {
            TextBlock TextBlock = new TextBlock();
            ThirdUI ThirdUI = (ThirdUI)grid.Children[0];
            Third Third = ThirdUI.Third;
            RotateTransform textRT = new RotateTransform();

            //if it's a full wedge
            if (size == (360 / Wheel.ThirdCount) * 3) {
                if (Third.Type == ThirdType.Bankrupt || Third.Type == ThirdType.FreePlay) {
                    TextBlock.FontSize = 30;
                    TextBlock.Margin = new Thickness(0, 375 - 60, 0, 0);
                    textRT.Angle = -7.5;
                } else if (Third.Type == ThirdType.LoseATurn) {
                    TextBlock.FontSize = 24;
                    TextBlock.Margin = new Thickness(0, 375 - 56, 0, 0);
                    textRT.Angle = -7.5;
                } else {
                    TextBlock.FontSize = 36;
                    TextBlock.Margin = new Thickness(0, 375 - 65, 0, 0);
                    textRT.Angle = -7.5;
                }
                //if it's a 1/3 wedge
            } else {
                if (Third.Type == ThirdType.Bankrupt) {
                    TextBlock.FontSize = 15;
                    TextBlock.Margin = new Thickness(0, 375 - 82, -14, 0);
                    textRT.Angle = -12;
                } else {
                    TextBlock.FontSize = 15;
                    TextBlock.Margin = new Thickness(0, 375 - 82, -16, 0);
                    textRT.Angle = -12;
                }
            }

            TextBlock.IsHitTestVisible = false;
            TextBlock.FontWeight = FontWeights.ExtraBold;
            TextBlock.HorizontalAlignment = HorizontalAlignment.Right;
            TextBlock.Text = Third.Text;

            Third.TextColorChanger.ColorChanged += delegate (object sender, ColorArgs e) { TextBlock.Foreground = new SolidColorBrush(e.Color); };

            TextBlock.RenderTransform = textRT;
            TextBlock.RenderTransformOrigin = new Point(0.5, 0.5);

            grid.Children.Add(TextBlock);
        }
    }
}