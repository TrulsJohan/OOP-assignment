namespace Character_Generator
{
    public class Cleric : CharacterClassBase
    {
        public override string Name => "Cleric";
        public override string PrimeRequisiteAbbr => "WIS";
        public override int XPToLevel2 => 1500;

        public override int RollHitDie() => Random.Shared.Next(1, 7);

        public override int GetPrimeRequisiteValue(AbilityScores abilities) => abilities.Wisdom;
    }
}