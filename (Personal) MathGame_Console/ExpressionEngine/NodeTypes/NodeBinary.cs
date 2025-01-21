namespace Branch_Console;

using System;

internal class NodeBinary : Node
{

    public NodeBinary(Node lhs, Node rhs, Func<double, double, double> op)
    {
        _lhs = lhs;
        _rhs = rhs;
        _op = op;
    }

    Node _lhs;

    Node _rhs;

    Func<double, double, double> _op;


    internal override double Eval()
    {
        var lhsVal = _lhs.Eval();
        var rhsVal = _rhs.Eval();

        return _op(lhsVal, rhsVal);
    }


}
