using System;
using System.Collections.Generic;

namespace Character_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== BASIC D&D Character Creator ===\n");

            var abilities = AbilityScores.GenerateWithRerollOption();

            var character = new Character(abilities);

            character.SelectClass();

            character.CalculateHitPoints();

            character.AskForName();

            character.DisplayCharacterSheet();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}