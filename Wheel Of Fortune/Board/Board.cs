using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel_Of_Fortune.Enums;
using Wheel_Of_Fortune.PuzzleModel;

namespace Wheel_Of_Fortune.Board {
    class Board {
        List<Row> Rows;
        internal List<BoardFrame> BoardFrames;
        internal BoardFrame CurrentBoard;

        internal PuzzleFactory PuzzleFactory;
        internal Puzzle CurrentPuzzle;

        Random rnd = new Random();

        public Board() {
            PuzzleFactory = new PuzzleFactory();

            Rows = new List<Row>();
            Rows.Add(new Row(12));
            Rows.Add(new Row(14));
            Rows.Add(new Row(14));
            Rows.Add(new Row(12));

            AddTrilonChangeEvent();
            BoardFrames = new List<BoardFrame>();

            CurrentBoard = GetTrilonStates();
            OnBoardChanged(null);
        }

        public bool OnlyVowelsRemain() {
            foreach(Row r in Rows) {
                foreach(Trilon t in r.Trilons) {
                    if (t.State == TrilonState.Unrevealed && !Utilities.IsVowel(t.Letter)) {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool OnlyConsonantsRemain() {
            foreach (Row r in Rows) {
                foreach (Trilon t in r.Trilons) {
                    if (t.State == TrilonState.Unrevealed && Utilities.IsVowel(t.Letter)) {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsBoardAllRevealed() {
            foreach (Row r in Rows) {
                foreach (Trilon t in r.Trilons) {
                    if (t.State == TrilonState.Unrevealed) {
                        return false;
                    }
                }
            }

            return true;
        }

        public void NewBoard() {
            Clear();
            CurrentPuzzle = NewPuzzle();

            string[] splits = splits = CurrentPuzzle.Text.Split(' '); ;
            string[] remainders = null;

            remainders = Rows[1].AddAnswer(splits);

            if (remainders.Length != 0) {
                remainders = Rows[2].AddAnswer(remainders);
            }

            if (remainders.Length != 0) {
                if (rnd.Next(0, 2) == 0) {
                    this.Clear();
                    remainders = null;

                    remainders = Rows[1].AddAnswer(splits);
                    remainders = Rows[2].AddAnswer(remainders);
                    remainders = Rows[3].AddAnswer(remainders);

                    if (remainders.Length != 0) {
                        this.Clear();
                        remainders = null;

                        remainders = Rows[0].AddAnswer(splits);
                        remainders = Rows[1].AddAnswer(remainders);
                        remainders = Rows[2].AddAnswer(remainders);
                        remainders = Rows[3].AddAnswer(remainders);
                    }
                } else {
                    this.Clear();
                    remainders = null;

                    remainders = Rows[0].AddAnswer(splits);
                    remainders = Rows[1].AddAnswer(remainders);
                    remainders = Rows[2].AddAnswer(remainders);


                    if (remainders.Length != 0) {
                        this.Clear();
                        remainders = null;

                        remainders = Rows[0].AddAnswer(splits);
                        remainders = Rows[1].AddAnswer(remainders);
                        remainders = Rows[2].AddAnswer(remainders);
                        remainders = Rows[3].AddAnswer(remainders);
                    }
                }
            }

            if (remainders.Length != 0 && !remainders[0].Equals("")) {
                StringBuilder sb = new StringBuilder();
                sb.Append(CurrentPuzzle.Text + "\t" + CurrentPuzzle.Number + "\n");

                File.AppendAllText(@"C:\users\jrstan17\desktop\errors.txt", sb.ToString());

                NewPuzzle();
                return;
            }

            CurrentBoard = GetTrilonStates();
            OnBoardChanged(null);
        }

        public Puzzle NewPuzzle(int puzzleLine) {
            return PuzzleFactory.NewPuzzle(puzzleLine - 1);
        }

        public Puzzle NewPuzzle() {
            return PuzzleFactory.NewPuzzle(RoundType.Regular);
        }

        public void AddTrilonChangeEvent() {
            foreach (Row r in Rows) {
                foreach (Trilon t in r.Trilons) {
                    t.TrilonChange += T_TrilonChange;
                }
            }
        }

        private void T_TrilonChange(object sender, EventArgs e) {
            Trilon trilon;
            if (sender is Trilon) {
                trilon = (Trilon)sender;
            } else {
                return;
            }

            if (trilon.State == TrilonState.Revealed) {
                BoardFrames.Add(GetTrilonStates());
            }
        }

        public BoardEventArgs Reveal(char letter) {
            BoardFrames.Clear();

            int count = 0;

            foreach (Row r in Rows) {
                count += r.Reveal(letter);
            }

            BoardEventArgs args = new BoardEventArgs();
            args.TrilonsChanged = count;
            args.LetterRevealed = letter;
            
            if (Utilities.IsVowel(letter)) {
                args.AreVowelsRevealed = true;
            }

            CurrentBoard = GetTrilonStates();
            OnBoardChanged(args);

            return args;
        }

        public void RevealAll() {
            BoardFrames.Clear();

            foreach (Row r in Rows) {
                r.RevealAll();
            }

            CurrentBoard = GetTrilonStates();
            OnBoardChanged(null);
        }

        public BoardFrame GetTrilonStates() {
            BoardFrame toReturn = new BoardFrame();

            foreach (Row r in Rows) {
                foreach (Trilon t in r.Trilons) {
                    toReturn.Add(t);
                }
            }

            return toReturn;
        }

        public void Clear() {
            foreach (Row r in Rows) {
                r.Clear();
            }
        }

        private int MaxTrilonsInARow() {
            int max = 0;

            for (int i = 0; i < Rows.Count; i++) {
                if (Rows[i].Size() > max) {
                    max = Rows[i].Size();
                }
            }

            return max;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            int maxTrilons = MaxTrilonsInARow();

            foreach (Row r in Rows) {
                int extraSpace = (maxTrilons - r.Size()) / 2;

                for (int i = 0; i < extraSpace; i++) {
                    sb.Append(" ");
                }

                sb.Append(r);

                for (int i = 0; i < extraSpace; i++) {
                    sb.Append(" ");
                }

                sb.Append("\n");
            }

            return sb.ToString();
        }

        protected virtual void OnBoardChanged(BoardEventArgs e) {
            EventHandler<BoardEventArgs> handler = BoardChanged;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<BoardEventArgs> BoardChanged;
    }
}
