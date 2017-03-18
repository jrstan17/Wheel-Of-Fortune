using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fortune.PuzzleModel {
    class Puzzle {
        internal string Text;
        internal string Category;
        internal DateTime Airdate;
        internal int Number;

        public Puzzle(string rawLine, int number) {
            //rawLine = "VOTING FOR YOUR FAVORITE AMERICAN IDOL CONTESTANT	Phrase	4/11/06";

            string[] splits = rawLine.Split('\t');

            Text = splits[0];
            Category = splits[1];
            Airdate = DateTime.Parse(splits[2]);
            Number = number;
        }
    }
}
