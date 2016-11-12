using MySql.Data.MySqlClient;
using NFramework.DBTool.Common;
using NFramework.ObjectTool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace NFramework.DBTool.QueryTool.Mysql
{
    /// <summary>
    /// 
    /// </summary>
    public class MysqlDalFactoryBase : IDalFactoryBase
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
        
        private string currentSchema;

        public string CurrentSchema
        {
            get
            {
                return currentSchema;
            }
        }
        
        #endregion

        #region Public Constructors 

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentConnectionString">当前数据库连接字符串</param>
        public MysqlDalFactoryBase(string currentConnectionString)
        {
            this.currentConnectionString = currentConnectionString;
            MySqlConnection dbConn = new MySqlConnection(this.CurrentConnectionString);
            dbConn.Open();
            this.currentSchema = dbConn.Database;
            dbConn.Close();
            dbConn.Dispose();
            dbConn = null;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbConnection OpenNewDbConnection()
        {
            MySqlConnection dbConn = new MySqlConnection(this.CurrentConnectionString);
            dbConn.Open();
            return dbConn;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CloseDbConnection(IDbConnection dbConn)
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
        /// 开启一个新的事务，并返回事务对象，供其他需要在该事务中执行的步骤使用
        /// </summary>
        /// <returns>返回中间事务对象</returns>
        public ICTransaction BeginTransaction()
        {
            ICTransaction tran = new MysqlTransaction(this);
            return tran;
        }

        public bool IsExistTable(string tableName)
        {
            return IsExistTable(tableName, null);
        }

        public bool IsExistTable(string tableName, ICTransaction tran)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"SELECT ");
            query.AppendLine(@"  COUNT(*) ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"  `information_schema`.`tables` ");
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"  table_schema=@TableSchema ");
            query.AppendLine(@"AND ");
            query.AppendLine(@"  table_name=@TableName ");

            MySqlParameter[] paramCollection = new MySqlParameter[2];
            paramCollection[0] = new MySqlParameter("TableSchema", MySqlDbType.String, 100);
            paramCollection[1] = new MySqlParameter("TableName", MySqlDbType.String, 100);

            paramCollection[0].Value = this.CurrentSchema;
            paramCollection[1].Value = tableName;

            object result = null;
            MySqlConnection dbConn = null;

            if (tran == null)
            {
                dbConn = (MySqlConnection)this.OpenNewDbConnection();
                result = MySqlHelper.ExecuteScalar(dbConn, query.ToString(), paramCollection);
                this.CloseDbConnection(dbConn);
            }
            else
            {
                dbConn = (MySqlConnection)tran.Connection;
                result = MySqlHelper.ExecuteScalar(dbConn, query.ToString(), paramCollection);
            }

            if (result is DBNull || Convert.ToInt32(result) == 0)
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
            query.AppendLine(@"  COUNT(*) ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"  `information_schema`.`columns` ");
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"  table_schema = @TableSchema ");
            query.AppendLine(@"AND ");
            query.AppendLine(@"  table_name = @TableName ");
            query.AppendLine(@"AND ");
            query.AppendLine(@"  column_name = @ColumnName ");

            MySqlParameter[] paramCollection = new MySqlParameter[3];
            paramCollection[0] = new MySqlParameter("TableSchema", MySqlDbType.String);
            paramCollection[1] = new MySqlParameter("TableName", MySqlDbType.String);
            paramCollection[2] = new MySqlParameter("ColumnName", MySqlDbType.String);

            paramCollection[0].Value = this.CurrentSchema;
            paramCollection[1].Value = tableName;
            paramCollection[2].Value = columnName;

            object result = null;
            MySqlConnection dbConn = null;

            if (tran == null)
            {
                dbConn = (MySqlConnection)this.OpenNewDbConnection();
                result = MySqlHelper.ExecuteScalar(dbConn, query.ToString(), paramCollection);
                this.CloseDbConnection(dbConn);
            }
            else
            {
                dbConn = (MySqlConnection)tran.Connection;
                result = MySqlHelper.ExecuteScalar(dbConn, query.ToString(), paramCollection);
            }

            if (result is DBNull || Convert.ToInt32(result) == 0)
            {
                return false;
            }

            return true;
        }

        public void CreateTable(TableInfo tableInfo)
        {
            ICTransaction tran = this.BeginTransaction();

            try
            {
                CreateTable(tableInfo, tran);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message, ex);
            }
        }

        public void CreateTable(TableInfo tableInfo, ICTransaction tran)
        {
            if (tableInfo == null)
            {

            }

            StringBuilder query = new StringBuilder();
            IList<ColumnInfo> pkList = new List<ColumnInfo>();
            IList<ColumnInfo> uniqueList = new List<ColumnInfo>();
            IList<ColumnInfo> fkList = new List<ColumnInfo>();
            int colCount = 0;
            query.AppendLine(@"CREATE TABLE ");
            query.AppendLine(@"  " + tableInfo.TableName);

            if (tableInfo.Columns != null && tableInfo.Columns.Count > 0)
            {
                query.AppendLine(@"( ");

                foreach (ColumnInfo column in tableInfo.Columns)
                {
                    colCount++;
                    query.AppendLine(string.Format(@"    {0} {1} {2}", column.ColumnName, ToSqlDbType(column), column.IsNullable ? "NULL " : "NOT NULL "));

                    if(column.DefaultValue != null && !string.IsNullOrWhiteSpace(column.DefaultValue.ToString()))
                    {
                        query.Append(" DEFAULT '" + column.DefaultValue.ToString() + "' ");
                    }

                    if(!string.IsNullOrWhiteSpace(column.Remarks))
                    {
                        query.Append(" COMMENT '" + column.Remarks + "' ");
                    }

                    if (colCount != tableInfo.Columns.Count)
                    {
                        query.Append(", ");
                    }
                    
                    if (column.IsPK)
                    {
                        pkList.Add(column);
                    }
                    else if (column.IsUnique)
                    {
                        uniqueList.Add(column);
                    }

                    if (column.IsFK)
                    {
                        fkList.Add(column);
                    }
                }

                if (pkList.Count > 0)
                {
                    query.AppendLine(" ,PRIMARY KEY (");

                    foreach (ColumnInfo pkCol in pkList)
                    {
                        if(pkList.IndexOf(pkCol) > 0)
                        {
                            query.Append(", ");
                        }

                        query.Append(pkCol.ColumnName);
                    }

                    query.Append(") ");
                }

                query.AppendLine(@") ");
            }

            if(!string.IsNullOrWhiteSpace(tableInfo.Remarks))
            {
                query.AppendLine("COMMENT = '" + tableInfo.Remarks + "'; ");
            }
            
            query.AppendLine(AddUniqueConstraintSql(tableInfo, uniqueList));

            foreach (ColumnInfo fkColumnInfo in fkList)
            {
                query.AppendLine(AddFKSql(tableInfo, fkColumnInfo.RefTableName, fkColumnInfo, fkColumnInfo.RefColumnName));
            }

            if (tran == null)
            {
                MySqlHelper.ExecuteNonQuery(this.CurrentConnectionString, query.ToString());
            }
            else
            {
                MySqlHelper.ExecuteNonQuery((MySqlConnection)tran.Connection, query.ToString());
            }
        }

        public void EditTable(TableInfo oldTableInfo, TableInfo newTableInfo)
        {
            EditTable(oldTableInfo, newTableInfo, null);
        }

        public void EditTable(TableInfo oldTableInfo, TableInfo newTableInfo, ICTransaction tran)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(string.Format(@"ALTER TABLE `{0}` COMMENT = '{1}'", oldTableInfo.TableName, newTableInfo.Remarks));

            if (!newTableInfo.TableName.Equals(oldTableInfo.TableName))
            {
                query.Append(string.Format(@" , RENAME TO `{0}`", newTableInfo.TableName));
            }

            query.Append("; ");

            if (tran == null)
            {
                MySqlHelper.ExecuteNonQuery(this.CurrentConnectionString, query.ToString());
            }
            else
            {
                MySqlHelper.ExecuteNonQuery((MySqlConnection)tran.Connection, query.ToString());
            }
        }

        public void DropTable(string tableName)
        {
            DropTable(tableName, null);
        }

        public void DropTable(string tableName, ICTransaction tran)
        {
            if (IsExistTable(tableName))
            {
                StringBuilder dropQuery = new StringBuilder();
                dropQuery.AppendLine(string.Format(@"DROP TABLE {0}; ", tableName));

                if (tran == null)
                {
                    MySqlHelper.ExecuteNonQuery(this.CurrentConnectionString, dropQuery.ToString());
                }
                else
                {
                    MySqlHelper.ExecuteNonQuery((MySqlConnection)tran.Connection, dropQuery.ToString());
                }
            }
        }

        /// <summary>
        /// 添加字段时，可能修改主键组成，唯一组成
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <param name="columnInfo"></param>
        public void AddColumn(TableInfo tableInfo, ColumnInfo columnInfo)
        {
            ICTransaction tran = this.BeginTransaction();

            try
            {
                AddColumn(tableInfo, columnInfo, tran);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message, ex);
            }
        }

        public void AddColumn(TableInfo tableInfo, ColumnInfo columnInfo, ICTransaction tran)
        {
            StringBuilder colQuery = new StringBuilder();
            colQuery.AppendLine(AddColumnSql(tableInfo, columnInfo));
            colQuery.AppendLine(AddColumnConstraintSql(tableInfo, columnInfo, tran));

            if (tran == null)
            {
                MySqlHelper.ExecuteNonQuery(this.CurrentConnectionString, colQuery.ToString());
            }
            else
            {
                MySqlHelper.ExecuteNonQuery((MySqlConnection)tran.Connection, colQuery.ToString());
            }
        }

        /// <summary>
        /// 修改字段时，可能修改主键组成，唯一组成；需要先加字段，copy数据，再删除字段
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <param name="oldColumnInfo"></param>
        /// <param name="newColumnInfo"></param>
        public void EditColumn(TableInfo tableInfo, ColumnInfo oldColumnInfo, ColumnInfo newColumnInfo)
        {
            ICTransaction tran = this.BeginTransaction();

            try
            {
                EditColumn(tableInfo, oldColumnInfo, newColumnInfo, tran);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message, ex);
            }
        }

        public void EditColumn(TableInfo tableInfo, ColumnInfo oldColumnInfo, ColumnInfo newColumnInfo, ICTransaction tran)
        {
            bool isNew = false;
            StringBuilder colQuery = new StringBuilder();
            colQuery.AppendLine(DropColumnConstraintSql(tableInfo, oldColumnInfo, tran));

            if (!newColumnInfo.ColumnName.Equals(oldColumnInfo.ColumnName) 
                || (!string.IsNullOrWhiteSpace(newColumnInfo.Remarks) && !newColumnInfo.Remarks.Equals(oldColumnInfo.Remarks)) 
                || (!string.IsNullOrWhiteSpace(oldColumnInfo.Remarks) && !oldColumnInfo.Remarks.Equals(newColumnInfo.Remarks))
                || newColumnInfo.MaxLength != oldColumnInfo.MaxLength)
            {
                if (oldColumnInfo.DBType == newColumnInfo.DBType)
                {
                    colQuery.AppendLine(EditColumnSql(tableInfo, oldColumnInfo, newColumnInfo));
                }
                else
                {
                    colQuery.AppendLine(AddColumnSql(tableInfo, newColumnInfo));
                    colQuery.AppendLine(AddColumnConstraintSql(tableInfo, newColumnInfo, tran));
                    isNew = true;
                }
            }
            else if (oldColumnInfo.DBType != newColumnInfo.DBType)
            {
                ColumnInfo changeNameColInfo = ObjectFactory.Clone<ColumnInfo>(oldColumnInfo);
                changeNameColInfo.ColumnName += "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                colQuery.AppendLine(EditColumnSql(tableInfo, oldColumnInfo, changeNameColInfo));
                colQuery.AppendLine(AddColumnSql(tableInfo, newColumnInfo));
                colQuery.AppendLine(AddColumnConstraintSql(tableInfo, newColumnInfo, tran));
                isNew = true;
            }

            if (!isNew)
            {
                colQuery.AppendLine(AddColumnConstraintSql(tableInfo, newColumnInfo, tran));
            }

            if (tran == null)
            {
                MySqlHelper.ExecuteNonQuery(this.CurrentConnectionString, colQuery.ToString());
            }
            else
            {
                MySqlHelper.ExecuteNonQuery((MySqlConnection)tran.Connection, colQuery.ToString());
            }
        }

        public void DropColumn(TableInfo tableInfo, ColumnInfo columnInfo)
        {
            ICTransaction tran = this.BeginTransaction();

            try
            {
                DropColumn(tableInfo, columnInfo, tran);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message, ex);
            }
        }

        public void DropColumn(TableInfo tableInfo, ColumnInfo columnInfo, ICTransaction tran)
        {
            StringBuilder dropQuery = new StringBuilder();
            
            dropQuery.AppendLine(DropColumnConstraintSql(tableInfo, columnInfo, tran));
            dropQuery.AppendLine(DropColumnSql(tableInfo, columnInfo));

            if (tran == null)
            {
                MySqlHelper.ExecuteNonQuery(this.CurrentConnectionString, dropQuery.ToString());
            }
            else
            {
                MySqlHelper.ExecuteNonQuery((MySqlConnection)tran.Connection, dropQuery.ToString());
            }

            tableInfo.Columns.RemoveAll(c => columnInfo.ColumnName.Equals(c.ColumnName));
        }

        public void AddFK(TableInfo fkTableInfo, string refTableName, ColumnInfo fkColumnInfo, string refColumnName)
        {
            AddFK(fkTableInfo, refTableName, fkColumnInfo, refColumnName, null);
        }

        public void AddFK(TableInfo fkTableInfo, string refTableName, ColumnInfo fkColumnInfo, string refColumnName, ICTransaction tran)
        {
            StringBuilder addFKQuery = new StringBuilder();
            addFKQuery.AppendLine(AddFKSql(fkTableInfo, refTableName, fkColumnInfo, refColumnName));

            if (tran == null)
            {
                MySqlHelper.ExecuteNonQuery(this.CurrentConnectionString, addFKQuery.ToString());
            }
            else
            {
                MySqlHelper.ExecuteNonQuery((MySqlConnection)tran.Connection, addFKQuery.ToString());
            }
        }

        public void DropFK(TableInfo fkTableInfo, ColumnInfo fkColumnInfo, string refColumnName)
        {
            DropFK(fkTableInfo, fkColumnInfo, refColumnName, null);
        }

        public void DropFK(TableInfo fkTableInfo, ColumnInfo fkColumnInfo, string refColumnName, ICTransaction tran)
        {
            StringBuilder dropFKQuery = new StringBuilder();
            dropFKQuery.AppendLine(DropFKSql(fkTableInfo, fkColumnInfo, refColumnName));

            if (tran == null)
            {
                MySqlHelper.ExecuteNonQuery(this.CurrentConnectionString, dropFKQuery.ToString());
            }
            else
            {
                MySqlHelper.ExecuteNonQuery((MySqlConnection)tran.Connection, dropFKQuery.ToString());
            }
        }

        #endregion

        private bool CheckExistConstraint(TableInfo tableInfo, string indexName, ICTransaction tran)
        {
            string existQuery = string.Format(@"SELECT COUNT(*) FROM information_schema.statistics WHERE table_schema = '{0}' and table_name = '{1}' and index_name = '{2}'; ", this.CurrentSchema, tableInfo.TableName, indexName);
            object count = 0;

            if (tran == null)
            {
                count = MySqlHelper.ExecuteScalar(this.CurrentConnectionString, existQuery);
            }
            else
            {
                count = MySqlHelper.ExecuteScalar((MySqlConnection)tran.Connection, existQuery);
            }

            return Convert.ToInt32(count) > 0 ? true : false;
        }

        private string AddColumnConstraintSql(TableInfo tableInfo, ColumnInfo columnInfo, ICTransaction tran)
        {
            StringBuilder colQuery = new StringBuilder();

            if (columnInfo.IsPK)
            {// 如果原来有主键，则先删除主键，再添加组合主键
                int pkCount = tableInfo.Columns.Count<ColumnInfo>(c => c.IsPK);
                IList<ColumnInfo> pkColList = new List<ColumnInfo>();

                if (pkCount != 0 && CheckExistConstraint(tableInfo, "PRIMARY", tran))
                {
                    pkColList = tableInfo.Columns.Where<ColumnInfo>(c => c.IsPK).ToList<ColumnInfo>();

                    if (pkColList.Count > 0)
                    {
                        colQuery.AppendLine(DropPKSql(tableInfo));
                    }
                }

                if (pkColList.Count(pc => pc.ColumnName.Equals(columnInfo.ColumnName)) == 0)
                {
                    pkColList.Add(columnInfo);
                }

                colQuery.AppendLine(AddPKSql(tableInfo, pkColList));
            }
            else
            {
                if (columnInfo.IsUnique)
                {// 如果是唯一项，检查是否和原唯一项设置为一组，如果是，则先删除原有约束，再添加
                    int uqCount = tableInfo.Columns.Count<ColumnInfo>(c => c.IsUnique && columnInfo.UniqueConstraintName.Equals(c.UniqueConstraintName));
                    IList<ColumnInfo> uqColList = new List<ColumnInfo>();

                    if (uqCount != 0 && CheckExistConstraint(tableInfo, columnInfo.UniqueConstraintName, tran))
                    {
                        uqColList = tableInfo.Columns.Where<ColumnInfo>(c => c.IsUnique && columnInfo.UniqueConstraintName.Equals(c.UniqueConstraintName)).ToList<ColumnInfo>();
                        colQuery.AppendLine(DropUniqueConstraintSql(tableInfo, uqColList[0].UniqueConstraintName));
                    }

                    if (uqColList.Count(pc => pc.ColumnName.Equals(columnInfo.ColumnName)) == 0)
                    {
                        uqColList.Add(columnInfo);
                    }

                    colQuery.AppendLine(AddUniqueConstraintSql(tableInfo, uqColList));
                }

                if (columnInfo.IsFK)
                {
                    colQuery.AppendLine(AddFKSql(tableInfo, columnInfo.RefTableName, columnInfo, columnInfo.RefColumnName));
                }
            }

            return colQuery.ToString();
        }

        private string DropColumnConstraintSql(TableInfo tableInfo, ColumnInfo columnInfo, ICTransaction tran)
        {
            StringBuilder colQuery = new StringBuilder();

            if (columnInfo.IsPK && CheckExistConstraint(tableInfo, "PRIMARY", tran))
            {
                colQuery.AppendLine(DropPKSql(tableInfo));
                int pkCount = tableInfo.Columns.Count<ColumnInfo>(c => c.IsPK && !c.ColumnName.Equals(columnInfo.ColumnName));
                IList<ColumnInfo> pkColList = new List<ColumnInfo>();

                if (pkCount != 0)
                {
                    pkColList = tableInfo.Columns.Where<ColumnInfo>(c => c.IsPK && !c.ColumnName.Equals(columnInfo.ColumnName)).ToList<ColumnInfo>();
                    colQuery.AppendLine(AddPKSql(tableInfo, pkColList));
                }
            }

            if (columnInfo.IsUnique && CheckExistConstraint(tableInfo, columnInfo.UniqueConstraintName, tran))
            {
                colQuery.AppendLine(DropUniqueConstraintSql(tableInfo, columnInfo.UniqueConstraintName));
                int uqCount = tableInfo.Columns.Count<ColumnInfo>(c => c.IsUnique && columnInfo.UniqueConstraintName.Equals(c.UniqueConstraintName) && !c.ColumnName.Equals(columnInfo.ColumnName));
                IList<ColumnInfo> uqColList = new List<ColumnInfo>();

                if (uqCount != 0)
                {
                    uqColList = tableInfo.Columns.Where<ColumnInfo>(c => c.IsUnique && columnInfo.UniqueConstraintName.Equals(c.UniqueConstraintName) && !c.ColumnName.Equals(columnInfo.ColumnName)).ToList<ColumnInfo>();
                    colQuery.AppendLine(AddUniqueConstraintSql(tableInfo, uqColList));
                }
            }

            if (columnInfo.IsFK)
            {
                colQuery.AppendLine(DropFKSql(tableInfo, columnInfo, columnInfo.RefColumnName));
            }

            return colQuery.ToString();
        }

        private string AddUniqueConstraintSql(TableInfo tableInfo, ColumnInfo uqColumn)
        {
            return AddUniqueConstraintSql(tableInfo, new ColumnInfo[] { uqColumn });
        }

        private string AddUniqueConstraintSql(TableInfo tableInfo, IList<ColumnInfo> uqColumnList)
        {
            StringBuilder uniqueQuery = new StringBuilder();
            IList<ColumnInfo> uqColumnSortedList = null;

            if (uqColumnList.Count > 0)
            {
                uqColumnSortedList = uqColumnList.OrderBy(u => u.UniqueConstraintName).ToList<ColumnInfo>();
                string uniqueConstraintName = string.Empty;
                int uqCount = 0;

                foreach (ColumnInfo uniqueCol in uqColumnSortedList)
                {
                    if (!uniqueCol.UniqueConstraintName.Equals(uniqueConstraintName))
                    {
                        uniqueConstraintName = uniqueCol.UniqueConstraintName;

                        if (uqCount != 0)
                        {
                            uniqueQuery.AppendLine(@"); ");
                        }

                        uniqueQuery.AppendLine(string.Format(@"ALTER TABLE `{0}` ADD UNIQUE INDEX `{1}` ", tableInfo.TableName, uniqueConstraintName));
                        uniqueQuery.Append(@"(");
                        uqCount = 0;
                    }

                    uniqueQuery.Append(string.Format((uqCount > 0 ? "," : string.Empty) + @"`{0}` ASC ", uniqueCol.ColumnName));
                    uqCount++;
                }

                uniqueQuery.Append(@"); ");
            }

            return uniqueQuery.ToString();
        }

        private string DropUniqueConstraintSql(TableInfo tableInfo, string uqConstraintName)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(string.Format(@"ALTER TABLE `{0}` DROP INDEX `{1}`; ", tableInfo.TableName, uqConstraintName));
            
            return query.ToString();
        }

        private string AddPKSql(TableInfo tableInfo, IList<ColumnInfo> pkColumnList)
        {
            StringBuilder pkQuery = new StringBuilder();

            if (pkColumnList.Count > 0)
            {
                int pkCount = 0;
                pkQuery.AppendLine(string.Format(@"ALTER TABLE `{0}` ADD PRIMARY KEY ", tableInfo.TableName));
                pkQuery.Append(@"(");

                foreach (ColumnInfo pkCol in pkColumnList)
                {
                    pkCount++;
                    pkQuery.Append(string.Format(@"`{0}`" + (pkCount == pkColumnList.Count ? string.Empty : ","), pkCol.ColumnName));
                }

                pkQuery.Append(@"); ");
            }

            return pkQuery.ToString();
        }

        private string DropPKSql(TableInfo tableInfo)
        {
            StringBuilder pkQuery = new StringBuilder();
            pkQuery.AppendLine(string.Format(@"ALTER TABLE `{0}` DROP PRIMARY KEY; ", tableInfo.TableName));
            return pkQuery.ToString();
        }

        private string AddColumnSql(TableInfo tableInfo, ColumnInfo columnInfo)
        {
            StringBuilder colQuery = new StringBuilder();
            colQuery.AppendLine(string.Format(@"ALTER TABLE `{0}` ADD COLUMN `{1}` {2} {3} {4} COMMENT '{5}'; ", 
                                              tableInfo.TableName, 
                                              columnInfo.ColumnName, 
                                              ToSqlDbType(columnInfo), 
                                              (columnInfo.IsNullable ? "NULL" : "NOT NULL"), 
                                              (ConvertDefaultValueSql(columnInfo)), 
                                              (string.IsNullOrWhiteSpace(columnInfo.Remarks) ? string.Empty : columnInfo.Remarks)));
            
            return colQuery.ToString();
        }

        private string EditColumnSql(TableInfo tableInfo, ColumnInfo oldColumnInfo, ColumnInfo newColumnInfo)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(string.Format(@"ALTER TABLE `{0}` CHANGE COLUMN `{1}` `{2}` {3} {4} {5} COMMENT '{6}'; ", 
                                           tableInfo.TableName, 
                                           oldColumnInfo.ColumnName, 
                                           newColumnInfo.ColumnName,
                                           ToSqlDbType(newColumnInfo),
                                           (newColumnInfo.IsNullable ? "NULL" : "NOT NULL"),
                                           (ConvertDefaultValueSql(newColumnInfo)),
                                           (string.IsNullOrWhiteSpace(newColumnInfo.Remarks) ? string.Empty : newColumnInfo.Remarks)));
            return query.ToString();
        }

        private string DropColumnSql(TableInfo tableInfo, ColumnInfo columnInfo)
        {
            StringBuilder colQuery = new StringBuilder();
            
            colQuery.AppendLine(string.Format(@"ALTER TABLE `{0}` DROP COLUMN `{1}`; ", tableInfo.TableName, columnInfo.ColumnName));
            return colQuery.ToString();
        }
        
        private string AddFKSql(TableInfo fkTableInfo, string refTableName, ColumnInfo fkColumnInfo, string refColumnName)
        {
            StringBuilder addFKQuery = new StringBuilder();
            addFKQuery.AppendLine(string.Format(@"ALTER TABLE `{0}` ADD INDEX `IDX_FK_{1}` (`{1}` ASC); ", fkTableInfo.TableName, fkColumnInfo.ColumnName));
            addFKQuery.AppendLine(string.Format(@"ALTER TABLE `{0}` ADD CONSTRAINT `FK_{1}_{3}` FOREIGN KEY (`{1}`) REFERENCES `{2}` (`{3}`) ON DELETE NO ACTION ON UPDATE NO ACTION; ", fkTableInfo.TableName, fkColumnInfo.ColumnName, refTableName, refColumnName));
            return addFKQuery.ToString();
        }
        
        private string DropFKSql(TableInfo fkTableInfo, ColumnInfo fkColumnInfo, string refColumnName)
        {
            StringBuilder dropFKQuery = new StringBuilder();
            dropFKQuery.AppendLine(string.Format(@"ALTER TABLE `{0}` DROP FOREIGN KEY `FK_{1}_{2}`; ALTER TABLE `{0}` DROP INDEX `IDX_FK_{1}`; ", fkTableInfo.TableName, fkColumnInfo.ColumnName, refColumnName));
            return dropFKQuery.ToString();
        }

        private string ConvertDefaultValueSql(ColumnInfo column)
        {
            StringBuilder defaultValueQuery = new StringBuilder();
            defaultValueQuery.Append(" DEFAULT ");

            if (column.DefaultValue == null)
            {
                switch (column.DBType)
                {
                    case DbType.AnsiString:
                    case DbType.AnsiStringFixedLength:
                    case DbType.String:
                    case DbType.StringFixedLength:
                        defaultValueQuery.Append(" '' ");
                        break;
                    case DbType.Date:
                    case DbType.DateTime:
                    case DbType.DateTime2:
                    case DbType.DateTimeOffset:
                        if(column.IsNullable)
                        {
                            defaultValueQuery.Append(" NULL ");
                        }
                        else
                        {
                            defaultValueQuery.Append(" '1753-01-01' ");
                        }

                        break;
                    case DbType.Currency:
                    case DbType.Decimal:
                    case DbType.Double:
                    case DbType.Int32:
                    case DbType.Int64:
                    case DbType.Single:
                        defaultValueQuery.Append(" 0 ");
                        break;
                }
            }
            else
            {
                switch (column.DBType)
                {
                    case DbType.AnsiString:
                    case DbType.AnsiStringFixedLength:
                    case DbType.String:
                    case DbType.StringFixedLength:
                    case DbType.Currency:
                    case DbType.Decimal:
                    case DbType.Double:
                    case DbType.Int32:
                    case DbType.Int64:
                    case DbType.Single:
                        defaultValueQuery.Append(" '" + column.DefaultValue.ToString() + "' ");
                        break;
                    case DbType.Date:
                        defaultValueQuery.Append(" '" + (column.DefaultValue is DateTime ? Convert.ToDateTime(column.DefaultValue).ToString("yyyy-MM-dd") : column.DefaultValue.ToString()) + "' ");
                        break;
                    case DbType.DateTime:
                    case DbType.DateTime2:
                    case DbType.DateTimeOffset:
                        defaultValueQuery.Append(" '" + (column.DefaultValue is DateTime ? Convert.ToDateTime(column.DefaultValue).ToString("yyyy-MM-dd HH:mm:ss") : column.DefaultValue.ToString()) + "' ");
                        break;
                }
            }

            return defaultValueQuery.ToString();
        }

        private string ToSqlDbType(ColumnInfo column)
        {
            string sqlDbType = string.Empty;

            switch (column.DBType)
            {
                case DbType.AnsiString:
                    if (column.MaxLength > 0 && column.MaxLength <= 8000)
                    {
                        sqlDbType = string.Format("varchar({0})", column.MaxLength);
                    }
                    else
                    {
                        sqlDbType = "text";
                    }
                    break;
                case DbType.AnsiStringFixedLength:
                    sqlDbType = string.Format("char({0})", column.MaxLength);
                    break;
                case DbType.Date:
                case DbType.DateTime:
                case DbType.DateTime2:
                case DbType.DateTimeOffset:
                    sqlDbType = "datetime";
                    break;
                case DbType.Currency:
                case DbType.Decimal:
                    sqlDbType = string.Format("decimal({0}, {1})", column.Precision, column.Scale);
                    break;
                case DbType.Double:
                    sqlDbType = "double";
                    break;
                case DbType.Int32:
                    sqlDbType = "int(11)";
                    break;
                case DbType.Int64:
                    sqlDbType = "bigint(18)";
                    break;
                case DbType.Single:
                    sqlDbType = "float";
                    break;
                case DbType.String:
                    if (column.MaxLength > 0 && column.MaxLength <= 4000)
                    {
                        sqlDbType = string.Format("nvarchar({0})", column.MaxLength);
                    }
                    else
                    {
                        sqlDbType = "longtext";
                    }
                    break;
                case DbType.StringFixedLength:
                    sqlDbType = string.Format("char({0})", column.MaxLength);
                    break;
            }

            return sqlDbType;
        }
    }
}
