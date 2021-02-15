namespace BlockBase.BBLinq.Dictionaries
{
    public sealed class BbSqlDictionary : BaseSqlDictionary
    {
        public string AllSelector => "*";
        public string DifferentFrom => "!=";
        public string QueryEnd => ";";
        public string EqualOrGreaterThan => ">=";
        public string EqualOrLessThan => "<=";
        public string LessThan => "<";
        public string GreaterThan => ">";
        public string LeftListWrapper => "(";
        public string RightListWrapper => ")";
        public string LeftTextWrapper => "'";
        public string RightTextWrapper => "'";
        public string ColumnSeparator => ",";
        public string TableColumnSeparator => ".";
        public string ValueEquals => "=";
        public string Encrypted => "ENCRYPTED";
        public string PrimaryKey => "PRIMARY KEY";
        public string References => "REFERENCES";
        public string NonEncryptedColumn => "!";
    }
}
