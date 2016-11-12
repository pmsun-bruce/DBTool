
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using NFramework.DBTool.Common;

namespace NFramework.DBTool.QueryTool.Mysql
{
    /// <summary>
    /// 
    /// </summary>
    public class MysqlTransaction : ICTransaction
    {
        #region Fields & Properties

        /// <summary>
        /// 当前打开的Hibernate的事务对象
        /// </summary>
        private IDbConnection connection;
        /// <summary>
        /// 
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return connection;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private IsolationLevel isolationLevel;
        /// <summary>
        /// 
        /// </summary>
        public IsolationLevel IsolationLevel
        {
            get
            {
                return isolationLevel;
            }
        }

        protected IDbTransaction CurrentTransaction
        {
            get;
            set;
        }

        protected IDalFactoryBase CurrentDalFactory
        {
            get;
            set;
        }

        #endregion

        #region Public Constructors

        public MysqlTransaction(IDalFactoryBase dalFactory)
        {
            this.CurrentDalFactory = dalFactory;
            this.connection = this.CurrentDalFactory.OpenNewDbConnection();
            this.CurrentTransaction = this.Connection.BeginTransaction();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            this.CurrentTransaction.Commit();
            this.CurrentDalFactory.CloseDbConnection(this.Connection);
        }
        
        public void Rollback()
        {
            this.CurrentTransaction.Rollback();
            this.CurrentDalFactory.CloseDbConnection(this.Connection);
        }

        #endregion
    }
}
