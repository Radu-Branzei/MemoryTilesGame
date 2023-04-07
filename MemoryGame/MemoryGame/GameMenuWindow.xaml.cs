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
    /// Interaction logic for GameMenuWindow.xaml
    /// </summary>
    public partial class GameMenuWindow : Window
    {
        public GameMenuWindow(MainWindow mainWindow, User user)
        {
            InitializeComponent();
            previousWindow = mainWindow;
            loggedInUser = user;
        }

        private MainWindow previousWindow;
        public User loggedInUser;


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
            Application.Current.Shutdown();
        }
        private void buttonInfo_Click(object sender, RoutedEventArgs e)
        {
            StudentInfoWindow studentInfoWindow = new StudentInfoWindow();
            studentInfoWindow.Show();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow(this, true);
            this.Hide();
            gameWindow.Show();
        }
        private void OpenGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (loggedInUser.lastGame != "0")
            {
                GameWindow gameWindow = new GameWindow(this, false);
                this.Hide();
                gameWindow.Show();
            }
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow(this);
            this.Hide();
            statisticsWindow.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            previousWindow.Show();
        }
    }
}
