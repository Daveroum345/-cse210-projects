using System;

public class Entry
{
    private string _date;
    private string _prompt;
    private string _response;
    private string _mood;

    public Entry(string date, string prompt, string response, string mood)
    {
        _date = date;
        _prompt = prompt;
        _response = response;
        _mood = mood;
    }

    public string GetDate()
    {
        return _date;
    }

    public string GetPrompt()
    {
        return _prompt;
    }

    public string GetResponse()
    {
        return _response;
    }

    public string GetMood()
    {
        return _mood;
    }

    public void Display()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Mood: {_mood}");
        Console.WriteLine($"Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}");
        Console.WriteLine("----------------------------------------");
    }

    public string ToFileString()
    {
        return $"{_date}|{_mood}|{_prompt}|{_response}";
    }

    public static Entry FromFileString(string line)
    {
        string[] parts = line.Split('|');

        if (parts.Length < 4)
        {
            return null;
        }

        string date = parts[0];
        string mood = parts[1];
        string prompt = parts[2];
        string response = string.Join("|", parts, 3, parts.Length - 3);

        return new Entry(date, prompt, response, mood);
    }
}