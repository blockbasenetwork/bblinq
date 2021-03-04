using System;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// For properties that should not be null
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullAttribute : BlockBaseDataAnnotationAttribute
    {

    }
}
