using System;

// EXCEEDING REQUIREMENTS (creativity):
//
// Reference:
//  - Verses given in the wrong order (6-5) are automatically corrected.
//  - GetVerseCount() gives the number of verses of the passage.
//
// Word:
//  - Hint mode: the first letter of each hidden word can stay visible.
//  - Punctuation is never hidden, so the sentence structure stays readable.
//
// Scripture:
//  - Only words that are not already hidden are picked (stretch challenge).
//  - The header shows the number of verses, for example "(2 verses)".
//  - A progress percentage shows how much of the scripture is hidden.
//
// ScriptureLibrary:
//  - Scriptures are loaded from a file (scriptures.txt) and chosen at random.
//  - A default scripture is used if the file is missing or empty.
//  - Invalid lines in the file are skipped instead of crashing the program.
//  - The same scripture is never chosen twice in a row.
//
// Program:
//  - The user chooses a difficulty level (2, 4 or 6 words hidden per round).
//  - The user can enable or disable hint mode.
//  - Colors and a progress indicator make the display clearer.
//  - User input is validated (invalid answers ask the question again).

class Program
{
    static void Main(string[] args)
    {
        ScriptureLibrary library = new ScriptureLibrary("Scriptures.txt");
        Scripture scripture = library.GetRandomScripture();

        int wordsPerRound = AskDifficulty();
        bool showHint = AskHintMode();

        while (true)
        {
            DisplayScripture(scripture, showHint);

            if (scripture.IsCompletelyHidden())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Well done! Every word is hidden now.");
                Console.ResetColor();
                break;
            }

            Console.Write("Press enter to continue or type 'quit' to finish: ");
            string input = Console.ReadLine();

            if (input != null && input.Trim().ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords(wordsPerRound);
        }
    }

    static void DisplayScripture(Scripture scripture, bool showHint)
    {
        // On the final screen, everything must be hidden (no hint letters),
        // as the assignment requires.
        bool hint = showHint && !scripture.IsCompletelyHidden();

        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(scripture.GetDisplayText(hint));
        Console.ResetColor();

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Progress: {scripture.GetProgressPercentage()}% hidden");
        Console.ResetColor();
        Console.WriteLine();
    }

    static int AskDifficulty()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose a difficulty:");
            Console.WriteLine("1. Easy (2 words per round)");
            Console.WriteLine("2. Medium (4 words per round)");
            Console.WriteLine("3. Hard (6 words per round)");
            Console.Write("Your choice (1-3): ");

            string choice = Console.ReadLine();

            if (choice == "1") return 2;
            if (choice == "2") return 4;
            if (choice == "3") return 6;

            // Invalid answer: the loop asks the question again
        }
    }

    static bool AskHintMode()
    {
        while (true)
        {
            Console.Clear();
            Console.Write("Enable hint mode (first letter stays visible)? (y/n): ");

            string answer = Console.ReadLine();

            if (answer != null)
            {
                answer = answer.Trim().ToLower();

                if (answer == "y") return true;
                if (answer == "n") return false;
            }
        }
    }
}