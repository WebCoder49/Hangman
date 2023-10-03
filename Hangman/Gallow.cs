using System;

namespace Hangman
{
    internal class Gallow
    {
<<<<<<< HEAD

=======
>>>>>>> 807a49e0d00dfe4737be1e84b41fa7cd766b791c
        // takes the number of wrong guesses(int) and display the gallow
        public static string DrawGallow(int a)
        {
            string[] Gallow = { "", "   \n    |       \n    |        \n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |       \n    |        \n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        \n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |        \n    |        \n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |        |\n    |        |\n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | \n    |        |\n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | \n    |        |\n    |        \n    |\n____|___\n\n", "   ___________\n    |/       |\n    |        O\n    |      \\ | /\n    |        |\n    |       /\n    |\n____|___\n\n", $"   ___________\n    |/       |\n    |        O\n    |      \\ | /\n    |        |\n    |       / \\\n    |\n____|___     {ANSIColor.BAD}Good Game! You are dead!{ANSIColor.RESET}\n\n" };
            return Gallow[a];
        }
    }
}