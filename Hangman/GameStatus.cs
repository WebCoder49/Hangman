using System;

namespace Hangman
{
   internal class GameStatus
   {
        // Checks if the player is out of guesses
        public static Boolean OutOfGuesses(int number_of_guesses)
        {
            if (number_of_guesses <= 9)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Checks if the word has been guessed
        public static Boolean WordGuessed(string guess, string word)
        {
            if (guess == word)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks if the guess is valid
        public static Boolean IsGuessValid(char letter_guess, List<char> l)
        {
            int count = 0;
            if (letter_guess is char)
            {
                foreach (char guess in l)
                {
                    if (guess == letter_guess)
                    {
                        count += 1;
                    }
                }
                
                if (count == 0)
                {
                    return true;
                }

            }

            return false;
           
        }
    }
}