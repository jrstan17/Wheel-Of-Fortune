using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Wheel_Of_Fortune.Enums;

namespace Wheel_Of_Fortune.Board {
    class BoardUI {

        const int BOARD_X_START = 17;

        internal Board Board;
        BoardWindow Window;
        Canvas BoardCanvas;
        FontFamily family;

        List<Rectangle> TrilonScreens;
        List<TextBlock> LetterBlocks;
        List<LetterTextBlock> UsedLetterTextBlocks;
        List<char> UsedLetters = new List<char>();

        TextBlock CategoryText;
        TextBlock DateText;

        const int TRILON_WIDTH = 75;
        const int TRILON_HEIGHT = (int) (TRILON_WIDTH * 1.1);

        internal Player CurrentPlayer;

        public BoardUI(BoardWindow window) {
            Window = window;
            BoardCanvas = Window.BoardCanvas;
            CurrentPlayer = Window.CurrentPlayer;

            BoardCanvas.Width = 1050 + BOARD_X_START;

            TrilonScreens = new List<Rectangle>();
            LetterBlocks = new List<TextBlock>();
            family = new FontFamily(new Uri(@"C:\Users\JRStan17\Desktop\letters.ttf", UriKind.Absolute), "Arial");

            Initialize();
            Board = new Board();
            Board.BoardChanged += Board_BoardChanged;

            Board.NewBoard();
        }

        public void ToggleUsedLetters(bool enable) {
            if (enable) {
                foreach(TextBlock block in LetterBlocks) {
                    if (!UsedLetters.Contains(block.Text[0])) {
                        block.IsEnabled = true;
                    }
                }
            } else {
                foreach (TextBlock block in LetterBlocks) {
                    block.IsEnabled = false;
                }
            }
        }

        public void NewPuzzle() {
            Board.NewBoard();
            ResetUsedLetters();
        }

        private void Board_BoardChanged(object sender, BoardEventArgs e) {
            BoardFrame Update = Board.CurrentBoard;
            for(int i = 0; i < Update.Count; i++) {
                if (Update[i].State == TrilonState.NotInUse) {
                    TrilonScreens[i].Fill = new SolidColorBrush(Colors.DarkGreen);
                    LetterBlocks[i].Text = "";
                } else if (Update[i].State == TrilonState.Unrevealed) {
                    TrilonScreens[i].Fill = new SolidColorBrush(Colors.White);
                    LetterBlocks[i].Text = "";
                } else if (Update[i].State == TrilonState.Revealed) {
                    TrilonScreens[i].Fill = new SolidColorBrush(Colors.White);
                    LetterBlocks[i].Text = Update[i].Letter.ToString();
                }
            }

            if (e != null) {
                if (!e.AreVowelsRevealed) {
                    Window.UpdateRoundWinnings(e.TrilonsChanged);
                }
            }

            CategoryText.Text = Board.CurrentPuzzle.Category;
            DateText.Text = "Puzzle Date:  " + (String.Format("{0:MMMM d, yyyy}", Board.CurrentPuzzle.Airdate) + "  (#" + Board.CurrentPuzzle.Number + ")");
        }

        public void RevealAll() {
            Board.RevealAll();
        }

        public void Initialize() {
            int x = BOARD_X_START;
            int y = 0;

            for (int i = 0; i < 14; i++) {
                Grid g;

                if (i != 0 && i != 13) {
                    g = CreateTrilonGrid(x, y);
                    BoardCanvas.Children.Add(g);
                }

                x += TRILON_WIDTH;
            }

            y += TRILON_HEIGHT;
            x = BOARD_X_START;

            for (int i = 0; i < 2; i++) {
                for (int j = 0; j < 14; j++) {
                    Grid g = CreateTrilonGrid(x, y);

                    x += TRILON_WIDTH;
                    BoardCanvas.Children.Add(g);
                }

                y += TRILON_HEIGHT;
                x = BOARD_X_START;
            }

            for (int i = 0; i < 14; i++) {
                Grid g;

                if (i != 0 && i != 13) {
                    g = CreateTrilonGrid(x, y);
                    BoardCanvas.Children.Add(g);
                }

                x += TRILON_WIDTH;
            }

            CreateCategoryText(y + 83);
            CreateDateText(y + 93);
            CreateUsedLetters();
        }

        private void CreateCategoryText(double y) {
            CategoryText = new TextBlock();
            CategoryText.Width = 500;
            CategoryText.Height = 60;

            Canvas.SetTop(CategoryText, y);
            double x = (BoardCanvas.Width - CategoryText.Width) / 2;
            Canvas.SetLeft(CategoryText, BOARD_X_START + x);

            CategoryText.TextAlignment = TextAlignment.Center;
            CategoryText.FontSize = 36;
            CategoryText.FontWeight = FontWeights.Bold;
            BoardCanvas.Children.Add(CategoryText);
        }

        private void CreateDateText(double y) {
            DateText = new TextBlock();
            DateText.Width = 500;
            DateText.Height = 60;

            Canvas.SetTop(DateText, y);
            double x = BoardCanvas.Width - DateText.Width;
            Canvas.SetLeft(DateText, BOARD_X_START + x);

            DateText.TextAlignment = TextAlignment.Right;
            DateText.FontSize = 16;
            BoardCanvas.Children.Add(DateText);
        }

        private void CreateUsedLetters() {
            double y = 385;
            int letterBlockWidth = 42;
            double x = (BoardCanvas.Width - letterBlockWidth * 26) / 2;

            UsedLetterTextBlocks = new List<LetterTextBlock>();

            for (int i = 0; i < 26; i++) {
                LetterTextBlock letterBlock = new LetterTextBlock();
                letterBlock.Width = letterBlockWidth;
                letterBlock.Height = 45;
                letterBlock.TextAlignment = TextAlignment.Center;
                letterBlock.FontSize = 36;
                letterBlock.FontWeight = FontWeights.ExtraBold;
                Canvas.SetLeft(letterBlock, x);
                Canvas.SetTop(letterBlock, y);

                char letter = (char)(i + 65);
                letterBlock.Letter = letter;
                letterBlock.Text = letter.ToString();

                UsedLetterTextBlocks.Add(letterBlock);
                BoardCanvas.Children.Add(letterBlock);

                x += letterBlock.Width;

                letterBlock.MouseLeftButtonUp += Letter_MouseLeftButtonUp;
            }
        }

        private void Letter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            LetterTextBlock block = (LetterTextBlock)sender;
            block.Foreground = new SolidColorBrush(Colors.LightGray);
            BoardEventArgs args = Board.Reveal(block.Letter);
            block.IsEnabled = false;
            UsedLetters.Add(block.Letter);

            if (args.TrilonsChanged == 0) {
                Window.GoToNextPlayer();
            }

            Window.ToggleButtons();
            ToggleUsedLetters(false);
        }

        private void ResetUsedLetters() {
            foreach(LetterTextBlock block in UsedLetterTextBlocks) {
                block.Foreground = new SolidColorBrush(Colors.Black);
                block.IsEnabled = true;
            }

            UsedLetters.Clear();
        }

        private Grid CreateTrilonGrid(int x, int y) {
            Grid trilonGrid = new Grid();

            Rectangle backRect = new Rectangle();
            backRect.Fill = new SolidColorBrush(Colors.Teal);
            backRect.Width = TRILON_WIDTH;
            backRect.Height = TRILON_HEIGHT;

            Rectangle borderRect = new Rectangle();
            borderRect.Fill = new SolidColorBrush(Colors.Black);
            borderRect.Width = TRILON_WIDTH - 2;
            borderRect.Height = TRILON_HEIGHT - 2;

            Rectangle trilonRect = new Rectangle();
            trilonRect.Fill = new SolidColorBrush(Colors.White);
            trilonRect.Width = TRILON_WIDTH - 13;
            trilonRect.Height = TRILON_HEIGHT - 13;
            TrilonScreens.Add(trilonRect);

            TextBlock letterBlock = new TextBlock();
            letterBlock.Width = trilonRect.Width;
            letterBlock.Height = trilonRect.Height - 9;
            letterBlock.TextAlignment = TextAlignment.Center;
            letterBlock.FontFamily = family;
            letterBlock.FontSize = 58;
            letterBlock.FontWeight = FontWeights.ExtraBold;
            LetterBlocks.Add(letterBlock);

            trilonGrid.Children.Add(backRect);
            trilonGrid.Children.Add(borderRect);
            trilonGrid.Children.Add(trilonRect);
            trilonGrid.Children.Add(letterBlock);

            Canvas.SetLeft(trilonGrid, x);
            Canvas.SetTop(trilonGrid, y);

            return trilonGrid;
        }

        public void ToggleUsedLettersForSpin() {
            foreach(LetterTextBlock block in UsedLetterTextBlocks) {
                if (block.IsVowel() || UsedLetters.Contains(block.Letter)) {
                    block.Enable(false);
                } else {
                    block.Enable(true);
                }
            }
        }

        public void ToggleUsedLettersForBuyVowel() {
            foreach (LetterTextBlock block in UsedLetterTextBlocks) {
                if (!block.IsVowel() || UsedLetters.Contains(block.Letter)) {
                    block.Enable(false);
                } else {
                    block.Enable(true);
                }
            }
        }

        public void ToggleAllAvailableLetters() {
            foreach (LetterTextBlock block in UsedLetterTextBlocks) {
                if (UsedLetters.Contains(block.Letter)) {
                    block.Enable(false);
                } else {
                    block.Enable(true);
                }
            }
        }
    }
}