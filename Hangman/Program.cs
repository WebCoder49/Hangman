using System;

namespace Hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TEST WordPicker
            // WordPicker wordPicker = new();
            // Console.WriteLine($"Easy: {wordPicker.PickWord(WordPicker.Difficulty.Easy)}, {wordPicker.PickWord(WordPicker.Difficulty.Easy)}, {wordPicker.PickWord(WordPicker.Difficulty.Easy)}, {wordPicker.PickWord(WordPicker.Difficulty.Easy)}, {wordPicker.PickWord(WordPicker.Difficulty.Easy)}, {wordPicker.PickWord(WordPicker.Difficulty.Easy)}, {wordPicker.PickWord(WordPicker.Difficulty.Easy)}\nMedium: {wordPicker.PickWord(WordPicker.Difficulty.Medium)}\nHard: {wordPicker.PickWord(WordPicker.Difficulty.Hard)}");
            // wordPicker.PickQuote();
            // Console.WriteLine($"{wordPicker.GetQuoteAuthor()}: \n{wordPicker.GetQuote()}");

            // TEST GameStatus
            // Console.WriteLine(GameStatus.IsGuessValid('h', new List<char>{ 't', 'i', 'o' }));
            // Console.WriteLine(GameStatus.OutOfGuesses(7));
            // Console.WriteLine(GameStatus.WordGuessed("hello", "hello"));
            // TEST Gallow
            // for (int i = 0; i < 10; i++)
            // {
            //     Console.WriteLine($"Guess #{i}");
            //     Console.Write(Gallow.DrawGallow(i));
            // }

            WordPicker wordPicker = new();
            Game(wordPicker.PickWord(WordPicker.Difficulty.Easy));

            static void Game(string wordStr)
            {
                List<char> word = new(wordStr);
                char[] guessedWord = new char [word.Count];
                // Copy length of word to guessedWord as underscore characters
                for (int i = 0; i < word.Count; i++)
                {
                    guessedWord[i] = '_';
                }

                int guesses = 0;
                List<char> wrongLetters = new();
                while (true) // Return to break out of loop
                {
                    Console.Clear();

                    // TODO (ahe127) Display letters and dashes
                    Console.Write($"Wrong letters:   ");
                    Console.Write(String.Join(", ", wrongLetters));
                    Console.WriteLine();
                    Console.Write($"Correct letters: ");
                    Console.Write(guessedWord);
                    Console.WriteLine();

                    Console.Write(Gallow.DrawGallow(guesses));

                    if (GameStatus.WordGuessed(new String(guessedWord), wordStr))
                    {
                        Console.WriteLine($"You Guessed Correctly!");
                        return; // Won
                    }

                    Console.Write("Enter your guess for a letter: ");
                    char guessedLetter = Console.ReadLine().ToUpper()[0];
                    // TODO (SophiaNass) Validate

                    if (GameStatus.IsGuessValid(guessedLetter, wrongLetters))
                    {

                        List<int> letterIndexes = GetLetterIndexes(guessedLetter, word);
                        for (int i = 0; i < letterIndexes.Count; i++)
                        {
                            guessedWord[letterIndexes[i]] = guessedLetter;
                        }

                        if (letterIndexes.Count == 0)
                        {
                            // Letter not in word
                            wrongLetters = wrongLetters.Append(guessedLetter).ToList();

                            guesses++;
                            if (guesses >= 7)
                            {
                                Gallow.DrawGallow(guesses);
                                return; // Lost
                            }
                        }
                    }
                }
            }

            static List<int> GetLetterIndexes(char chr, List<char> word)
            {
                // TODO (ahe127) replace with your function
                List<int> letterIndexes = new();
                for (int i = 0; i < word.Count; i++)
                {
                    if (word[i] == chr)
                    {
                        letterIndexes = letterIndexes.Append(i).ToList();
                    }
                }

                return letterIndexes;
            }
        }
    }
}