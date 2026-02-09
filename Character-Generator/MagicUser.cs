namespace Character_Generator
{
    public class MagicUser : CharacterClassBase
    {
        public override string Name => "Magic-User";
        public override string PrimeRequisiteAbbr => "INT";
        public override int XPToLevel2 => 2500;

        public override int RollHitDie() => Random.Shared.Next(1, 5);

        public override int GetPrimeRequisiteValue(AbilityScores abilities) => abilities.Intelligence;
    }
}