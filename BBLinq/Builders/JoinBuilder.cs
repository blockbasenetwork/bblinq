using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Nodes;
using System;
using System.Collections.Generic;

namespace BlockBase.BBLinq.Builders
{
    internal static class JoinBuilder
    {
        public static Type[] DrawTypesFromJoinNodes(JoinNode[] joinNodes)
        {
            var types = new List<Type>();
            foreach (var joinNode in joinNodes)
            {
                var leftType = joinNode.Left.Property.ReflectedType;
                var rightType = joinNode.Right.Property.ReflectedType;

                var addLeft = true;
                var addRight = leftType != rightType;
                foreach (var type in types)
                {
                    if (addLeft && leftType == type)
                    {
                        addLeft = false;
                    }
                    if (addRight && rightType == type)
                    {
                        addRight = false;
                    }
                }

                if (addLeft)
                {
                    types.Add(joinNode.Left.Property.ReflectedType);
                }
                if (addRight)
                {
                    types.Add(joinNode.Right.Property.ReflectedType);
                }
            }

            return types.ToArray();
        }

        public static JoinNode[] BuildJoins(Type[] entities)
        {
            var joinList = new List<JoinNode>();
            var sortedEntities = new List<Type>(entities.SortByDependency());
            var currentEntity = sortedEntities[0];

            while (sortedEntities.Count > 1)
            {
                sortedEntities.Remove(currentEntity);
                JoinNode join = null;


                foreach (var entity in sortedEntities)
                {
                    join = BuildJoin(currentEntity, entity, BlockBaseJoinEnum.Inner);
                    if (join != null)
                    {
                        currentEntity = entity;
                        break;
                    }
                }
                if (join != null)
                {
                    joinList.Add(join);
                }
            }

            return joinList.ToArray();
        }

        public static JoinNode BuildJoin(Type[] types, Type newType, BlockBaseJoinEnum joinType)
        {
            foreach (var type in types)
            {
                var join = BuildJoin(type, newType, joinType);
                if (join != null)
                {
                    return join;
                }
            }
            return null;
        }

        public static JoinNode BuildJoin(Type left, Type right, BlockBaseJoinEnum type)
        {
            var leftParentOnRight = right.GetForeignKey(left);
            if (leftParentOnRight != null)
            {
                var leftPrimaryKey = left.GetPrimaryKey();
                return new JoinNode(leftPrimaryKey, leftParentOnRight, type);
            }

            var rightParentOnLeft = left.GetForeignKey(right);
            if (rightParentOnLeft == null)
            {
                return null;
            }
            var rightPrimaryKey = right.GetPrimaryKey();
            return new JoinNode(rightParentOnLeft, rightPrimaryKey, type);
        }
    }
}
