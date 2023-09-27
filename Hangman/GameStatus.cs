using System;

namespace Hangman
{
   internal class GameStatus
   {
        public static void Main(string[] args)
        {


        }

        public static Boolean OutOfGuesses(int number_of_guesses, int word_len)
        {
            if (number_of_guesses < word_len)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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

        public static Boolean IsGuessValid(char letter_guess, List<char> l)
        {
            foreach (char guess in l)
            {
                if (guess == letter_guess)
                {
                    return false;
                }
            }
            return true;
        }
    }
}