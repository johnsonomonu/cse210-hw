using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string _prompt;
    public string _response;
    public string _date;

    public void DisplayEntryInfo()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}\n");
    }
}

class Journal
{
    private List<Entry> entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        entries.Add(entry);
    }

    public void DisplayAllEntries()
    {
        foreach (var entry in entries)
        {
            entry.DisplayEntryInfo();
        }
    }

    public void SaveEntriesToFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine($"{entry._date},{entry._prompt},{entry._response}");
            }
        }
        Console.WriteLine("Journal saved to file.");
    }

    public void LoadEntriesFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        Entry entry = new Entry
                        {
                            _date = parts[0],
                            _prompt = parts[1],
                            _response = parts[2]
                        };
                        AddEntry(entry);
                    }
                }
            }
            Console.WriteLine("Journal loaded from file.");
        }
        else
        {
            Console.WriteLine("No journal file found.");
        }
    }
}

class PromptGenerator
{
    private string[] prompts = {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(prompts.Length);
        return prompts[index];
    }
}

class Program
{
    static void Main()
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        while (true)
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write a New Entry");
            Console.WriteLine("2. Display the Journal");
            Console.WriteLine("3. Save the Journal to a File");
            Console.WriteLine("4. Load the Journal from a File");
            Console.WriteLine("5. Exit");
            Console.WriteLine("What would you like to do?");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Ask a question using the PromptGenerator
                    string question = promptGenerator.GetRandomPrompt();
                    Console.WriteLine($"Question: {question}");

                    // Automatically generate the current date
                    string date = DateTime.Now.ToString("yyyy-MM-dd");

                    Console.WriteLine("Enter your response: ");
                    string response = Console.ReadLine();

                    Entry entry = new Entry
                    {
                        _prompt = question,
                        _response = response,
                        _date = date // Set the current date
                    };
                    journal.AddEntry(entry);
                    break;

                case 2:
                    journal.DisplayAllEntries();
                    break;

                case 3:
                    Console.WriteLine("Enter the file name to save: ");
                    string saveFileName = Console.ReadLine();
                    journal.SaveEntriesToFile(saveFileName); // Save the journal to the specified file
                    break;

                case 4:
                    Console.WriteLine("Enter the file name to load: ");
                    string loadFileName = Console.ReadLine();
                    journal.LoadEntriesFromFile(loadFileName); // Load the journal from the specified file
                    break;

                case 5:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
