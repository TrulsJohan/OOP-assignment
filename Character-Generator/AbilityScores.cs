using System;
using System.Collections.Generic;
using System.Linq;

namespace Character_Generator
{
    public class AbilityScores
    {
        public int Strength     { get; private set; }
        public int Intelligence { get; private set; }
        public int Wisdom       { get; private set; }
        public int Dexterity    { get; private set; }
        public int Constitution { get; private set; }
        public int Charisma     { get; private set; }

        public double Average => 
            (Strength + Intelligence + Wisdom + Dexterity + Constitution + Charisma) / 6.0;

        public AbilityScores()
        {
            RollAll();
        }
        
        public bool RollAll()
        {
            Strength     = Roll3d6();
            Intelligence = Roll3d6();
            Wisdom       = Roll3d6();
            Dexterity    = Roll3d6();
            Constitution = Roll3d6();
            Charisma     = Roll3d6();

            return true;
        }

        private static readonly Random _rnd = new Random();

        private static int Roll3d6()
        {
            return _rnd.Next(1, 7) + _rnd.Next(1, 7) + _rnd.Next(1, 7);
        }

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

        public void PrintToConsole()
        {
            Console.WriteLine("\nRolled ability scores:");
            Console.WriteLine($"  STR: {Strength,2}");
            Console.WriteLine($"  INT: {Intelligence,2}");
            Console.WriteLine($"  WIS: {Wisdom,2}");
            Console.WriteLine($"  DEX: {Dexterity,2}");
            Console.WriteLine($"  CON: {Constitution,2}");
            Console.WriteLine($"  CHA: {Charisma,2}");
            Console.WriteLine($"  Average: {Average:F1}");
        }
    }
}