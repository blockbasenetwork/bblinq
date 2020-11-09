using System;
namespace BlockBase.BBLinq.DataAnnotations.DataTypes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class DataTypeAttribute : Attribute
    {
    }
}
