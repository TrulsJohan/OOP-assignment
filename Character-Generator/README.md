# BASIC D&D Character Generator

A console-based character creation tool inspired by the classic BASIC D&D (Moldvay/Cook) rules.

Generates a 1st-level character by rolling ability scores, handling rerolls, selecting an eligible class, calculating hit points, and displaying the final character sheet.

## Features

- Roll 3d6 for the six classic abilities (STR, INT, WIS, DEX, CON, CHA)
- Reroll option if average ability score ≤ 8
- Class eligibility based on **prime requisite** being highest or second-highest ability score
- Proper handling of ties
- Ability score modifiers according to classic table
- Hit point calculation (hit die + CON modifier, minimum 1)
- XP required to reach level 2 shown for the chosen class
- Clean console character sheet with box drawing
- Object-oriented design with encapsulation, inheritance & polymorphism

## Classes Implemented

| Class       | Prime Requisite | Hit Die | XP to Level 2 |
|-------------|------------------|---------|---------------|
| Cleric      | WIS             | 1d6     | 1,500         |
| Fighter     | STR             | 1d8     | 2,000         |
| Magic-User  | INT             | 1d4     | 2,500         |
| Thief       | DEX             | 1d4     | 1,200         |

## Project Structure

Character_Generator/
├── Program.cs                  # Main entry point and flow control
├── AbilityScores.cs            # Ability rolling, modifiers, sorting
├── Character.cs                # Character entity + business logic
├── CharacterClassBase.cs       # Abstract base class for polymorphism
├── Cleric.cs
├── Fighter.cs
├── MagicUser.cs
├── Thief.cs
└── README.md                   # This file

## How to Run

1. Open the solution in Visual Studio / VS Code / Rider
2. Make sure target framework is .NET 6.0 or newer
3. Build and run


## AI-Prompts Used

1. I currently have all character classes in one public class. Is it better to have one class for each?
2. How does abstract classes work, and how to use override.
3. Can you make a clean characterDisplaySheet for the console. Make it look nice!
4. Make a readme file, with the important structure so I can fill it out.
