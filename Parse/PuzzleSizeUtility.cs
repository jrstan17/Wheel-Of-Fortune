using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    class PuzzleSizeUtility {

        static List<int> Sizes = new List<int>();
        static List<string> Answers = new List<string>();

        public static void Main() {
            StreamReader reader = new StreamReader(@"wof.txt");
            StringBuilder sb = new StringBuilder();           

            while (!reader.EndOfStream) {
                string[] splits = reader.ReadLine().Split('\t');

                if (splits.Length >= 4) {
                    if (splits[3][0] == 'R') {
                        int brLetters = 0;

                        foreach (char c in splits[0]) {
                            if (char.IsLetter(c)) {
                                brLetters++;
                            }
                        }

                        Sizes.Add(brLetters);
                        Answers.Add(splits[0]);
                    }
                }       
            }

            Console.WriteLine("Min Letters: " + FindMin());
            Console.WriteLine("Max Letters: " + FindMax());
            Console.WriteLine("Average Letters: " + FindAverage());
            Console.WriteLine("Median Letters: " + FindMedian());
        }

        public static int FindMax() {
            int max = 0;
            string largestAnswer = "";

            int count = 0;
            foreach(int i in Sizes) {
                if (i > max) {
                    max = i;
                    largestAnswer = Answers[count];
                }

                count++;
            }

            return max;
        }

        public static int FindMin() {
            int min = int.MaxValue;

            foreach (int i in Sizes) {
                if (i < min) {
                    min = i;
                }
            }

            return min;
        }

        public static int FindAverage() {
            int sum = 0;

            foreach (int i in Sizes) {
                sum += i;
            }

            double avg = (double)sum / Sizes.Count;

            return (int)Math.Round(avg);
        }

        public static int FindMedian() {
            Sizes.Sort();
            return Sizes[Sizes.Count / 2];
        }
    }
}
