using System.Reflection;
using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.ExtensionMethods;

namespace BlockBase.BBLinq.Pocos
{
    public class ColumnDefinition
    {
        public string Name { get; set; }
        public BbSqlDataTypeEnum Type { get; set; }
        public string Table { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsColumnEncrypted { get; set; }
        public bool IsValueEncrypted { get; set; }
        public int BucketCount { get; set; }
        public bool HasBuckets { get; set; }
        public bool IsRange { get; set; }
        public int RangeMinimum { get; set; }
        public int RangeMaximum { get; set; }
        public bool IsForeignKey { get; set; }
        public string ParentTableName { get; set; }
        public string ParentTableKeyName { get; set; }
        public bool IsNotNull { get; set; }


        /// <summary>
        /// Generates a Field info from a property type and the other sets on a context.
        /// </summary>
        public static ColumnDefinition From(PropertyInfo property)
        {
            var propertyType = property.IsNullable() ? property.PropertyType.GetNullableType() : property.PropertyType;

            var column = new ColumnDefinition
            {
                Name = property.GetColumnName(),
                Table = property.DeclaringType.GetTableName(),
                IsPrimaryKey = property.IsPrimaryKey(),
                IsColumnEncrypted = property.IsColumnEncrypted(),
                IsValueEncrypted = property.IsValueEncrypted(),
                IsRange = property.IsRange(),
                IsForeignKey = property.IsForeignKey(),
                IsNotNull = property.IsNotNull(),
            };

            if (column.IsRange)
            {
                var range = property.GetRange();
                column.BucketCount = range.Buckets;
                column.RangeMaximum = range.Maximum;
                column.RangeMinimum = range.Minimum;
            }

            if (column.IsValueEncrypted)
            {
                var encrypted = property.GetValueEncryptedProperties()[0];
                column.BucketCount = encrypted.Buckets;
            }

            column.HasBuckets = column.BucketCount > 0;


            if (column.IsForeignKey)
            {

                var fkConstraint = property.GetForeignKeys()[0];
                var primaryKey = fkConstraint.Parent.GetPrimaryKeyProperty();
                column.ParentTableKeyName = primaryKey.GetColumnName();
                column.ParentTableName = primaryKey.DeclaringType.GetTableName();

            }

            column.Type = column.IsValueEncrypted ? BbSqlDataTypeEnum.Encrypted : propertyType.ToBbSqlType();
            return column;
        }
    }
}
