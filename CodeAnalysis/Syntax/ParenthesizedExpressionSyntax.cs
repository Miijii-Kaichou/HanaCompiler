using System.Collections.Generic;

namespace Hana.CodeAnalysis.Syntax
{
    public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public ParenthesizedExpressionSyntax(SyntaxToken lParenToken, ExpressionSyntax expression, SyntaxToken rParenToken)
        {
            LParenToken = lParenToken;
            Expression = expression;
            RParenToken = rParenToken;
        }

        public SyntaxToken LParenToken { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken RParenToken { get; }

        public override SyntaxKind Kind => SyntaxKind.ParenExpressionToken;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LParenToken;
            yield return Expression;
            yield return RParenToken;
        }
    }
}
