using System;
using System.Collections.Generic;
using System.Threading;

public class ListingActivity : Activity
{
    private RandomPicker _promptPicker;

    public ListingActivity()
        : base("Listing Activity",
               "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _promptPicker = new RandomPicker(new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        });
    }

    public override void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine("List as many responses as you can to the following prompt:\n");
        Console.WriteLine($" --- {_promptPicker.GetNext()} ---\n");
        Console.Write("You may begin in: ");
        ShowCountDown(5);
        Console.WriteLine("\n");

        List<string> items = GetListFromUser();

        Console.WriteLine($"\nYou listed {items.Count} items!");

        DisplayEndingMessage();
    }

    private List<string> GetListFromUser()
    {
        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        Console.Write("> ");

        while (DateTime.Now < endTime)
        {
            if (Console.KeyAvailable)
            {
                string item = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(item))
                {
                    items.Add(item);
                }

                Console.Write("> ");
            }
            else
            {
                Thread.Sleep(50);
            }
        }

        Console.WriteLine();
        return items;
    }
}