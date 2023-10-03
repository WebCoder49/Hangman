using System;

namespace Hangman
{
    internal class ANSIColor
    {
        public const string RESET = "\x1b[0m";
        public const string BOLD = "\x1b[1m";
        
        public const string CYAN = "\x1b[36m";
        public const string BLUE = "\x1b[34m";
        public const string GREEN = "\x1b[32m";
        public const string RED = "\x1b[31m";
        
        public const string DISPLAY = BOLD+CYAN;
        public const string PROMPT = BLUE;
        public const string GOOD = BOLD+GREEN;
        public const string BAD = BOLD+RED;
    }
}