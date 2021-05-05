using BlockBase.BBLinq.Enumerables;
using System;

namespace BlockBase.BBLinq.ExtensionMethods
{
    internal static class DataTypeExtensionMethods
    {
        public static BlockBaseDataTypeEnum ToBbSqlType(this Type type)
        {
            if (type == typeof(bool))
                return BlockBaseDataTypeEnum.Bool;
            if (type == typeof(int) || type == typeof(long))
                return BlockBaseDataTypeEnum.Int;
            if (type == typeof(decimal))
                return BlockBaseDataTypeEnum.Decimal;
            if (type == typeof(DateTime))
                return BlockBaseDataTypeEnum.DateTime;
            if (type == typeof(double) || type == typeof(float))
                return BlockBaseDataTypeEnum.Decimal;
            return type == typeof(TimeSpan) ?
                BlockBaseDataTypeEnum.Duration :
                BlockBaseDataTypeEnum.Text;
        }
    }
}
