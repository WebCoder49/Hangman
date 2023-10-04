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

                // TODO: Add multiplayer functionality / choose 1 word for 1 player
                string? word = wordPicker.PickWord(difficulty);
                if (word == null)
                {
                    Console.WriteLine("Couldn't get word.");
                }
                else
                {
                    // TODO (cheng-chengccc) Make work with UI asking number of players + allow players to enter other players' words
                    Game(new []{(string)word, (string)wordPicker.PickWord(difficulty)}, 2);
                }
            }
        }

        public static void Game(string[] wordsStr, int numPlayers)
        {
                bool displayUIOnly = false; // Show UI without possibility of entering letter after each go for multiplayer mode when true
                int roundNumber = 1; // 1 letter guessed (correct or wrong) in 1st round
                bool stillPlayingThisRound = true; // True if players are still playing this round
                
                // Arrays - one item per player
                int[] numRoundsToWin = new int[numPlayers]; // Each item represents players in order, and is 0 if they haven't finished, -1 if they ran out of guesses and the number of guesses it took them to finish otherwise.
                List<char>[] word = new List<char>[numPlayers];
                char[][] guessedWord = new char[numPlayers][];
                int[] guesses = new int[numPlayers];
                List<char>[] wrongLetters = new List<char>[numPlayers];
                
                for (int player = 0; player < numPlayers; player++)
                {
                    // Set up variables
                    numRoundsToWin[player] = 0;
                    guesses[player] = 0;
                    wrongLetters[player] = new();
                    word[player] =  new List<char>(wordsStr[player]);
                    // Copy length of word to guessedWord as underscore characters
                    guessedWord[player] = new char[word[player].Count];
                    for (int i = 0; i < word[player].Count; i++)
                    {
                        guessedWord[player][i] = '_';
                    }
                }
                
                int playerI = 0; // 0-indexed
                while (true) // Return to break out of loop
                {
                    Console.Clear();

                    // TODO (ahe127) Display letters and dashes
                    if (playerI >= numPlayers && !displayUIOnly)
                    {
                        if (!stillPlayingThisRound)
                        {
                            // No more players playing
                            if (numPlayers > 1)
                            {
                                DisplayLeaderboard(numRoundsToWin);
                            }
                            return;
                        }
                        playerI = 0;
                        // Next round
                        roundNumber++;
                        stillPlayingThisRound = false;
                    }

                    if (numRoundsToWin[playerI] != 0)
                    {
                        // Finished game - go on to next player
                        playerI++;
                        continue;
                    }
                    stillPlayingThisRound = true;

                    // Display player number with a hashtag UI.
                    string hashtags = "";
                    int numHashtagsBefore = (19 * playerI) / numPlayers;
                    int numHashtagsAfter = (19 * (numPlayers - playerI - 1)) / numPlayers;
                    int numHashtagsHighlighted = 19 - numHashtagsBefore - numHashtagsAfter;
                    for (int i = 0; i < numHashtagsBefore; i++)
                    {
                        hashtags += '#';
                    }
                    hashtags += ANSIColor.DISPLAY;
                    for (int i = 0; i < numHashtagsHighlighted; i++)
                    {
                        hashtags += '#';
                    }
                    hashtags += ANSIColor.RESET;
                    for (int i = 0; i < numHashtagsAfter; i++)
                    {
                        hashtags += '#';
                    }
                    string leftHashtag = "#";
                    if (numHashtagsBefore == 0) leftHashtag = ANSIColor.DISPLAY + leftHashtag + ANSIColor.RESET;
                    string rightHashtag = "#";
                    if (numHashtagsAfter == 0) rightHashtag = ANSIColor.DISPLAY + rightHashtag + ANSIColor.RESET;
                    Console.WriteLine($"{hashtags}\n{leftHashtag} Player {ANSIColor.DISPLAY}{playerI + 1}{ANSIColor.RESET}'s turn {rightHashtag}\n{hashtags}\n");
                    
                    // Display wrong letters already guessed
                    Console.Write($"Wrong letters:   {ANSIColor.BAD}");
                    Console.Write(String.Join($"{ANSIColor.RESET}, {ANSIColor.BAD}", wrongLetters[playerI]));
                    Console.WriteLine($"{ANSIColor.RESET}");
                    
                    // Display correct letters
                    Console.Write($"Correct letters: ");
                    for (int i = 0; i < guessedWord[playerI].Length; i++)
                    {
                        if (guessedWord[playerI][i] == '_')
                        {
                            Console.Write("_ ");
                        }
                        else
                        {
                            Console.Write(ANSIColor.GOOD+guessedWord[playerI][i]+" "+ANSIColor.RESET);
                        }
                    }
                    Console.WriteLine();

                    Console.Write(Gallow.DrawGallow(guesses[playerI]));

                    if (GameStatus.WordGuessed(new String(guessedWord[playerI]), wordsStr[playerI]))
                    {
                        Console.WriteLine($"{ANSIColor.GOOD}You Guessed Correctly!{ANSIColor.RESET}");
                        numRoundsToWin[playerI] = roundNumber;
                        playerI++;
                        WaitForKeypressGameEnded(numPlayers);
                        continue; // Won
                    }

                    if (displayUIOnly)
                    {
                        // Just show UI.
                        playerI++;
                        
                        Console.Write($"{ANSIColor.PROMPT}Press ANY KEY for player {(playerI + 2)%numPlayers}: {ANSIColor.RESET}"); // Next player: +1 as 0-indexed to 1-indexed; +1 as next player; %numPlayers so turns work
                        Console.ReadKey();
                        displayUIOnly = false;
                        continue; // Next iteration - don't let guess be inputted.
                    }

                    Console.Write($"{ANSIColor.PROMPT}Enter your guess for a letter:{ANSIColor.RESET} ");
                    char guessedLetter = Console.ReadLine().ToUpper()[0];
                    // TODO (SophiaNass) Validate

                    if (GameStatus.IsGuessValid(guessedLetter, wrongLetters[playerI]))
                    {

                        List<int> letterIndexes = GetLetterIndexes(guessedLetter, word[playerI]);
                        for (int i = 0; i < letterIndexes.Count; i++)
                        {
                            guessedWord[playerI][letterIndexes[i]] = guessedLetter;
                        }

                        if (letterIndexes.Count == 0)
                        {
                            // Letter not in word
                            wrongLetters[playerI] = wrongLetters[playerI].Append(guessedLetter).ToList();

                            guesses[playerI]++;
                            if (GameStatus.OutOfGuesses(guesses[playerI]))
                            {
                                Console.Clear();
                                Console.WriteLine($"The word was:\n\t{ANSIColor.DISPLAY}{wordsStr[playerI]}{ANSIColor.RESET}");
                                Console.Write(Gallow.DrawGallow(guesses[playerI]));
                                numRoundsToWin[playerI] = -1;
                                playerI++;
                                WaitForKeypressGameEnded(numPlayers);
                                continue; // Lost
                            }
                        }
                    }

                    if (numPlayers > 1)
                    {
                        // Show result with a UI-only, no-input loop after guess in multiplayer
                        displayUIOnly = true;
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

            public static void WaitForKeypressGameEnded(int numPlayers)
            {
                
                if (numPlayers > 1)
                {
                    Console.Write($"{ANSIColor.PROMPT}Press ANY KEY to let the other players keep playing:{ANSIColor.RESET} ");
                }
                else
                {
                    Console.Write($"{ANSIColor.PROMPT}Press ANY KEY to end the game:{ANSIColor.RESET} ");
                }
                Console.ReadKey();
            }

            public static void DisplayLeaderboard(int[] numRounds)
            {
                
                // numRounds: Each item represents players in order,
                // and is 0 if they haven't finished, -1 if they ran
                // out of guesses and the number of guesses it took
                // them to finish otherwise.
                
                // Move num rounds per player to num rounds indexed where each item is tuple (numRounds, player index)
                Tuple<int,int>[] numRoundsIndexed = new Tuple<int,int>[numRounds.Length];
                for (int i = 0; i < numRounds.Length; i++)
                {
                    numRoundsIndexed[i] = new Tuple<int, int>(numRounds[i], i);
                }
                
                Array.Sort<Tuple<int,int>>(numRoundsIndexed, (a, b) =>
                {
                    // Descending order so returned-value meanings reversed
                    if (a.Item1 == b.Item1)
                    {
                        return 0;
                    }
                    else if (a.Item1 < b.Item1)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                });
                for (int i = 0; i < numRounds.Length; i++)
                {
                    if (numRoundsIndexed[i].Item1 == -1)
                    {
                        Console.WriteLine($"Player {numRoundsIndexed[i].Item2+1}: {ANSIColor.DISPLAY}Ran out of guesses{ANSIColor.RESET}");
                    }
                    else
                    {
                        Console.WriteLine($"Player {numRoundsIndexed[i].Item2+1}: took {ANSIColor.DISPLAY}{numRoundsIndexed[i].Item1}{ANSIColor.RESET} guesses");
                    }
                }
            }
    }
}