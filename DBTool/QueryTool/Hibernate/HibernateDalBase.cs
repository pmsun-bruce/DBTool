namespace NFramework.DBTool.QueryTool.Hibernate
{
    #region Reference 
    
    using NFramework.DBTool.Common;

    #endregion

    /// <summary>
    /// 使用Hibernate时的Dal的抽象类
    /// </summary>
    public abstract class HibernateDalBase
    {
        #region Fields & Porperties

        /// <summary>
        /// Dal工厂，使用IDalFactoryBase类型，可用于外部参数的传递。
        /// </summary>
        public IDalFactoryBase DalFactory
        {
            get;
            set;
        }

        /// <summary>
        /// 获取当前Hibernate的配置对象
        /// </summary>
        public HibernateConfig CurrentHibernateConfig
        {
            get
            {
                return ((HibernateDalFactoryBase)this.DalFactory).CurrentHibernateConfig;
            }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 传入一个具体的使用Hibernate的Dal工厂对象
        /// </summary>
        /// <param name="dalFactory"></param>
        public HibernateDalBase(HibernateDalFactoryBase dalFactory)
        {
            this.DalFactory = dalFactory;
        }

        #endregion
    }
}
