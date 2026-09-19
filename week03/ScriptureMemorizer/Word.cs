public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public void Show()
    {
        _isHidden = false;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    // showHint is optional: if we don't pass it, it is false,
    // so the default behavior respects the assignment (all letters hidden).
    public string GetDisplayText(bool showHint = false)
    {
        if (!_isHidden)
        {
            return _text;
        }

        char[] chars = _text.ToCharArray();
        bool firstLetterFound = false;

        for (int i = 0; i < chars.Length; i++)
        {
            if (char.IsLetter(chars[i]))
            {
                // CREATIVITY: hint mode. When it is on, the first letter of each
                // hidden word stays visible ("G__" instead of "___") to help memory.
                if (showHint && !firstLetterFound)
                {
                    firstLetterFound = true;
                }
                else
                {
                    chars[i] = '_';
                }
            }
            // CREATIVITY: punctuation (commas, periods, semicolons) is never
            // replaced, so the structure of the sentence stays readable.
        }

        return new string(chars);
    }
}