using System;
using BlockBase.BBLinq.DataAnnotations.Base;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// Primary key attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : BbLinqAttribute
    {
    }
}
