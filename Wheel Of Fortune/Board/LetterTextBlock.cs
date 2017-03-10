using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wheel_Of_Fortune.Board {
    class LetterTextBlock : TextBlock {
        internal bool IsUsed = false;
        internal char Letter = ' ';

        public bool IsVowel() {
            char c = this.Text[0];
            return (c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U');
        }

        public void Enable(bool boolean) {
            if (boolean) {
                Foreground = new SolidColorBrush(Colors.Black);
            } else {
                Foreground = new SolidColorBrush(Colors.LightGray);
            }

            IsEnabled = boolean;
        }
    }
}
