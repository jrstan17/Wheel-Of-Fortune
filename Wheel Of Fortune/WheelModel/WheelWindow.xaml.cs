using System;
using System.Windows;
using Wheel_Of_Fortune.WheelModel;

namespace Wheel_Of_Fortune {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        internal WheelUI WheelUI;

        public MainWindow() {
            InitializeComponent();
            CurrentText.IsHitTestVisible = false;
            WheelUI = new WheelUI(this);  
        }

        private void Window_ContentRendered(object sender, EventArgs e) {
            WheelUI.ToggleArrow(true);
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            WheelUI.ToggleArrow(true);
            SajakText.FontSize = 26;
            SajakText.Text = "\"Click the red arrow to spin the wheel!\"";
        }

        private void RandomizeButton_Click(object sender, RoutedEventArgs e) {
            WheelUI.RandomSeek();
        }

        internal void Dispose() {
            WheelUI.Dispose();
        }
    }
}
