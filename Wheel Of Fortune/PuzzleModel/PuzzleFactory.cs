using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel_Of_Fortune.Enums;

namespace Wheel_Of_Fortune.PuzzleModel {

    class PuzzleFactory {

        const int REGULAR_ROUND_PUZZLE_SIZE_MIN = 9;
        const int BONUS_ROUND_PUZZLE_SIZE_MAX = 16;

        List<Puzzle> Puzzles;
        int previousPuzzleIndex = -1;
        Random rnd = new Random();

        public Puzzle NewPuzzle(RoundType roundType) {
            InitPuzzleList();

            int randomIndex = -1;
            randomIndex = rnd.Next(0, Puzzles.Count);
            while (previousPuzzleIndex == randomIndex || !IsRandomRoundPuzzleValid(randomIndex, roundType)) {
                randomIndex = rnd.Next(0, Puzzles.Count);
            }
            previousPuzzleIndex = randomIndex;

            return Puzzles[randomIndex];
            //return new Puzzle("PLAYER CREATION SCREEN\tCategory\t10/16/2014", 1);
        }

        private bool IsRandomRoundPuzzleValid(int randomIndex, RoundType type) {
            string answer = Puzzles[randomIndex].Text;
            int letters = 0;
            foreach(char c in answer) {
                if (char.IsLetter(c)) {
                    letters++;
                }
            }

            if (type == RoundType.Regular) {
                if (letters >= REGULAR_ROUND_PUZZLE_SIZE_MIN) {
                    return true;
                }
            } else if (type == RoundType.Bonus) {
                if (letters <= BONUS_ROUND_PUZZLE_SIZE_MAX) {
                    return true;
                }
            }

            return false;
        }

        internal Puzzle NewPuzzle(int puzzleIndex) {
            InitPuzzleList();
            previousPuzzleIndex = puzzleIndex;
            return Puzzles[puzzleIndex];
        }

        private void InitPuzzleList() {
            if (Puzzles == null) {
                int count = 1;
                Puzzles = new List<Puzzle>();
                StreamReader reader = new StreamReader("data.txt");
                while (!reader.EndOfStream) {
                    Puzzles.Add(new Puzzle(reader.ReadLine(), count));
                    count++;
                }
            }
        }
    }
}
