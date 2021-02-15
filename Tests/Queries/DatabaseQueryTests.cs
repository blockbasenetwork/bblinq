using System;
using System.Collections.Generic;
using System.Text;
using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Queries.BlockBaseQueries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Queries
{
    [Table("Users")]
    public class CreateDbTestClassA
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [Range(0, 10000, 5000)]
        [Column("birthDate")]
        public DateTime BirthDate { get; set; }

        [Encrypted]
        [Column("name")]
        public string Name { get; set; }

        public List<CreateDbTestClassB> Contacts { get; set; }
    }

    [Table("Contacts")]
    public class CreateDbTestClassB
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(CreateDbTestClassA))]
        public Guid UserId { get; set; }

        public virtual CreateDbTestClassA User { get; set; }

        [Encrypted(8)]
        [Column("contact")]
        public string Contact { get; set; }

        [NotMapped]
        public bool OnDevice { get; set; }
    }

    [TestClass]
    public class DatabaseQueryTests
    {
        [TestMethod]
        public void TestCreateRightClass()
        {
            var query = new BlockBaseCreateDatabaseQuery("testGnut",
                new[] {typeof(CreateDbTestClassA), typeof(CreateDbTestClassB)});

            var res = query.ToString();
        }

        [TestMethod]
        public void TestDrop()
        {
            var query = new BlockBaseDropDatabaseQuery("testGnut");
            var res = query.ToString();
        }
    }
}
