using System.Windows.Controls;

namespace Wheel_Of_Fortune.Board {
    internal class PlayerLabelSet {
        public TextBlock Name { get; set; }
        public TextBlock RoundWinnings { get; set; }
        public TextBlock TotalWinnings { get; set; }

        public PlayerLabelSet(TextBlock name, TextBlock roundWinnings, TextBlock totalWinnings) {
            Name = name;
            RoundWinnings = roundWinnings;
            TotalWinnings = totalWinnings;
        }

    }
}