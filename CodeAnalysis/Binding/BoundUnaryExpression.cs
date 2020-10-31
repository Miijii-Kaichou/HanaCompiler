using System;

namespace Hana.CodeAnalysis.Binding

{
    internal sealed class BoundUnaryExpression : BoundExpression
    {
        public BoundUnaryExpression(BoundUnaryOperator op, BoundExpression operand)
        {
            Op = op;
            Operand = operand;
        }
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;
        public BoundUnaryOperator Op { get; }
        public BoundExpression Operand { get; }

        public override Type Type => Op.Type;
    }
}
