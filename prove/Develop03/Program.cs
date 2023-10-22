using System;
using System.Collections.Generic;
using System.Linq;

class Reference
{
    private string _book;
    private int _chapter;
    private int _verseStart;
    private int _verseEnd;

    public Reference(string referenceText)
    {
        // Implement parsing the reference from the referenceText
        // Example: Parse "John 3:16" into book, chapter, and verse.
    }

    public override string ToString()
    {
        // Return a formatted string of the reference.
        return $"{_book} {_chapter}:{_verseStart}-{_verseEnd}";
    }
}

class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(string referenceText, string text)
    {
        _reference = new Reference(referenceText);
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void HideRandomWord()
    {
        Random random = new Random();
        List<Word> visibleWords = _words.Where(word => !word.IsHidden()).ToList();
        if (visibleWords.Count > 0)
        {
            int randomIndex = random.Next(visibleWords.Count);
            visibleWords[randomIndex].Hide();
        }
    }

    public string GetRenderedText()
    {
        string renderedText = _reference.ToString() + " ";

        foreach (Word word in _words)
        {
            renderedText += word.GetRenderedText() + " ";
        }

        return renderedText;
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(word => word.IsHidden());
    }
}

class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false; // Initialize as not hidden
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

    public string GetRenderedText()
    {
        if (_isHidden)
        {
            // Return some placeholder like "_____" for hidden words
            return "_____";
        }
        return _text;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a Scripture object with the reference and text
        Scripture scripture = new Scripture("John 3:16", "For God so loved the world...");

        Console.WriteLine(scripture.GetRenderedText());

        while (!scripture.IsCompletelyHidden())
        {
            Console.WriteLine("Please press Enter to continue or type 'quit' to finish.");
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "quit")
            {
                break; // Exit the program if the user types "quit"
            }

            // Hide a random word in the scripture
            scripture.HideRandomWord();
            Console.WriteLine(scripture.GetRenderedText());
        }
    }
}
