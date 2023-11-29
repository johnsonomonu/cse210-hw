using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Create instances of each activity type
        Activity runningActivity = new Running(new DateTime(2022, 11, 3), 30, 3.0);
        Activity bicyclingActivity = new Bicycling(new DateTime(2022, 11, 3), 30, 6.0);
        Activity swimmingActivity = new Swimming(new DateTime(2022, 11, 3), 30, 10);

        // Create a list containing each activity instance
        List<Activity> activities = new List<Activity>
        {
            runningActivity,
            bicyclingActivity,
            swimmingActivity
        };

        // Iterate through the list and call the GetSummary method for each activity
        foreach (var activity in activities)
        {
            // Display the results of the GetSummary method
            Console.WriteLine(activity.GetSummary());
            Console.WriteLine("------------------------");
        }
    }
}

class Activity
{
    private DateTime _date;
    protected int _lengthMinutes;

    public Activity(DateTime date, int lengthMinutes)
    {
        _date = date;
        _lengthMinutes = lengthMinutes;
    }

    public virtual double GetDistance()
    {
        return 0.0; // Default implementation (to be overridden in derived classes)
    }

    public virtual double GetSpeed()
    {
        return 0.0; // Default implementation (to be overridden in derived classes)
    }

    public virtual double GetPace()
    {
        return 0.0; // Default implementation (to be overridden in derived classes)
    }

    public virtual string GetSummary()
    {
        return $"{_date.ToShortDateString()} - {_lengthMinutes} min";
    }
}

class Running : Activity
{
    private double _distance;

    public Running(DateTime date, int lengthMinutes, double distance)
        : base(date, lengthMinutes)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return _distance / (_lengthMinutes / 60.0);
    }

    public override double GetPace()
    {
        return (_lengthMinutes / _distance);
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} Running - Distance: {_distance} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }
}

class Bicycling : Activity
{
    private double _speed;

    public Bicycling(DateTime date, int lengthMinutes, double speed)
        : base(date, lengthMinutes)
    {
        _speed = speed;
    }

    public double GetSpeedKph()
    {
        return _speed;
    }

    public override double GetSpeed()
    {
        return _speed / 0.621371; // Convert speed from kph to mph
    }

    public override double GetPace()
    {
        return 60.0 / (_speed / 0.621371); // Convert speed from kph to mph and calculate pace
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} Bicycling - Speed: {_speed} kph, Speed: {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }
}

class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int lengthMinutes, int laps)
        : base(date, lengthMinutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        return _laps * 50.0 / 1000.0; // Convert laps to distance in kilometers
    }

    public override double GetSpeed()
    {
        return GetDistance() / (_lengthMinutes / 60.0);
    }

    public override double GetPace()
    {
        return (_lengthMinutes / GetDistance());
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} Swimming - Laps: {_laps}, Distance: {GetDistance()} km, Speed: {GetSpeed()} kph, Pace: {GetPace()} min per km";
    }
}
