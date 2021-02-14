﻿using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BlockBase.BBLinq.Validators.AnnotationValidators;

namespace Tests.ValidatorTests
{
    public class ForeignKeyClassAa
    {
        [Key]
        public Guid Prop { get; set; }
    }

    public class ForeignKeyClassAb
    {
        [ForeignKey(typeof(ForeignKeyClassAa))]
        public string AaId { get; set; }
    }

    public class ForeignKeyClassBa
    {
        [Key]
        public Guid Prop { get; set; }
    }

    public class ForeignKeyClassBb
    {
        [ForeignKey("ForeignKeyClassBA")]
        public string AaId { get; set; }
    }

    [Table("foreignKeyClassCA")]
    public class ForeignKeyClassCa
    {
        [Key]
        public Guid Prop { get; set; }
    }

    public class ForeignKeyClassCb
    {
        [ForeignKey("foreignKeyClassCA")]
        public string AaId { get; set; }
    }

    public class ForeignKeyClassDb
    {
        [ForeignKey("foreignKeyClassBA")]
        public string AaId { get; set; }
    }
    public class ForeignKeyTestClassE
    {
        public string AaId { get; set; }
    }


    [TestClass]
    public class ForeignKeyAnnotationTests
    {
        [TestMethod]
        public void TestWithNoParent()
        {
            try
            {
                var type = typeof(ForeignKeyClassDb);
                ForeignKeyValidator.Validate(type, new List<Type>());
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidParentTypeException);
            }
        }

        [TestMethod]
        public void TestWithTypeString()
        {
            try
            {
                var type = typeof(ForeignKeyClassBb);
                ForeignKeyValidator.Validate(type, new List<Type>(){typeof(ForeignKeyClassBa)});
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestWithType()
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
        public void TestWithTableName()
        {
            try
            {
                var type = typeof(ForeignKeyClassCb);
                ForeignKeyValidator.Validate(type, new List<Type>() { typeof(ForeignKeyClassCa) });
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
                var type = typeof(ForeignKeyTestClassE);
                ForeignKeyValidator.Validate(type, new List<Type>());
                Assert.IsTrue(true);
            }
            catch(Exception)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
