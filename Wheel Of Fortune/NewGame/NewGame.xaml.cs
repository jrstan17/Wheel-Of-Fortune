using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wheel_Of_Fortune.Board;

namespace Wheel_Of_Fortune.NewGame {
    /// <summary>
    /// Interaction logic for NewGame.xaml
    /// </summary>
    public partial class NewGame : Window {

        List<string> Names;

        public NewGame() {
            InitializeComponent();

#if DEBUG
            if (MessageBox.Show("Auto generate players?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                List<Player> autoPlayers = new List<Player>();
                autoPlayers.Add(new Player("Jason"));
                autoPlayers.Add(new Player("David"));
                autoPlayers.Add(new Player("Philip"));
                autoPlayers.Add(new Player("Leslie"));

                BoardWindow board = new BoardWindow(autoPlayers);
                board.Show();
                this.Close();
            }            
#endif

            Names = new List<string>();
            NameTextBox.Focus();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            if (NameTextBox.Text.Length > 0) {
                if (Names.Count + 1 > 7) {
                    MessageBox.Show("The maximum number of players is 7.", "Additional Player Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }

                Names.Add(NameTextBox.Text);
                NameListBox.Items.Add(NameTextBox.Text);
                NameTextBox.Clear();
                NameTextBox.Focus();
            }
        }

        private void NameTextBox_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                AddButton_Click(null, null);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            string selectedName = (string)NameListBox.SelectedItem;

            if (selectedName != null) {
                Names.Remove(selectedName);

                NameListBox.Items.Clear();

                foreach (string name in Names) {
                    NameListBox.Items.Add(name);
                }
            }
        }

        private void NameListBox_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Delete) {
                DeleteButton_Click(null, null);
            }
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e) {
            if (Names.Count < 2) {
                MessageBox.Show("The minimum number of players is 2.", "Additional Player Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }

            RandomizeWindow randomize = new RandomizeWindow(Names);
            randomize.Show();
            this.Close();
        }
    }
}
