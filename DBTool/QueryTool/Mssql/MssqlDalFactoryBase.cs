namespace NFramework.DBTool.QueryTool.Mssql
{
    #region Reference

    using System.Data.SqlClient;

    using NFramework.DBTool.Common;

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
    }
}
