public class NegativeGoal : Goal
{
    public NegativeGoal(string name, string description, int penalty)
        : base(name, description, penalty)
    {
    }

    public override int RecordEvent()
    {
        return -GetPoints();
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetDetailsString()
    {
        return $"[!] {GetShortName()} ({GetDescription()}) -- Costs {GetPoints()} points each time";
    }

    public override string GetStringRepresentation()
    {
        return $"NegativeGoal|{GetShortName()}|{GetDescription()}|{GetPoints()}";
    }
}