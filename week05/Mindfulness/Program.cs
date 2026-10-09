using System;
using System.Threading;

// EXCEEDING THE REQUIREMENTS
// 1. No repeated prompts or questions: the RandomPicker class picks random
//    prompts and questions, but never repeats one until all of them have
//    been used at least once.
// 2. Activity log saved to a file: the ActivityLog class records how many
//    times each activity was done and the total seconds spent on it. The log
//    is saved in "mindfulness_log.txt" and loaded again at the next start.
//    It can be viewed from the menu (option 4).
// 3. Animated breathing: a bar grows while the user breathes in and shrinks
//    while the user breathes out, with the seconds remaining shown beside it.

class Program
{
    static void Main(string[] args)
    {
        ActivityLog log = new ActivityLog("mindfulness_log.txt");
        log.Load();

        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. View activity log");
            Console.WriteLine("  5. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();
            Activity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectingActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    Console.Clear();
                    log.Display();
                    Console.WriteLine("\nPress Enter to return to the menu.");
                    Console.ReadLine();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    Thread.Sleep(1500);
                    break;
            }

            if (activity != null)
            {
                activity.Run();
                log.Record(activity.GetName(), activity.GetDuration());
                log.Save();
            }
        }

        Console.Clear();
        Console.WriteLine("Thank you for taking time to be mindful. Goodbye!");
    }
}