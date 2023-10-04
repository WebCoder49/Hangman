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

        public static List<char> UpdateIncorrectGuesses(char guess, string word, List<char> incorrectGuesses)
        {
            int count = 0;
            char.ToString(guess);
            
            

            foreach(var g in word)
            {
                if (guess == g)
                {
                    count += 1;
                    
                }
            }
            if (count == 0){
                incorrectGuesses.Add(guess);
            }

            return incorrectGuesses;
        }
    }


}