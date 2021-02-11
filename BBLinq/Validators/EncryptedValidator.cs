using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Reflection;

namespace BlockBase.BBLinq.Validators
{
    public static class EncryptedValidator
    {
        public static void Validate(Type type, PropertyInfo property)
        {
            var encryptedAttribute = property.GetEncrypted();

            ValidateBucket(type, property, encryptedAttribute);
        }

        public static void ValidateBucket(Type type, PropertyInfo property, EncryptedAttribute encrypted)
        {
            if (encrypted.Buckets < 1)
            {
                throw new InvalidBucketException(type, property, encrypted.Buckets);
            }
        }
    }
}
