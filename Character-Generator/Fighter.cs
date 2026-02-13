namespace Character_Generator
{
    public class Fighter : CharacterClassBase
    {
        public override string Name => "Fighter";
        public override string PrimeRequisiteAbbr => "STR";
        public override int XPToLevel2 => 2000;

        public override int RollHitDie() => Random.Shared.Next(1, 9);

        public override int GetPrimeRequisiteValue(AbilityScores abilities) => abilities.Strength;
    }
}