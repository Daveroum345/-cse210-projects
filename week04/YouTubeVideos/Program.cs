using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Learn C# in 10 Minutes", "CodeWithAmina", 600);
        video1.AddComment(new Comment("Kouame", "Very clear explanation, thank you!"));
        video1.AddComment(new Comment("Sarah", "I finally understand classes."));
        video1.AddComment(new Comment("Ibrahim", "Can you make one about lists?"));
        videos.Add(video1);

        Video video2 = new Video("Street Food Tour in Abidjan", "TasteOfWestAfrica", 845);
        video2.AddComment(new Comment("Fatou", "Now I am hungry!"));
        video2.AddComment(new Comment("Marc", "Best alloco recipe ever."));
        video2.AddComment(new Comment("Aya", "Please do Bouake next."));
        video2.AddComment(new Comment("Daniel", "Great video quality."));
        videos.Add(video2);

        Video video3 = new Video("5 Tips for Social Media Growth", "DigitalGrowthHub", 420);
        video3.AddComment(new Comment("Jean", "Tip number 3 worked for me."));
        video3.AddComment(new Comment("Mariam", "Short and useful."));
        video3.AddComment(new Comment("Paul", "Subscribed!"));
        videos.Add(video3);

        Video video4 = new Video("Morning Workout Routine", "FitLifeDaily", 930);
        video4.AddComment(new Comment("Esther", "Did this today, I feel great."));
        video4.AddComment(new Comment("Yao", "Too hard for me, but I will try."));
        video4.AddComment(new Comment("Linda", "Love the music you chose."));
        videos.Add(video4);

        foreach (Video video in videos)
        {
            video.Display();
        }
    }
}