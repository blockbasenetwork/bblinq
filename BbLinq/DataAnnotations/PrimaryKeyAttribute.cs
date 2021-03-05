using System;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// Primary key attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : BlockBaseDataAnnotationAttribute
    {
    }
}
