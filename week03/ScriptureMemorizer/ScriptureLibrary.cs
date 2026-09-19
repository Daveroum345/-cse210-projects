using System;
using System.Collections.Generic;
using System.IO;

public class ScriptureLibrary
{
    private List<string> _lines = new List<string>();
    private Random _random = new Random();
    private int _lastIndex = -1;

    public ScriptureLibrary(string filePath)
    {
        if (File.Exists(filePath))
        {
            LoadFromFile(filePath);
        }

        // CREATIVITY: safety net. If the file is missing or empty,
        // the program still works with a default scripture instead of crashing.
        if (_lines.Count == 0)
        {
            _lines.Add("John|3|16||For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.");
        }
    }

    private void LoadFromFile(string filePath)
    {
        foreach (string line in File.ReadAllLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            // CREATIVITY: each line is tested before being kept.
            // A badly written line (missing part, letters instead of a number)
            // is skipped silently instead of crashing the whole program.
            try
            {
                ParseLine(line);
                _lines.Add(line);
            }
            catch (Exception)
            {
                // Invalid line: we ignore it
            }
        }
    }

    // Transforms one line of the file into a brand new Scripture object
    private Scripture ParseLine(string line)
    {
        string[] parts = line.Split('|');

        string book = parts[0];
        int chapter = int.Parse(parts[1]);
        int startVerse = int.Parse(parts[2]);
        string text = parts[4];

        Reference reference;
        if (string.IsNullOrWhiteSpace(parts[3]))
        {
            reference = new Reference(book, chapter, startVerse);
        }
        else
        {
            reference = new Reference(book, chapter, startVerse, int.Parse(parts[3]));
        }

        return new Scripture(reference, text);
    }

    public Scripture GetRandomScripture()
    {
        int index = _random.Next(_lines.Count);

        // CREATIVITY: never gives the same scripture twice in a row
        // (when the library has more than one), so practice stays varied.
        if (_lines.Count > 1)
        {
            while (index == _lastIndex)
            {
                index = _random.Next(_lines.Count);
            }
        }

        _lastIndex = index;
        return ParseLine(_lines[index]);
    }
}