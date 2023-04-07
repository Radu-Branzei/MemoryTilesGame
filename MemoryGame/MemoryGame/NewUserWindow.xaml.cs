using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        public NewUserWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            previousWindow = mainWindow;
        }

        private MainWindow previousWindow;
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
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UserName.Text;
            string filePath = "C:\\Users\\rbr72\\OneDrive\\Desktop\\Semestrul 2\\Retele Calculatoare\\TEMA1_CPP\\MemoryGame\\MemoryGame\\users_info.txt";
            try
            {
                string output = "";
                int newID = -1;

                List<string> lines = new List<string>();
                List<int> IDs = new List<int>();
                lines = File.ReadAllLines(filePath).ToList();

                foreach (string line in lines)
                {
                    string[] split_data = line.Split(' ');
                    IDs.Add(int.Parse(split_data[1]));
                    output += line + '\n';
                }

                IDs.Sort();

                int prev = 0;
                foreach (int ID in IDs)
                {
                    if (ID > prev + 1)
                    {
                        newID = prev + 1;
                        break;
                    }
                    prev = ID;
                }
                if (newID == -1)
                {
                    newID = prev + 1;
                }

                previousWindow.users.Add(new User(username, newID, 0, 0, "0", 0, 0));
                output += username + " " + newID.ToString() + " 0 0 0 0 0\n";

                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            this.Close();
        }
    }
}
