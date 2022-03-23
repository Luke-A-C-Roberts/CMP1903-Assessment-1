using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMP1903M_Assessment_1.Debugging;

namespace CMP1903M_Assessment_1
{
    /// <summary>
    /// Stores and analyses the user input
    /// </summary>
    public class Analyse
    {
        private string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            private set { _text = value; }
        }

        public Analyse(string text)
        {
            Text = text;
        }

        readonly char[] Vowels = { 'a', 'e', 'i', 'o', 'u' };

        /*
        0. Sentence Count
        1. Vowels Count
        2. Sonsonants Count
        3. Upper Case Letters Count
        4. Lower Case Letters Count
        */
        public int[] Statistics = new int[5];

        public int[] LetterFrequencies = new int[26]; // add 97 to the index to get the char value

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int[] AnalyseText()
        {
            int[] values = new int[5];
            values.DefaultIfEmpty<int>(0);

            char[] inputChars = Text.ToCharArray();

            #region sentences
            List<string> sentences = new List<string>();
            string sentenceProgress = "";
            foreach (char c in inputChars)
            {
                if (c == '!' || c == '.' || c == '?')
                {
                    sentences.Add(sentenceProgress);
                    sentenceProgress = "";
                    continue;
                }
                sentenceProgress += c.ToString();
            }
            // string[] sentences = Text.Split('.');
            foreach (string s in sentences)
            {
                if (s.Trim().Length != 0) { values[0]++; };
            }
            #endregion

            #region characters
            int[] characterFrequencies = new int[26];
            foreach (char c in inputChars)
            {
                if (Vowels.Contains(Char.ToLower(c))) { values[1]++; } // increments vowels' count
                else if (!(Vowels.Contains(Char.ToLower(c))) && Char.IsLetter(c)) { values[2]++; }; // increments consonants' count

                if (Char.IsUpper(c)) { values[3]++; } // increments upper case count
                else { values[4]++; } // increments lower case count

                // TODO: Count freq. of characters and stuff
                if (Char.IsLetter(c)) {
                    characterFrequencies[Convert.ToInt32(Char.ToLower(c)) - 97]++;
                }
            }

            LetterFrequencies = characterFrequencies;
            Statistics = values;
            return values;
            #endregion
        }

        private const int LongWordThreshold = 7;

        public List<string> GetLongWords()
        {
            List<string> longWordsList = new List<string>();

            string wordProgress = "";
            foreach (char c in Text.ToCharArray())
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    // not a letter so split the word
                    if (wordProgress.Length > LongWordThreshold)
                    {
                        // word is long
                        // TODO: Could check if the word is already added to not have duplicates?
                        longWordsList.Add(wordProgress);
                    }
                    wordProgress = "";
                    continue;
                }
                wordProgress += c.ToString();
            }

            return longWordsList;
        }
    }
}
