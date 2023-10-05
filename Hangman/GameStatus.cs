using System;
using System.Collections.Generic;

namespace Hangman
{
   internal class GameStatus
   {
        // Checks if the player is out of guesses
        public static Boolean OutOfGuesses(int number_of_guesses)
        {
            if (number_of_guesses <= 8)
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

            
            foreach (char guess in l)
            {
                if (guess == letter_guess)
                {
                    count += 1;
                }
            }
                
            if (count == 0 && char.IsLetter(letter_guess))
            {
                return true;
            }

            

            return false;
           
        }

        public static Boolean GuessInCorrectList(char letter_guess, List<char> l)
        {
            int count = 0;


            foreach (char guess in l)
            {
                if (guess == letter_guess)
                {
                    count += 1;
                }
            }

            if (count != 0)
            {
                return true;
            }
            return false;
        }


        // returns guess as upper case char if guess is valid

        public static char? ReturnUpperCase(string guess, List<char> l, List<char> d)
        {
            guess = guess.Trim().ToUpper();
            if (guess.Length == 0 || guess.Length > 1)
            {
                return null;
            }
            if (GuessInCorrectList(char.Parse(guess), d)){
                return null;
            }
                
            
            if (IsGuessValid(guess[0], l))
            {
                return guess[0];
            }

            return null;
        }
    }
}