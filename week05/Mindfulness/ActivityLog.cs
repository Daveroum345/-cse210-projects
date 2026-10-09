using System;
using System.Collections.Generic;
using System.IO;

public class ActivityLog
{
    private string _filePath;
    private Dictionary<string, int> _counts = new Dictionary<string, int>();
    private Dictionary<string, int> _seconds = new Dictionary<string, int>();

    public ActivityLog(string filePath)
    {
        _filePath = filePath;
    }

    public void Record(string activityName, int seconds)
    {
        if (!_counts.ContainsKey(activityName))
        {
            _counts[activityName] = 0;
            _seconds[activityName] = 0;
        }

        _counts[activityName] += 1;
        _seconds[activityName] += seconds;
    }

    public void Load()
    {
        if (!File.Exists(_filePath))
        {
            return;
        }

        foreach (string line in File.ReadAllLines(_filePath))
        {
            string[] parts = line.Split('|');

            if (parts.Length == 3 && int.TryParse(parts[1], out int count) && int.TryParse(parts[2], out int seconds))
            {
                _counts[parts[0]] = count;
                _seconds[parts[0]] = seconds;
            }
        }
    }

    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (string name in _counts.Keys)
        {
            lines.Add($"{name}|{_counts[name]}|{_seconds[name]}");
        }

        File.WriteAllLines(_filePath, lines);
    }

    public void Display()
    {
        Console.WriteLine("Your activity log:\n");

        if (_counts.Count == 0)
        {
            Console.WriteLine("No activities logged yet.");
            return;
        }

        foreach (string name in _counts.Keys)
        {
            Console.WriteLine($"{name}: {_counts[name]} time(s), {_seconds[name]} seconds in total");
        }
    }
}