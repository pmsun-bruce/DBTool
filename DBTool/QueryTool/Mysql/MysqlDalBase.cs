using NFramework.DBTool.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFramework.DBTool.QueryTool.Mysql
{
    /// <summary>
    /// 
    /// </summary>
    public class MysqlDalBase
    {
        #region Fields & Porperties

        /// <summary>
        /// Dal工厂，使用IDalFactoryBase类型，可用于外部参数的传递。
        /// </summary>
        private MysqlDalFactoryBase dalFactory;

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
                this.dalFactory = (MysqlDalFactoryBase)value;
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
        /// 传入一个具体的使用MYSQL的Dal工厂对象
        /// </summary>
        /// <param name="dalFactory"></param>
        public MysqlDalBase(MysqlDalFactoryBase dalFactory)
        {
            this.DalFactory = dalFactory;
        }

        #endregion
    }
}
