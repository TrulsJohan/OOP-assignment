using System;
using System.Diagnostics;

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
                    Console.WriteLine("\nScores are acceptable (average > 8).");
                    accepted = true;
                    continue;
                }

                Console.WriteLine("\nYour ability scores are below average.");
                Console.Write("Would you like to reroll? (Y/N): ");

                string input = Console.ReadLine()?.Trim().ToUpperInvariant() ?? "N";

                if (input == "Y" || input == "YES")
                {
                    Console.WriteLine("Rerolling...\n");
                    abilities.RollAll();
                }
                else
                {
                    Console.WriteLine("Keeping current scores.");
                    accepted = true;
                }
            }
            
            Console.WriteLine("\nFinal scores accepted!");
            abilities.PrintToConsole();

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            
            Console.WriteLine("\n┌────────────────────────────────────┐");
            Console.WriteLine("│         Class Selection            │");
            Console.WriteLine("└────────────────────────────────────┘");
            
            int highest = abilities.GetHighestScore();
            int second = abilities.GetSecondHighestScore();
            
            CharacterClass selectedClass;
            
            Console.WriteLine($"Your highest score is {highest}, and second is {second}\n");
            
            var allClasses = CharacterClass.GetAllClasses();
            var eligible = new List<CharacterClass>();

            foreach (var cls in allClasses)
            {
                int primeValue = cls.PrimeRequisite switch
                {
                    "STR" => abilities.Strength,
                    "INT" => abilities.Intelligence,
                    "WIS" => abilities.Wisdom,
                    "DEX" => abilities.Dexterity,
                    _ => 0
                };

                if (primeValue == highest || primeValue == second)
                {
                    eligible.Add(cls);
                }
            }

            if (eligible.Count == 0)
            {
                Console.WriteLine("ERROR: No eligible classes found\n");
                Console.WriteLine("Using Fighter as fallback...");
                selectedClass = allClasses[1];
            } else if (eligible.Count == 1)
            {
                selectedClass = eligible[0];
                Console.WriteLine($"Only one class is eligible: {selectedClass.Name}");
                Console.WriteLine($"   (Prime Requisite {selectedClass.PrimeRequisite} matches your top scores)");
            }
            else
            {
                Console.WriteLine("You can choose from these classes:");
                for (int i = 0; i < eligible.Count; i++)
                {
                    var c = eligible[i];
                    Console.WriteLine($" {i + 1,2}) {c.Name,-10}  (Prime: {c.PrimeRequisite})");
                }
                
                Console.Write($"\nChoose your class (1–{eligible.Count}): ");
                
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > eligible.Count)
                {
                    Console.Write($"Please enter a number between 1 and {eligible.Count}: ");
                }

                selectedClass = eligible[choice - 1];
            }
            
            Console.WriteLine($"\n→ Selected class: **{selectedClass.Name}**");
            
            var character = new Character(abilities);
            character.SetClass(selectedClass);
            
            int primeMod = character.GetPrimeRequisiteModifier();
            int conMod = character.GetConstitutionModifier();

            Console.WriteLine("\nAbility Score Modifiers:");
            Console.WriteLine($"Prime Requisite ({selectedClass.PrimeRequisite}): {primeMod:+#;-#;0}");
            Console.WriteLine($"Constitution: {conMod:+#;-#;0}");
        }
    }
}