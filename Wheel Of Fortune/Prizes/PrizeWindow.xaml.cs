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

        BoardWindow Board;

        internal PrizeWindow(BoardWindow board) {
            Board = board;
            InitializeComponent();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e) {
            this.Hide();
            Board.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            Board.CurrentPrize = Board.PrizeFactory.GetRandom();

            PrizeText.Text = Board.CurrentPrize.Text;
            PrizeValue.Text = Board.CurrentPrize.Value.ToString("$#,0");
        }
    }
}
