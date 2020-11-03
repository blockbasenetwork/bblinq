using System;
namespace agap2IT.Labs.BlockBase.BBLinq.DataAnnotations.DataTypes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class DataTypeAttribute : Attribute
    {
    }
}
