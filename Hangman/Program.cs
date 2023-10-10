using System;

namespace Hangman
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            WordPicker wordPicker = new();
            // Game loop
            int NumofPlayer = 1;
            while (true)
            {
                Console.Write($"\n###########\n# {ANSIColor.DISPLAY}HANGMAN{ANSIColor.RESET} #\n###########\n\n");
                // Ask for difficulty/mode initially
                WordPicker.Difficulty difficulty = WordPicker.Difficulty.Unset;
                while (difficulty == WordPicker.Difficulty.Unset)
                {
                    Console.Write(
                        $"{ANSIColor.PROMPT}Word difficulty / Action ([E]asy, [M]edium, [H]ard, m[U]ltiplayer or [Q]uit Game):{ANSIColor.RESET} ");
                    string difficultyStr = Console.ReadLine()
                        .Trim()
                        .ToUpper();

                    if (difficultyStr == "EASY" || difficultyStr == "E")
                    {
                        difficulty = WordPicker.Difficulty.Easy;
                    }
                    else if (difficultyStr == "MEDIUM" || difficultyStr == "M")
                    {
                        difficulty = WordPicker.Difficulty.Medium;
                    }
                    else if (difficultyStr == "HARD" || difficultyStr == "H")
                    {
                        difficulty = WordPicker.Difficulty.Hard;
                    }
                    else if (difficultyStr == "QUIT GAME" || difficultyStr == "QUIT" || difficultyStr == "Q")
                    {
                        return;
                    }
                    else if (difficultyStr == "MULTIPLAYER" || difficultyStr == "U" || difficultyStr == "MULTI")
                    {
                        string InputNumofPlayer;
                        Console.Write($"{ANSIColor.PROMPT}How many players? {ANSIColor.RESET}");
                        InputNumofPlayer = Console.ReadLine();
                        while (!Gallow.IsNumValid(InputNumofPlayer) == true)
                        {
                            Console.Write($"{ANSIColor.PROMPT}Please enter a valid number. {ANSIColor.RESET}");
                            InputNumofPlayer = Console.ReadLine();
                        }

                        // Get difficulty for multiplayer game
                        while (difficulty == WordPicker.Difficulty.Unset)
                        {
                            Console.Write(
                                $"{ANSIColor.PROMPT}Word difficulty for multiplayer ([E]asy, [M]edium or [H]ard):{ANSIColor.RESET} ");
                            difficultyStr = Console.ReadLine()
                                .Trim()
                                .ToUpper();

                            if (difficultyStr == "EASY" || difficultyStr == "E")
                            {
                                difficulty = WordPicker.Difficulty.Easy;
                            }
                            else if (difficultyStr == "MEDIUM" || difficultyStr == "M")
                            {
                                difficulty = WordPicker.Difficulty.Medium;
                            }
                            else if (difficultyStr == "HARD" || difficultyStr == "H")
                            {
                                difficulty = WordPicker.Difficulty.Hard;
                            }
                        }

                        NumofPlayer = Convert.ToInt32(InputNumofPlayer);
                    }
                }

                string[] wordsStr = new string[NumofPlayer];
                for (int i = 0;
                     i < NumofPlayer;
                     i++)
                {
                    string? word = wordPicker.PickWord(difficulty);
                    if (word == null)
                    {
                        Console.WriteLine($"{ANSIColor.BAD}Couldn't get word.{ANSIColor.RESET}");
                        break;
                    }

                    wordsStr[i] = word;
                }

                Game(wordsStr,
                    NumofPlayer);
            }
        }

        public static void Game(string[] wordsStr,
            int numPlayers)
        {
            bool
                displayUIOnly =
                    false; // Show UI without possibility of entering letter after each go for multiplayer mode when true
            int roundNumber = 1; // 1 letter guessed (correct or wrong) in 1st round
            bool stillPlayingThisRound = true; // True if players are still playing this round

            // Arrays - one item per player
            int[]
                numRoundsToWin =
                    new int[numPlayers]; // Each item represents players in order, and is 0 if they haven't finished, -1 if they ran out of guesses and the number of guesses it took them to finish otherwise.
            List<char>[] word = new List<char>[numPlayers];
            char[][] guessedWord = new char[numPlayers][];
            int[] guesses = new int[numPlayers];
            List<char>[] wrongLetters = new List<char>[numPlayers];

            for (int player = 0;
                 player < numPlayers;
                 player++)
            {
                // Set up variables
                numRoundsToWin[player] = 0;
                guesses[player] = 0;
                wrongLetters[player] = new();
                word[player] = new List<char>(wordsStr[player]);
                // Copy length of word to guessedWord as underscore characters
                guessedWord[player] = new char[word[player].Count];
                for (int i = 0;
                     i < word[player].Count;
                     i++)
                {
                    guessedWord[player][i] = '_';
                }
            }

            int playerI = 0; // 0-indexed
            while (true) // Return to break out of loop
            {
                Console.Clear();

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

                DisplayGameHeader(displayUIOnly,
                    playerI,
                    numPlayers);
                DisplayGameUI(wrongLetters[playerI],
                    guessedWord[playerI],
                    guesses[playerI]);

                if (GameStatus.WordGuessed(new String(guessedWord[playerI]),
                        wordsStr[playerI]))
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

                    Console.Write(
                        $"{ANSIColor.PROMPT}Press ANY KEY for player {(playerI + 1) % numPlayers}: {ANSIColor.RESET}");
                    Console.ReadKey();
                    displayUIOnly = false;
                    continue; // Next iteration - don't let guess be inputted.
                }

                char guessedLetter = InputGuessedLetter(wrongLetters[playerI],
                    guessedWord[playerI]);

                List<int> letterIndexes =
                    Gallow.GetLetterIndexes(guessedLetter,
                        wordsStr[playerI],
                        ref wrongLetters[playerI]);
                for (int i = 0;
                     i < letterIndexes.Count;
                     i++)
                {
                    // Fill word with guessed letter
                    guessedWord[playerI][letterIndexes[i]] = guessedLetter;
                }

                if (letterIndexes.Count == 0)
                {
                    // Letter not in word - wrong letters already updated
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

                if (numPlayers > 1)
                {
                    // Show result with a UI-only, no-input loop after guess in multiplayer
                    displayUIOnly = true;
                }
            }
        }

        public static void DisplayGameHeader(bool displayUIOnly,
            int playerI,
            int numPlayers)
        {
            if (displayUIOnly)
            {
                // Display UI showing result
                Console.WriteLine(
                    $"#######################\n# {ANSIColor.GOOD}RESULT for Player {playerI + 1}{ANSIColor.RESET} #\n#######################\n");
            }
            else
            {
                // Display player number with a hashtag UI.
                string hashtags = "";
                int numHashtagsBefore = (19 * playerI) / numPlayers;
                int numHashtagsAfter = (19 * (numPlayers - playerI - 1)) / numPlayers;
                int numHashtagsHighlighted = 19 - numHashtagsBefore - numHashtagsAfter;
                for (int i = 0;
                     i < numHashtagsBefore;
                     i++)
                {
                    hashtags += '#';
                }

                hashtags += ANSIColor.DISPLAY;
                for (int i = 0;
                     i < numHashtagsHighlighted;
                     i++)
                {
                    hashtags += '#';
                }

                hashtags += ANSIColor.RESET;
                for (int i = 0;
                     i < numHashtagsAfter;
                     i++)
                {
                    hashtags += '#';
                }

                string leftHashtag = "#";
                if (numHashtagsBefore == 0)
                    leftHashtag = ANSIColor.DISPLAY + leftHashtag + ANSIColor.RESET;
                string rightHashtag = "#";
                if (numHashtagsAfter == 0)
                    rightHashtag = ANSIColor.DISPLAY + rightHashtag + ANSIColor.RESET;
                Console.WriteLine(
                    $"{hashtags}\n{leftHashtag} Player {ANSIColor.DISPLAY}{playerI + 1}{ANSIColor.RESET}'s turn {rightHashtag}\n{hashtags}\n");
            }
        }

        public static void DisplayGameUI(List<char> wrongLetters,
            char[] guessedWord,
            int guesses)
        {
            // Display wrong letters already guessed
            Console.Write($"Wrong letters:   {ANSIColor.BAD}");
            Console.Write(String.Join($"{ANSIColor.RESET}, {ANSIColor.BAD}",
                wrongLetters));
            Console.WriteLine($"{ANSIColor.RESET}");

            // Display correct letters
            Console.Write($"Correct letters: ");
            for (int i = 0;
                 i < guessedWord.Length;
                 i++)
            {
                if (guessedWord[i] == '_')
                {
                    Console.Write("_ ");
                }
                else
                {
                    Console.Write(ANSIColor.GOOD + guessedWord[i] + " " + ANSIColor.RESET);
                }
            }

            Console.WriteLine();

            Console.Write(Gallow.DrawGallow(guesses));
        }

        public static char InputGuessedLetter(List<char> wrongLetters,
            char[] guessedWord)
        {
            Console.Write($"{ANSIColor.PROMPT}Enter your guess for a letter:{ANSIColor.RESET} ");
            // Ensure letter entered is not guessed before.
            string? guessedLetterStr = Console.ReadLine()
                .ToUpper();
            // Ensure letter is entered correctly.
            while (guessedLetterStr == null ||
                   guessedLetterStr.Length < 1 ||
                   !GameStatus.IsGuessValid(guessedLetterStr[0],
                       wrongLetters) ||
                   GameStatus.GuessInCorrectList(guessedLetterStr[0],
                       guessedWord))
            {
                if (guessedLetterStr == null || guessedLetterStr.Length < 1)
                {
                    Console.Write(
                        $"{ANSIColor.PROMPT}There was no letter there. Enter your guess for a letter:{ANSIColor.RESET} ");
                }
                else
                {
                    Console.Write(
                        $"{ANSIColor.PROMPT}You already guessed that. Enter your guess for a letter:{ANSIColor.RESET} ");
                }

                guessedLetterStr = Console.ReadLine()
                    .ToUpper();
            }

            char guessedLetter = guessedLetterStr.ToUpper()[0];
            return guessedLetter;
        }

        public static void WaitForKeypressGameEnded(int numPlayers)
        {
            if (numPlayers > 1)
            {
                Console.Write(
                    $"{ANSIColor.PROMPT}Press ANY KEY to let the other players keep playing:{ANSIColor.RESET} ");
            }
            else
            {
                Console.Write($"{ANSIColor.PROMPT}Press ANY KEY to end the game:{ANSIColor.RESET} ");
            }

            Console.ReadKey();
        }

        public static void DisplayLeaderboard(int[] numRounds)
        {
            Console.Write($"\n###############\n# {ANSIColor.GOOD}Leaderboard{ANSIColor.RESET} #\n###############\n\n");

            // numRounds: Each item represents players in order,
            // and is 0 if they haven't finished, -1 if they ran
            // out of guesses and the number of guesses it took
            // them to finish otherwise.

            // Move num rounds per player to num rounds indexed where each item is tuple (numRounds, player index)
            Tuple<int, int>[] numRoundsIndexed = new Tuple<int, int>[numRounds.Length];
            for (int i = 0;
                 i < numRounds.Length;
                 i++)
            {
                numRoundsIndexed[i] = new Tuple<int, int>(numRounds[i],
                    i);
            }

            Array.Sort<Tuple<int, int>>(numRoundsIndexed,
                (a,
                    b) =>
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
            for (int i = 0;
                 i < numRounds.Length;
                 i++)
            {
                if (numRoundsIndexed[i].Item1 == -1)
                {
                    Console.WriteLine(
                        $"{ANSIColor.DISPLAY}Player {numRoundsIndexed[i].Item2 + 1}{ANSIColor.RESET}: {ANSIColor.BAD}Ran out of guesses{ANSIColor.RESET}");
                }
                else
                {
                    Console.WriteLine(
                        $"{ANSIColor.DISPLAY}Player {numRoundsIndexed[i].Item2 + 1}{ANSIColor.RESET}: took {ANSIColor.GOOD}{numRoundsIndexed[i].Item1}{ANSIColor.RESET} guesses");
                }
            }
        }
    }
}