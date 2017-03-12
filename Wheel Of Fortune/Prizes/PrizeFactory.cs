using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fortune.Prizes {
    class PrizeFactory {

        List<Prize> Prizes;
        public PrizeFactory() {
            Prizes = new List<Prize>();
            StreamReader reader = new StreamReader(@"Prizes\prizes.txt");

            while (!reader.EndOfStream) {
                Prizes.Add(new Prize(reader.ReadLine()));
            }
        }

        public Prize GetRandom() {
            return Prizes[Utilities.rnd.Next(0, Prizes.Count)];
        }
    }
}
