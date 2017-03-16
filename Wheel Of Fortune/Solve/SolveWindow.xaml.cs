using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using Wheel_Of_Fortune.Board;
using Wheel_Of_Fortune.Countdown;
using Wheel_Of_Fortune.PuzzleModel;

namespace Wheel_Of_Fortune.Solve {

    public partial class SolveWindow : Window {

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public SolveWindow() {
            InitializeComponent();
        }

        BoardWindow Window;
        Puzzle Puzzle;
        TimerMechanic timerMech;
        DispatcherTimer Timer;

        public SolveWindow(BoardWindow window) {
            InitializeComponent();

            Timer = new DispatcherTimer(DispatcherPriority.Normal);
            Timer.Tick += Timer_Tick;
            Timer.Interval = TimeSpan.FromMilliseconds(100);
            Timer.IsEnabled = true;

            Window = window;
            Puzzle = Window.BoardUI.Board.CurrentPuzzle;

            timerMech = new LargeTimerMechanic(Puzzle.Text.Length * 1000);
            TimerTextBlock.Text = timerMech.GetText();
        }

        public void SubmitButton_Click(object sender, RoutedEventArgs e) {
            Timer.IsEnabled = false;

            string guess = SolveTextBox.Text.ToUpper();
            SolveResultEventArgs args = new SolveResultEventArgs();

            if (guess.Equals(Puzzle.Text)) {
                args.IsWin = true;              
                Window.BoardUI.RevealAll();
                SolveResult(this, args);
            } else {
                args.IsWin = false;
                Wrong("That is incorrect.");
                SolveResult(this, args);
            }
        }

        public void Wrong(string toolStripMessage) {
            SubmitButton.IsEnabled = false;
            TimerTextBlock.IsEnabled = false;
            SolveTextBox.IsEnabled = false;
            SajakBlock.Text = toolStripMessage;

            DispatcherTimer waitTimer = new DispatcherTimer(DispatcherPriority.Normal);
            waitTimer.Interval = TimeSpan.FromMilliseconds(2500);
            waitTimer.Tick += new EventHandler(WaitTimer_Tick);
            waitTimer.IsEnabled = true;
        }

        private void WaitTimer_Tick(object sender, EventArgs e) {
            string guess = SolveTextBox.Text.ToUpper();

            if (guess.Equals(Puzzle.Text)) {
                Window.SolveResult(true);
                Close();
            } else {
                Window.SolveResult(false);
            }

            DispatcherTimer t = (DispatcherTimer)sender;
            t.IsEnabled = false;
            Close();
        }



        private void Timer_Tick(object sender, EventArgs e) {
            timerMech.UpdateTick(Timer.Interval.Milliseconds);
            TimerTextBlock.Text = timerMech.GetText();

            if (timerMech.Tick <= 0) {
                Timer.IsEnabled = false;
                Wrong("Sorry, your time is up.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            SolveTextBox.Focus();            
        }

        private void SolveTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                SubmitButton_Click(null, null);
            }
        }

        public virtual void OnSolveResult(SolveResultEventArgs e) {
            EventHandler<SolveResultEventArgs> handler = SolveResult;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<SolveResultEventArgs> SolveResult;
    }
}
