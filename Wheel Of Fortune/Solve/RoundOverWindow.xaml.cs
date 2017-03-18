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
using Wheel_Of_Fortune.Prizes;

namespace Wheel_Of_Fortune.Solve {
    /// <summary>
    /// Interaction logic for RoundOverWindow.xaml
    /// </summary>
    public partial class RoundOverWindow : Window {

        BoardWindow Board;

        public RoundOverWindow(BoardWindow boardWindow) {
            Board = boardWindow;
            InitializeComponent();

            foreach(Player p in Board.Players) {
                p.WonRoundPrizes = new List<Prize>();
            }

            NameText.Text = BoardWindow.CurrentPlayer.Name;
            CurrentRoundText.Text = "You've Won Round " + BoardWindow.CurrentRound + "!";
            RoundWinningsText.Text = BoardWindow.CurrentPlayer.RoundWinnings.ToString("C0");
            NextRoundText.Text = "Onto Round " + (BoardWindow.CurrentRound + 1) + "!";
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e) {
            this.Hide();
            Board.NewGame();
            this.Close();
        }
    }
}
