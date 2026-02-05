using System;
using System.Collections.Generic;

namespace Character_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== BASIC D&D Character Creator ===\n");

            var abilities = new AbilityScores();

            // Reroll loop
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

            // Class Selection
            Console.WriteLine("\n┌────────────────────────────────────┐");
            Console.WriteLine("│         Class Selection            │");
            Console.WriteLine("└────────────────────────────────────┘");

            int highest = abilities.GetHighestScore();
            int second = abilities.GetSecondHighestScore();

            Console.WriteLine($"Your top two scores: {highest} and {second}\n");

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

            CharacterClass selectedClass;

            if (eligible.Count == 0)
            {
                Console.WriteLine("ERROR: No eligible classes found");
                Console.WriteLine("Using Fighter as fallback...");
                selectedClass = allClasses.Find(c => c.Name == "Fighter") ?? allClasses[1];
            }
            else if (eligible.Count == 1)
            {
                selectedClass = eligible[0];
                Console.WriteLine($"Only one class is eligible: {selectedClass.Name}");
                Console.WriteLine($"   (Prime Requisite {selectedClass.PrimeRequisite} matches your top scores)");
            }
            else
            {
                Console.WriteLine("You can choose from these classes:\n");
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

            Console.WriteLine($"\n→ Selected class: {selectedClass.Name}");

            var character = new Character(abilities);
            character.SetClass(selectedClass);

            // Modifiers
            int primeMod = character.GetPrimeRequisiteModifier();
            int conMod = character.GetConstitutionModifier();

            Console.WriteLine("\nAbility Score Modifiers:");
            Console.WriteLine($"  Prime Requisite ({selectedClass.PrimeRequisite}): {primeMod:+#;-#;0}");
            Console.WriteLine($"  Constitution: {conMod:+#;-#;0}");

            // Hit Points
            Console.WriteLine("\nHit Points Calculation:");
            int hitDieRoll = RollHitDie(selectedClass.HitDie);
            int constitutionMod = character.GetConstitutionModifier();
            int hitPoints = Math.Max(1, hitDieRoll + constitutionMod);
            character.HitPoints = hitPoints;

            Console.WriteLine($"  Rolled {selectedClass.HitDie,-4}: {hitDieRoll}");
            Console.WriteLine($"  CON modifier:     {constitutionMod,+2}");
            Console.WriteLine($"  Total HP:         {hitPoints}");

            // XP to level 2
            Console.WriteLine("\nExperience Points:");
            Console.WriteLine($"  XP required for level 2: {selectedClass.XPToLevel2:N0}");

            // Name
            Console.Write("\nEnter your character's name: ");
            string nameInput = Console.ReadLine()?.Trim();

            character.Name = !string.IsNullOrWhiteSpace(nameInput) 
                ? nameInput 
                : "Unnamed Adventurer";

            if (string.IsNullOrWhiteSpace(nameInput))
                Console.WriteLine("(Using default name since no input was provided)");

            // Final character sheet
            Console.Clear();

            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine($"║             {character.Name,-34} ║");
            Console.WriteLine("╠════════════════════════════════════════════════════╣");
            Console.WriteLine("║ Ability Scores                                     ║");
            Console.WriteLine($"║   STR     {abilities.Strength,2}    ({character.GetModifier(abilities.Strength),+2}) ║");
            Console.WriteLine($"║   INT     {abilities.Intelligence,2}    ({character.GetModifier(abilities.Intelligence),+2}) ║");
            Console.WriteLine($"║   WIS     {abilities.Wisdom,2}    ({character.GetModifier(abilities.Wisdom),+2}) ║");
            Console.WriteLine($"║   DEX     {abilities.Dexterity,2}    ({character.GetModifier(abilities.Dexterity),+2}) ║");
            Console.WriteLine($"║   CON     {abilities.Constitution,2}    ({character.GetModifier(abilities.Constitution),+2}) ║");
            Console.WriteLine($"║   CHA     {abilities.Charisma,2}    ({character.GetModifier(abilities.Charisma),+2}) ║");
            Console.WriteLine("╠════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ Class              {selectedClass.Name,-30} ║");
            Console.WriteLine($"║ Prime Requisite    {selectedClass.PrimeRequisite,-3}   ({character.GetPrimeRequisiteModifier(),+2}) ║");
            Console.WriteLine($"║ Hit Points         {character.HitPoints,-30} ║");
            Console.WriteLine($"║ XP to level 2      {selectedClass.XPToLevel2:N0,-30} ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            Console.WriteLine("\nCharacter creation complete! Press any key to exit...");
            Console.ReadKey();
        }

        private static int RollHitDie(string hitDie)
        {
            var rnd = new Random();

            return hitDie switch
            {
                "1d4" => rnd.Next(1, 5),
                "1d6" => rnd.Next(1, 7),
                "1d8" => rnd.Next(1, 9),
                _     => 1
            };
        }
    }
}