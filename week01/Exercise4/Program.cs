using System.Collections.Generic;

List<int> numbers = new List<int>();

Console.WriteLine("Enter a list of numbers, type 0 when finished.");

int number = -1;

while (number != 0)
{
    Console.Write("Enter number: ");
    number = int.Parse(Console.ReadLine());

    if (number != 0)
    {
        numbers.Add(number);
    }
}

int sum = 0;

foreach (int currentNumber in numbers)
{
    sum += currentNumber;
}

Console.WriteLine($"The sum is: {sum}");
double average = (double)sum / numbers.Count;

Console.WriteLine($"The average is: {average}");
