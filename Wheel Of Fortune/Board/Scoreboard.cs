using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wheel_Of_Fortune.Board {
    class Scoreboard : List<PlayerLabelSet> {
        public void Add(Player player, TextBlock name, TextBlock roundWinnings, TextBlock totalWinnings) {
            base.Add(new PlayerLabelSet(name, roundWinnings, totalWinnings));
            AttachRoundWinningLabel(player);
            AttachTotalWinningLabel(player);
        }

        private void AttachRoundWinningLabel(Player player) {
            player.RoundWinningChanged += delegate (object sender, EventArgs e) {
                GetRoundWinningsTextBlock(player).Text = "$" + player.RoundWinnings;
            };
        }

        private void AttachTotalWinningLabel(Player player) {
            player.RoundWinningChanged += delegate (object sender, EventArgs e) {
                GetTotalWinningsTextBlock(player).Text = "$" + player.TotalWinnings;
            };
        }

        private TextBlock GetRoundWinningsTextBlock(Player player) {
            foreach(PlayerLabelSet set in this) {
                if (set.Name.Text.Equals(player.Name)) {
                    return set.RoundWinnings;
                }
            }

            return null;
        }

        private TextBlock GetTotalWinningsTextBlock(Player player) {
            foreach (PlayerLabelSet set in this) {
                if (set.Name.Text.Equals(player.Name)) {
                    return set.TotalWinnings;
                }
            }

            return null;
        }

        public void RemoveActiveColors() {
            foreach (PlayerLabelSet set in this) {
                set.Name.Background = SystemColors.WindowBrush;
                set.RoundWinnings.Background = SystemColors.WindowBrush;
                set.TotalWinnings.Background = SystemColors.WindowBrush;
            }
        }

        public void SetActiveColors(Player player) {
            foreach (PlayerLabelSet set in this) {
                if (set.Name.Text.Equals(player.Name)) {
                    set.Name.Background = new SolidColorBrush(Colors.Yellow);
                    set.RoundWinnings.Background = new SolidColorBrush(Colors.Yellow);
                    set.TotalWinnings.Background = new SolidColorBrush(Colors.Yellow);
                }
            }
        }
    }
}
