using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    private string[] _titles =
    {
        "Novice Adventurer",
        "Apprentice",
        "Seeker",
        "Guardian",
        "Champion",
        "Eternal Legend"
    };

    public void Start()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine();
            DisplayPlayerInfo();
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoalDetails();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }

        Console.WriteLine("Keep going on your Eternal Quest. Goodbye!");
    }

    private int GetLevel()
    {
        return Math.Max(0, _score) / 1000 + 1;
    }

    private string GetTitle(int level)
    {
        int index = Math.Min(level - 1, _titles.Length - 1);
        return _titles[index];
    }

    private void DisplayPlayerInfo()
    {
        int level = GetLevel();
        int intoLevel = Math.Max(0, _score) % 1000;
        int filled = intoLevel / 100;
        string bar = new string('#', filled) + new string('-', 10 - filled);

        Console.WriteLine($"You have {_score} points.");
        Console.WriteLine($"Level {level}: {GetTitle(level)}  [{bar}]");
    }

    private void ListGoalNames()
    {
        Console.WriteLine("The goals are:");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetShortName()}");
        }
    }

    private void ListGoalDetails()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet.");
            return;
        }

        Console.WriteLine("The goals are:");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.WriteLine("  4. Negative Goal (a bad habit to avoid)");
        int type = ReadInt("Which type of goal would you like to create? ");

        if (type < 1 || type > 4)
        {
            Console.WriteLine("Invalid goal type.");
            return;
        }

        string name = ReadText("What is the name of your goal? ");
        string description = ReadText("What is a short description of it? ");

        if (type == 4)
        {
            int penalty = ReadInt("How many points are lost each time? ");
            _goals.Add(new NegativeGoal(name, description, penalty));
            return;
        }

        int points = ReadInt("What is the amount of points associated with this goal? ");

        if (type == 1)
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (type == 2)
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else
        {
            int target = ReadInt("How many times does this goal need to be accomplished for a bonus? ");
            int bonus = ReadInt("What is the bonus for accomplishing it that many times? ");
            _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
        }
    }

    private void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet. Create one first.");
            return;
        }

        ListGoalNames();
        int number = ReadInt("Which goal did you accomplish? ");

        if (number < 1 || number > _goals.Count)
        {
            Console.WriteLine("That goal number does not exist.");
            return;
        }

        Goal goal = _goals[number - 1];

        if (goal.IsComplete())
        {
            Console.WriteLine("That goal is already complete.");
            return;
        }

        int levelBefore = GetLevel();
        int points = goal.RecordEvent();
        _score += points;

        if (points >= 0)
        {
            Console.WriteLine($"Congratulations! You have earned {points} points!");
        }
        else
        {
            Console.WriteLine($"Oh no! You lost {-points} points. Keep going!");
        }

        if (goal.IsComplete())
        {
            Console.WriteLine("Goal completed!");
        }

        int levelAfter = GetLevel();

        if (levelAfter > levelBefore)
        {
            Console.WriteLine($"*** LEVEL UP! You are now level {levelAfter}: {GetTitle(levelAfter)} ***");
        }

        Console.WriteLine($"You now have {_score} points.");
    }

    private void SaveGoals()
    {
        string filename = ReadText("What is the filename for the goal file? ");

        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(_score);

            foreach (Goal goal in _goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved.");
    }

    private void LoadGoals()
    {
        string filename = ReadText("What is the filename for the goal file? ");

        if (!File.Exists(filename))
        {
            Console.WriteLine("That file does not exist.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);
            List<Goal> loaded = new List<Goal>();
            int score = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');

                switch (parts[0])
                {
                    case "SimpleGoal":
                        loaded.Add(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4])));
                        break;
                    case "EternalGoal":
                        loaded.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                        break;
                    case "ChecklistGoal":
                        loaded.Add(new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]),
                            int.Parse(parts[5]), int.Parse(parts[4]), int.Parse(parts[6])));
                        break;
                    case "NegativeGoal":
                        loaded.Add(new NegativeGoal(parts[1], parts[2], int.Parse(parts[3])));
                        break;
                }
            }

            _score = score;
            _goals = loaded;
            Console.WriteLine("Goals loaded.");
        }
        catch (Exception)
        {
            Console.WriteLine("That file could not be read. It may not be a valid goal file.");
        }
    }

    private int ReadInt(string prompt)
    {
        int value;
        Console.Write(prompt);

        while (!int.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("Please enter a whole number: ");
        }

        return value;
    }

    private string ReadText(string prompt)
    {
        Console.Write(prompt);
        string text = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(text))
        {
            Console.Write("This cannot be empty. Try again: ");
            text = Console.ReadLine();
        }

        return text.Replace("|", " ").Trim();
    }
}