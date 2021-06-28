using System;
using BlockBase.BBLinq.DataAnnotations.Base;

namespace BlockBase.BBLinq.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueAttribute : BbLinqAttribute
    {
    }
}
