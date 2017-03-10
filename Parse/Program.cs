using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    class Program {
        static void Main(string[] args) {
            StreamReader reader = new StreamReader(@"wof.txt");
            StringBuilder sb = new StringBuilder();

            while (!reader.EndOfStream) {
                string[] splits = reader.ReadLine().Split('\t');

                if (splits.Length != 1) {
                    splits[0] = RemoveStartingSpace(splits[0]);
                    string first = SplitTooLargeHyphens(splits[0]);

                    sb.Append(first);
                    sb.Append("\t");
                    sb.Append(RemoveStartingSpace(splits[1]));

                    if (splits.Length >= 3) {
                        sb.Append("\t");
                        string spaceRemoved = RemoveStartingSpace(splits[2]);
                        string dateStr = spaceRemoved.Split(' ')[0];
                        DateTime dt = DateTime.Parse(dateStr);
                        sb.Append(dt.ToShortDateString());
                        sb.Append("\n");
                    } else {
                        sb.Append("\n");
                    }
                    
                } else {
                    reader.ReadLine();
                }
            }

            File.WriteAllText(@"C:\users\jrstan17\desktop\wof_output.txt", sb.ToString());
        }

        public static string SplitTooLargeHyphens(string splitZero) {
            StringBuilder toReturn = new StringBuilder();
            string[] splits = splitZero.Split(' ');
            for (int i = 0; i < splits.Length; i++) {
                StringBuilder sb = new StringBuilder(splits[i]);

                if (splits[i].Length > 14 && splits[i].Contains("-")) {
                    int hyphenIndex = IndexOfHyphen(splits[i]);                    
                    sb.Insert(hyphenIndex + 1, ' ');
                }

                toReturn.Append(sb.ToString() + " ");
            }

            return toReturn.Remove(toReturn.Length - 1, 1).ToString();
        }

        public static int IndexOfHyphen(string str) {
            for (int i = 0; i < str.Length; i++) {
                if (str[i] == '-') {
                    return i;
                }
            }

            return -1;
        }

        public static string RemoveStartingSpace(string str) {
            return str.TrimStart(new char[] { ' ' });
        }

        public static string AlterAmpersand(string str) {
            StringBuilder toReturn = new StringBuilder();

            foreach(char c in str) {
                if (c == '&') {
                    toReturn.Append("&&");
                } else {
                    toReturn.Append(c);
                }
            }

            return toReturn.ToString();
        }
    }
}
