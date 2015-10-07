using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hangman
{
    public partial class StartForm : Form
    {
        private string word;
        private string currentWord;
        private int pictureCount = 1;

        public StartForm()
        {
            InitializeComponent();
            exitButton2.Visible = false;
            buttonNewGame.Visible = false;
            inputTextBox.ReadOnly = false;
            GameEngine();
        }

        public Dictionary<string, int> ReadWordsFromFile()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            string line;
            StreamReader file = new StreamReader("../../Resources/dictionary.txt");
            int count = 1;

            while ((line = file.ReadLine()) != null)
            {
                dictionary.Add(line, count);
                count++;
            }
            
            file.Close();
            return dictionary;
        }

        public string getRandomWord()
        {
            Dictionary<string, int> dictionary = ReadWordsFromFile();
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, dictionary.Count + 1);
            string randomWord = dictionary.FirstOrDefault(x => x.Value == randomNumber).Key;
            return randomWord;
        }


        public void GameEngine()
        {
            pictureCount = 1;
            word = getRandomWord();
            currentWord = new string('*', word.Length);
            wordLabel.Text = currentWord;
            pictureBox.Image = Image.FromFile("../../Resources/0.jpg");
            exitButton2.Visible = false;
            buttonNewGame.Visible = false;
            inputTextBox.ReadOnly = false;
            inputTextBox.Text = "";
            
        }

        private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            string currentChar = e.KeyCode.ToString();
            if (word.ToUpper().Contains(currentChar))
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word.ToUpper()[i].ToString() == currentChar)
                    {
                        currentWord = currentWord.Substring(0, i) + currentChar + currentWord.Substring(i + 1, currentWord.Length - i - 1);
                    }
                }

                wordLabel.Text = currentWord;
                inputTextBox.Text = "";
                if (IsWin())
                {
                    Win();
                }
            }
            else
            {
                if (IsWin())
                {
                    Win();
                }
                if (pictureCount == 7 )
                {
                    Lose();
                    return;
                }
                inputTextBox.Text = "";
                pictureBox.Image = Image.FromFile("../../Resources/" + pictureCount + ".jpg");
                pictureCount++;
            }
        }

        private void Lose()
        {
            ResetData();
            buttonNewGame.Focus();
            pictureBox.Image = Image.FromFile("../../Resources/lose.jpg");
            playSound("lose.wav");
        }

        private void Win()
        {
            buttonNewGame.Focus();
            pictureBox.Image = Image.FromFile("../../Resources/win.jpg");
            ResetData();
            playSound("win.wav");

        }

        private void playSound(string soundName)
        {
            using (var soundPlayer = new SoundPlayer("../../Resources/" + soundName))

            {
                soundPlayer.Play(); // can also use soundPlayer.PlaySync()
            }
        }

        private void ResetData()
        {
            exitButton2.Visible = true;
            buttonNewGame.Visible = true;
            inputTextBox.ReadOnly = true;
            pictureCount = 1;
            return;
        }

        private bool IsWin()
        {
            bool isEqual = (word.ToUpper().Equals(currentWord));
            return isEqual;
        }

        private void exitButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            GameEngine();
        }
    }
}
