using System;
using System.Collections.Generic;
using System.IO;

// Base class for goals
class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    // Constructor
    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    // Virtual method to record an event and return points
    public virtual int RecordEvent()
    {
        return _points;
    }

    // Virtual method to check if the goal is complete
    public virtual bool IsComplete()
    {
        return false;
    }

    // Getter for the goal name
    public string GetName()
    {
        return _name;
    }

    // Getter for the goal description
    public string GetDescription()
    {
        return _description;
    }

    // Getter for the goal points
    public int GetPoints()
    {
        return _points;
    }
}

// SimpleGoal class derived from Goal
class SimpleGoal : Goal
{
    // Constructor
    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    // Override RecordEvent for SimpleGoal
    public override int RecordEvent()
    {
        // Additional logic for SimpleGoal
        return base.RecordEvent();
    }
}

// EternalGoal class derived from Goal
class EternalGoal : Goal
{
    // Constructor
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
        // No need to override RecordEvent for EternalGoal
    }
}

// ChecklistGoal class derived from Goal
class ChecklistGoal : Goal
{
    private int _completionTarget;
    private int _bonusPoints;
    private int _completedTimes;

    // Constructor
    public ChecklistGoal(string name, string description, int points, int completionTarget, int bonusPoints)
        : base(name, description, points)
    {
        _completionTarget = completionTarget;
        _bonusPoints = bonusPoints;
    }

    // Override RecordEvent for ChecklistGoal
    public override int RecordEvent()
    {
        // Additional logic for ChecklistGoal
        _completedTimes++;

        if (_completedTimes >= _completionTarget)
        {
            // Bonus points for completing the checklist
            return _points + _bonusPoints;
        }

        return _points;
    }

    // Getter for the completed times
    public int GetCompletedTimes()
    {
        return _completedTimes;
    }

    // Getter for the completion target
    public int GetCompletionTarget()
    {
        return _completionTarget;
    }

    // Getter for the bonus points
    public int GetBonusPoints()
    {
        return _bonusPoints;
    }
}

// UserInterface class
class UserInterface
{
    private List<Goal> _goals;
    private int _userScore;

    public int GoalsCount { get; internal set; }

    // Constructor
    public UserInterface()
    {
        _goals = new List<Goal>();
        _userScore = 0;
    }

    // Method to display the user's score
    public void DisplayScore()
    {
        Console.WriteLine($"User Score: {_userScore}");
    }

    // Method to create a new goal
    public Goal CreateGoal(string type, string name, string description, int points, int completionTarget, int bonusPoints)
    {
        Goal goal;

        switch (type.ToLower())
        {
            case "simple":
                goal = new SimpleGoal(name, description, points);
                break;
            case "eternal":
                goal = new EternalGoal(name, description, points);
                break;
            case "checklist":
                goal = new ChecklistGoal(name, description, points, completionTarget, bonusPoints);
                break;
            default:
                throw new ArgumentException("Invalid goal type");
        }

        _goals.Add(goal);
        return goal;
    }

    // Method to record an event for a goal
    public int RecordEvent(Goal goal)
    {
        int points = goal.RecordEvent();
        _userScore += points;
        return points;
    }

    // Method to display the list of goals
    public void DisplayGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            var goal = _goals[i];
            Console.WriteLine($"{i}. {goal.GetName()} - {_userScore} {(goal.IsComplete() ? "[X]" : "[ ]")}");

            if (goal is ChecklistGoal checklistGoal)
            {
                Console.WriteLine($"   Completed {checklistGoal.GetCompletedTimes()}/{checklistGoal.GetCompletionTarget()} times");
            }
        }
    }

    // Method to get a goal by index
    public Goal GetGoalByIndex(int index)
    {
        return _goals[index];
    }

    // Method to save user progress to a file
    public void SaveUserProgress(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var goal in _goals)
            {
                writer.WriteLine($"{goal.GetName()}|{goal.GetDescription()}|{goal.GetPoints()}|{(goal is ChecklistGoal checklist ? checklist.GetCompletionTarget() : 0)}|{(goal is ChecklistGoal bonus ? bonus.GetBonusPoints() : 0)}");
            }

            writer.WriteLine($"UserScore|{_userScore}");
        }
    }

    // Method to load user progress from a file
    public void LoadUserProgress(string fileName)
    {
        _goals.Clear(); // Clear existing goals
        _userScore = 0; // Reset user score

        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('|');

                if (parts[0] == "UserScore")
                {
                    // Load user score
                    _userScore = int.Parse(parts[1]);
                }
                else
                {
                    // Load goal based on type
                    Goal goal;

                    switch (parts[0].ToLower())
                    {
                        case "simple":
                            goal = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                            break;
                        case "eternal":
                            goal = new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
                            break;
                        case "checklist":
                            goal = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
                            break;
                        default:
                            throw new ArgumentException("Invalid goal type");
                    }

                    _goals.Add(goal);
                }
            }
        }
    }
}

class Program
{
    static void Main()
    {
        UserInterface ui = new UserInterface();

        int choice;

        do
        {
            // Display menu
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Enter your choice (1-6): ");

            // Get user choice
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                continue;
            }

            // Perform action based on user choice
            switch (choice)
            {
                case 1:
                    CreateNewGoal(ui);
                    break;
                case 2:
                    ui.DisplayGoals();
                    break;
                case 3:
                    ui.SaveUserProgress("userProgress.txt");
                    break;
                case 4:
                    ui.LoadUserProgress("userProgress.txt");
                    break;
                case 5:
                    RecordEvent(ui);
                    break;
                case 6:
                    Console.WriteLine("Quitting the program.");
                    break;
            }

            // Display score and goals after each action
            ui.DisplayScore();
            ui.DisplayGoals();

        } while (choice != 6);
    }

    static void CreateNewGoal(UserInterface ui)
    {
        // Get user input for goal parameters
        Console.Write("Enter goal type (simple/eternal/checklist): ");
        string type = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();

        Console.Write("Enter goal points: ");
        int points;
        while (!int.TryParse(Console.ReadLine(), out points) || points < 0)
        {
            Console.WriteLine("Invalid input. Please enter a non-negative integer for points.");
            Console.Write("Enter goal points: ");
        }

        int completionTarget = 0;
        int bonusPoints = 0;

        if (type.ToLower() == "checklist")
        {
            Console.Write("Enter completion target: ");
            while (!int.TryParse(Console.ReadLine(), out completionTarget) || completionTarget <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive integer for completion target.");
                Console.Write("Enter completion target: ");
            }

            Console.Write("Enter bonus points: ");
            while (!int.TryParse(Console.ReadLine(), out bonusPoints) || bonusPoints < 0)
            {
                Console.WriteLine("Invalid input. Please enter a non-negative integer for bonus points.");
                Console.Write("Enter bonus points: ");
            }
        }

        // Create and add the goal
        ui.CreateGoal(type, name, description, points, completionTarget, bonusPoints);
    }

    static void RecordEvent(UserInterface ui)
    {
        // Get user input for recording event
        Console.Write("Enter the index of the goal to record an event for: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < ui.GoalsCount)
        {
            Goal selectedGoal = ui.GetGoalByIndex(index);
            ui.RecordEvent(selectedGoal);
            Console.WriteLine($"Event recorded for goal: {selectedGoal.GetName()}");
        }
        else
        {
            Console.WriteLine("Invalid index. Please enter a valid index.");
        }
    }
}
