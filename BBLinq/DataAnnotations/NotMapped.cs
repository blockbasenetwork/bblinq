using System;
using BlockBase.BBLinq.DataAnnotations.Base;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// For properties that should not be mapped
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotMappedAttribute : BbLinqAttribute
    {

    }
}
