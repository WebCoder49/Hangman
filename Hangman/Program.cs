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


        }
    }
}