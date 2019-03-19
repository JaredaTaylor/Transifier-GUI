using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Windows.Documents;

namespace TransifierGUI
{
    public partial class TransifierGUI : Form
    {
        /*
         * @param word = string to change
         * @param lang = replacement for selected term
         * @param replace = characters to replace
         * @return String from replace func
         * @func access string of letters being changed
         */ 
        public static string Translator(string word, string lang, string replace)
        {
            word = word.ToLower();
            if (replace == "vowels")
            {
                replace = "aeiou";
            }
            if (replace == "consonants")
            {
                replace = "qwrtypsdfghjklzxcvbnm";
            }
            else
            {
                replace = replace.ToLower();
            }
            return Replacer(word, lang, replace);
        }

        /*
         * @param word = string to change
         * @param lang = replacement for selected term
         * @param replace = characters to replace
         * @return String from replace func
         * @func replaces characters with lang
         */
        public static string Replacer(string word, string lang, string replace)
        {
            int wordLength = word.Length;
            int repLeng = replace.Length;
            string newWord = "";
            word = word.ToLower();
            for (int i = 0; i < wordLength; i++)
            {
                for (int j = 0; j < repLeng; j++)
                {
                    if (word[i] == replace[j])
                    {
                        newWord = newWord + lang;
                        break;
                    }
                    if (j == (replace.Length - 1))
                    {
                        newWord = newWord + word[i];
                    }
                }
            }
            return newWord;
        }

        /*
         * @param sentence or word supplied
         * @return modified sentence
         * @func split sentence and change individual words then reform sentence
         * 
         */
        public static string Splitter(string sent)
        {
            string newSent = "";
            string[] wordList = sent.Split(' ');
            foreach (string word in wordList)
            {
                string word2 = Piglatin(word);
                newSent = newSent + word2 + " ";
            }
            return newSent;
        }

        /*
         * @param original word being tested
         * @return modified word (in pig latin)
         * @func take word split it at the vowel add letters before vowel
         * 
         */
        public static string Piglatin(string word)
        {
            string vow = "aeiou";
            int wordLength = word.Length;
            int vowLeng = vow.Length;
            string newWord = "";
            string wordStart = "";
            string wordEnd = "";
            word = word.ToLower();
            Boolean foundV = false;
            Boolean vFirst = false;
            for (int i = 0; i < wordLength; i++) //letters in word
            {
                for (int j = 0; j < vowLeng; j++) //letters in vowels
                {
                    if (word[i] == vow[j]) //when letter is equal to vowel
                    {
                        if (i == 0) //if its the first letter
                        {
                            vFirst = true;
                            break;
                        }
                        else
                        {
                            foundV = true;
                        }
                    }
                }
                if (vFirst == true) //if first letter is a vowel
                {
                    break;
                }
                if (foundV == true) //if vowel found
                {
                    wordStart = wordStart + word[i];
                }
                else
                {
                    wordEnd = wordEnd + word[i];
                }
            }
            if (vFirst == true) //if first letter vowl
            {
                newWord = word + "way";
            }
            else if (foundV == true) //if vowel found
            {
                newWord = newWord + wordStart + wordEnd + "ay";
            }
            else
            {
                newWord = word + "ay";
            }
            return newWord;
        }

/*
 * @param transWord = final word after translation
 * @func verbally outputs final word
 */
public static void Speech(string transWord)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak(transWord);
        }

        public TransifierGUI()
        {
            InitializeComponent();
        }

        /*
         * @func initializes options for comboBoxes
         */
        private void TransifierGUI_Load(object sender, EventArgs e)
        {
            replace.Items.Add("vowels");
            replace.Items.Add("consonants");
            replace.Items.Add("pig latin");
            replace.SelectedIndex = 0;
            comboBox1.Items.Add("oob");
            comboBox1.Items.Add("erb");
            comboBox1.Items.Add("ooz");
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /*
         * @func accesses validity of combo boxes then calls translator func if they're valid
         *      finally calls speech function using converted word
         */
        private void button1_Click(object sender, EventArgs e)
        {
            string combo = replace.Text.ToString();
            if (textBox1.Text == "")
            {
                MessageBox.Show("No input");
            }
            if (combo == "pig latin")
            {
                comboBox1.Text = "";
                string phrase = textBox1.Text.ToString();
                phrase = Splitter(phrase);
                textBox2.Text = phrase;
                Speech(phrase);
            }
            else
            {
                string sampleWord;
                sampleWord = textBox1.Text;
                string choice = comboBox1.Text.ToString();
                //MessageBox.Show(choice);
                if (choice == "")
                {
                    MessageBox.Show("No choice given for replacement.");
                }
                else
                {
                    string replace = this.replace.Text.ToString();
                    if (replace == "")
                    {
                        MessageBox.Show("No choice given to replace.");
                    }
                    else
                    {
                        string newWrd = Translator(sampleWord, choice, replace);
                        textBox2.Text = newWrd;
                        Speech(newWrd);
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
