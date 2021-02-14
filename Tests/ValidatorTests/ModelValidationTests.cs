using System;
using System.Collections.Generic;
using System.Text;
using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Validators;
using BlockBase.BBLinq.Validators.AnnotationValidators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ValidatorTests
{
    [Table("A")]
    public class TestModelA
    {

    }
    [Table("B")]

    public class TestModelB
    {

    }

    [Table("A")]
    public class TestModelC
    {

    }

    [TestClass]
    public class ModelValidationTests
    {
        [TestMethod]
        public void TestDuplicateTablesSuccess()
        {
            try
            {
                var tables = new Type[] {typeof(TestModelA), typeof(TestModelB)};
                ModelValidator.ValidateDuplicates(tables);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestDuplicateTablesFail()
        {
            try
            {
                var tables = new Type[] { typeof(TestModelA), typeof(TestModelC) };
                ModelValidator.ValidateDuplicates(tables);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is DuplicatedTablesOnModelException);
            }
        }
    }
}
