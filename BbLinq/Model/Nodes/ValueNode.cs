using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Model.Nodes
{
    internal class ValueNode : ExpressionNode
    {
        public object Value { get; set; }

        public ValueNode(object value)
        {
            Value = value;
        }
    }
}
