using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using System;
using System.Reflection;

namespace BlockBase.BBLinq.Validators.AnnotationValidators
{
    public static class EncryptedValidator
    {
        public static void Validate(Type type, PropertyInfo property)
        {
            var encryptedAttribute = property.GetEncrypted();
            if(encryptedAttribute != null)
            {
                ValidateBucket(type, property, encryptedAttribute);
            }
        }

        public static void ValidateBucket(Type type, PropertyInfo property, EncryptedAttribute encrypted)
        {
            if (encrypted.Buckets < 0)
            {
                throw new InvalidBucketException(type.Name, property.Name, encrypted.Buckets);
            }
        }
    }
}
