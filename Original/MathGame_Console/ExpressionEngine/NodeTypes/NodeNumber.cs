namespace Branch_Console;
internal class NodeNumber : Node
{
    public NodeNumber(double number)
    {
        _number = number;
    }

    double _number;

    internal override double Eval()
    {
        return _number;
    }


}