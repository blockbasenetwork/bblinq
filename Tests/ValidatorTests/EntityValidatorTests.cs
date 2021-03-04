using System;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ValidatorTests
{
    public class EntityValidatorTestClassA
    {
        [Column("A")]
        public int A { get; set; }

        [Column("A")]
        public int B { get; set; }
    }

    public class EntityValidatorTestClassB
    {
        [Column("A")]
        public int A { get; set; }

        [Column("B")]
        public int B { get; set; }
    }

    public class EntityValidatorTestClassC
    {
        public int A { get; set; }

        [Column("A")]
        public int B { get; set; }
    }

    [TestClass]
    public class EntityValidatorTests
    {
        [TestMethod]
        public void TestDuplicatedColumnsFail()
        {
            try
            {
                EntityValidator.ValidateDuplicateColumns(typeof(EntityValidatorTestClassA),
                    typeof(EntityValidatorTestClassA).GetProperties());
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is DuplicatedColumnsOnTableException);
            }
        }

        [TestMethod]
        public void TestDuplicatedColumnsSuccess()
        {
            try
            {
                EntityValidator.ValidateDuplicateColumns(typeof(EntityValidatorTestClassB),
                    typeof(EntityValidatorTestClassB).GetProperties());
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestDuplicatedColumnsByName()
        {
            try
            {
                EntityValidator.ValidateDuplicateColumns(typeof(EntityValidatorTestClassC),
                    typeof(EntityValidatorTestClassC).GetProperties());
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is DuplicatedColumnsOnTableException);
            }
        }
    }
}
