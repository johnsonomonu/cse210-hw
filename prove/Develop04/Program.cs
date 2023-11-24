using System;
using System.Collections.Generic;
using System.Threading;

// Base class for Mindfulness Activities
class Activity
{
    private string _name;
    private string _description;
    protected int _duration;

    public Activity(string name, string description, int duration)
    {
        _name = name;
        _description = description;
        _duration = duration;
    }

    public void DisplayStartingMessage()
    {
        Console.WriteLine($"Welcome to {_name}!");
        Console.WriteLine($"Description: {_description}");

        // Prompt user to set the duration
        Console.Write("Enter the duration in seconds: ");
        _duration = GetUserInput();

        Console.WriteLine("Get ready to begin...");
        PauseWithCountdown(3); // Pause for 3 seconds
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine($"Congratulations! You've completed {_name} for {_duration} seconds.");
        PauseWithCountdown(3); // Pause for 3 seconds
    }

    public void PauseWithSpinner()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.Write("-");
            Thread.Sleep(200);
            Console.Write("\b");
        }
        Console.WriteLine();
    }

    public void PauseWithCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.WriteLine($"Countdown: {i}");
            Thread.Sleep(1000);
        }
    }

    public void RunActivity()
    {
        DisplayStartingMessage();
        RunSpecificActivity();
        DisplayEndingMessage();
    }

    protected virtual void RunSpecificActivity()
    {
        // Implementation of specific activity in derived classes
    }

    protected int GetUserInput()
    {
        // Implementation of getting user input
        return int.Parse(Console.ReadLine()); // Placeholder value, add proper error handling
    }
}

// BreathingActivity class
class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.", 0)
    {
        // Additional setup for BreathingActivity
    }

    protected override void RunSpecificActivity()
    {
        Console.WriteLine("Let's begin the breathing exercise:");
        for (int i = 0; i < _duration; i++)
        {
            Console.WriteLine((i % 2 == 0) ? "Breathe in..." : "Breathe out...");
            PauseWithCountdown(3); // Pause for 3 seconds
        }
    }
}

// ReflectingActivity class
class ReflectingActivity : Activity
{
    private List<string> _reflectionPrompts;
    private List<string> _reflectionQuestions;

    public ReflectingActivity() : base("Reflecting Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.", 0)
    {
        _reflectionPrompts = new List<string>
        {
            "Reflect Prompt 1",
            "Reflect Prompt 2",
            // Add more prompts as needed
        };

        _reflectionQuestions = new List<string>();
        // Add reflection questions as needed
    }

    protected override void RunSpecificActivity()
    {
        Console.WriteLine("Let's begin reflecting:");
        for (int i = 0; i < _duration; i++)
        {
            DisplayPrompt();
            DisplayQuestionsAndCollectAnswers();
        }
    }

    private void DisplayPrompt()
    {
        Console.WriteLine(GetRandomPrompt());
    }

    private void DisplayQuestionsAndCollectAnswers()
    {
        foreach (var question in _reflectionQuestions)
        {
            Console.WriteLine(question);
            PauseWithSpinner();
        }
    }

    private string GetRandomPrompt()
    {
        // Implementation of getting a random prompt
        if (_reflectionPrompts.Count == 0)
            return "Default Reflection Prompt";

        int index = new Random().Next(0, _reflectionPrompts.Count);
        return _reflectionPrompts[index];
    }
}

// ListingActivity class
class ListingActivity : Activity
{
    private List<string> _listingPrompts;

    public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.", 0)
    {
        _listingPrompts = new List<string>
        {
            "Listing Prompt 1",
            "Listing Prompt 2",
            // Add more prompts as needed
        };
    }

    protected override void RunSpecificActivity()
    {
        Console.WriteLine("Let's begin listing:");
        for (int i = 0; i < _duration; i++)
        {
            DisplayPrompt();
            ListItems();
        }
    }

    private void DisplayPrompt()
    {
        Console.WriteLine(GetRandomListingPrompt());
        PauseWithCountdown(5); // Pause for 5 seconds to think about the prompt
    }

    private void ListItems()
    {
        Console.WriteLine("List as many items as you can. Press Enter after each item:");
        List<string> items = new List<string>();
        string input;
        do
        {
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                items.Add(input);
            }
        } while (!string.IsNullOrEmpty(input));

        Console.WriteLine($"Number of items listed: {items.Count}");
    }

    private string GetRandomListingPrompt()
    {
        // Implementation of getting a random listing prompt
        if (_listingPrompts.Count == 0)
            return "Default Listing Prompt";

        int index = new Random().Next(0, _listingPrompts.Count);
        return _listingPrompts[index];
    }
}

// Program class
class Program
{
    static void Main()
    {
        DisplayMenu();
        int choice = GetUserInput();
        RunSelectedActivity(choice);
    }

    public static void DisplayMenu()
    {
        Console.WriteLine("Select an activity:");
        Console.WriteLine("1. Breathing Activity");
        Console.WriteLine("2. Reflecting Activity");
        Console.WriteLine("3. Listing Activity");
    }

    public static void RunSelectedActivity(int choice)
    {
        Activity selectedActivity = null;

        switch (choice)
        {
            case 1:
                selectedActivity = new BreathingActivity();
                break;
            case 2:
                selectedActivity = new ReflectingActivity();
                break;
            case 3:
                selectedActivity = new ListingActivity();
                break;
            default:
                Console.WriteLine("Invalid choice. Exiting program.");
                Environment.Exit(0);
                break;
        }

        selectedActivity.RunActivity();
    }

    public static int GetUserInput()
    {
        int choice;

        while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 3))
        {
            Console.WriteLine("Invalid input. Please enter a valid choice.");
        }

        return choice;
    }
}
