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
        #region Public Static Methods

        #region Connection Control

        /// <summary>
        /// 开启新的数据库连接
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>返回连接对象</returns>
        public static DbConnection OpenNewConnection(string connectionString)
        {
            Database dbBase = new SqlDatabase(connectionString);
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

        /// <summary>
        /// 开启新的数据库事务
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>返回事务对象</returns>
        public static DbTransaction OpenNewTransaction(string connectionString)
        {
            DbConnection dbCon = OpenNewConnection(connectionString);
            return dbCon.BeginTransaction();
        }

        /// <summary>
        /// 关闭事务
        /// </summary>
        /// <param name="tran">需要关闭的事务对象</param>
        public static void CloseTransaction(DbTransaction tran)
        {
            if (tran == null)
            {
                return;
            }

            CloseConnection(tran.Connection);
            tran.Dispose();
            tran = null;
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
            Database dbBase = new SqlDatabase(connectionString);
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
            Database dbBase = new SqlDatabase(tran.Connection.ConnectionString);
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
            Database dbBase = new SqlDatabase(connectionString);
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
            Database dbBase = new SqlDatabase(tran.Connection.ConnectionString);
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
            Database dbBase = new SqlDatabase(connectionString);
            DbCommand dbCommand = null;

            if (commandType == CommandType.Text)
            {
                dbCommand = dbBase.GetSqlStringCommand(query);
            }
            else if (commandType == CommandType.StoredProcedure)
            {
                dbCommand = dbBase.GetStoredProcCommand(query);
            }

            dbCommand.Connection = dbBase.CreateConnection();
            dbCommand.Connection.Open();
            AddParamToCommand(dbBase, dbCommand, dbParamCollection);
            DbDataAdapter dataAdapter = dbBase.GetDataAdapter();
            dataAdapter.SelectCommand = dbCommand;

            if (page != null)
            {
                dataAdapter.Fill(result, page.StartRecord, page.PageSize, srcTableName);
            }
            else
            {
                dataAdapter.Fill(result);
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
            Database dbBase = new SqlDatabase(tran.Connection.ConnectionString);
            DbCommand dbCommand = null;

            if (commandType == CommandType.Text)
            {
                dbCommand = dbBase.GetSqlStringCommand(query);
            }
            else if (commandType == CommandType.StoredProcedure)
            {
                dbCommand = dbBase.GetStoredProcCommand(query);
            }

            dbCommand.Connection = tran.Connection;
            dbCommand.Transaction = tran;
            AddParamToCommand(dbBase, dbCommand, dbParamCollection);
            DbDataAdapter dataAdapter = dbBase.GetDataAdapter();
            dataAdapter.SelectCommand = dbCommand;

            if (page != null)
            {
                dataAdapter.Fill(result, page.StartRecord, page.PageSize, srcTableName);
            }
            else
            {
                dataAdapter.Fill(result);
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
