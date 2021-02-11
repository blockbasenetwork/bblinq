using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BlockBase.BBLinq.Validators
{
    public class ColumnValidator
    {
        public static void Validate(Type type, PropertyInfo property)
        {
            var columnAttribute = property.GetColumn();
            ValidateNameForWrongCharacters(type, property, columnAttribute);
        }

        

        private static void ValidateNameForWrongCharacters(Type type, PropertyInfo property, ColumnAttribute column)
        {
            var regex = new Regex("([^(A-Za-z_0-9)])");
            if (regex.IsMatch(column.Name))
            {
                throw new InvalidColumnNameException(type, property, column.Name);
            }
        }
        
    }
}
