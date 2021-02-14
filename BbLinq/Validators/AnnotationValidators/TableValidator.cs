using System;
using System.Reflection;
using System.Text.RegularExpressions;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;

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
        /// <param name="type">the model type</param>
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
        /// <param name="type">the model type</param>
        /// <param name="tableName">the table name</param>
        private static void ValidateNameForWrongCharacters(MemberInfo type, string tableName)
        {
            var regex = new Regex("([^(A-Za-z_0-9)])");
            if(regex.IsMatch(tableName))
            {
                throw new InvalidTableNameException(type.Name, tableName);
            }
        }
    }
}
