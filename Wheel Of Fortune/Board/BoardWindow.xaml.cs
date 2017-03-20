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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wheel_Of_Fortune.Enums;
using Wheel_Of_Fortune.NewGame;
using Wheel_Of_Fortune.Prizes;
using Wheel_Of_Fortune.Solve;
using Wheel_Of_Fortune.WheelModel;

namespace Wheel_Of_Fortune.Board {
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window {

        internal Game Game;
        internal BoardUI BoardUI;
        Third CurrentThird;

        internal static int CurrentRound { get; set; }

        internal Prize CurrentPrize;
        internal PrizeFactory PrizeFactory = new PrizeFactory();

        MainWindow wheelWindow;

        const int PLAYER_BOARD_WIDTHS = 150;
        const int PLAYER_BOARD_HEIGHTS = 38;
        const int PLAYER_BOARD_SPACING = 5;
        const int PLAYER_BOARD_START_Y = 480;

        const int VOWEL_COST = 300;
        const int MIN_WIN = 2000;

        internal List<Player> Players;
        internal static Player CurrentPlayer;
        int currentPlayerIndex = -1;

        internal Scoreboard Scoreboard;

        internal BoardWindow(List<Player> players) {
            InitializeComponent();

            CurrentRound = 0;

#if !DEBUG
            Menu.Items.Remove(DebugMenuItem);
#endif

            Players = players;
            InitPlayerTextBlocks();
            GoToNextPlayer();

            BoardUI = new BoardUI(this);
            Game = new Game();
            Game.OnPlayerChoiceChange += Game_OnPlayerChoiceChange;
            Game.PlayerChoice = PlayerChoice.SpinOnly;

            NewGame();
        }

        private void WheelUI_WedgeClicked(object sender, WedgeClickedEventArgs e) {
            if (e.Type == ThirdType.Prize) {
                CurrentPlayer.WonRoundPrizes.Add(CurrentPrize);
            } else if (e.Type == ThirdType.Million) {
                CurrentPlayer.HasMillionWedge = true;
            }
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
                nameBlock.HorizontalAlignment = HorizontalAlignment.Left;
                nameBlock.VerticalAlignment = VerticalAlignment.Top;
                nameBlock.TextAlignment = TextAlignment.Center;
                nameBlock.Width = 150;
                nameBlock.Height = 38;
                nameBlock.Margin = new Thickness(x, y, 0, 0);

                y += nameBlock.Height;

                TextBlock roundBlock = new TextBlock();
                roundBlock.Text = "$" + Players[i].RoundWinnings.ToString();
                roundBlock.FontSize = 26;
                roundBlock.HorizontalAlignment = HorizontalAlignment.Left;
                roundBlock.VerticalAlignment = VerticalAlignment.Top;
                roundBlock.TextAlignment = TextAlignment.Center;
                roundBlock.Width = 150;
                roundBlock.Height = 38;
                roundBlock.Margin = new Thickness(x, y, 0, 0);

                y += nameBlock.Height;

                TextBlock totalBlock = new TextBlock();
                totalBlock.Text = "$" + Players[i].TotalWinnings.ToString();
                totalBlock.FontSize = 12;
                totalBlock.HorizontalAlignment = HorizontalAlignment.Left;
                totalBlock.VerticalAlignment = VerticalAlignment.Top;
                totalBlock.TextAlignment = TextAlignment.Center;
                totalBlock.Width = 150;
                totalBlock.Height = 38;
                totalBlock.Margin = new Thickness(x, y, 0, 0);

                x += PLAYER_BOARD_SPACING + PLAYER_BOARD_WIDTHS;

                Scoreboard.Add(Players[i], nameBlock, roundBlock, totalBlock);

                MainGrid.Children.Add(nameBlock);
                MainGrid.Children.Add(roundBlock);
                MainGrid.Children.Add(totalBlock);
            }
        }

        private void Game_OnPlayerChoiceChange(object sender, EventArgs e) {
            if (Game.PlayerChoice == PlayerChoice.SolveOnly) {
                BoardUI.UsedLetterBoard.HardDisableLetters(LetterType.Both);
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
                if (BoardUI.Board.OnlyConsonantsRemain()) {
                    BoardUI.UsedLetterBoard.HardDisableLetters(LetterType.Vowel);
                }
                SpinButton.IsEnabled = true;
                BuyButton.IsEnabled = false;
                BuyButton.Foreground = SystemColors.ControlDarkBrush;
                SolveButton.IsEnabled = true;
                SolveButton.Foreground = SystemColors.ControlTextBrush;
            } else if (Game.PlayerChoice == PlayerChoice.VowelOnly) {
                if (BoardUI.Board.OnlyVowelsRemain()) {
                    BoardUI.UsedLetterBoard.HardDisableLetters(LetterType.Consonant);
                }
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
                CurrentPlayer.HasMillionWedge = false;
                CurrentPlayer.FreePlays = 0;
                CurrentPlayer.WonRoundPrizes.Clear();
                BoardUI.UsedLetterBoard.DisableLetters(LetterType.Both, true);
                GoToNextPlayer();
            } else if (CurrentThird.Type == ThirdType.LoseATurn) {
                BoardUI.UsedLetterBoard.DisableLetters(LetterType.Both, true);
                AskFreePlayQuestion();
                ToggleButtons();
            } else if (CurrentThird.Type == ThirdType.FreePlay) {
                BoardUI.UsedLetterBoard.DisableLetters(LetterType.Vowel, true);
                CurrentPlayer.FreePlays++;
            } else {
                if (CurrentThird.Type == ThirdType.Million) {
                    CurrentPlayer.HasMillionWedge = true;
                }
                BoardUI.UsedLetterBoard.DisableLetters(LetterType.Vowel, true);
                Game.PlayerChoice = PlayerChoice.Disabled;
            }
        }

        internal void AskFreePlayQuestion() {
            if (CurrentPlayer.FreePlays > 0) {
                if (MessageBox.Show("You have a Free Play! Would you like to use it now?", "Use Free Play?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                    CurrentPlayer.FreePlays--;
                    ToggleButtons();
                } else {
                    GoToNextPlayer();
                }
            } else {
                GoToNextPlayer();
            }
        }

        private void SpinButton_Click(object sender, RoutedEventArgs e) {
            Game.PlayerChoice = PlayerChoice.Disabled;
            BoardUI.UsedLetterBoard.DisableLetters(LetterType.Vowel, true);

            //wheelWindow.WheelUI.WheelMouseRightButton_Click(null, null);
            wheelWindow.Show();

            //        RenderTargetBitmap renderTargetBitmap =
            //new RenderTargetBitmap((int)wheelWindow.Width * 3, (int)wheelWindow.Height * 3, 300, 300, PixelFormats.Pbgra32);
            //        renderTargetBitmap.Render(wheelWindow);
            //        PngBitmapEncoder pngImage = new PngBitmapEncoder();
            //        pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            //        using (Stream fileStream = File.Create(@"C:\users\jrstan17\desktop\text.png")) {
            //            pngImage.Save(fileStream);
            //        }
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e) {
            Game.PlayerChoice = PlayerChoice.Disabled;
            BoardUI.UsedLetterBoard.DisableLetters(LetterType.Consonant, true);
            CurrentPlayer.RoundWinnings -= VOWEL_COST;
        }

        private void Window_Closed(object sender, EventArgs e) {
            Application.Current.Shutdown();
        }

        private void SolveButton_Click(object sender, RoutedEventArgs e) {
            Game.PlayerChoice = PlayerChoice.Disabled;
            SolveWindow window = new SolveWindow(this);
            window.SolveResult += Window_SolveResult;
            window.ShowDialog();
        }

        private void Window_SolveResult(object sender, SolveResultEventArgs e) {
            SolveWindow solveWindow = (SolveWindow)sender;
            solveWindow.Close();

            if (e.IsWin) {
                CollectCurrentPlayersWinnings();
                RoundOverWindow roundWindow = new RoundOverWindow(this);
                roundWindow.ShowDialog();
                GoToNextPlayer();
            } else {
                AskFreePlayQuestion();
            }
        }

        public void CollectCurrentPlayersWinnings() {
            int roundWinnings = CurrentPlayer.CurrentRoundValue();

            if (roundWinnings < MIN_WIN) {
                CurrentPlayer.RoundWinnings = MIN_WIN;
                CurrentPlayer.TotalWinnings += MIN_WIN;
            } else {
                CurrentPlayer.RoundWinnings = roundWinnings;
                CurrentPlayer.TotalWinnings += roundWinnings;
            }

            CurrentPlayer.MoveWonPrizesToBank();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e) {
            NewGame();
        }

        public void NewGame() {
            if (wheelWindow != null) {
                wheelWindow.Close();
            }

            CurrentRound++;

            wheelWindow = new MainWindow();
            wheelWindow.WheelUI.WheelStopped += WheelUI_WheelStopped;
            wheelWindow.WheelUI.WedgeClicked += WheelUI_WedgeClicked;

            BoardUI.NewPuzzle();

            PrizeWindow prizeWindow = new PrizeWindow(this);
            prizeWindow.ShowDialog();

            foreach (Player player in Players) {
                player.RoundWinnings = 0;
                player.FreePlays = 0;
            }

            Game.PlayerChoice = PlayerChoice.SpinOnly;
        }

        public void ToggleButtons() {
            if (Game != null) {
                bool canBuy = (CurrentPlayer.RoundWinnings >= VOWEL_COST);

                if (canBuy) {
                    Game.PlayerChoice = PlayerChoice.SpinAndBuy;
                } else {
                    Game.PlayerChoice = PlayerChoice.SpinOnly;
                }

                if (BoardUI.Board.OnlyConsonantsRemain()) {
                    if (canBuy) {
                        Game.PlayerChoice = PlayerChoice.SpinOnly;
                    } else {
                        Game.PlayerChoice = PlayerChoice.SolveOnly;
                    }
                } else if (BoardUI.Board.OnlyVowelsRemain()) {
                    if (canBuy) {
                        Game.PlayerChoice = PlayerChoice.VowelOnly;
                    } else {
                        Game.PlayerChoice = PlayerChoice.SolveOnly;
                    }
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

            ToggleButtons();
        }

        private void ShowSolution_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show(BoardUI.Board.CurrentPuzzle.Text, "Puzzle Solution", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
