using BlockBase.BBLinq.DataAnnotations.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// The Column attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : BbLinqAttribute
    {
        [RegularExpression("[A-z]")]
        public string Name { get; set; }
    }
}
