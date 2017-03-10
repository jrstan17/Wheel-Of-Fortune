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
using Wheel_Of_Fortune.Enums;
using Wheel_Of_Fortune.WheelModel;

namespace Wheel_Of_Fortune.Board {
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window {

        internal Game Game;
        BoardUI BoardUI;
        Third CurrentThird;
        MainWindow wheelWindow;

        const int PLAYER_BOARD_WIDTHS = 150;
        const int PLAYER_BOARD_HEIGHTS = 38;
        const int PLAYER_BOARD_SPACING = 5;
        const int PLAYER_BOARD_START_Y = 480;

        internal List<Player> Players;
        internal Player CurrentPlayer;
        int currentPlayerIndex = -1;

        internal Scoreboard Scoreboard;

        internal BoardWindow(List<Player> players) {
            InitializeComponent();

            Players = players;
            InitPlayerTextBlocks();
            GoToNextPlayer();

            wheelWindow = new MainWindow();
            BoardUI = new BoardUI(this);
            Game = new Game();
            Game.OnPlayerChoiceChange += Game_OnPlayerChoiceChange;
            Game.PlayerChoice = PlayerChoice.SpinOnly;

            wheelWindow.WheelUI.WheelStopped += WheelUI_WheelStopped;
        }

        private void InitPlayerTextBlocks() {
            Scoreboard = new Scoreboard();

            double scoreboardWidth = Players.Count * PLAYER_BOARD_WIDTHS + (Players.Count - 1) * PLAYER_BOARD_SPACING;

            double x = (MainGrid.Width - scoreboardWidth) / 2;

            for (int i = 0; i < Players.Count; i++) {
                double y = PLAYER_BOARD_START_Y;

                TextBlock nameBlock = new TextBlock();
                nameBlock.Text = Players[i].Name;
                nameBlock.FontSize = 26;
                nameBlock.Background = new SolidColorBrush(Colors.LightBlue);
                nameBlock.HorizontalAlignment = HorizontalAlignment.Left;
                nameBlock.VerticalAlignment = VerticalAlignment.Top;
                nameBlock.TextAlignment = TextAlignment.Center;
                nameBlock.Width = 150;
                nameBlock.Height = 38;
                nameBlock.Margin = new Thickness(x, y, 0, 0);

                y += nameBlock.Height;

                TextBlock winningsBlock = new TextBlock();
                winningsBlock.Text = "$" + Players[i].RoundWinnings.ToString();
                winningsBlock.FontSize = 26;
                winningsBlock.Background = new SolidColorBrush(Colors.LightBlue);
                winningsBlock.HorizontalAlignment = HorizontalAlignment.Left;
                winningsBlock.VerticalAlignment = VerticalAlignment.Top;
                winningsBlock.TextAlignment = TextAlignment.Center;
                winningsBlock.Width = 150;
                winningsBlock.Height = 38;
                winningsBlock.Margin = new Thickness(x, y, 0, 0);

                x += PLAYER_BOARD_SPACING + PLAYER_BOARD_WIDTHS;

                Scoreboard.Add(Players[i], nameBlock, winningsBlock);

                MainGrid.Children.Add(nameBlock);
                MainGrid.Children.Add(winningsBlock);                
            }
        }

        private void Game_OnPlayerChoiceChange(object sender, EventArgs e) {
            if (Game.PlayerChoice == PlayerChoice.SolveOnly) {
                SpinButton.IsEnabled = false;
                BuyButton.IsEnabled = false;
                BuyButton.Foreground = SystemColors.ControlDarkBrush;
                SolveButton.IsEnabled = true;
                SolveButton.Foreground = SystemColors.ControlTextBrush;
            } else if (Game.PlayerChoice == PlayerChoice.SpinAndBuy) {
                SpinButton.IsEnabled = true;
                BuyButton.IsEnabled = true;
                BuyButton.Foreground = SystemColors.ControlTextBrush;
                SolveButton.IsEnabled = true;
                SolveButton.Foreground = SystemColors.ControlTextBrush;
            } else if (Game.PlayerChoice == PlayerChoice.SpinOnly) {
                SpinButton.IsEnabled = true;
                BuyButton.IsEnabled = false;
                BuyButton.Foreground = SystemColors.ControlDarkBrush;
                SolveButton.IsEnabled = true;
                SolveButton.Foreground = SystemColors.ControlTextBrush;
            } else if (Game.PlayerChoice == PlayerChoice.VowelOnly) {
                SpinButton.IsEnabled = false;
                BuyButton.IsEnabled = true;
                BuyButton.Foreground = SystemColors.ControlTextBrush;
                SolveButton.IsEnabled = true;
                SolveButton.Foreground = SystemColors.ControlTextBrush;
            } else if (Game.PlayerChoice == PlayerChoice.Disabled) {
                SpinButton.IsEnabled = false;
                BuyButton.IsEnabled = false;
                BuyButton.Foreground = SystemColors.ControlDarkBrush;
                SolveButton.IsEnabled = false;
                SolveButton.Foreground = SystemColors.ControlDarkBrush;
            }
        }

        public void UpdateRoundWinnings(int trilonCount) {
            CurrentPlayer.RoundWinnings += trilonCount * CurrentThird.Value;
        }

        private void WheelUI_WheelStopped(object sender, WheelStoppedArgs e) {
            CurrentThird = e.CurrentThird;
            textBlock.Text = CurrentThird.Text;

            if (CurrentThird.Type == ThirdType.Bankrupt) {
                CurrentPlayer.RoundWinnings = 0;
                GoToNextPlayer();
            } else if (CurrentThird.Type == ThirdType.LoseATurn) {
                GoToNextPlayer();
            }

            ToggleButtons();
        }

        private void SpinButton_Click(object sender, RoutedEventArgs e) {
            Game.PlayerChoice = PlayerChoice.Disabled;
            BoardUI.ToggleUsedLettersForSpin();

            wheelWindow.ShowDialog();                   
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e) {
            Game.PlayerChoice = PlayerChoice.Disabled;
            BoardUI.ToggleUsedLettersForBuyVowel();
            CurrentPlayer.RoundWinnings -= 250;            
        }

        private void Window_Closed(object sender, EventArgs e) {
            wheelWindow.Close();
        }

        private void SolveButton_Click(object sender, RoutedEventArgs e) {
            BoardUI.RevealAll();
            Game.PlayerChoice = PlayerChoice.Disabled;
        }

        private void NewGame_Click(object sender, RoutedEventArgs e) {
            BoardUI.NewPuzzle();
            Game.PlayerChoice = PlayerChoice.SpinOnly;
        }

        public void ToggleButtons() {
            if (Game != null) {
                if (CurrentPlayer.RoundWinnings >= 250) {
                    Game.PlayerChoice = PlayerChoice.SpinAndBuy;
                } else {
                    Game.PlayerChoice = PlayerChoice.SpinOnly;
                }

                if (BoardUI.Board.OnlyConsonantsRemain()) {
                    Game.PlayerChoice = PlayerChoice.SpinOnly;
                } else if (BoardUI.Board.OnlyVowelsRemain()) {
                    Game.PlayerChoice = PlayerChoice.VowelOnly;
                } else if (BoardUI.Board.IsBoardAllRevealed()) {
                    Game.PlayerChoice = PlayerChoice.SolveOnly;
                }
            }
        }

        public void GoToNextPlayer() {
            if (currentPlayerIndex + 1 >= Players.Count) {
                currentPlayerIndex = 0;
            } else {
                currentPlayerIndex++;
            }

            CurrentPlayer = Players[currentPlayerIndex];

            Scoreboard.RemoveActiveColors();
            Scoreboard.SetActiveColors(CurrentPlayer);
        }

        private void Window_ContentRendered(object sender, EventArgs e) {

        }
    }
}
