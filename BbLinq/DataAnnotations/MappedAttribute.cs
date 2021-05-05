using System;
using BlockBase.BBLinq.DataAnnotations.Base;

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
