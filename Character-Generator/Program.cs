using System;

namespace Character_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== BASIC D&D Character Creator (Step 1) ===\n");

            // Create and roll abilities
            var abilities = new AbilityScores();

            // Show the result
            abilities.PrintToConsole();

            Console.WriteLine("\nPress any key to roll again (or close the window)...");
            Console.ReadKey();

            // Optional: demonstrate reroll
            Console.WriteLine("\nRerolling...\n");
            abilities.RollAll();
            abilities.PrintToConsole();

            Console.WriteLine("\nDone for now. Press any key to exit.");
            Console.ReadKey();
        }
    }
}