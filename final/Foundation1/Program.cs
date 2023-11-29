using System;
using System.Collections.Generic;

class Video
{
    private string _title;
    private string _author;
    private int _lengthInSeconds;
    private List<Comment> _comments;

    public Video()
    {
        _comments = new List<Comment>();
    }

    public void TrackTitle(string title)
    {
        _title = title;
    }

    public void TrackAuthor(string author)
    {
        _author = author;
    }

    public void TrackLength(int lengthInSeconds)
    {
        _lengthInSeconds = lengthInSeconds;
    }

    public void AddComment(string commenterName, string commentText)
    {
        Comment comment = new Comment();
        comment.TrackCommenterName(commenterName);
        comment.TrackCommentText(commentText);
        _comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    public void DisplayVideoInformation()
    {
        Console.WriteLine($"Title: {_title}");
        Console.WriteLine($"Author: {_author}");
        Console.WriteLine($"Length: {_lengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");

        Console.WriteLine("Comments:");
        foreach (var comment in _comments)
        {
            comment.DisplayCommentInformation();
        }

        Console.WriteLine();
    }
}

class Comment
{
    private string _commenterName;
    private string _commentText;

    public void TrackCommenterName(string name)
    {
        _commenterName = name;
    }

    public void TrackCommentText(string text)
    {
        _commentText = text;
    }

    public void DisplayCommentInformation()
    {
        Console.WriteLine($"Commenter: {_commenterName}");
        Console.WriteLine($"Text: {_commentText}");
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>();

        // Create and populate videos
        Video video1 = new Video();
        video1.TrackTitle("Title1");
        video1.TrackAuthor("Author1");
        video1.TrackLength(300);
        video1.AddComment("Commenter1", "Great video!");
        video1.AddComment("Commenter2", "Interesting content.");
        videos.Add(video1);

        // Add more videos...

        // Display video information
        foreach (var video in videos)
        {
            video.DisplayVideoInformation();
        }
    }
}
