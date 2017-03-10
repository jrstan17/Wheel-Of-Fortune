using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    class PuzzleSizeUtility {
        public static void Main() {
            StreamReader reader = new StreamReader(@"wof.txt");
            StringBuilder sb = new StringBuilder();

            int letterCount = 0;
            int roundCount = 0;
            int maxLetters = 0;

            while (!reader.EndOfStream) {
                string[] splits = reader.ReadLine().Split('\t');

                if (splits.Length >= 4) {
                    if (splits[3].Equals("BR")) {
                        int brLetters = 0;

                        foreach (char c in splits[0]) {
                            if (c != ' ') {
                                brLetters++;
                            }
                        }

                        if (brLetters > maxLetters) {
                            maxLetters = brLetters;
                        }

                        letterCount += brLetters;
                        roundCount++;
                    }
                }       
            }

            Console.WriteLine("Total Letters: " + letterCount);
            Console.WriteLine("Total Bonus Rounds: " + roundCount);
            Console.WriteLine("Max Letters: " + maxLetters);
        }
    }
}
