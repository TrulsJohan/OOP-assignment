using System;
using System.Collections.Generic;
using System.Linq;

namespace Character_Generator
{
    public class AbilityScores
    {
        public int Strength { get; private set; }
        public int Intelligence { get; private set; }
        public int Wisdom { get; private set; }
        public int Dexterity { get; private set; }
        public int Constitution { get; private set; }
        public int Charisma { get; private set; }

        // Average of all six scores
        public double Average => 
            (Strength + Intelligence + Wisdom + Dexterity + Constitution + Charisma) / 6.0;

        public AbilityScores()
        {
            RollAll();
        }

        public void RollAll()
        {
            Strength     = Roll3d6();
            Intelligence = Roll3d6();
            Wisdom       = Roll3d6();
            Dexterity    = Roll3d6();
            Constitution = Roll3d6();
            Charisma     = Roll3d6();
        }

        private static int Roll3d6()
        {
            Random rnd = new Random();
            return rnd.Next(1, 7) + rnd.Next(1, 7) + rnd.Next(1, 7);
            // Range: 3–18
        }

        // Helper: returns scores sorted high → low with labels
        public List<(string Label, int Value)> GetSorted()
        {
            return new List<(string Label, int Value)>
                {
                    ("STR", Strength),
                    ("INT", Intelligence),
                    ("WIS", Wisdom),
                    ("DEX", Dexterity),
                    ("CON", Constitution),
                    ("CHA", Charisma)
                }
                .OrderByDescending(x => x.Value)
                .ToList();
        }

        public int GetHighest() => GetSorted()[0].Value;

        public int GetSecondHighest() => GetSorted()[1].Value;

        // For nice display
        public void PrintToConsole()
        {
            Console.WriteLine("Rolled ability scores:");
            Console.WriteLine($"  STR: {Strength}");
            Console.WriteLine($"  INT: {Intelligence}");
            Console.WriteLine($"  WIS: {Wisdom}");
            Console.WriteLine($"  DEX: {Dexterity}");
            Console.WriteLine($"  CON: {Constitution}");
            Console.WriteLine($"  CHA: {Charisma}");
            Console.WriteLine($"  Average: {Average:F1}");
        }
    }
}