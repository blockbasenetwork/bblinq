using BlockBase.BBLinq.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BlockBase.BBLinq.Validators.AnnotationValidators;

namespace Tests.ValidatorTests
{
    [Table("T1")]
    public class TableTestClassA
    {
        public string Prop { get; set; }
    }

    [Table("T\"_1")]
    public class TableTestClassB
    {
        public string Prop { get; set; }
    }

    public class TableTestClassC
    {
        public string Prop { get; set; }
    }

    [TestClass]
    public class TableAnnotationTests
    {
        [TestMethod]
        public void TestCorrectTable()
        {
            try
            {
                var type = typeof(TableTestClassA);
                TableValidator.Validate(type);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestTableWithWrongName()
        {
            try
            {
                var type = typeof(TableTestClassB);
                TableValidator.Validate(type);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidTableNameException);
            }
        }

        [TestMethod]
        public void TestTableWithNoTable()
        {
            try
            {
                var type = typeof(TableTestClassC);
                TableValidator.Validate(type);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
