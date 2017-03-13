﻿using System;
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
using System.Windows.Threading;
using Wheel_Of_Fortune.Board;
using Wheel_Of_Fortune.Prizes;

namespace Wheel_Of_Fortune.NewGame {
    /// <summary>
    /// Interaction logic for RandomizeWindow.xaml
    /// </summary>
    public partial class RandomizeWindow : Window {

        List<string> Names;
        Random rnd = new Random();
        int previous = -1;
        int startingPlayer = -1;

        DispatcherTimer smallTimer;

        public RandomizeWindow(List<string> names) {
            InitializeComponent();
            Names = names;

            DescriptText.Text = "The dice is rolling... Who is going to be first?";
            StartButton.IsEnabled = false;

            smallTimer = new DispatcherTimer(DispatcherPriority.Normal);
            smallTimer.Interval = TimeSpan.FromMilliseconds(100);

            DispatcherTimer largeTimer = new DispatcherTimer();
            largeTimer.Interval = TimeSpan.FromMilliseconds(4000);

            smallTimer.Tick += SmallTimer_Tick;
            largeTimer.Tick += LargeTimer_Tick;

            smallTimer.IsEnabled = true;
            largeTimer.IsEnabled = true;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e) {
            PrizeWindow window = new PrizeWindow();
            this.Hide();
            window.ShowDialog();            
        }

        internal List<Player> GetPlayerList() {
            List<Player> players = new List<Player>();

            players.Add(new Player(Names[startingPlayer]));
            players[0].IsStartingPlayer = true;
            Names.RemoveAt(startingPlayer);

            while (Names.Count != 0) {
                int rndIndex = rnd.Next(0, Names.Count);
                players.Add(new Player(Names[rndIndex]));
                Names.RemoveAt(rndIndex);
            }

            return players;            
        }

        private void LargeTimer_Tick(object sender, EventArgs e) {
            smallTimer.IsEnabled = false;
            DescriptText.Text = "With the roll of a dice, it looks like " + NameText.Text + " will be starting us off!";
            StartButton.IsEnabled = true;
        }

        private void SmallTimer_Tick(object sender, EventArgs e) {
            int randomIndex = rnd.Next(0, Names.Count);

            while (randomIndex == previous) {
                randomIndex = rnd.Next(0, Names.Count);
            }

            NameText.Text = Names[randomIndex];
            startingPlayer = randomIndex;
        }
    }
}
