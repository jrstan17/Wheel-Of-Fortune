using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Wheel_Of_Fortune.Board;
using Wheel_Of_Fortune.NewGame;

namespace Wheel_Of_Fortune.Prizes {
    /// <summary>
    /// Interaction logic for PrizeWindow.xaml
    /// </summary>
    public partial class PrizeWindow : Window {

        RandomizeWindow rndWindow;
        BoardWindow board;

        public PrizeWindow() {
            InitializeComponent();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e) {
            this.Hide();
            board.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            RandomizeWindow rndWindow = null;

            foreach (Window w in Application.Current.Windows) {
                if (w is RandomizeWindow) {
                    rndWindow = (RandomizeWindow)w;
                }
            }

            board = new BoardWindow(rndWindow.GetPlayerList());
            RandomizePrize();
        }

        private void RandomizePrize() {
            board.CurrentPrize = board.PrizeFactory.GetRandom();

            PrizeText.Text = board.CurrentPrize.Text;
            PrizeValue.Text = board.CurrentPrize.Value.ToString("$#,0");
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if ((bool)e.NewValue && board != null) {
                RandomizePrize();
            }
        }
    }
}
