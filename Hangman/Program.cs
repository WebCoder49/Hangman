using System;

namespace Hangman
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //foreach(var i in (Gallow.ListOfIncorrectGuesses('h', "gallow")))
            //{
                //Console.WriteLine(i);
            //}
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
            // Game loop
            while (true)
            {
                Console.Write($"\n###########\n# {ANSIColor.DISPLAY}HANGMAN{ANSIColor.RESET} #\n###########\n\n");
                // Ask for difficulty
                WordPicker.Difficulty difficulty = WordPicker.Difficulty.Unset;
                while (difficulty == WordPicker.Difficulty.Unset)
                {
                    Console.Write($"{ANSIColor.PROMPT}Word difficulty ([E]asy, [M]edium, [H]ard, or [Q]uit Game):{ANSIColor.RESET} ");
                    string difficultyStr = Console.ReadLine().Trim().ToUpper();
                    if (difficultyStr == "EASY" || difficultyStr == "E")
                    {
                        difficulty = WordPicker.Difficulty.Easy;
                    }
                    else if (difficultyStr == "MEDIUM" || difficultyStr == "M")
                    {
                        difficulty = WordPicker.Difficulty.Medium;
                    } else if (difficultyStr == "HARD" || difficultyStr == "H")
                    {
                        difficulty = WordPicker.Difficulty.Hard;
                    } else if (difficultyStr == "QUIT GAME" || difficultyStr == "QUIT" || difficultyStr == "Q")
                    {
                        return;
                    }
                }

                string? word = wordPicker.PickWord(difficulty);
                if (word == null)
                {
                    Console.WriteLine("Couldn't get word.");
                }
                else
                {
                    Game((string)word);
                }
            }
        }

        public static void Game(string wordStr)
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
                List<char> correctLetters = new();
                while (true) // Return to break out of loop
                {
                    Console.Clear();

                    // TODO (ahe127) Display letters and dashes
                    Console.Write($"Wrong letters:   {ANSIColor.BAD}");
                    Console.Write(String.Join($"{ANSIColor.RESET}, {ANSIColor.BAD}", wrongLetters));
                    Console.WriteLine($"{ANSIColor.RESET}");
                    Console.Write($"Correct letters: ");
                    for (int i = 0; i < guessedWord.Length; i++)
                    {
                        if (guessedWord[i] == '_')
                        {
                            Console.Write("_");
                        }
                        else
                        {
                            Console.Write(ANSIColor.GOOD+guessedWord[i]+ANSIColor.RESET);
                        }
                    }
                    Console.WriteLine();

                    Console.Write(Gallow.DrawGallow(guesses));

                    if (GameStatus.WordGuessed(new String(guessedWord), wordStr))
                    {
                        Console.WriteLine($"{ANSIColor.GOOD}You Guessed Correctly!{ANSIColor.RESET}");
                        return; // Won
                    }

                    Console.Write($"{ANSIColor.PROMPT}Enter your guess for a letter:{ANSIColor.RESET} ");
                    char? guessedLetterOrNull = GameStatus.ReturnUpperCase(Console.ReadLine(), wrongLetters, correctLetters);
                    while(guessedLetterOrNull == null){
                        Console.Write($"{ANSIColor.PROMPT}That was invalid or already used. Please enter it again: {ANSIColor.RESET}");
                        guessedLetterOrNull = GameStatus.ReturnUpperCase(Console.ReadLine(), wrongLetters, correctLetters);
                    }
                    // TODO (SophiaNass) Validate guessedLetter
                    char guessedLetter = (char)guessedLetterOrNull;
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
                        if (GameStatus.OutOfGuesses(guesses))
                        {
                            Console.Clear();
                            Console.WriteLine($"The word was:\n\t{ANSIColor.DISPLAY}{wordStr}{ANSIColor.RESET}");
                            Console.Write(Gallow.DrawGallow(guesses));
                            return; // Lost
                        }
                    }
                    else
                    {
                        correctLetters = correctLetters.Append(guessedLetter).ToList();

                    }
                }
                }
            }

            public static List<int> GetLetterIndexes(char chr, List<char> word)
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