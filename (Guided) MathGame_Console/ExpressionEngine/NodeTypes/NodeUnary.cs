namespace Branch_Console;

using System;

internal class NodeUnary : Node
{

    public NodeUnary(Node rhs, Func<double, double> op)
    {

        _rhs = rhs;
        _op = op;
    }

    Node _rhs;

    Func<double, double> _op;


    internal override double Eval()
    {
        var rhsVal = _rhs.Eval();

        return _op(rhsVal);
    }


}
