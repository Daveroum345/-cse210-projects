public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;

    // Constructor 1: single verse, for example "John 3:16"
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = verse;
    }

    // Constructor 2: verse range, for example "Proverbs 3:5-6"
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;

        // CREATIVITY: if the verses are given in the wrong order (6-5),
        // the program fixes it instead of showing a wrong reference.
        if (endVerse < startVerse)
        {
            int temp = startVerse;
            startVerse = endVerse;
            endVerse = temp;
        }

        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    public string GetDisplayText()
    {
        if (_startVerse == _endVerse)
        {
            return $"{_book} {_chapter}:{_startVerse}";
        }

        return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
    }

    // CREATIVITY: tells how many verses the passage has.
    // It will be used later to display "(2 verses)" for the user.
    public int GetVerseCount()
    {
        return _endVerse - _startVerse + 1;
    }
}