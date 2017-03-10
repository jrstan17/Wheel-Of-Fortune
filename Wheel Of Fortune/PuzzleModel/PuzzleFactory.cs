using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fortune.PuzzleModel {

    class PuzzleFactory {

        List<Puzzle> Puzzles;
        int previousPuzzleIndex = -1;
        Random rnd = new Random();

        public Puzzle NewPuzzle() {
            InitPuzzleList();

            int randomIndex = -1;
            randomIndex = rnd.Next(0, Puzzles.Count);
            while (previousPuzzleIndex == randomIndex) {
                randomIndex = rnd.Next(0, Puzzles.Count);
            }
            previousPuzzleIndex = randomIndex;
            
            return Puzzles[randomIndex];
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
