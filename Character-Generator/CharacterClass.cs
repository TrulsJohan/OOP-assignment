using System.Collections.Generic;

namespace Character_Generator
{
    public class CharacterClass
    {
        public string Name { get; }
        public string PrimeRequisite { get; }
        public string HitDie { get; }
        public int XPToLevel2 { get; }

        public CharacterClass(string name, string prime, string hitDie, int xpToLevel2)
        {
            Name = name;
            PrimeRequisite = prime;
            HitDie = hitDie;
            XPToLevel2 = xpToLevel2;
        }

        public static List<CharacterClass> GetAllClasses()
        {
            return new List<CharacterClass>
            {
                new CharacterClass("Cleric",    "WIS", "1d6", 1500),
                new CharacterClass("Fighter",   "STR", "1d8", 2000),
                new CharacterClass("Magic-User","INT", "1d4", 2500),
                new CharacterClass("Thief",     "DEX", "1d4", 1200)
            };
        }
    }
}