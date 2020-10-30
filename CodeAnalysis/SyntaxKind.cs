namespace Hana.CodeAnalysis
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
        MultiplyToken,
        DivideToken,
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
        BinaryExpressionToken,
        ParenExpressionToken
    }
}
