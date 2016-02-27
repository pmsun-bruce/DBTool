using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NFramework.DBTool.Common;
using NFramework.DBTool.QueryTool.Mssql;

using NFramework.DBTool.Test.IDal;

namespace NFramework.DBTool.Test.MSSQLTest.Dal
{
    /// <summary>
    /// Dal工厂类
    /// </summary>
    public class DalFactory : NFramework.DBTool.QueryTool.Mssql.MssqlDalFactoryBase, NFramework.DBTool.Test.IDal.IDalFactory
    {
        #region Public Methods

        /// <summary>
        /// 创建公司Dal类
        /// </summary>
        /// <returns>返回D公司al类</returns>
        public ICompanyDal CreateCompanyDal()
        {
            return new CompanyDal(this);
        }

        /// <summary>
        /// 创建部门Dal类
        /// </summary>
        /// <returns>返回部门Dal类</returns>
        public IDepartmentDal CreateDepartmentDal()
        {
            return new DepartmentDal(this);
        }

        /// <summary>
        /// 创建职位Dal类
        /// </summary>
        /// <returns>返回职位Dal类</returns>
        public IPositionDal CreatePositionDal()
        {
            return new PositionDal(this);
        }

        /// <summary>
        /// 创建员工Dal类
        /// </summary>
        /// <returns>返回员工Dal类</returns>
        public IEmployeeDal CreateEmployeeDal()
        {
            return new EmployeeDal(this);
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currConnectionString">数据库连接字符串</param>
        public DalFactory(string currConnectionString)
            : base(currConnectionString)
        {

        }

        #endregion
    }
}
