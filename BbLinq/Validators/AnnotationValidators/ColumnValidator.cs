using System;
using System.Reflection;
using System.Text.RegularExpressions;
using BlockBase.BBLinq.Annotations;
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
        /// <param name="type">the model type</param>
        /// <param name="property">the property that holds the column attribute</param>
        public static void Validate(Type type, PropertyInfo property)
        {
            var columnAttribute = property.GetColumn();
            if(columnAttribute != null)
            {
                ValidateNameForWrongCharacters(type, property, columnAttribute);
            }
        }

        
        /// <summary>
        /// Checks if a column has an appropriate name
        /// </summary>
        /// <param name="type">the model type</param>
        /// <param name="property">the property that holds the column attribute</param>
        /// <param name="column">the column attribute</param>
        private static void ValidateNameForWrongCharacters(Type type, PropertyInfo property, ColumnAttribute column)
        {
            var regex = new Regex("([^(A-Za-z_0-9)])");
            if (regex.IsMatch(column.Name))
            {
                throw new InvalidColumnNameException(type.Name, property.PropertyType.Name, column.Name);
            }
        }
        
    }
}
