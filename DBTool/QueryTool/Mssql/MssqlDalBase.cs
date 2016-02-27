namespace NFramework.DBTool.QueryTool.Mssql
{
    #region Reference

    using NFramework.DBTool.Common;

    #endregion

    /// <summary>
    /// MSSQL的Dal抽象对象
    /// </summary>
    public abstract class MssqlDalBase
    {
        #region Fields & Porperties
        
        /// <summary>
        /// Dal工厂，使用IDalFactoryBase类型，可用于外部参数的传递。
        /// </summary>
        private MssqlDalFactoryBase dalFactory;

        /// <summary>
        /// Dal工厂，使用IDalFactoryBase类型，可用于外部参数的传递。
        /// </summary>
        public IDalFactoryBase DalFactory
        {
            get
            {
                return this.dalFactory;
            }
            set
            {
                this.dalFactory = (MssqlDalFactoryBase)value;
            }
        }
        
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected string CurrentConnectionString
        {
            get
            {
                return this.dalFactory.CurrentConnectionString;
            }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 传入一个具体的使用MSSQL的Dal工厂对象
        /// </summary>
        /// <param name="dalFactory"></param>
        public MssqlDalBase(MssqlDalFactoryBase dalFactory)
        {
            this.DalFactory = dalFactory;
        }

        #endregion
    }
}
