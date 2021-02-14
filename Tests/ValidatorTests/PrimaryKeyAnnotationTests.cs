using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using BlockBase.BBLinq.Validators.AnnotationValidators;

namespace Tests.ValidatorTests
{
    public class PrimaryKeyTestClassA
    {
        [Key]
        public string Prop { get; set; }
    }

    public class PrimaryKeyTestClassB
    {
        [Key]
        public Guid Prop { get; set; }
    }

    public class PrimaryKeyTestClassC
    {
        public Guid Prop { get; set; }
    }


    public class PrimaryKeyTestClassD
    {
        [Key]
        public Guid Prop { get; set; }
        
        [Key]
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
                var property = type.GetProperty("Prop");
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
                var property = type.GetProperty("Prop");
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
                var property = type.GetProperty("Prop");
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
                var property = type.GetProperty("Prop");
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
