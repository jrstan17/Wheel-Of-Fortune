using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Wheel_Of_Fortune.Enums;
using Wheel_Of_Fortune.WheelModel;

namespace Wheel_Of_Fortune {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        internal WheelUI WheelUI;

        public MainWindow() {
            InitializeComponent();

#if DEBUG
            RandomizeButton.Visibility = Visibility.Visible;
#endif

            CurrentText.IsHitTestVisible = false;
            WheelUI = new WheelUI(this);  
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            WheelUI.RandomSeek();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) {
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            
        }

        private void Window_ContentRendered(object sender, EventArgs e) {
            WheelUI.ToggleArrow(true);
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            WheelUI.ToggleArrow(true);
            SajakText.FontSize = 26;
            SajakText.Text = "\"Click the red arrow to spin the wheel!\"";
        }
    }
}
