namespace Hana.CodeAnalysis
{
    /// <summary>
    /// Store all information of our syntax
    /// </summary>
    internal static class SyntaxFacts
    {
        internal static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
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

    }
}
