using BlockBase.BBLinq.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BlockBase.BBLinq.Validators.AnnotationValidators;

namespace Tests.ValidatorTests
{
    public class ColumnTestClassA
    {
        [Column("prop")]
        public string Prop { get; set; }
    }

    public class ColumnTestClassB
    {
        [Column("pro$p")]
        public string Prop { get; set; }
    }
    public class ColumnTestClassC
    {
        public string Prop { get; set; }
    }

    [TestClass]
    public class ColumnAnnotationTests
    {
        [TestMethod]
        public void TestColumnWithRightName()
        {
            try
            {
                var type = typeof(ColumnTestClassA);
                var property = type.GetProperty("Prop");
                ColumnValidator.Validate(type, property);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestColumnWithWrongName()
        {
            try
            {
                var type = typeof(ColumnTestClassB);
                var property = type.GetProperty("Prop");
                ColumnValidator.Validate(type, property);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidColumnNameException);
            }
        }


        [TestMethod]
        public void TestColumnWitNoColumn()
        {
            try
            {
                var type = typeof(ColumnTestClassC);
                var property = type.GetProperty("Prop");
                ColumnValidator.Validate(type, property);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
