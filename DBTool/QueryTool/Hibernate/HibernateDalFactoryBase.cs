namespace NFramework.DBTool.QueryTool.Hibernate
{
    #region Reference
    
    using NFramework.DBTool.Common;

    #endregion

    /// <summary>
    /// 使用Hibernate时的基础Dal工厂对象，由具体的工厂对象继承
    /// </summary>
    public abstract class HibernateDalFactoryBase : IDalFactoryBase
    {
        #region Fields & Properties

        /// <summary>
        /// Hibernate配置对象
        /// </summary>
        private HibernateConfig currentHibernateConfig;
        /// <summary>
        /// Hibernate配置对象
        /// </summary>
        public HibernateConfig CurrentHibernateConfig
        {
            get
            {
                return this.currentHibernateConfig;
            }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hibernateConfig">传入一个具体的Hibernate配置对象，用于数据库连接</param>
        public HibernateDalFactoryBase(HibernateConfig hibernateConfig)
        {
            this.currentHibernateConfig = hibernateConfig;
        }

        #endregion

        /// <summary>
        /// 开启一个新的事务，并返回事务对象，供其他需要在该事务中执行的步骤使用
        /// </summary>
        /// <returns>返回中间事务对象</returns>
        public ICTransaction BeginTransaction()
        {
            ICTransaction tran = new HibernateTransaction();
            tran.Begin(this);
            return tran;
        }
    }
}
