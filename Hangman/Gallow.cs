using System;
using System.Collections.Generic;

namespace Hangman
{
    internal class Gallow
    {

        // takes the number of wrong guesses(int) and display the gallow
        public static string DrawGallow(int a)
        {
            string[] Gallow = { "", "   \n    |       \n    |        \n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |       \n    |        \n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        \n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |        |\n    |        |\n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | \n    |        |\n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | \n    |        |\n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | /\n    |        |\n    |       /\n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | /\n    |        |\n    |       / \\\n    |\n____|___     Good Game! You are dead!\n\n" };
            return Gallow[a];
        }

        public static List<char> ListOfIncorrectGuesses(char guess, string word)
        {
            int count = 0;
            List<char> incorrectGuesses = new List<char>();
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