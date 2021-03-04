using BlockBase.BBLinq.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BlockBase.BBLinq.Validators.AnnotationValidators;

namespace Tests.ValidatorTests
{
    public class PrimaryKeyTestClassA
    {
        [PrimaryKey]
        public string Prop { get; set; }
    }

    public class PrimaryKeyTestClassB
    {
        [PrimaryKey]
        public Guid Prop { get; set; }
    }

    public class PrimaryKeyTestClassC
    {
        public Guid Prop { get; set; }
    }


    public class PrimaryKeyTestClassD
    {
        [PrimaryKey]
        public Guid Prop { get; set; }
        
        [PrimaryKey]
        public Guid PropB { get; set; }
    }

    [TestClass]
    public class PrimaryKeyAnnotationTests
    {
        [TestMethod]
        public void TestWrongTypeKey()
        {
            try
            {
                var type = typeof(PrimaryKeyTestClassA);
                PrimaryKeyValidator.Validate(type);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidPrimaryKeyTypeException);
            }
        }

        [TestMethod]
        public void TestRightPrimaryKey()
        {
            try
            {
                var type = typeof(PrimaryKeyTestClassB);
                PrimaryKeyValidator.Validate(type);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }


        [TestMethod]
        public void TestNoKey()
        {
            try
            {
                var type = typeof(PrimaryKeyTestClassC);
                PrimaryKeyValidator.Validate(type);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is NoPrimaryKeyFoundException);
            }
        }


        [TestMethod]
        public void TestTwoKeys()
        {
            try
            {
                var type = typeof(PrimaryKeyTestClassD);
                PrimaryKeyValidator.Validate(type);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is TooManyPrimaryKeysException);
            }
        }
    }
}
