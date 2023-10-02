using System;

namespace Hangman
{
    internal class Gallow
    {

        // takes the number of wrong guesses(int) and display the gallow
        public static string DrawGallow(int a)
        {
            string[] Gallow = { "", "   \n    |       \n    |        \n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |       \n    |        \n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        \n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |        |\n    |        |\n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | \n    |        |\n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | \n    |        |\n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | /\n    |        |\n    |       /\n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | /\n    |        |\n    |       / \\\n    |\n____|___     Good Game! You are dead!\n\n" };
            return Gallow[a];
        }
    }
}