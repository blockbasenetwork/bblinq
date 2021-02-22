using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Reflection;

namespace BlockBase.BBLinq.Validators.AnnotationValidators
{
    /// <summary>
    /// Performs a validation to a class that has a table name
    /// </summary>
    public class TableValidator
    {
        /// <summary>
        /// Checks if a table is valid
        /// </summary>
        public static void Validate(Type type)
        {
            var tableName = type.GetTableName();
            if (tableName != null)
            {
                ValidateNameForWrongCharacters(type, tableName);
            }
        }

        /// <summary>
        /// Checks if a table has an appropriate name
        /// </summary>
        private static void ValidateNameForWrongCharacters(MemberInfo type, string tableName)
        {
            if (tableName.HasNonAlphanumericOrUnderscore())
            {
                throw new InvalidTableNameException(type.Name, tableName);
            }
        }
    }
}
