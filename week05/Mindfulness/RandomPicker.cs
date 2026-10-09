using System;
using System.Collections.Generic;

public class RandomPicker
{
    private List<string> _all;
    private List<string> _remaining;
    private Random _random = new Random();

    public RandomPicker(List<string> items)
    {
        _all = items;
        _remaining = new List<string>(items);
    }

    public string GetNext()
    {
        if (_remaining.Count == 0)
        {
            _remaining = new List<string>(_all);
        }

        int index = _random.Next(_remaining.Count);
        string item = _remaining[index];
        _remaining.RemoveAt(index);

        return item;
    }
}