﻿namespace Hana.CodeAnalysis.Syntax
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
                case SyntaxKind.MinusToken:
                    return 3;

                default:
                    return 0;
            }
        }

        internal static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 1;

                case SyntaxKind.StarToken:
                case SyntaxKind.FSlashToken:
                    return 2;

                default:
                    return 0;
            }
        }

    }
}
