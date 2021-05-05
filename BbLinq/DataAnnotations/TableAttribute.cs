using System;
using System.ComponentModel.DataAnnotations;
using BlockBase.BBLinq.DataAnnotations.Base;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// The Column attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : BbLinqAttribute
    {
        [RegularExpression("[A-z]")]
        public string Name { get; set; }
    }
}
