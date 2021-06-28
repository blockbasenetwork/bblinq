using BlockBase.BBLinq.Dictionaries.Base;

namespace BlockBase.BBLinq.Dictionaries
{
    internal class BbSqlDictionary : BaseSqlDictionary, IDictionary
    {
        public string Begin => "BEGIN;";
        public string Transaction => "TRANSACTION";
        public string Commit => "COMMIT;";
        public string Rollback => "ROLLBACK";
        public string NullValue => "NULL";
        public string Unique => "UNIQUE";
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
        public string If => "IF";
        public string Execute => "EXECUTE";
        public string LeftJoin => "LEFT JOIN";
        public string RightJoin => "RIGHT JOIN";
        public string QueryBatchWrapperLeft => "{";
        public string QueryBatchWrapperRight => "}";
        public string NotNull => "NOT NULL";
    }
}
