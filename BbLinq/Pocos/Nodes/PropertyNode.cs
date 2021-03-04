using System.Reflection;

namespace BlockBase.BBLinq.Pocos.Nodes
{
    public class PropertyNode : ExpressionNode
    {
        public PropertyInfo Property { get; }

        public PropertyNode(PropertyInfo property)
        {
            Property = property;
        }
    }
}
