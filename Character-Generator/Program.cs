using System;

namespace Character_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== BASIC D&D Character Creator (Step 1) ===\n");
            
            var abilities = new AbilityScores();
            
            bool accepted = false;
            
            while (!accepted)
            {
                abilities.PrintToConsole();
                if (abilities.Average > 8.0)
                {
                    Console.WriteLine("\nScores are acceptable");
                    accepted = true;
                    break;
                }
                
                Console.WriteLine("\nYour scores are below average");
                Console.Write("Would you like to reroll? (Y/N)");
                
                string input = Console.ReadLine()?.Trim().ToUpperInvariant() ?? "N";

                if (input == "Y" || input == "YES")
                {
                    Console.WriteLine("Rerolling...\n");
                    abilities.RollAll();
                }
                else
                {
                    Console.WriteLine("Keeping current scores");
                    accepted = true;
                }
                
                Console.WriteLine("\nFinal scores accepted!");
                abilities.PrintToConsole();

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}