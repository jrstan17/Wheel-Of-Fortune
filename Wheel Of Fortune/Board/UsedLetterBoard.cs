using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wheel_Of_Fortune.Enums;

namespace Wheel_Of_Fortune.Board {
    class UsedLetterBoard {

        BoardWindow Window;
        Canvas BoardCanvas;
        Board Board;

        List<LetterTextBlock> UsedLetterTextBlocks;
        List<char> UsedLetters = new List<char>();

        public void SetBoardUI(BoardUI boardUI) {
            Window = boardUI.Window;
            Board = boardUI.Board;
            BoardCanvas = boardUI.BoardCanvas;
        }

        public void CreateUsedLetters() {
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

            DisableLetters(LetterType.Both, true);
        }

        private void Letter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            LetterTextBlock block = (LetterTextBlock)sender;

            if (Board != null) {
                block.Foreground = new SolidColorBrush(Colors.LightGray);
                BoardEventArgs args = Board.Reveal(block.Letter);
                UsedLetters.Add(block.Letter);

                if (args.TrilonsChanged == 0) {
                    Window.GoToNextPlayer();
                }

                DisableLetters(LetterType.Both, true);
                Window.ToggleButtons();                
            }
        }

        public void ResetUsedLetters() {
            foreach (LetterTextBlock block in UsedLetterTextBlocks) {
                block.Enable(true);
            }

            UsedLetters.Clear();
        }

        public void HardDisableLetters(LetterType type) {
            if (type == LetterType.Consonant) {
                foreach (LetterTextBlock block in UsedLetterTextBlocks) {
                    if (!Utilities.IsVowel(block.Letter)) {
                        block.Enable(false);
                    }
                }
            } else if (type == LetterType.Vowel) {
                foreach (LetterTextBlock block in UsedLetterTextBlocks) {
                    if (Utilities.IsVowel(block.Letter)) {
                        block.Enable(false);
                    }
                }
            } else {
                foreach (LetterTextBlock block in UsedLetterTextBlocks) {
                    block.Enable(false);
                }
            }
        }

        public void DisableLetters(LetterType type, bool includeUsed) {
            if (type == LetterType.Consonant) {
                foreach (LetterTextBlock block in UsedLetterTextBlocks) {
                    if (!Utilities.IsVowel(block.Letter) || includeUsed && UsedLetters.Contains(block.Letter)) {
                        block.IsEnabled = false;
                    } else {
                        block.IsEnabled = true;
                    }
                }
            } else if (type == LetterType.Vowel) {
                foreach (LetterTextBlock block in UsedLetterTextBlocks) {
                    if (Utilities.IsVowel(block.Letter) || includeUsed && UsedLetters.Contains(block.Letter)) {
                        block.IsEnabled = false;
                    } else {
                        block.IsEnabled = true;
                    }
                }
            } else {
                foreach (LetterTextBlock block in UsedLetterTextBlocks) {
                    if (includeUsed) {
                        block.IsEnabled = false;
                    } else {
                        block.IsEnabled = true;
                    }
                }
            }
        }
    }
}