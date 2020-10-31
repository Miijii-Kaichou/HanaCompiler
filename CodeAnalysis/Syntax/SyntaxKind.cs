namespace Hana.CodeAnalysis.Syntax
{
    public enum SyntaxKind
    {
        // Tokens
        BadToken,
        EOFToken,
        WhiteSpaceToken,
        NumberToken,     
        PlusToken,
        MinusToken,
        StarToken,
        FSlashToken,
        LParenToken,
        RParenToken,
        LBrackToken,
        RBrackToken,
        LCurlToken,
        RCurlToken,
        OperatorToken,
        KeywordToken,
        
        //Expressions
        LiteralExpressionToken,
        UnaryExpressionToken,
        BinaryExpressionToken,
        ParenExpressionToken
    }
}
