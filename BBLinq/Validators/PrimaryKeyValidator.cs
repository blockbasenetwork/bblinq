using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BlockBase.BBLinq.Validators
{
    /// <summary>
    /// Performs a validation to a class that has a primary key
    /// </summary>
    public static class PrimaryKeyValidator
    {
        private static readonly Type[] _acceptableTypes = new[] { typeof(Guid), typeof(int) };
        
        /// <summary>
        /// Executes the validation process
        /// </summary>
        /// <param name="obj">the object to validate</param>
        public static void Validate(object obj)
        {
            var type = obj.GetType();
            ValidatePrimaryKeyCount(type);
            var key = type.GetPrimaryKey();
            ValidatePrimaryKeyType(key);
        }

        /// <summary>
        /// Checks if the number of keys is different from the expected (1)
        /// </summary>
        /// <param name="type"></param>
        private static void ValidatePrimaryKeyCount(Type type)
        {
            var primaryKeys = type.GetPrimaryKeys() as List<PropertyInfo>;
            if(primaryKeys.Count== 0)
            {
                throw new NoPrimaryKeyFoundException(type);
            }
            if (primaryKeys.Count > 1)
            {
                throw new TooManyPrimaryKeysException(type, primaryKeys);
            }
        }

        /// <summary>
        /// Checks if the key's type matches one of the accepted types
        /// </summary>
        /// <param name="property">the property</param>
        private static void ValidatePrimaryKeyType(PropertyInfo property)
        {

            foreach(var acceptableType in _acceptableTypes)
            {
                if(property.PropertyType == acceptableType)
                {
                    return;
                }
            }
            throw new InvalidPrimaryKeyTypeException(property, _acceptableTypes);
        }
    }
}
