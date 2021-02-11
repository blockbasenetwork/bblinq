using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Reflection;

namespace BlockBase.BBLinq.Validators
{
    public static class RangeValidator
    {
        public static void Validate(Type type, PropertyInfo property)
        {
            var rangeAttribute = property.GetRange();
            if(rangeAttribute != default)
            {
                ValidateBucket(type, property, rangeAttribute);
            }
        }

        public static void ValidateBucket(Type type, PropertyInfo property, RangeAttribute range)
        {
            if(range.Buckets < 1)
            {
                throw new InvalidBucketException(type, property, range.Buckets);
            }
        }

        public static void ValidateRange(Type type, PropertyInfo property, RangeAttribute range)
        {
            if(range.MinimumValue > range.MaximumValue)
            {
                throw new InvalidRangeException(type, property, range);
            }
        }
    }
}
