namespace Character_Generator
{
    public abstract class CharacterClassBase
    {
        public abstract string Name { get; }
        public abstract string PrimeRequisiteAbbr { get; }
        public abstract int XPToLevel2 { get; }

        public abstract int RollHitDie();

        public abstract int GetPrimeRequisiteValue(AbilityScores abilities);

        public virtual int GetPrimeRequisiteModifier(AbilityScores abilities)
        {
            return abilities.GetModifier(GetPrimeRequisiteValue(abilities));
        }
    }
}