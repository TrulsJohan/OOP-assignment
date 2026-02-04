using System.Collections.Generic;

namespace Character_Generator
{
    public class CharacterClass
    {
        public string Name { get; }
        public string PrimeRequisite { get; }
        public string HitDie { get; }

        public CharacterClass(string name, string prime, string hitDie)
        {
            Name = name;
            PrimeRequisite = prime;
            HitDie = hitDie;
        }
        
        public static List<CharacterClass> GetAllClasses()
        {
            return new List<CharacterClass>
            {
                new CharacterClass("Cleric",    "WIS", "1d6"),
                new CharacterClass("Fighter",   "STR", "1d8"),
                new CharacterClass("Magic-User","INT", "1d4"),
                new CharacterClass("Thief",     "DEX", "1d4")
            };
        }
    }
}