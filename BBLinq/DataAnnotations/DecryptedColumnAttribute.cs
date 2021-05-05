using System;
using BlockBase.BBLinq.DataAnnotations.Base;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// Sets the column as encrypted
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DecryptedColumnAttribute : BbLinqAttribute
    {
    }
}
