using System;
using System.Collections.Generic;
using System.Text;
using BlockBase.BBLinq.DataAnnotations.Base;

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
