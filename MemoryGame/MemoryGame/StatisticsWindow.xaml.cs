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

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow(GameMenuWindow gameMenuWindow)
        {
            InitializeComponent();
            previousWindow = gameMenuWindow;
            string path = @"C:\Users\rbr72\OneDrive\Desktop\Images\image" + previousWindow.loggedInUser.ID.ToString() + ".jpg";
            StatsAvatarImg.ImageSource = new BitmapImage(new Uri(path));
            StatsMsg.Text = "Welcome,\n" + previousWindow.loggedInUser.Name + "!\nThese are your stats: ";
            GamesPlayedTag.Text = previousWindow.loggedInUser.gamesPlayed.ToString();
            GamesWonTag.Text = previousWindow.loggedInUser.gamesWon.ToString();
            HighestLevelTag.Text = previousWindow.loggedInUser.highestLevel.ToString();
        }

        private GameMenuWindow previousWindow;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            previousWindow.Show();
        }
    }
}
