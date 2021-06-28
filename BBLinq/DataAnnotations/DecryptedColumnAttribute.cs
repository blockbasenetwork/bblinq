using BlockBase.BBLinq.DataAnnotations.Base;
using System;

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
