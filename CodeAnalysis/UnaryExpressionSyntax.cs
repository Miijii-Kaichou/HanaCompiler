using System.Collections.Generic;

namespace Hana.CodeAnalysis
{
    public sealed class UnaryExpressionSyntax : ExpressionSyntax
    {
        public UnaryExpressionSyntax(SyntaxToken operatorToken, ExpressionSyntax operand)
        {
            OperatorToken = operatorToken;
            Operand = operand;
        }

        public SyntaxToken OperatorToken { get; }
        public ExpressionSyntax Operand { get; }
        public override SyntaxKind Kind => SyntaxKind.UnaryExpressionToken;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OperatorToken;
            yield return Operand;
        }
    }
}
