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
using System.Threading;
using Wheel_Of_Fortune.Board;

namespace Wheel_Of_Fortune.WheelModel {
    class WheelUI {
        Wheel Wheel;
        Third CurrentThird;
        Canvas WheelCanvas;
        Grid MainWheelGrid;
        Storyboard story;
        DispatcherTimer StopTimer;
        internal bool IsPaused = false;
        double Speed = 0;
        double Angle = 0;

        Random rnd;
        DispatcherTimer FrameTimer;
        DispatcherTimer WaitTimer;
        MainWindow Window;

        Image arrow;

        public WheelUI(MainWindow Window) {
            Wheel = new Wheel();
            story = new Storyboard();
            FrameTimer = new DispatcherTimer();
            WaitTimer = new DispatcherTimer();
            MainWheelGrid = new Grid();
            rnd = new Random();

            StopTimer = new DispatcherTimer();
            StopTimer.Interval = TimeSpan.FromMilliseconds(50);
            StopTimer.Tick += StopTimer_Tick;

            FrameTimer = new DispatcherTimer(DispatcherPriority.Render);
            FrameTimer.Interval = TimeSpan.FromMilliseconds(10);
            FrameTimer.Tick += FrameTimer_Tick;

            WaitTimer = new DispatcherTimer(DispatcherPriority.Render);
            WaitTimer.Interval = TimeSpan.FromMilliseconds(5000);
            WaitTimer.Tick += WaitTimer_Tick;

            this.Window = Window;
            WheelCanvas = Window.WheelCanvas;

#if DEBUG
            WheelCanvas.MouseWheel += delegate (object sender, MouseWheelEventArgs e) {
                TimeSpan current = story.GetCurrentTime();

                long ticks = current.Ticks + TimeSpan.FromMilliseconds(e.Delta * 5).Ticks;

                if (ticks >= 0) {
                    story.Seek(TimeSpan.FromTicks(ticks));
                    FrameTimer_Tick(null, null);
                }
            };
#endif

            AddToCanvas();
            SetupMainWheelGrid();
            ToggleArrow(true);

            story.Begin();
            story.Pause();

            FrameTimer.IsEnabled = true;

            RandomSeek();
        }

        private void WaitTimer_Tick(object sender, EventArgs e) {
            WheelStoppedArgs args = new WheelStoppedArgs();
            args.CurrentThird = CurrentThird;
            OnWheelStopped(args);

            WaitTimer.IsEnabled = false;

            Window.Hide();
        }

        public void RandomSeek() {
            int randomSeek = rnd.Next(0, 60000);
            story.Seek(TimeSpan.FromMilliseconds(randomSeek));
            FrameTimer_Tick(null, null);

            WheelStoppedArgs args = new WheelStoppedArgs();
            args.CurrentThird = CurrentThird;
            OnWheelStopped(args);
        }

        private void FrameTimer_Tick(object sender, EventArgs e) {
            Angle = ((RotateTransform)MainWheelGrid.RenderTransform).Angle;

            if (Angle >= 360) {
                Angle -= 360;
            }

            int index = (int)Angle / 5;
            CurrentThird = Wheel.GetThird(index);

            Window.CurrentText.Text = CurrentThird.Text;
        }

        private void StopTimer_Tick(object sender, EventArgs e) {

            Speed -= 0.3;

            if (Speed > 0.01) {
                story.SetSpeedRatio(Speed);
            } else {
                Speed = 0;
                story.Pause();
                StopTimer.IsEnabled = false;

                if (!(CurrentThird.Type == ThirdType.Prize)) {
                    WaitTimer.IsEnabled = true;
                    if (CurrentThird.Type == ThirdType.Bankrupt) {
                        Window.SajakText.FontSize = 26;
                        Window.SajakText.Text = "\"Ouch! You've landed on Bankrupt. I'm sorry!\"";
                    } else if (CurrentThird.Type == ThirdType.FreePlay) {
                        Window.SajakText.FontSize = 24;
                        Window.SajakText.Text = "\"You got a Free Play! The current value is $500. Please choose a consonant.\"";
                    } else if (CurrentThird.Type == ThirdType.HighAmount) {
                        Window.SajakText.FontSize = 36;
                        Window.SajakText.Text = "\"" + CurrentThird.Text + "! Now choose wisely!\"";
                    } else if (CurrentThird.Type == ThirdType.LoseATurn) {
                        Window.SajakText.FontSize = 26;
                        Window.SajakText.Text = "\"So sorry, " + BoardWindow.CurrentPlayer.Name + ". You've lost your turn.\"";
                    } else if (CurrentThird.Type == ThirdType.Million) {
                        Window.SajakText.FontSize = 18;
                        Window.SajakText.Text = "\"A Million Dollars!\"";
                    } else if (CurrentThird.Type == ThirdType.Regular) {
                        Window.SajakText.FontSize = 38;
                        Window.SajakText.Text = "\"" + CurrentThird.Value + ".\"";
                    }
                } else {
                    Window.SajakText.FontSize = 26;
                    Window.SajakText.Text = "\"You've landed on the Prize! Click the Prize wedge to pick it up!\"";
                }
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
            Speed = rnd.Next(25, 35);

            FrameTimer.IsEnabled = true;
            story.SetSpeedRatio(Speed);

            story.Resume();
            Window.SajakText.Text = "\"Good Luck!\"";
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
                        if (CurrentThird.Equals(thirdUI.Third) && IsPaused) {
                            WedgeClickedEventArgs args = new WedgeClickedEventArgs();
                            args.Type = CurrentThird.Type;
                            WedgeClicked(this, args);

                            Wheel.RemoveWedgeWith(thirdUI.Third);

                            Clear();
                            AddToCanvas();
                            SetupMainWheelGrid();
                            ToggleArrow(false);

                            story.Begin();
                            story.Pause();

                            Window.SajakText.Text = "\"The current value is $500. Please choose a consonant.\"";

                            WaitTimer.IsEnabled = true;
                        }
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

        public void ToggleArrow(bool enabled) {
            if (enabled) {
                if (arrow != null) {
                    WheelCanvas.Children.Remove(arrow);
                }

                arrow = SetNewArrowImage("arrow_enabled.png");

                arrow.MouseLeftButtonUp += delegate (object sender, MouseButtonEventArgs e) {
                    StartAnimation();
                    PauseAnimation();

                    ToggleArrow(false);
                };
            } else {
                if (arrow != null) {
                    WheelCanvas.Children.Remove(arrow);
                }

                arrow = SetNewArrowImage("arrow_disabled.png");
            }
        }

        public Image SetNewArrowImage(string fileName) {
            BitmapImage arrow = new BitmapImage(new Uri(fileName, UriKind.Relative));
            Image arrowImage = new Image();
            arrowImage.Source = arrow;
            Canvas.SetLeft(arrowImage, 730);
            Canvas.SetTop(arrowImage, 310);
            Canvas.SetZIndex(arrowImage, 1000);
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

        protected virtual void OnWheelStopped(WheelStoppedArgs e) {
            EventHandler<WheelStoppedArgs> handler = WheelStopped;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<WheelStoppedArgs> WheelStopped;

        protected virtual void OnWedgeClicked(WedgeClickedEventArgs e) {
            EventHandler<WedgeClickedEventArgs> handler = WedgeClicked;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<WedgeClickedEventArgs> WedgeClicked;
    }
}