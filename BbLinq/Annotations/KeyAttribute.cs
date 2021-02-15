using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// Primary key attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : BlockBaseAnnotationAttribute
    {
    }
}
