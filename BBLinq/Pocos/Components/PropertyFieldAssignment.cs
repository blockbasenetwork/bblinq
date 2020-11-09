using System.Reflection;

namespace BlockBase.BBLinq.Pocos.Components
{
    public struct PropertyFieldAssignment
    {
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public PropertyInfo Property { get; set; }

    }
}
