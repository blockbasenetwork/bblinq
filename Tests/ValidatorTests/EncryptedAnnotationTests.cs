using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Validators.AnnotationValidators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.ValidatorTests
{
    public class EncryptionTestClassA
    {
        [Encrypted(Buckets = 1)]
        public string Prop { get; set; }
    }

    public class EncryptionTestClassB
    {
        [Encrypted]
        public string Prop { get; set; }
    }

    public class EncryptionTestClassC
    {
        [Encrypted(Buckets = 0)]
        public string Prop { get; set; }
    }

    public class EncryptionTestClassD
    {
        public string Prop { get; set; }
    }

    [TestClass]
    public class EncryptedAnnotationTests
    {

        [TestMethod]
        public void TestEncryptionWithBuckets()
        {
            try
            {
                var type = typeof(EncryptionTestClassA);
                var property = type.GetProperty("Prop");
                EncryptedValidator.Validate(type, property);
                Assert.IsTrue(true);
            }
            catch(Exception)
            {
                Assert.IsTrue(false);
            }
        }
        
        [TestMethod]
        public void TestEncrytionNoBuckets()
        {
            try
            {
                var type = typeof(EncryptionTestClassB);
                var property = type.GetProperty("Prop");
                EncryptedValidator.Validate(type, property);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }


        [TestMethod]
        public void TestEncryptionZeroBuckets()
        {
            try
            {
                var type = typeof(EncryptionTestClassC);
                var property = type.GetProperty("Prop");
                EncryptedValidator.Validate(type, property);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidBucketException);
            }
        }



        [TestMethod]
        public void TestEncryptionNonEncrypted()
        {
            try
            {
                var type = typeof(EncryptionTestClassD);
                var property = type.GetProperty("Prop");
                EncryptedValidator.Validate(type, property);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
