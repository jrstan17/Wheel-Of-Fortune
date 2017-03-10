using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel_Of_Fortune;

namespace Wheel {
    class Program {
        static void Main(string[] args) {
            MainWindow Window = new MainWindow();
            Third test = new Third(Window, WheelColors.ORANGE, WheelColors.PURPLE);
            Window.ShowDialog();
        }
    }
}
