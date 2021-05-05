using BlockBase.BBLinq.Model.Base;
using System.Reflection;

namespace BlockBase.BBLinq.Model.Nodes
{
    internal class PropertyNode : ExpressionNode
    {
        public PropertyInfo Property { get; }

        public PropertyNode(PropertyInfo property)
        {
            Property = property;
        }
    }
}
