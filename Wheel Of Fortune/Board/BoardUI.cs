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
        internal BoardWindow Window;
        internal Canvas BoardCanvas;
        FontFamily family;

        List<Rectangle> TrilonScreens;
        List<TextBlock> LetterBlocks;

        internal UsedLetterBoard UsedLetterBoard;

        TextBlock CategoryText;
        TextBlock DateText;

        const int TRILON_WIDTH = 75;
        const int TRILON_HEIGHT = (int) (TRILON_WIDTH * 1.1);

        internal Player CurrentPlayer;

        public BoardUI(BoardWindow window) {
            Window = window;
            BoardCanvas = Window.BoardCanvas;
            CurrentPlayer = Window.CurrentPlayer;
            LetterBlocks = new List<TextBlock>();

            BoardCanvas.Width = 1050 + BOARD_X_START;

            TrilonScreens = new List<Rectangle>();
            UsedLetterBoard = new UsedLetterBoard();
            
            family = new FontFamily(new Uri(@"C:\Users\JRStan17\Desktop\letters.ttf", UriKind.Absolute), "Arial");

            Board = new Board();
            Board.BoardChanged += Board_BoardChanged;

            UsedLetterBoard.SetBoardUI(this);
            Initialize();
            Board.NewBoard();
        }

        public void NewPuzzle() {
            Board.NewBoard();
            Console.WriteLine("Puzzle Solution: " + Board.CurrentPuzzle.Text);
            UsedLetterBoard.ResetUsedLetters();
            UsedLetterBoard.DisableLetters(LetterType.Both, true);
        }

        private void Board_BoardChanged(object sender, BoardEventArgs e) {
            BoardFrame Update = Board.CurrentBoard;
            for(int i = 0; i < Update.Count; i++) {
                if (Update[i].State == TrilonState.NotInUse) {
                    TrilonScreens[i].Fill = new SolidColorBrush(Colors.DarkGreen);
                    ChangeText(i, "");
                } else if (Update[i].State == TrilonState.Unrevealed) {
                    TrilonScreens[i].Fill = new SolidColorBrush(Colors.White);
                    ChangeText(i, "");
                } else if (Update[i].State == TrilonState.Revealed) {
                    TrilonScreens[i].Fill = new SolidColorBrush(Colors.White);
                    ChangeText(i, Update[i].Letter.ToString());
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
            UsedLetterBoard.CreateUsedLetters();
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

        public void ChangeText(int index, string text) {
            LetterBlocks[index].Text = text;
        }
    }
}