using BlockBase.BBLinq.DataAnnotations.Base;
using System;

namespace BlockBase.BBLinq.DataAnnotations
{

    /// <summary>
    /// For properties that are DateTime and can be compared
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ComparableDateAttribute : BbLinqAttribute
    {

    }
}
