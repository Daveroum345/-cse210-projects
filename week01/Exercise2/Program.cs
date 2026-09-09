Console.Write("Enter your grade percentage: ");
string input = Console.ReadLine();
int percentage = int.Parse(input);

string letter;

if (percentage >= 90)
{
    letter = "A";
}
else if (percentage >= 80)
{
    letter = "B";
}
else if (percentage >= 70)
{
    letter = "C";
}
else if (percentage >= 60)
{
    letter = "D";
}
else
{
    letter = "F";
}

Console.WriteLine($"Your grade is: {letter}");

if (percentage >= 70)
{
    Console.WriteLine("Congratulations! You passed the course.");
}
else
{
    Console.WriteLine("Keep working hard. You can do better next time.");
}