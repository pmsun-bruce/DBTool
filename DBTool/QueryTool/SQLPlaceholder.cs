namespace NFramework.DBTool.QueryTool
{
    #region Reference
    
    #endregion

    /// <summary>
    /// SQL语句占位符
    /// </summary>
    public static class SQLPlaceholder
    {
        #region Fields & Properties

        /// <summary>
        /// 字段名占位
        /// </summary>
        private static string colName = "${col:name}";
        /// <summary>
        /// 字段名占位
        /// </summary>
        public static string ColName
        {
            get
            {
                return SQLPlaceholder.colName;
            }
        }

        /// <summary>
        /// 表名占位
        /// </summary>
        private static string tableName = "${table:name}";
        /// <summary>
        /// 表名占位
        /// </summary>
        public static string TableName
        {
            get
            {
                return SQLPlaceholder.tableName;
            }
        }

        #endregion
    }
}
