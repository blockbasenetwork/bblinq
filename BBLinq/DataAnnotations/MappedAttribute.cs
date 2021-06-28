using BlockBase.BBLinq.DataAnnotations.Base;
using System;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// A property that is mapped and converted to string
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MappedAttribute : BbLinqAttribute
    {


    }
}
