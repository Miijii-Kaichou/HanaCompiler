namespace Hana.CodeAnalysis.Syntax
{
    /// <summary>
    /// Store all information of our syntax
    /// </summary>
    internal static class SyntaxFacts
    {
        internal static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.ExclamToken:
                case SyntaxKind.MinusToken:
                    return 5;

                default:
                    return 0;
            }
        }

        internal static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.StarToken:
                case SyntaxKind.FSlashToken:
                    return 4;

                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 3;

                case SyntaxKind.AndToken:
                    return 2;

                case SyntaxKind.OrToken:
                    return 1;

                default:
                    return 0;
            }
        }

        public static SyntaxKind GetKeywordKind(string text)
        {
            switch (text)
            {
                case "true":
                case "TRUE":
                    return SyntaxKind.TrueKeyword;

                case "false":
                case "FALSE":
                    return SyntaxKind.FalseKeyword;

                default:
                    return SyntaxKind.IdentifierToken;
            }
        }
    }
}
