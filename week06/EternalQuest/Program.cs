// EXCEEDING THE REQUIREMENTS
// 1. Levels and titles: every 1000 points the player goes up a level and
//    earns a new title (from "Novice Adventurer" to "Eternal Legend"). A
//    progress bar toward the next level is shown with the score, and a
//    "LEVEL UP" message appears when a new level is reached.
// 2. Negative goals: a fourth kind of goal (the NegativeGoal class) for bad
//    habits. Each time one is recorded, the player loses points.
// 3. Extra care with files: goals and the score are saved and loaded, and a
//    file that cannot be read shows a message instead of crashing.

class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        goalManager.Start();
    }
}