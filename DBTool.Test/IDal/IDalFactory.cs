using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NFramework.DBTool.Common;

using NFramework.DBTool.Test.IDal;

namespace NFramework.DBTool.Test.IDal
{
    /// <summary>
    /// Dal工厂接口
    /// </summary>
    public interface IDalFactory : NFramework.DBTool.Common.IDalFactoryBase
    {
        /// <summary>
        /// 创建公司Dal类
        /// </summary>
        /// <returns>返回D公司al类</returns>
        ICompanyDal CreateCompanyDal();
        /// <summary>
        /// 创建部门Dal类
        /// </summary>
        /// <returns>返回部门Dal类</returns>
        IDepartmentDal CreateDepartmentDal();
        /// <summary>
        /// 创建职位Dal类
        /// </summary>
        /// <returns>返回职位Dal类</returns>
        IPositionDal CreatePositionDal();
        /// <summary>
        /// 创建员工Dal类
        /// </summary>
        /// <returns>返回员工Dal类</returns>
        IEmployeeDal CreateEmployeeDal();
    }
}
