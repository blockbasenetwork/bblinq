using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Reflection;

namespace BlockBase.BBLinq.Validators.AnnotationValidators
{
    public static class EncryptedValidator
    {
        private const int MinimumBuckets = 0;
        public static void Validate(Type type, PropertyInfo property)
        {
            var encryptedAttributes = property.GetEncrypted();
            if (encryptedAttributes == null || encryptedAttributes.Length < 1)
            {
                return;
            }
            var encrypted = encryptedAttributes[0];
            ValidateBucket(type, property, encrypted);
        }

        public static void ValidateBucket(Type type, PropertyInfo property, EncryptedAttribute encrypted)
        {
            if (encrypted.Buckets < MinimumBuckets)
            {
                throw new InvalidBucketException(type.Name, property.Name, encrypted.Buckets);
            }
        }
    }
}
