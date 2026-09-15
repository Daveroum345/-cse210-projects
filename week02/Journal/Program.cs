using System;

// Creativity enhancements:
// 1. Each journal entry records the user's mood.
// 2. Users can search journal entries using keywords.
// 3. Users can view journal statistics, including the most common mood.
// 4. The program includes additional original journal prompts.

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();

        string[] prompts =
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "What was the strongest emotion I felt today?",
            "What is something I learned today?",
            "What is one thing I want to improve tomorrow?",
            "What am I grateful for today?",
            "What challenge did I overcome today?"
        };

        Random random = new Random();

        string choice = "";

        while (choice != "7")
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("           JOURNAL PROGRAM");
            Console.WriteLine("========================================");
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Search the journal");
            Console.WriteLine("6. View journal statistics");
            Console.WriteLine("7. Quit");
            Console.Write("What would you like to do? ");

            choice = Console.ReadLine();

            if (choice == "1")
            {
                WriteNewEntry(journal, prompts, random);
            }
            else if (choice == "2")
            {
                journal.Display();
            }
            else if (choice == "3")
            {
                Console.Write("Enter the filename to save: ");
                string filename = Console.ReadLine();

                journal.SaveToFile(filename);
            }
            else if (choice == "4")
            {
                Console.Write("Enter the filename to load: ");
                string filename = Console.ReadLine();

                journal.LoadFromFile(filename);
            }
            else if (choice == "5")
            {
                Console.Write("Enter a keyword to search for: ");
                string keyword = Console.ReadLine();

                journal.Search(keyword);
            }
            else if (choice == "6")
            {
                journal.ShowStatistics();
            }
            else if (choice == "7")
            {
                Console.WriteLine();
                Console.WriteLine("Thank you for using the Journal Program!");
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Invalid choice. Please select a number from 1 to 7.");
            }
        }
    }

    static void WriteNewEntry(
        Journal journal,
        string[] prompts,
        Random random)
    {
        string prompt = prompts[random.Next(prompts.Length)];

        Console.WriteLine();
        Console.WriteLine("========== NEW JOURNAL ENTRY ==========");
        Console.WriteLine($"Prompt: {prompt}");

        Console.Write("Your response: ");
        string response = Console.ReadLine();

        Console.Write("How are you feeling today? ");
        string mood = Console.ReadLine();

        string date = DateTime.Now.ToShortDateString();

        Entry newEntry = new Entry(
            date,
            prompt,
            response,
            mood
        );

        journal.AddEntry(newEntry);

        Console.WriteLine();
        Console.WriteLine("Entry added successfully!");
    }
}