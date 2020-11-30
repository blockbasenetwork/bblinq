namespace BlockBase.BBLinq.Dictionaries
{
    public class BbSqlDictionary : SqlDictionary
    {
        public virtual string AllSelector => "*";
        public virtual string DifferentFrom => "!=";
        public virtual string QueryEnd => ";";
        public virtual string EqualOrGreaterThan => ">=";
        public virtual string EqualOrLessThan => "<=";
        public virtual string LessThan => "<";
        public virtual string GreaterThan => ">";
        public virtual string LeftListWrapper => "(";
        public virtual string RightListWrapper => ")";
        public virtual string LeftTextWrapper => "'";
        public virtual string RightTextWrapper => "'";
        public virtual string FieldSeparator => ",";
        public virtual string TableFieldSeparator => ".";
        public virtual string ValueEquals => "=";
        public virtual string Encrypted => "ENCRYPTED";
        public virtual string PrimaryKey => "PRIMARY KEY";
        public virtual string References => "REFERENCES";
        public virtual string NonEncryptedField => "!";
    }
}
