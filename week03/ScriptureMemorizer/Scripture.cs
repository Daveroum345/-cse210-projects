using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;

        // Split the text into words and create one Word object for each
        _words = text
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(word => new Word(word))
            .ToList();
    }

    // Stretch challenge: we only pick among the words that are NOT already hidden,
    // so every round really hides new words.
    public void HideRandomWords(int numberToHide)
    {
        List<Word> visibleWords = _words.Where(w => !w.IsHidden()).ToList();

        // We can't hide more words than what is still visible
        int count = Math.Min(numberToHide, visibleWords.Count);

        for (int i = 0; i < count; i++)
        {
            int index = _random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public string GetDisplayText(bool showHint = false)
    {
        string text = string.Join(" ", _words.Select(w => w.GetDisplayText(showHint)));

        // CREATIVITY: the header shows the reference AND the number of verses,
        // for example "Proverbs 3:5-6 (2 verses)". It uses GetVerseCount() from Reference.
        int verseCount = _reference.GetVerseCount();
        string verseLabel = verseCount == 1 ? "verse" : "verses";

        return $"{_reference.GetDisplayText()} ({verseCount} {verseLabel})\n\n{text}";
    }

    // CREATIVITY: progress percentage. Returns how much of the scripture is hidden
    // (0 to 100). The program will display it as a progress indicator each round.
    public int GetProgressPercentage()
    {
        int hiddenCount = _words.Count(w => w.IsHidden());
        return hiddenCount * 100 / _words.Count;
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }
}