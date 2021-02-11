using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Text.RegularExpressions;

namespace BlockBase.BBLinq.Validators
{
    public class TableValidator
    {
        public static void Validate(object obj)
        {
            var type = obj.GetType();
            var tableName = type.GetTableName();
            ValidateNameForWrongCharacters(type, tableName);
        }

        private static void ValidateNameForWrongCharacters(Type type, string tableName)
        {
            var regex = new Regex("([^(A-Za-z_0-9)])");
            if(regex.IsMatch(tableName))
            {
                throw new InvalidTableException(type, tableName);
            }
        }
    }
}
