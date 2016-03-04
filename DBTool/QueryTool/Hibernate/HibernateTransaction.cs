namespace NFramework.DBTool.QueryTool.Hibernate
{
    #region Reference
    
    using NHibernate;

    using NFramework.DBTool.Common;

    #endregion

    /// <summary>
    /// 使用Hibernate时，使用的事务处理对象，继承ICTransaction接口，方便业务层使用事务时和数据层解耦
    /// </summary>
    public class HibernateTransaction : ICTransaction
    {
        #region Fields & Properties

        /// <summary>
        /// 当前事务使用的Session
        /// </summary>
        private ISession currentSession;
        /// <summary>
        /// 当前事务使用的Session
        /// </summary>
        public ISession CurrentSession
        {
            get { return currentSession; }
            set { currentSession = value; }
        }

        /// <summary>
        /// 当前打开的Hibernate的事务对象
        /// </summary>
        private ITransaction currentTransaction;
        /// <summary>
        /// 当前打开的Hibernate的事务对象
        /// </summary>
        public ITransaction CurrentTransaction
        {
            get { return currentTransaction; }
            set { currentTransaction = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 打开事务
        /// </summary>
        /// <param name="dalFactory">传入一个Hibernate的DalFactory对象，DalFactory对象含有Hibernate的配置对象，可用于打开一个事务</param>
        public void Begin(IDalFactoryBase dalFactory)
        {
            ISession session = ((HibernateDalFactoryBase)dalFactory).CurrentHibernateConfig.SessionFactory.OpenSession();
            this.CurrentSession = session;
            this.CurrentTransaction = session.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            this.CurrentSession.Flush();
            this.CurrentTransaction.Commit();
            this.CurrentSession.Close();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollBack()
        {
            this.CurrentTransaction.Rollback();
            this.CurrentSession.Close();
        }

        #endregion
    }
}
