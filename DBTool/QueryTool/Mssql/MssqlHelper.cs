namespace NFramework.DBTool.QueryTool.Mssql
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

    using NFramework.DBTool.Common;

    #endregion

    /// <summary>
    /// MSSQL的数据库执行帮助类
    /// </summary>
    public class MssqlHelper
    {
        #region Fields & Properties

        /// <summary>
        /// 锁对象
        /// </summary>
        private static object lockKey = new object();

        /// <summary>
        /// 数据库连接池
        /// </summary>
        private static Dictionary<string, Database> DbBasePool
        {
            get;
            set;
        }

        #endregion

        #region Public Static Methods

        #region Connection Control

        /// <summary>
        /// 开启新的数据库连接
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>返回连接对象</returns>
        public static DbConnection OpenNewConnection(string connectionString)
        {
            Database dbBase = GetDatabase(connectionString);
            DbConnection dbConn = dbBase.CreateConnection();
            dbConn.Open();
            return dbConn;
        }

        /// <summary>
        /// 关闭一个数据库连接
        /// </summary>
        /// <param name="dbConn">数据库连接对象</param>
        public static void CloseConnection(DbConnection dbConn)
        {
            if (dbConn == null || dbConn.State != ConnectionState.Open)
            {
                return;
            }

            dbConn.Close();
            dbConn.Dispose();
            dbConn = null;
        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// 执行SQL，并返回执行影响的行数
        /// </summary>
        /// <param name="connectionString">数据库连接</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="query">需要执行的SQL语句</param>
        /// <returns>返回执行影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string query)
        {
            int result = ExecuteNonQuery(connectionString, commandType, query, null);
            return result;
        }

        /// <summary>
        /// 执行SQL，并返回执行影响的行数
        /// </summary>
        /// <param name="connectionString">数据库连接</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="query">需要执行的SQL语句</param>
        /// <param name="dbParamCollection">执行中需要用到的参数集合</param>
        /// <returns>返回执行影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string query, DBParamCollection dbParamCollection)
        {
            int result = 0;
            Database dbBase = GetDatabase(connectionString);
            DbCommand dbCommand = null;

            if (commandType == CommandType.Text)
            {
                dbCommand = dbBase.GetSqlStringCommand(query);
            }
            else if (commandType == CommandType.StoredProcedure)
            {
                dbCommand = dbBase.GetStoredProcCommand(query);
            }

            AddParamToCommand(dbBase, dbCommand, dbParamCollection);
            result = dbBase.ExecuteNonQuery(dbCommand);
            return result;
        }

        /// <summary>
        /// 执行SQL，并返回执行影响的行数
        /// </summary>
        /// <param name="tran">当前执行所在事务的事务对象</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="query">需要执行的SQL语句</param>
        /// <returns>返回执行影响的行数</returns>
        public static int ExecuteNonQuery(DbTransaction tran, CommandType commandType, string query)
        {
            int result = ExecuteNonQuery(tran, commandType, query, null);
            return result;
        }

        /// <summary>
        /// 执行SQL，并返回执行影响的行数
        /// </summary>
        /// <param name="tran">当前执行所在事务的事务对象</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="query">需要执行的SQL语句</param>
        /// <param name="dbParamCollection">执行中需要用到的参数集合</param>
        /// <returns>返回执行影响的行数</returns>
        public static int ExecuteNonQuery(DbTransaction tran, CommandType commandType, string query, DBParamCollection dbParamCollection)
        {
            int result = 0;
            Database dbBase = GetDatabase(tran.Connection.ConnectionString);
            DbCommand dbCommand = null;

            if (commandType == CommandType.Text)
            {
                dbCommand = dbBase.GetSqlStringCommand(query);
            }
            else if (commandType == CommandType.StoredProcedure)
            {
                dbCommand = dbBase.GetStoredProcCommand(query);
            }

            AddParamToCommand(dbBase, dbCommand, dbParamCollection);
            result = dbBase.ExecuteNonQuery(dbCommand, tran);
            return result;
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// 执行SQL，并返回查询到的第一行第一列的值
        /// </summary>
        /// <param name="connectionString">数据库连接</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="query">需要执行的SQL语句</param>
        /// <returns>返回查询到的第一行第一列的值</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string query)
        {
            object result = ExecuteScalar(connectionString, commandType, query, null);
            return result;
        }

        /// <summary>
        /// 执行SQL，并返回查询到的第一行第一列的值
        /// </summary>
        /// <param name="connectionString">数据库连接</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="query">需要执行的SQL语句</param>
        /// <param name="dbParamCollection">执行中需要用到的参数集合</param>
        /// <returns>返回查询到的第一行第一列的值</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string query, DBParamCollection dbParamCollection)
        {
            object result = null;
            Database dbBase = GetDatabase(connectionString);
            DbCommand dbCommand = null;

            if (commandType == CommandType.Text)
            {
                dbCommand = dbBase.GetSqlStringCommand(query);
            }
            else if (commandType == CommandType.StoredProcedure)
            {
                dbCommand = dbBase.GetStoredProcCommand(query);
            }

            AddParamToCommand(dbBase, dbCommand, dbParamCollection);
            result = dbBase.ExecuteScalar(dbCommand);
            return result;
        }

        /// <summary>
        /// 执行SQL，并返回查询到的第一行第一列的值
        /// </summary>
        /// <param name="tran">当前执行所在事务的事务对象</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="query">需要执行的SQL语句</param>
        /// <returns>返回查询到的第一行第一列的值</returns>
        public static object ExecuteScalar(DbTransaction tran, CommandType commandType, string query)
        {
            object result = ExecuteScalar(tran, commandType, query, null);
            return result;
        }

        /// <summary>
        /// 执行SQL，并返回查询到的第一行第一列的值
        /// </summary>
        /// <param name="tran">当前执行所在事务的事务对象</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="query">需要执行的SQL语句</param>
        /// <param name="dbParamCollection">执行中需要用到的参数集合</param>
        /// <returns>返回查询到的第一行第一列的值</returns>
        public static object ExecuteScalar(DbTransaction tran, CommandType commandType, string query, DBParamCollection dbParamCollection)
        {
            object result = null;
            Database dbBase = GetDatabase(tran.Connection.ConnectionString);
            DbCommand dbCommand = null;

            if (commandType == CommandType.Text)
            {
                dbCommand = dbBase.GetSqlStringCommand(query);
            }
            else if (commandType == CommandType.StoredProcedure)
            {
                dbCommand = dbBase.GetStoredProcCommand(query);
            }

            AddParamToCommand(dbBase, dbCommand, dbParamCollection);
            result = dbBase.ExecuteScalar(dbCommand, tran);
            return result;
        }

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string query)
        {
            DataSet result = ExecuteDataSet(connectionString, commandType, query, null, null, null);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <param name="dbParamCollection"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string query, DBParamCollection dbParamCollection)
        {
            DataSet result = ExecuteDataSet(connectionString, commandType, query, null, null, dbParamCollection);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="srcTableName"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string query, Pager page, string srcTableName)
        {
            DataSet result = ExecuteDataSet(connectionString, commandType, query, page, srcTableName, null);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="srcTableName"></param>
        /// <param name="dbParamCollection"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string query, Pager page, string srcTableName, DBParamCollection dbParamCollection)
        {
            DataSet result = new DataSet();
            Database dbBase = GetDatabase(connectionString);
            DbCommand dbCommand = null;

            if (commandType == CommandType.Text)
            {
                dbCommand = dbBase.GetSqlStringCommand(query);
            }
            else if (commandType == CommandType.StoredProcedure)
            {
                dbCommand = dbBase.GetStoredProcCommand(query);
            }

            AddParamToCommand(dbBase, dbCommand, dbParamCollection);
            
            if (page != null)
            {
                using (DbDataAdapter dataAdapter = dbBase.GetDataAdapter())
				{
					dbCommand.Connection = dbBase.CreateConnection();
					dbCommand.Connection.Open();
					dataAdapter.SelectCommand = dbCommand;
					dataAdapter.Fill(result, page.StartRecord, page.PageSize, srcTableName);
					CloseConnection(dbCommand.Connection);
					dbCommand.Dispose();
					dbCommand = null;
				}
            }
            else
            {
                result = dbBase.ExecuteDataSet(dbCommand);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(DbTransaction tran, CommandType commandType, string query)
        {
            DataSet result = ExecuteDataSet(tran, commandType, query, null, null, null);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <param name="dbParamCollection"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(DbTransaction tran, CommandType commandType, string query, DBParamCollection dbParamCollection)
        {
            DataSet result = ExecuteDataSet(tran, commandType, query, null, null, dbParamCollection);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="srcTableName"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(DbTransaction tran, CommandType commandType, string query, Pager page, string srcTableName)
        {
            DataSet result = ExecuteDataSet(tran, commandType, query, page, srcTableName, null);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="srcTableName"></param>
        /// <param name="dbParamCollection"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(DbTransaction tran, CommandType commandType, string query, Pager page, string srcTableName, DBParamCollection dbParamCollection)
        {
            DataSet result = new DataSet();
            Database dbBase = GetDatabase(tran.Connection.ConnectionString);
            DbCommand dbCommand = null;
            
            if (commandType == CommandType.Text)
            {
                dbCommand = dbBase.GetSqlStringCommand(query);
            }
            else if (commandType == CommandType.StoredProcedure)
            {
                dbCommand = dbBase.GetStoredProcCommand(query);
            }

            AddParamToCommand(dbBase, dbCommand, dbParamCollection);
            
            if (page != null)
            {
                using (DbDataAdapter dataAdapter = dbBase.GetDataAdapter())
				{
					dataAdapter.SelectCommand = dbCommand;
					dbCommand.Connection = tran.Connection;
					dbCommand.Transaction = tran;
					dataAdapter.Fill(result, page.StartRecord, page.PageSize, srcTableName);
				}
            }
            else
            {
                result = dbBase.ExecuteDataSet(dbCommand, tran);
            }

            return result;
        }

        #endregion

        #region ExecuteDataTable

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string query)
        {
            DataSet result = ExecuteDataSet(connectionString, commandType, query);

            if (result == null)
            {
                return null;
            }

            return result.Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <param name="dbParamCollection"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string query, DBParamCollection dbParamCollection)
        {
            DataSet result = ExecuteDataSet(connectionString, commandType, query, dbParamCollection);

            if (result == null)
            {
                return null;
            }

            return result.Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="srcTableName"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string query, Pager page, string srcTableName)
        {
            DataSet result = ExecuteDataSet(connectionString, commandType, query, page, srcTableName);

            if (result == null)
            {
                return null;
            }

            return result.Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="srcTableName"></param>
        /// <param name="dbParamCollection"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string query, Pager page, string srcTableName, DBParamCollection dbParamCollection)
        {
            DataSet result = ExecuteDataSet(connectionString, commandType, query, page, srcTableName, dbParamCollection);

            if (result == null)
            {
                return null;
            }

            return result.Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(DbTransaction tran, CommandType commandType, string query)
        {
            DataSet result = ExecuteDataSet(tran, commandType, query);

            if (result == null)
            {
                return null;
            }

            return result.Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <param name="dbParamCollection"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(DbTransaction tran, CommandType commandType, string query, DBParamCollection dbParamCollection)
        {
            DataSet result = ExecuteDataSet(tran, commandType, query, dbParamCollection);

            if (result == null)
            {
                return null;
            }

            return result.Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="srcTableName"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(DbTransaction tran, CommandType commandType, string query, Pager page, string srcTableName)
        {
            DataSet result = ExecuteDataSet(tran, commandType, query, page, srcTableName);

            if (result == null)
            {
                return null;
            }

            return result.Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="commandType"></param>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="srcTableName"></param>
        /// <param name="dbParamCollection"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(DbTransaction tran, CommandType commandType, string query, Pager page, string srcTableName, DBParamCollection dbParamCollection)
        {
            DataSet result = ExecuteDataSet(tran, commandType, query, page, srcTableName, dbParamCollection);

            if (result == null)
            {
                return null;
            }

            return result.Tables[0];
        }

        #endregion

        #endregion

        #region Private Static Methods

        #region Connection Methods

        /// <summary>
        /// 获取一个数据库连接
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>返回一个数据库连接对象</returns>
        private static Database GetDatabase(string connectionString)
        {
            lock (lockKey)
            {
                if (DbBasePool == null)
                {
                    DbBasePool = new Dictionary<string, Database>();
                }

                if (DbBasePool.ContainsKey(connectionString))
                {
                    return DbBasePool[connectionString];
                }

                Database dbBase = new SqlDatabase(connectionString);
                DbBasePool.Add(connectionString, dbBase);

                return dbBase;
            }
        }

        #endregion

        #region Params Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbBase"></param>
        /// <param name="command"></param>
        /// <param name="dbParamCollection"></param>
        private static void AddParamToCommand(Database dbBase, DbCommand command, DBParamCollection dbParamCollection)
        {
            if (dbParamCollection == null || dbParamCollection.Count == 0)
            {
                return;
            }
            
            foreach (DbParameter dbParam in dbParamCollection)
            {
                dbBase.AddInParameter(command, dbParam.ParameterName, dbParam.DbType, dbParam.Value);
            }
        }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        private MssqlHelper()
        {
            
        }

        #endregion
    }
}
