using System;
using System.Threading;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing Activity",
               "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (true)
        {
            int remaining = GetSecondsLeft(endTime);
            if (remaining <= 0)
            {
                break;
            }

            Console.WriteLine("Breathe in...");
            Breathe(Math.Min(4, remaining), true);

            remaining = GetSecondsLeft(endTime);
            if (remaining <= 0)
            {
                break;
            }

            Console.WriteLine("Breathe out...");
            Breathe(Math.Min(6, remaining), false);
            Console.WriteLine();
        }

        DisplayEndingMessage();
    }

    private void Breathe(int seconds, bool growing)
    {
        int steps = 20;
        int delay = seconds * 1000 / steps;

        for (int i = 1; i <= steps; i++)
        {
            int filled = growing ? i : steps - i;
            int secondsLeft = (int)Math.Ceiling(seconds * (steps - i) / (double)steps);

            Console.Write("\r[" + new string('#', filled) + new string(' ', steps - filled) + "] " + secondsLeft + "s  ");
            Thread.Sleep(delay);
        }

        Console.WriteLine();
    }
}