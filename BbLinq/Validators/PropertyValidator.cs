using BlockBase.BBLinq.Validators.AnnotationValidators;
using System;
using System.Reflection;

namespace BlockBase.BBLinq.Validators
{
    /// <summary>
    /// Performs a type's property
    /// </summary>
    public static class PropertyValidator
    {
        public static void Validate(Type type, PropertyInfo property)
        {
            ColumnValidator.Validate(type, property);
            RangeValidator.Validate(type, property);
            EncryptedValidator.Validate(type, property);
        }


    }
}
