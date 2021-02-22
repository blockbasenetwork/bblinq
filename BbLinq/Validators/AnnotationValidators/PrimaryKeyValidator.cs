using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BlockBase.BBLinq.Validators.AnnotationValidators
{
    /// <summary>
    /// Performs a validation to a class that has a primary key
    /// </summary>
    public static class PrimaryKeyValidator
    {
        private static readonly Type[] AcceptableTypes = { typeof(Guid), typeof(int) };
        private const int MaximumPrimaryKeys = 1;
        private const int MinimumPrimaryKeys = 1;

        /// <summary>
        /// Executes the validation process
        /// </summary>
        /// <param name="type">the object to validate</param>
        public static void Validate(Type type)
        {
            ValidatePrimaryKeyCount(type);
            var key = type.GetPrimaryKeyProperties()[0];
            ValidatePrimaryKeyType(key);
        }

        /// <summary>
        /// Checks if the number of keys is different from the expected (1)
        /// </summary>
        /// <param name="type"></param>
        private static void ValidatePrimaryKeyCount(Type type)
        {
            var primaryKeys = type.GetPrimaryKeyProperties();
            if (primaryKeys == null || primaryKeys.Length < MinimumPrimaryKeys)
            {
                throw new NoPrimaryKeyFoundException(type.Name);
            }

            if (primaryKeys.Length <= MaximumPrimaryKeys)
            {
                return;
            }
            var primaryKeyNames = new List<string>();
            foreach (var primaryKey in primaryKeys)
            {
                primaryKeyNames.Add(primaryKey.Name);
            }
            throw new TooManyPrimaryKeysException(type.Name, primaryKeyNames);
        }

        /// <summary>
        /// Checks if the key's type matches one of the accepted types
        /// </summary>
        private static void ValidatePrimaryKeyType(PropertyInfo property)
        {

            foreach (var acceptableType in AcceptableTypes)
            {
                if (property.PropertyType == acceptableType)
                {
                    return;
                }
            }
            var acceptableTypes = new string[AcceptableTypes.Length];

            for (var counter = 0; counter < acceptableTypes.Length; counter++)
            {
                acceptableTypes[counter] = AcceptableTypes[counter].Name;
            }

            throw new InvalidPrimaryKeyTypeException(property.Name, acceptableTypes);
        }


    }
}
