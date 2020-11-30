using System;
using System.Reflection;
using BlockBase.BBLinq.Enums;
using BlockBase.BBLinq.ExtensionMethods;

namespace BlockBase.BBLinq.Pocos
{
    /// <summary>
    /// Set of information on a field
    /// </summary>
    public class DbFieldInfo
    {
        public string Name { get; set; }
        public BbSqlType Type { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsEncrypted { get; set; }
        public bool IsNotNull { get; set; }
        public bool HasBuckets { get; set; }
        public int BucketCount { get; set; }
        public bool IsRange { get; set; }
        public int MinRange { get; set; }
        public int MaxRange { get; set; }
        public bool IsForeignKey { get; set; }
        public string ForeignTable { get; set; }
        public string ForeignField { get; set; }

        /// <summary>
        /// Generates a Field info from a property type and the other sets on a context.
        /// </summary>
        /// <param name="property">a property</param>
        /// <param name="otherSets">the other sets, unrelated to the current set in question</param>
        /// <returns>a field info</returns>
        public static DbFieldInfo From(PropertyInfo property, Type[] otherSets)
        {
            var field = new DbFieldInfo
            {
                Name = property.GetFieldName(),
                IsPrimaryKey = property.IsPrimaryKey(),
                IsEncrypted = property.IsEncrypted(),
                IsNotNull = property.IsNotNull(),
                IsForeignKey = property.IsForeignKey(),
                IsRange = property.IsRanged(),
                BucketCount = property.GetBuckets()
            };

            field.HasBuckets = field.BucketCount > 0;

            if (field.IsForeignKey)
            {
                var fkConstraint = property.GetForeignField(otherSets);
                field.ForeignField = fkConstraint.Field;
                field.ForeignTable = fkConstraint.Table;
            }

            if (field.IsEncrypted && field.BucketCount > 0)
                field.Type = BbSqlType.Encrypted;
            else
                field.Type = property.PropertyType.ToBbSqlType();

            if (field.IsRange)
            {
                var range = property.GetRange();
                field.MinRange = range.Minimum;
                field.MaxRange = range.Maximum;
                field.BucketCount = range.Buckets;
            }

            return field;
        }
    }
}
