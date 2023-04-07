using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        IDictionary<int, Brush> colourCodes = new Dictionary<int, Brush>()
        {
            {1, Brushes.Yellow},
            {2, Brushes.Red},
            {3, Brushes.Blue},
            {4, Brushes.Purple},
            {5, Brushes.Orange},
            {6, Brushes.Green},
            {7, Brushes.Brown},
            {8, Brushes.Cyan}
        };
        public GameWindow(GameMenuWindow gameMenuWindow, bool isNewGame)
        {
            InitializeComponent();
            previousWindow = gameMenuWindow;
            this.isNewGame = isNewGame;
            resetGame();
        }

        private GameMenuWindow previousWindow;
        public string gameCode;

        private int currentLevel = 0;
        private int pairsLeft;

        private Button previousButton1;
        private Button previousButton2;
        private int previousIndex;
        private bool pressed = false;
        private bool isNewGame = false;

        private void initializeButtons()
        {
            for(int index_line = 0; index_line < 4; index_line++)
            {
                for(int index_column = 0; index_column < 4; index_column++)
                {
                    Button newButton = new Button();
                    newButton.Name = "Button" + (4 * index_line + index_column).ToString();
                    newButton.Click += new RoutedEventHandler(button_Click);


                    newButton.Style = this.FindResource("buttonTemp") as Style;
                    Grid.SetRow(newButton, index_line);
                    Grid.SetColumn(newButton, index_column);

                    if (gameCode[4 * index_line + index_column] == '0')
                    {
                        newButton.Background = Brushes.Transparent;
                        newButton.BorderBrush = Brushes.Transparent;
                        newButton.Cursor = Cursors.Arrow;
                        newButton.IsEnabled = false;
                    }

                    ButtonsGrid.Children.Add(newButton);
                }
            }
        }

        private void SaveGameButton_Click(object sender, RoutedEventArgs e)
        {
            previousWindow.loggedInUser.lastGame = gameCode;
            previousWindow.loggedInUser.lastLevel = currentLevel;
            previousWindow.loggedInUser.updateData();
            this.Close();
            previousWindow.Show();
        }

        private void resetGame()
        {   

            if (isNewGame == true)
            {
                
                if (currentLevel % 3 == 0 && currentLevel > 0)
                {
                    previousWindow.loggedInUser.gamesWon++;
                    previousWindow.loggedInUser.gamesPlayed++;
                }
                currentLevel++;

                if (currentLevel > previousWindow.loggedInUser.highestLevel)
                {
                    previousWindow.loggedInUser.highestLevel = currentLevel;
                }

                gameCode = "1122334455667788";
                pairsLeft = gameCode.Length / 2;
                Random num = new Random();
                gameCode = new string(gameCode.ToCharArray().OrderBy(s => (num.Next(2) % 2) == 0).ToArray());
            }
            else
            {
                gameCode = previousWindow.loggedInUser.lastGame;
                currentLevel = previousWindow.loggedInUser.lastLevel;
                int notZeros = 0;
                foreach(char ch in gameCode)
                {
                    if(ch != '0')
                    {
                        notZeros++;
                    }
                }

                pairsLeft = notZeros / 2;
                isNewGame = true;
            }

            LevelTag.Text = currentLevel.ToString();
            PairsTag.Text = pairsLeft.ToString();

            initializeButtons();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button pressedButton = sender as Button;

            string nr = Regex.Replace(pressedButton.Name.ToString(), "[^0-9.]", "");
            int button_index = int.Parse(nr);
            int colourCode = Convert.ToInt32(new string(gameCode[button_index], 1));

            if (pressed == false)
            {
                pressed = true;
                previousIndex = button_index;

                pressedButton.Background = colourCodes[colourCode];

                if(previousButton1 != null && previousButton2 != null)
                {
                    previousButton1.Background = Brushes.Black;
                    previousButton2.Background = Brushes.Black;
                    //previousButton1.Style = this.FindResource("buttonTemp") as Style;
                    //previousButton2.Style = this.FindResource("buttonTemp") as Style;
                    //TO BE FIXED!

                    previousButton1 = null;
                    previousButton2 = null;
                }

                previousButton1 = pressedButton;
            }
            else
            {   
                if(button_index != previousIndex)
                {
                    pressed = false;

                    if (colourCode == Convert.ToInt32(new string(gameCode[previousIndex], 1)))
                    {
                        previousButton1.Background = Brushes.Transparent;
                        previousButton1.BorderBrush = Brushes.Transparent;
                        previousButton1.Cursor = Cursors.Arrow;
                        previousButton1.IsEnabled = false;
                        pressedButton.Background = Brushes.Transparent;
                        pressedButton.BorderBrush = Brushes.Transparent;
                        pressedButton.Cursor = Cursors.Arrow;
                        pressedButton.IsEnabled = false;

                        for (int index_code = 0; index_code < gameCode.Length; index_code++)
                        {
                            if (index_code == previousIndex || index_code == button_index)
                            {
                                gameCode = gameCode.Remove(index_code, 1).Insert(index_code, "0");
                            }
                        }

                        pairsLeft--;
                        PairsTag.Text = pairsLeft.ToString();

                        if (pairsLeft == 0)
                        {
                            resetGame();
                        }

                        previousButton1 = null;
                    }
                    else
                    {
                        pressedButton.Background = colourCodes[colourCode];

                        if (previousButton1 != null)
                        {
                            previousButton2 = pressedButton;
                        }
                    }
                }
            }
        }

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
            previousWindow.loggedInUser.gamesPlayed++;
            previousWindow.loggedInUser.updateData();
            this.Close();
            previousWindow.Show();
        }
    }
}
