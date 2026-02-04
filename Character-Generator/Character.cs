using System;

namespace Character_Generator
{
    public class Character
    {
        public AbilityScores Abilities { get; }
        public CharacterClass Class { get; private set; }
        public string Name { get; set; }

        public Character(AbilityScores abilities)
        {
            Abilities = abilities;
        }

        public void SetClass(CharacterClass cls)
        {
            Class = cls;
        }

        public int GetModifier(int score)
        {
            if (score <= 3) return -3;
            if (score <= 5) return -2;
            if (score <= 8) return -1;
            if (score <= 12) return 0;
            if (score <= 15) return 1;
            if (score <= 17) return 2;
            return 3;
        }

        public int GetPrimeRequisiteModifier()
        {
            int score = Class.PrimeRequisite switch
            {
                "STR" => Abilities.Strength,
                "INT" => Abilities.Intelligence,
                "WIS" => Abilities.Wisdom,
                "DEX" => Abilities.Dexterity,
                _ => 9 // fallback to neutral
            };
            return GetModifier(score);
        }

        public int GetConstitutionModifier() => GetModifier(Abilities.Constitution);
    }
}