using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            users = new ObservableCollection<User>();

            InitializeComponent();
            initializeUsers();
        }

        public ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get { return users; }
            set { users = value; }
        }

        private void initializeUsers()
        {
            string filePath = "C:\\Users\\rbr72\\OneDrive\\Desktop\\Semestrul 2\\Retele Calculatoare\\TEMA1_CPP\\MemoryGame\\MemoryGame\\users_info.txt";
            try
            {
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(filePath).ToList();

                foreach(string line in lines)
                {
                    string[] split_data = line.Split(' ');
                    users.Add(new User(split_data[0], int.Parse(split_data[1]), int.Parse(split_data[2]),
                        int.Parse(split_data[3]), split_data[4], int.Parse(split_data[5]), int.Parse(split_data[6])));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
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

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersListView.SelectedIndex != -1)
            {
                User selectedUser = UsersListView.SelectedItem as User;
                GameMenuWindow gameMenuWindow = new GameMenuWindow(this, selectedUser);
                this.Hide();
                gameMenuWindow.Show();
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NewUserButton_Click(object sender, RoutedEventArgs e)
        {
            NewUserWindow newUserWindow = new NewUserWindow(this);
            newUserWindow.Show();
        }
        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "C:\\Users\\rbr72\\OneDrive\\Desktop\\Semestrul 2\\Retele Calculatoare\\TEMA1_CPP\\MemoryGame\\MemoryGame\\users_info.txt";
            User selectedUser = UsersListView.SelectedItem as User;
            try
            {
                string output = "";

                List<string> lines = new List<string>();
                lines = File.ReadAllLines(filePath).ToList();

                foreach (string line in lines)
                {
                    string[] split_data = line.Split(' ');
                    if (int.Parse(split_data[1]) != selectedUser.ID)
                    {
                        output += line + '\n';
                    }
                }

                File.WriteAllText(filePath, output);
                users.Remove(selectedUser);

                //string path = @"C:\Users\rbr72\OneDrive\Desktop\Images\image0.jpg";
                //AvatarImg.ImageSource = new BitmapImage(new Uri(path));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selectedUser = UsersListView.SelectedItem as User;
            string path = @"C:\Users\rbr72\OneDrive\Desktop\Images\image" + selectedUser.ID.ToString() + ".jpg";
            AvatarImg.ImageSource = new BitmapImage(new Uri(path));
        }
    }
}
