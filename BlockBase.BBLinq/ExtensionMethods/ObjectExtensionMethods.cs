using System.Collections.Generic;
using BlockBase.BBLinq.Pocos;

namespace BlockBase.BBLinq.ExtensionMethods
{
    /// <summary>
    /// Static methods for types
    /// </summary>
    public static class ObjectExtensionMethods
    {
        /// <summary>
        /// Deconstructs the object into a (Table, Field, Value) object
        /// </summary>
        /// <param name="object">the object</param>
        /// <returns></returns>
        public static FieldValue[] GetFields(this object @object)
        {
            var type = @object.GetType();
            var properties = type.GetProperties();
            var list = new List<FieldValue>();
            foreach (var property in properties)
            {
                var tableName = type.GetTableName();
                var fieldName = property.GetFieldName();
                var field = new FieldValue() {Table = tableName, Field = fieldName, Value = property.GetValue(@object)};
                list.Add(field);
            }

            return list.ToArray();
        }
    }
}
