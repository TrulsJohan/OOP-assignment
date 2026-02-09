namespace Character_Generator
{
    public class Thief : CharacterClassBase
    {
        public override string Name => "Thief";
        public override string PrimeRequisiteAbbr => "DEX";
        public override int XPToLevel2 => 1200;

        public override int RollHitDie() => Random.Shared.Next(1, 5);

        public override int GetPrimeRequisiteValue(AbilityScores abilities) => abilities.Dexterity;
    }
}