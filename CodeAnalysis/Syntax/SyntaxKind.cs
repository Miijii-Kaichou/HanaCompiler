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
        IdentifierToken,
        ExclamToken,
        AndToken,
        OrToken,
        RArrowToken,
        DoubleLArrowToken,
        LArrowToken,
        AmpToken,
        PipeToken,
        EquilityToken,
        InEqulityToken,

        //Keywords
        FalseKeyword,
        TrueKeyword,
        
        //Expressions
        LiteralExpressionToken,
        UnaryExpressionToken,
        BinaryExpressionToken,
        ParenExpressionToken
    }
}
