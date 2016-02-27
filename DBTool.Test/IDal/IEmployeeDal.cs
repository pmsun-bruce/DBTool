using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NFramework.DBTool.Common;
using NFramework.DBTool.QueryTool;
using NFramework.ExceptionTool;

using NFramework.DBTool.Test.Entity;
using NFramework.DBTool.Test.Searcher;
using System.Data;

namespace NFramework.DBTool.Test.IDal
{
    /// <summary>
    /// 员工数据接口
    /// </summary>
    public interface IEmployeeDal : NFramework.DBTool.Common.IDalBase
    {
        /// <summary>
        /// 新建员工
        /// </summary>
        /// <param name="employee">员工实体对象</param>
        /// <returns>返回处理后的员工实体对象</returns>
        Employee Add(Employee employee);
        /// <summary>
        /// 新建员工
        /// </summary>
        /// <param name="employee">员工实体对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的员工实体对象</returns>
        Employee Add(Employee employee, ICTransaction tran);
        /// <summary>
        /// 批量新建员工
        /// </summary>
        /// <param name="employeeList">员工实体对象集合</param>
        /// <returns>返回处理后的员工实体对象集合</returns>
        IList<Employee> Add(IList<Employee> employeeList);
        /// <summary>
        /// 批量新建员工
        /// </summary>
        /// <param name="employeeList">员工实体对象集合</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的员工实体对象集合</returns>
        IList<Employee> Add(IList<Employee> employeeList, ICTransaction tran);
        /// <summary>
        /// 查询员工数量
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <returns>返回查询到的数量</returns>
        long Count(EmployeeSearcher employeeSearcher);
        /// <summary>
        /// 查询员工数量
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查询到的数量</returns>
        long Count(EmployeeSearcher employeeSearcher, ICTransaction tran);
        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        void Delete(string employeeId);
        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="tran">中间事务对象</param>
        void Delete(string employeeId, ICTransaction tran);
        /// <summary>
        /// 根据指定条件删除员工
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        void Delete(EmployeeSearcher employeeSearcher);
        /// <summary>
        /// 根据指定条件删除员工
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="tran">中间事务对象</param>
        void Delete(EmployeeSearcher employeeSearcher, ICTransaction tran);
        /// <summary>
        /// 查找指定ID的员工
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>返回员工实体对象</returns>
        Employee FindSingle(string employeeId);
        /// <summary>
        /// 查找指定ID的员工
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回员工实体对象</returns>
        Employee FindSingle(string employeeId, ICTransaction tran);
        /// <summary>
        /// 查找指定条件的员工集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <returns>返回员工实体对象集合</returns>
        IList<Employee> FindList(EmployeeSearcher employeeSearcher);
        /// <summary>
        /// 查找指定条件的员工集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回员工实体对象集合</returns>
        IList<Employee> FindList(EmployeeSearcher employeeSearcher, ICTransaction tran);
        /// <summary>
        /// 查找指定条件的员工分页集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        PageList<Employee> FindList(EmployeeSearcher employeeSearcher, Pager pager);
        /// <summary>
        /// 查找指定条件的员工分页集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        PageList<Employee> FindList(EmployeeSearcher employeeSearcher, Pager pager, ICTransaction tran);
        /// <summary>
        /// 根据指定条件查找员工集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <returns>返回一个DataTable对象</returns>
        DataTable FindDataTable(EmployeeSearcher employeeSearcher);
        /// <summary>
        /// 根据指定条件查找员工集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回一个DataTable对象</returns>
        DataTable FindDataTable(EmployeeSearcher employeeSearcher, ICTransaction tran);
        /// <summary>
        /// 根据指定条件查找员工分页集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        PageDataTable FindDataTable(EmployeeSearcher employeeSearcher, Pager pager);
        /// <summary>
        /// 根据指定条件查找员工分页集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        PageDataTable FindDataTable(EmployeeSearcher employeeSearcher, Pager pager, ICTransaction tran);
        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employee">员工实体对象</param>
        void Update(Employee employee);
        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employee">员工实体对象</param>
        /// <param name="tran">中间事务对象</param>
        void Update(Employee employee, ICTransaction tran);
        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employeeList">员工实体对象</param>
        void Update(IList<Employee> employeeList);
        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employeeList">员工实体对象</param>
        /// <param name="tran">中间事务对象</param>
        void Update(IList<Employee> employeeList, ICTransaction tran);
    }
}
