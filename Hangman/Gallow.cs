using System;
using System.Collections.Generic;

namespace Hangman
{
    internal class Gallow
    {

        // takes the number of wrong guesses(int) and display the gallow
        public static string DrawGallow(int a)
        {
            string[] Gallow = { "", "   \n    |       \n    |        \n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |       \n    |        \n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        \n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |        |\n    |        |\n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | \n    |        |\n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | \n    |        |\n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | /\n    |        |\n    |       /\n    |\n____|___\n\n", $"   ___________\n    |/       |\n    |        O\n    |      \\ | /\n    |        |\n    |       / \\\n    |\n____|___     {ANSIColor.BAD}Good Game! You are dead!{ANSIColor.RESET}\n\n" };
            return Gallow[a];
        }

        public static List<int> GetLetterIndexes(char guess, string word, ref List<char> incorrectGuesses)
        {
            int count = 0;
            List<int> letterIndexes = new();

            int i = 0;
            foreach(var g in word)
            {
                if (guess == g)
                {
                    count += 1;
                    letterIndexes.Add(i);
                }

                i += 1;
            }
            if (count == 0){
                incorrectGuesses.Add(guess);
            }

            return letterIndexes;
        }

        public static Boolean IsNumValid(string b)
        {
            if (int.TryParse(b, out _) == true  )
            {
                int B = Convert.ToInt32(b);
                if (B > 1)
                {
                    return true;
                }
                
            }
            return false;
        }
    }


}