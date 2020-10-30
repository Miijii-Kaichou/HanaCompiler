using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Hana.CodeAnalysis
{
    internal sealed class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private List<string> _diagnostics = new List<string>();
        private int _position;
        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.NextToken();
                if (token.Kind != SyntaxKind.WhiteSpaceToken &&
                    token.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(token);
                }
            } while (token.Kind != SyntaxKind.EOFToken);

            _tokens = tokens.ToArray();
            _diagnostics.AddRange(lexer.Diagnostics);
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private SyntaxToken Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1];

            return _tokens[index];
        }

        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        private SyntaxToken MatchToken(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();

            _diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}>, expected <{kind}>");

            return new SyntaxToken(kind, Current.Position, null, null);
        }


        public SyntaxTree Parse()
        {
            var expression = ParseExpression();
            var EOFToken = MatchToken(SyntaxKind.EOFToken);
            return new SyntaxTree(_diagnostics, expression, EOFToken);
        }

        private ExpressionSyntax ParseExpression(int parentPrecedence = 0)
        {
            var left = ParsePrimaryExpression();

            while (true)
            {
                var precedence = GetBinaryOperatorPrecedence(Current.Kind);
                if (precedence == 0 || precedence <= parentPrecedence)
                    break;

                var operatorToken = NextToken();
                var right = ParseExpression(precedence);
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

           return left;
        }

        private static int GetBinaryOperatorPrecedence(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.BadToken:
                    break;
                case SyntaxKind.EOFToken:
                    break;
                case SyntaxKind.WhiteSpaceToken:
                    break;
                case SyntaxKind.NumberToken:
                    break;
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 1;

                case SyntaxKind.MultiplyToken:
                case SyntaxKind.DivideToken:
                    return 2;
                case SyntaxKind.LParenToken:
                    break;
                case SyntaxKind.RParenToken:
                    break;
                case SyntaxKind.LBrackToken:
                    break;
                case SyntaxKind.RBrackToken:
                    break;
                case SyntaxKind.LCurlToken:
                    break;
                case SyntaxKind.RCurlToken:
                    break;
                case SyntaxKind.OperatorToken:
                    break;
                case SyntaxKind.KeywordToken:
                    break;
                case SyntaxKind.LiteralExpressionToken:
                    break;
                case SyntaxKind.BinaryExpressionToken:
                    break;
                case SyntaxKind.ParenExpressionToken:
                    break;
                default:
                    return 0;
            }

            return -1;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.LParenToken)
            {
                var left = NextToken();
                var expression = ParseExpression();
                var right = MatchToken(SyntaxKind.RParenToken);
                return new ParenthesizedExpressionSyntax(left, expression, right);
            }

            var numberToken = MatchToken(SyntaxKind.NumberToken);
            return new LiteralExpressionSyntax(numberToken);
        }
    }
}
