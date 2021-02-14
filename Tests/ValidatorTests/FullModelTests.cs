using System;
using System.Collections.Generic;
using System.Text;
using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ValidatorTests
{
    [Table("cc")]
    public class AaClass
    {
        [Key]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("birthday"), Range(1900, 2500, 4)]
        public DateTime BirthDate { get; set; }

        [ForeignKey(typeof(BbClass))]
        public int BbId { get; set; }
    }

    public class BbClass
    {
        [Key]
        public int Id { get; set; }

        [Encrypted]
        public string Content { get; set; }
    }

    [TestClass]
    public class FullModelTests
    {
        [TestMethod]
        public void TestCorrectClass()
        {
            try
            {
                ModelValidator.Validate(new []{typeof(AaClass), typeof(BbClass)});
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
