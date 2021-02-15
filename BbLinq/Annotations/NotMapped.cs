using System;

namespace BlockBase.BBLinq.Annotations
{
    /// <summary>
    /// For properties that should not be mapped
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotMappedAttribute : BlockBaseAnnotationAttribute
    {

    }
}
