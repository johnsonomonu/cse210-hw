using System;
using System.Collections.Generic;

class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }
}

class Journal
{
    private List<Entry> entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        entries.Add(entry);
    }

    public List<Entry> GetAllEntries()
    {
        return entries;
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
                        Prompt = question,
                        Response = response,
                        Date = date // Set the current date
                    };
                    journal.AddEntry(entry);
                    break;

                case 2:
                    // Display all journal entries with the current date
                    List<Entry> allEntries = journal.GetAllEntries();
                    foreach (var journalEntry in allEntries)
                    {
                        Console.WriteLine($"Date: {journalEntry.Date}");
                        Console.WriteLine($"Prompt: {journalEntry.Prompt}");
                        Console.WriteLine($"Response: {journalEntry.Response}\n");
                    }
                    break;

                case 3:
                    // Implement saving to a file
                    break;

                case 4:
                    // Implement loading from a file
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
