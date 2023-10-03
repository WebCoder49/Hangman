using System;
using System.Collections.Generic;
using System.IO;

namespace Hangman
{
    internal class WordPicker
    {
        // Sets of indexes of words/lines already used up.
        private HashSet<int> _takenIndexesEasy = new();
        private HashSet<int> _takenIndexesMedium = new();
        private HashSet<int> _takenIndexesHard = new();
        private HashSet<int> _takenIndexesQuotes = new();
        
        // For PickQuote
        private string _quote = "";
        private string _quoteAuthor = "";
        
        public enum Difficulty
        {
            Easy,
            Medium,
            Hard,
            Unset
        }
        public string? PickWord(Difficulty difficulty)
        {
            // Read from file
            StreamReader reader;
            int numWords;
            HashSet<int> takenIndexes;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    reader = new StreamReader("../../../resources/easy.txt"); // From bin/Debug/net7.0
                    numWords = 2135; // Hardcoded for efficiency
                    takenIndexes = _takenIndexesEasy;
                    break;
                case Difficulty.Medium: 
                    reader = new StreamReader("../../../resources/medium.txt"); // From bin/Debug/net7.0
                    numWords = 663; // Hardcoded for efficiency
                    takenIndexes = _takenIndexesMedium;
                    break;
                case Difficulty.Hard:
                    reader = new StreamReader("../../../resources/hard.txt"); // From bin/Debug/net7.0
                    numWords = 519; // Hardcoded for efficiency
                    takenIndexes = _takenIndexesHard;
                    break;
                default:
                    return null;
            }
            
            // Get random word (each on a separate line)
            // Avoid duplicate indexes
            int wordIndex = new Random().Next(0, numWords);
            while (takenIndexes.Contains(wordIndex))
            {
                if (takenIndexes.Count == numWords) return null; // Otherwise infinite loop
                wordIndex = new Random().Next(0, numWords);
            }
            takenIndexes.Add(wordIndex);
            
            for (int i = 0; i < wordIndex; i++)
            {
                reader.ReadLine();
            }
            if (reader.ReadLine() == null) return null;
            return reader.ReadLine().ToUpper();
        }
        
        public void PickQuote()
        {
            // Read from file
            StreamReader reader;
            int numLines;
            reader = new StreamReader("../../../resources/quotes.txt"); // From bin/Debug/net7.0
            numLines = 1615; // Number of lines excluding last quote. Hardcoded for efficiency
            
            // Get random line
            int lineIndex = new Random().Next(0, numLines);
            for (int i = 0; i < lineIndex; i++)
            {
                reader.ReadLine();
            }
            
            // Move to start of quote
            string line = reader.ReadLine();
            while (line != "")
            {
                line = reader.ReadLine();
            }
            string quote = "";
            // Move to author
            line = reader.ReadLine();
            while(line.Length < 3 || line.Substring(0, 3) != "-- ")
            {
                quote += line + "\n";
                line = reader.ReadLine();
            }
            string author = line.Substring(3);

            _quoteAuthor = author;
            _quote = quote.Substring(0, quote.Length - 1); // Remove last "\n"
        }

        public string GetQuote()
        {
            // Return quote picked using PickQuote
            return _quote;
        }
        public string GetQuoteAuthor()
        {
            // Return author of quote picked using PickQuote
            return _quoteAuthor;
        }
    }
}

