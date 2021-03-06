﻿namespace NFramework.DBTool.QueryTool.Mssql
{
    #region Reference

    using System;
    using System.Data;
    using System.Data.Common;

    using NFramework.DBTool.Common;

    #endregion

    /// <summary>
    /// 使用MSSQL时，使用的事务处理对象，继承ICTransaction接口，方便业务层使用事务时和数据层解耦
    /// </summary>
    public class MssqlTransaction : ICTransaction
    {
        #region Fields & Properties
        
        /// <summary>
        /// 当前打开的Hibernate的事务对象
        /// </summary>
        private DbTransaction currentTransaction;
        /// <summary>
        /// 当前打开的Hibernate的事务对象
        /// </summary>
        public DbTransaction CurrentTransaction
        {
            get { return currentTransaction; }
            set { currentTransaction = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 打开事务
        /// </summary>
        /// <param name="dalFactory">传入一个MSSQL的DalFactory对象，可用于打开一个事务</param>
        public void Begin(IDalFactoryBase dalFactory)
        {
            this.CurrentTransaction = MssqlHelper.OpenNewTransaction(((MssqlDalFactoryBase)dalFactory).CurrentConnectionString);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            this.CurrentTransaction.Commit();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollBack()
        {
            this.CurrentTransaction.Rollback();
        }

        #endregion
    }
}
