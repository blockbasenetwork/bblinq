using System.Reflection;
using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.ExtensionMethods;

namespace BlockBase.BBLinq.Pocos
{
    public class BlockBaseColumn
    {
        public string Name { get; private set; }
        public BbSqlDataTypeEnum Type { get; private set; }
        public bool IsPrimaryKey { get; private set; }
        public bool IsEncrypted { get; private set; }
        public bool IsNotNull { get; private set; }
        public bool HasBuckets { get; private set; }
        public int BucketCount { get; private set; }
        public bool IsRange { get; private set; }
        public int MinRange { get; private set; }
        public int MaxRange { get; private set; }
        public bool IsForeignKey { get; private set; }
        public string ForeignTable { get; private set; }
        public string ForeignColumn { get; private set; }

        /// <summary>
        /// Generates a Field info from a property type and the other sets on a context.
        /// </summary>
        public static BlockBaseColumn From(PropertyInfo property)
        {
            var primaryKeys = property.GetPrimaryKeys();
            var encrypted = property.GetEncrypted();
            var foreignKeys = property.GetForeignKeys();
            var ranges = property.GetRanges();
            var propertyType = property.IsNullable() ? property.PropertyType.GetNullableType() : property.PropertyType;

            var field = new BlockBaseColumn
            {
                Name = property.GetColumnName(),
                IsPrimaryKey = primaryKeys != null && primaryKeys.Length > 0,
                IsEncrypted = encrypted != null && encrypted.Length > 0,
                IsRange = ranges != null && ranges.Length > 0,
                IsForeignKey = foreignKeys != null && foreignKeys.Length > 0,
                IsNotNull = property.IsNotNull(),
            };

            if (field.IsRange && ranges != null)
            {
                field.BucketCount = ranges[0].Buckets;
                field.MaxRange = ranges[0].Maximum;
                field.MinRange = ranges[0].Minimum;
            }

            if (field.IsEncrypted && encrypted != null)
            {
                field.BucketCount = encrypted[0].Buckets;
            }

            field.HasBuckets = field.BucketCount > 0;


            if (field.IsForeignKey && foreignKeys != null)
            {
                var fkConstraint = foreignKeys[0];
                field.ForeignTable = fkConstraint.Parent.GetTableName();
                var primaryKey = fkConstraint.Parent.GetPrimaryKeyProperties();
                field.ForeignColumn = primaryKey[0].GetColumnName();
            }

            if (field.IsEncrypted && field.BucketCount > 0)
                field.Type = BbSqlDataTypeEnum.Encrypted;
            else
                field.Type = propertyType.ToBbSqlType();
            return field;
        }
    }
}
