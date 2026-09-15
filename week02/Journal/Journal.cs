using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void Display()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine();
            Console.WriteLine("Your journal is empty.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("========== YOUR JOURNAL ==========");

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }

        Console.WriteLine($"Total entries: {_entries.Count}");
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine(entry.ToFileString());
            }
        }

        Console.WriteLine();
        Console.WriteLine("Journal saved successfully!");
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine();
            Console.WriteLine("The file was not found.");
            return;
        }

        _entries.Clear();

        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                Entry entry = Entry.FromFileString(line);

                if (entry != null)
                {
                    _entries.Add(entry);
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Journal loaded successfully! {_entries.Count} entries loaded.");
    }

    public void Search(string keyword)
    {
        bool found = false;

        Console.WriteLine();
        Console.WriteLine($"========== SEARCH RESULTS FOR: {keyword} ==========");

        foreach (Entry entry in _entries)
        {
            if (entry.GetResponse().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                entry.GetPrompt().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                entry.GetMood().Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                entry.Display();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No matching entries were found.");
        }
    }

    public void ShowStatistics()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine();
            Console.WriteLine("There are no entries to analyze.");
            return;
        }

        Dictionary<string, int> moodCounts = new Dictionary<string, int>();

        foreach (Entry entry in _entries)
        {
            string mood = entry.GetMood();

            if (moodCounts.ContainsKey(mood))
            {
                moodCounts[mood]++;
            }
            else
            {
                moodCounts[mood] = 1;
            }
        }

        string mostCommonMood = "";
        int highestCount = 0;

        foreach (KeyValuePair<string, int> mood in moodCounts)
        {
            if (mood.Value > highestCount)
            {
                highestCount = mood.Value;
                mostCommonMood = mood.Key;
            }
        }

        Console.WriteLine();
        Console.WriteLine("========== JOURNAL STATISTICS ==========");
        Console.WriteLine($"Total entries: {_entries.Count}");
        Console.WriteLine($"Most common mood: {mostCommonMood}");
        Console.WriteLine($"Times recorded: {highestCount}");
    }
}