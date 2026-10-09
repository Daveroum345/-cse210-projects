using System;
using System.Collections.Generic;

public class ReflectingActivity : Activity
{
    private RandomPicker _promptPicker;
    private RandomPicker _questionPicker;

    public ReflectingActivity()
        : base("Reflecting Activity",
               "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
        _promptPicker = new RandomPicker(new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        });

        _questionPicker = new RandomPicker(new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        });
    }

    public override void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine("Consider the following prompt:\n");
        Console.WriteLine($" --- {_promptPicker.GetNext()} ---\n");
        Console.WriteLine("When you have something in mind, press enter to continue.");
        Console.ReadLine();

        Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
        Console.Write("You may begin in: ");
        ShowCountDown(5);
        Console.Clear();

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (GetSecondsLeft(endTime) > 0)
        {
            Console.Write($"> {_questionPicker.GetNext()} ");
            ShowSpinner(Math.Min(10, GetSecondsLeft(endTime)));
            Console.WriteLine("\n");
        }

        DisplayEndingMessage();
    }
}