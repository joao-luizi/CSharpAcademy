namespace Branch_Console;


internal class Parser
{
    public Parser(Tokenizer tokenizer)
    {
        _tokenizer = tokenizer;
    }
    public Parser(string expression)
    {
        _tokenizer = new Tokenizer(new StringReader(expression));
    }

    internal Tokenizer _tokenizer;

    public Node ParseExpression()
    {

        var expr = ParseAddSubtract();

        if (_tokenizer.Token != Token.EOF)
            throw new SyntaxException("Unexpected characters at end of expression");

        return expr;
    }


    Node ParseAddSubtract()
    {
        var lhs = ParseMultiplyDivide();

        while (true)
        {
            Func<double, double, double> op = null;
            if (_tokenizer.Token == Token.Add)
            {
                op = (a, b) => a + b;
            }
            else if (_tokenizer.Token == Token.Subtract)
            {
                op = (a, b) => a - b;
            }

            if (op == null)
                return lhs;             // no

            _tokenizer.NextToken();

            var rhs = ParseMultiplyDivide();

            lhs = new NodeBinary(lhs, rhs, op);
        }
    }

    Node ParseMultiplyDivide()
    {

        var lhs = ParseUnary();

        while (true)
        {
            Func<double, double, double> op = null;
            if (_tokenizer.Token == Token.Multiply)
            {
                op = (a, b) => a * b;
            }
            else if (_tokenizer.Token == Token.Divide)
            {
                op = (a, b) => a / b;
            }

            if (op == null)
                return lhs;             // no

            _tokenizer.NextToken();

            var rhs = ParseUnary();

            lhs = new NodeBinary(lhs, rhs, op);
        }
    }


    Node ParseUnary()
    {
        if (_tokenizer.Token == Token.Add)
        {
            _tokenizer.NextToken();
            return ParseUnary();
        }

        if (_tokenizer.Token == Token.Subtract)
        {

            _tokenizer.NextToken();

            var rhs = ParseUnary();

            return new NodeUnary(rhs, (a) => -a);
        }
        return ParseLeaf();
    }


    Node ParseLeaf()
    {
        if (_tokenizer.Token == Token.Number)
        {
            var node = new NodeNumber(_tokenizer.Number);
            _tokenizer.NextToken();
            return node;
        }

        if (_tokenizer.Token == Token.OpenParens)
        {
            _tokenizer.NextToken();

            var node = ParseAddSubtract();

            if (_tokenizer.Token != Token.CloseParens)
                throw new SyntaxException("Missing close parenthesis");
            _tokenizer.NextToken();

            return node;
        }
        throw new SyntaxException($"Unexpect token: {_tokenizer.Token}");
    }


    internal static Node Parse(string str)
    {
        return Parse(new Tokenizer(new StringReader(str)));
    }

    internal static Node Parse(Tokenizer tokenizer)
    {
        var parser = new Parser(tokenizer);
        return parser.ParseExpression();
    }


}
