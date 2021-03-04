using System;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// For properties that can be mapped
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MappedAttribute : BlockBaseDataAnnotationAttribute
    {

    }
}
