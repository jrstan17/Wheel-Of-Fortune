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

namespace Wheel_Of_Fortune.Solve {
    /// <summary>
    /// Interaction logic for RoundOverWindow.xaml
    /// </summary>
    public partial class RoundOverWindow : Window {

        BoardWindow Board;

        public RoundOverWindow(BoardWindow boardWindow) {
            Board = boardWindow;
            InitializeComponent();

            int currentRound = Board.CurrentRound;

            NameText.Text = Board.BoardUI.CurrentPlayer.Name;
            CurrentRoundText.Text = "You've Won Round " + currentRound + "!";
            RoundWinningsText.Text = Board.BoardUI.CurrentPlayer.RoundWinnings.ToString("C0");
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e) {

        }
    }
}
