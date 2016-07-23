namespace NFramework.DBTool.QueryTool.Mssql
{
    #region Reference

    using System.Data.SqlClient;

    using NFramework.DBTool.Common;
    using System.Text;
    using System.Data;
    using System.Data.Common;
    using System;

    #endregion

    /// <summary>
    /// 使用MSSQL时的基础Dal工厂对象，由具体的工厂对象继承
    /// </summary>
    public class MssqlDalFactoryBase : IDalFactoryBase
    {
        #region Fields & Properties

        /// <summary>
        /// SQL SERVER的数据库连接字符串
        /// </summary>
        private string currentConnectionString;
        /// <summary>
        /// SQL SERVER的数据库连接字符串
        /// </summary>
        public string CurrentConnectionString
        {
            get
            {
                return this.currentConnectionString;
            }
        }

        #endregion

        #region Public Constructors 

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentConnectionString">当前数据库连接字符串</param>
        public MssqlDalFactoryBase(string currentConnectionString)
        {
            this.currentConnectionString = currentConnectionString;
        }

        /// <summary>
        /// 开启一个新的事务，并返回事务对象，供其他需要在该事务中执行的步骤使用
        /// </summary>
        /// <returns>返回中间事务对象</returns>
        public ICTransaction BeginTransaction()
        {
            ICTransaction tran = new MssqlTransaction();
            tran.Begin(this);
            return tran;
        }

        #endregion

        #region Public Methods

        public bool IsExistTable(string tableName)
        {
            return IsExistTable(tableName, null);
        }

        public bool IsExistTable(string tableName, ICTransaction tran)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"SELECT ");
            query.AppendLine(@"  OBJECT_ID(@TableName) ");

            DBParamCollection paramCollection = new DBParamCollection();
            paramCollection.Add(new DBParam("@TableName", tableName, DbType.String, 100));

            object result = null;

            if (tran == null)
            {
                result = MssqlHelper.ExecuteScalar(this.CurrentConnectionString, CommandType.Text, query.ToString(), paramCollection);
            }
            else
            {
                DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;
                result = MssqlHelper.ExecuteScalar(dbTran, CommandType.Text, query.ToString(), paramCollection);
            }

            if (result is DBNull)
            {
                return false;
            }

            return true;
        }

        public bool IsExistColumn(string tableName, string columnName)
        {
            return IsExistColumn(tableName, columnName, null);
        }

        public bool IsExistColumn(string tableName, string columnName, ICTransaction tran)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"SELECT ");
            query.AppendLine(@"  COUNT(*) AS Num ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"  syscolumns ");
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"  id = OBJECT_ID(@TableName) ");
            query.AppendLine(@"AND ");
            query.AppendLine(@"  name = @ColumnName ");

            DBParamCollection paramCollection = new DBParamCollection();
            paramCollection.Add(new DBParam("@TableName", tableName, DbType.String, 100));
            paramCollection.Add(new DBParam("@ColumnName", columnName, DbType.String, 100));
            object result = null;

            if (tran == null)
            {
                result = MssqlHelper.ExecuteScalar(this.CurrentConnectionString, CommandType.Text, query.ToString(), paramCollection);
            }
            else
            {
                DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;
                result = MssqlHelper.ExecuteScalar(dbTran, CommandType.Text, query.ToString(), paramCollection);
            }

            if (result is DBNull || Convert.ToInt32(result) == 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
