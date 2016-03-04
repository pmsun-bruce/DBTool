namespace NFramework.DBTool.QueryTool
{
    #region Reference

    using System;
    using System.Data;
    using System.Data.Common;

    #endregion

    /// <summary>
    /// 条件参数对象
    /// </summary>
    [Serializable]
    public class DBParam : DbParameter
    {
        #region Fields & Properties

        /// <summary>
        /// 获取或设置参数数据类型
        /// </summary>
        public override DbType DbType { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示参数是只可输入、只可输出、双向还是存储过程返回值参数。
        /// </summary>
        public override ParameterDirection Direction { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示参数是否接受空值。
        /// </summary>
        public override bool IsNullable { get; set; }

        /// <summary>
        /// 获取或设置 DbParameter 的名称。
        /// </summary>
        public override string ParameterName { get; set; }

        /// <summary>
        /// 获取或设置列中数据的最大大小（以字节为单位）。
        /// </summary>
        public override int Size { get; set; }

        /// <summary>
        /// 获取或设置源列的名称，该源列映射到 DataSet 并用于加载或返回 Value。
        /// </summary>
        public override string SourceColumn { get; set; }

        /// <summary>
        /// 设置或获取一个值，该值指示源列是否可以为 null。
        /// 这使得 DbCommandBuilder 能够正确地为可以为 null 的列生成 Update 语句。
        /// </summary>
        public override bool SourceColumnNullMapping { get; set; }

        /// <summary>
        /// 获取或设置在加载 Value 时使用的 DataRowVersion。
        /// </summary>
        public override DataRowVersion SourceVersion { get; set; }

        /// <summary>
        /// 获取或设置该参数的值。
        /// </summary>
        public override object Value { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 将 DbType 属性重置为其原始设置。
        /// </summary>
        public override void ResetDbType()
        {

        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        public DBParam(string paramName, object paramValue)
        {
            this.ParameterName = paramName;
            this.Value = paramValue;
            this.IsNullable = true;
            this.Direction = ParameterDirection.InputOutput;
            this.DbType = DbType.Object;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="dbType">数据类型</param>
        public DBParam(string paramName, object paramValue, DbType dbType) : this(paramName, paramValue)
        {
            this.DbType = dbType;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="isNullable">是否为空</param>
        public DBParam(string paramName, object paramValue, DbType dbType, bool isNullable) : this(paramName, paramValue, dbType)
        {
            this.IsNullable = isNullable;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">参数大小</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size) : this(paramName, paramValue, dbType, true)
        {
            this.Size = size;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="isNullable">是否为空</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, bool isNullable) : this(paramName, paramValue, dbType, size)
        {
            this.IsNullable = isNullable;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="sourceColumn">源列的名称</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, string sourceColumn) : this(paramName, paramValue, dbType, size)
        {
            this.SourceColumn = sourceColumn;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="isNullable">是否为空</param>
        /// <param name="sourceColumn">源列的名称</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, bool isNullable, string sourceColumn) : this(paramName, paramValue, dbType, size, isNullable)
        {
            this.SourceColumn = sourceColumn;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="paramDirection">参数方向</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, ParameterDirection paramDirection) : this(paramName, paramValue, dbType, size, true)
        {
            this.Direction = paramDirection;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="isNullable">是否为空</param>
        /// <param name="paramDirection">参数方向</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, bool isNullable, ParameterDirection paramDirection) : this(paramName, paramValue, dbType, size, isNullable)
        {
            this.Direction = paramDirection;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="paramDirection">参数方向</param>
        /// <param name="sourceColumn">源列的名称</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, string sourceColumn, ParameterDirection paramDirection) : this(paramName, paramValue, dbType, size, true, sourceColumn)
        {
            this.Direction = paramDirection;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="isNullable">是否为空</param>
        /// <param name="sourceColumn">源列的名称</param>
        /// <param name="paramDirection">参数方向</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, bool isNullable, string sourceColumn, ParameterDirection paramDirection) : this(paramName, paramValue, dbType, size, isNullable, sourceColumn)
        {
            this.Direction = paramDirection;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="isNullable">是否为空</param>
        /// <param name="sourceColumn">源列的名称</param>
        /// <param name="paramDirection">参数方向</param>
        /// <param name="sourceVersion">paramDirection</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, bool isNullable, string sourceColumn, ParameterDirection paramDirection, DataRowVersion sourceVersion) : this(paramName, paramValue, dbType, size, isNullable, sourceColumn, paramDirection)
        {
            this.SourceVersion = sourceVersion;
        }

        #endregion

    }
}