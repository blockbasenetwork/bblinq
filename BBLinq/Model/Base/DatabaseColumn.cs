namespace BlockBase.BBLinq.Model.Base
{
    internal abstract class DatabaseColumn
    {
        public string Table { get; set; }
        public string Name { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public string ParentTableName { get; set; }
        public string ParentTableKeyName { get; set; }
        public bool IsRequired { get; set; }
    }
}
