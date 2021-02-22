using System;
using System.Reflection;

namespace BlockBase.BBLinq.Pocos.ExpressionParser
{
    public class PropertyExpression : ExpressionNode
    {
        public PropertyInfo Column { get; set; }

        public Type Table { get; set; }
    }
}
