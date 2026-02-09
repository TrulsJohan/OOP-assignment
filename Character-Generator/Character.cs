using System;

namespace Character_Generator
{
    public class Character
    {
        public AbilityScores Abilities { get; }
        public CharacterClassBase SelectedClass { get; private set; }
        public string Name { get; private set; } = "Unnamed Adventurer";
        public int HitPoints { get; private set; }

        public Character(AbilityScores abilities)
        {
            Abilities = abilities ?? throw new ArgumentNullException(nameof(abilities));
        }

        public void SelectClass()
        {
            var eligible = GetEligibleClasses();

            if (eligible.Count == 0)
            {
                Console.WriteLine("No eligible class found. Using Fighter as fallback.");
                SelectedClass = new Fighter();
                return;
            }

            if (eligible.Count == 1)
            {
                SelectedClass = eligible[0];
                Console.WriteLine($"\nOnly {SelectedClass.Name} is eligible.");
                return;
            }

            Console.WriteLine("\nEligible classes:");
            for (int i = 0; i < eligible.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {eligible[i].Name} (prime: {eligible[i].PrimeRequisiteAbbr})");
            }

            int choice = GetValidInt($"Choose class (1-{eligible.Count}): ", 1, eligible.Count);
            SelectedClass = eligible[choice - 1];
        }

        private List<CharacterClassBase> GetEligibleClasses()
        {
            var all = new List<CharacterClassBase>
            {
                new Cleric(), new Fighter(), new MagicUser(), new Thief()
            };

            var topTwo = Abilities.GetSortedDescending().Take(2).Select(x => x.Value).ToHashSet();

            return all.Where(c => topTwo.Contains(c.GetPrimeRequisiteValue(Abilities))).ToList();
        }

        public void CalculateHitPoints()
        {
            int roll = SelectedClass.RollHitDie();
            int conMod = Abilities.GetModifier(Abilities.Constitution);
            HitPoints = Math.Max(1, roll + conMod);
        }

        public void AskForName()
        {
            Console.Write("\nEnter character name: ");
            string input = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
                Name = input;
        }

        public void DisplayCharacterSheet()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {Name,-50} ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════╣");

            Console.WriteLine("║ Ability Scores                                         ║");
            Console.WriteLine($"║   STR     {Abilities.Strength,2}   ({Abilities.GetModifier(Abilities.Strength),+2}) ║");
            Console.WriteLine($"║   INT     {Abilities.Intelligence,2}   ({Abilities.GetModifier(Abilities.Intelligence),+2}) ║");
            Console.WriteLine($"║   WIS     {Abilities.Wisdom,2}   ({Abilities.GetModifier(Abilities.Wisdom),+2}) ║");
            Console.WriteLine($"║   DEX     {Abilities.Dexterity,2}   ({Abilities.GetModifier(Abilities.Dexterity),+2}) ║");
            Console.WriteLine($"║   CON     {Abilities.Constitution,2}   ({Abilities.GetModifier(Abilities.Constitution),+2}) ║");
            Console.WriteLine($"║   CHA     {Abilities.Charisma,2}   ({Abilities.GetModifier(Abilities.Charisma),+2}) ║");

            Console.WriteLine("╠════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ Class              {SelectedClass.Name,-34} ║");
            Console.WriteLine($"║ Prime Requisite    {SelectedClass.PrimeRequisiteAbbr,-3}   ({SelectedClass.GetPrimeRequisiteModifier(Abilities),+2}) ║");
            Console.WriteLine($"║ Hit Points         {HitPoints,-34} ║");
            Console.WriteLine($"║ XP to level 2      {SelectedClass.XPToLevel2} ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        }

        private static int GetValidInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                    return value;

                Console.WriteLine($"Please enter a number between {min} and {max}.");
            }
        }
    }
}