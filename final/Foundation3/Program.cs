using System;

class Program
{
    static void Main()
    {
        // Create instances of each event type
        Event lectureEvent = new Lecture("Tech Conference", "Exciting tech topics", new DateTime(2023, 12, 15), new TimeSpan(10, 30, 0),
            new Address("123 Tech Street", "Tech City", "Tech State", "Tech Country"), "John Doe", 100);

        Event receptionEvent = new Reception("Networking Mixer", "Meet and greet", new DateTime(2023, 12, 20), new TimeSpan(18, 0, 0),
            new Address("456 Mixer Street", "Mixer City", "Mixer State", "Mixer Country"));
        ((Reception)receptionEvent).SetRSVPInformation("info@example.com");

        Event outdoorEvent = new OutdoorGathering("Summer Picnic", "Enjoy the outdoors", new DateTime(2023, 7, 8), new TimeSpan(12, 0, 0),
            new Address("789 Park Street", "Park City", "Park State", "Park Country"));
        ((OutdoorGathering)outdoorEvent).SetWeatherStatement("Sunny weather expected");

        // Display details for each event
        DisplayEventDetails(lectureEvent);
        DisplayEventDetails(receptionEvent);
        DisplayEventDetails(outdoorEvent);
    }

    static void DisplayEventDetails(Event eventObj)
    {
        // Display standard details
        Console.WriteLine("Standard Details:");
        Console.WriteLine(eventObj.GenerateStandardDetails());

        // Display full details
        Console.WriteLine("\nFull Details:");
        Console.WriteLine(eventObj.GenerateFullDetails());

        // Display short description
        Console.WriteLine("\nShort Description:");
        Console.WriteLine(eventObj.GenerateShortDescription());

        Console.WriteLine("\n------------------------\n");
    }
}

class Event
{
    private string _eventTitle;
    private string _eventDescription;
    private DateTime _eventDate;
    private TimeSpan _eventTime;
    private Address _eventAddress;

    public Event(string title, string description, DateTime date, TimeSpan time, Address address)
    {
        _eventTitle = title;
        _eventDescription = description;
        _eventDate = date;
        _eventTime = time;
        _eventAddress = address;
    }

    public string GenerateStandardDetails()
    {
        return $"Title: {_eventTitle}\nDescription: {_eventDescription}\nDate: {_eventDate.ToShortDateString()}\nTime: {_eventTime}\nAddress: {_eventAddress.GetFullAddress()}";
    }

    public virtual string GenerateFullDetails()
    {
        return GenerateStandardDetails();
    }

    public string GenerateShortDescription()
    {
        return $"Type: Generic Event\nTitle: {_eventTitle}\nDate: {_eventDate.ToShortDateString()}";
    }
}

class Lecture : Event
{
    private string _speakerName;
    private int _capacity;

    public Lecture(string title, string description, DateTime date, TimeSpan time, Address address, string speakerName, int capacity)
        : base(title, description, date, time, address)
    {
        _speakerName = speakerName;
        _capacity = capacity;
    }

    public override string GenerateFullDetails()
    {
        return $"{base.GenerateFullDetails()}\nSpeaker: {_speakerName}\nCapacity: {_capacity}";
    }
}

class Reception : Event
{
    private string _rsvpEmail;

    public Reception(string title, string description, DateTime date, TimeSpan time, Address address)
        : base(title, description, date, time, address)
    {
    }

    public void SetRSVPInformation(string email)
    {
        _rsvpEmail = email;
    }

    public override string GenerateFullDetails()
    {
        return $"{base.GenerateFullDetails()}\nRSVP Email: {_rsvpEmail}";
    }
}

class OutdoorGathering : Event
{
    private string _weatherStatement;

    public OutdoorGathering(string title, string description, DateTime date, TimeSpan time, Address address)
        : base(title, description, date, time, address)
    {
    }

    public void SetWeatherStatement(string weatherStatement)
    {
        _weatherStatement = weatherStatement;
    }

    public override string GenerateFullDetails()
    {
        return $"{base.GenerateFullDetails()}\nWeather Statement: {_weatherStatement}";
    }
}

class Address
{
    private string _streetAddress;
    private string _city;
    private string _stateProvince;
    private string _country;

    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        _streetAddress = streetAddress;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    public string GetFullAddress()
    {
        return $"{_streetAddress}, {_city}, {_stateProvince}, {_country}";
    }
}
