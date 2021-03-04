using BlockBase.BBLinq.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BlockBase.BBLinq.Validators.AnnotationValidators;

namespace Tests.ValidatorTests
{
    public class RangeTestClassA
    {
        [Range(0, 100)]
        public string Prop { get; set; }
    }

    public class RangeTestClassB
    {
        [Range(0, 100, 0)]
        public string Prop { get; set; }
    }

    public class RangeTestClassC
    {
        [Range(200, 100)]
        public string Prop { get; set; }
    }

    public class RangeTestClassD
    {
        public string Prop { get; set; }
    }

    [TestClass]
    public class RangeAnnotationTests
    {
        [TestMethod]
        public void TestCorrectRange()
        {
            try
            {
                var type = typeof(RangeTestClassA);
                var property = type.GetProperty("Prop");
                RangeValidator.Validate(type, property);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestIncorrectBucket()
        {
            try
            {
                var type = typeof(RangeTestClassB);
                var property = type.GetProperty("Prop");
                RangeValidator.Validate(type, property);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidBucketException);
            }
        }


        [TestMethod]
        public void TestIncorrectRange()
        {
            try
            {
                var type = typeof(RangeTestClassC);
                var property = type.GetProperty("Prop");
                RangeValidator.Validate(type, property);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidRangeException);
            }
        }


        [TestMethod]
        public void TestNoRange()
        {
            try
            {
                var type = typeof(RangeTestClassD);
                var property = type.GetProperty("Prop");
                RangeValidator.Validate(type, property);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
