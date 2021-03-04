namespace BlockBase.BBLinq.Pocos.Nodes
{
    public class ValueNode : ExpressionNode
    {
        public object Value { get; }

        public ValueNode(object value)
        {
            Value = value;
        }
    }
}
