using System;
using System.Collections.Generic;
using System.Linq;

namespace Character_Generator
{
    public class AbilityScores
    {
        private const int MIN_SCORE = 3;
        private const int MAX_SCORE = 18;

        private readonly int[] _scores = new int[6];

        public int Strength     => _scores[0];
        public int Intelligence => _scores[1];
        public int Wisdom       => _scores[2];
        public int Dexterity    => _scores[3];
        public int Constitution => _scores[4];
        public int Charisma     => _scores[5];

        public double Average => _scores.Average();

        private static readonly Random _rng = new Random();

        private AbilityScores() { }

        public static AbilityScores GenerateWithRerollOption()
        {
            AbilityScores abilities;

            do
            {
                abilities = new AbilityScores();
                abilities.RollAll();

                abilities.Print();

                if (abilities.Average > 8.0)
                    break;

                Console.WriteLine("\nYour ability scores are below average.");
                if (!AskYesNo("Would you like to reroll? (Y/N)"))
                    break;

                Console.WriteLine("Rerolling...\n");

            } while (true);

            return abilities;
        }

        private void RollAll()
        {
            for (int i = 0; i < 6; i++)
            {
                _scores[i] = Roll3d6();
            }
        }

        private static int Roll3d6()
        {
            return _rng.Next(1, 7) + _rng.Next(1, 7) + _rng.Next(1, 7);
        }

        public int GetModifier(int score)
        {
            return score switch
            {
                <= 3  => -3,
                <= 5  => -2,
                <= 8  => -1,
                <= 12 =>  0,
                <= 15 => +1,
                <= 17 => +2,
                _     => +3
            };
        }
        
        public (int Value, string Abbr)[] GetSortedDescending()
        {
            var list = new[]
            {
                (Value: Strength,     Abbr: "STR"),
                (Value: Intelligence, Abbr: "INT"),
                (Value: Wisdom,       Abbr: "WIS"),
                (Value: Dexterity,    Abbr: "DEX"),
                (Value: Constitution, Abbr: "CON"),
                (Value: Charisma,     Abbr: "CHA")
            };

            return list
                .OrderByDescending(x => x.Value)
                .ToArray();
        }

        public void Print()
        {
            Console.WriteLine("\nRolled ability scores:");
            Console.WriteLine($"  STR {Strength,2}   INT {Intelligence,2}");
            Console.WriteLine($"  WIS {Wisdom,2}   DEX {Dexterity,2}");
            Console.WriteLine($"  CON {Constitution,2}   CHA {Charisma,2}");
            Console.WriteLine($"  Average: {Average:F1}");
        }

        private static bool AskYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " ");
                string input = Console.ReadLine()?.Trim().ToUpperInvariant();

                if (input == "Y" || input == "YES") return true;
                if (input == "N" || input == "NO")  return false;

                Console.WriteLine("Please enter Y or N.");
            }
        }
    }
}