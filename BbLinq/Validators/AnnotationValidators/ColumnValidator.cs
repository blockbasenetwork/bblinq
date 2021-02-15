using System;
using System.Reflection;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;

namespace BlockBase.BBLinq.Validators.AnnotationValidators
{
    /// <summary>
    /// Performs a validation to a property that has a column name
    /// </summary>
    public static class ColumnValidator
    {
        /// <summary>
        /// Checks if a column is valid
        /// </summary>
        public static void Validate(Type type, PropertyInfo property)
        {
            var columnAttribute = property.GetColumnName();
            if(columnAttribute != null)
            {
                ValidateNameForWrongCharacters(type, property, columnAttribute);
            }
        }

        
        /// <summary>
        /// Checks if a column has an appropriate name
        /// </summary>
        private static void ValidateNameForWrongCharacters(Type type, PropertyInfo property, string columnName)
        {
            if (columnName.HasNonAlphanumericOrUnderscore())
            {
                throw new InvalidColumnNameException(type.Name, property.PropertyType.Name, columnName);
            }
        }
        
    }
}
