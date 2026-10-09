Random randomGenerator = new Random();
int magicNumber = randomGenerator.Next(1, 101);

Console.Write("What is your guess? ");
string guessText = Console.ReadLine();
int guess = int.Parse(guessText);

while (guess != magicNumber)
{
    if (guess > magicNumber)
    {
        Console.WriteLine("Lower");
    }
    else
    {
        Console.WriteLine("Higher");
    }

    Console.Write("What is your guess? ");
    guessText = Console.ReadLine();
    guess = int.Parse(guessText);
}

Console.WriteLine("You guessed it!");