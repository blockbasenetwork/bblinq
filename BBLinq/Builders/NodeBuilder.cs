using System;
using System.Reflection;
using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Nodes;

namespace BlockBase.BBLinq.Builders
{
    internal static class NodeBuilder
    {
        public static ComparisonNode GenerateComparisonNodeOnObjectKey(Type type, object obj)
        {
            var primaryKey = type.GetPrimaryKey();
            var primaryKeyOnObject = obj.GetType().GetProperty(primaryKey.Name);
            if(primaryKeyOnObject == null || primaryKeyOnObject.PropertyType != primaryKey.PropertyType)
            {
                throw new NoPropertyFoundException(obj.GetType().ToString(), primaryKey.Name);
            }
            
            var leftNode = new PropertyNode(primaryKey);
            var rightNode = new ValueNode(primaryKeyOnObject.GetValue(obj));
            return new ComparisonNode(BlockBaseComparisonOperator.EqualTo, leftNode, rightNode);
        }

        public static ComparisonNode GenerateComparisonNodeOnKey(Type type, object obj)
        {
            var primaryKey = type.GetPrimaryKey();
            if (primaryKey == null || obj.GetType() != primaryKey.PropertyType)
            {
                throw new NoPropertyFoundException(obj.GetType().ToString(), primaryKey?.Name);
            }

            var leftNode = new PropertyNode(primaryKey);
            var rightNode = new ValueNode(obj);
            return new ComparisonNode(BlockBaseComparisonOperator.EqualTo, leftNode, rightNode);
        }


        public static ComparisonNode GenerateComparisonNodeOnKey(object obj)
        {
            return GenerateComparisonNodeOnObjectKey(obj.GetType(), obj);
        }

        internal static ComparisonNode GenerateNotNode(PropertyInfo property)
        {
            return GenerateSinglePropertyNode(property, false);
        }

        internal static ComparisonNode GenerateSinglePropertyNode(PropertyInfo property, bool value = true)
        {
            if (property.PropertyType != typeof(bool))
            {
                throw new PropertyNotBooleanException(property);
            }

            return new ComparisonNode(BlockBaseComparisonOperator.EqualTo, new PropertyNode(property), new ValueNode(false));
        }

       
    }
}

