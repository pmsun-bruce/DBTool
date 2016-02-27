using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NFramework.DBTool.Common;
using NFramework.DBTool.QueryTool;
using NFramework.ExceptionTool;

using NFramework.DBTool.Test.Entity;
using NFramework.DBTool.Test.Searcher;

namespace NFramework.DBTool.Test.IDal
{
    /// <summary>
    /// 部门数据接口
    /// </summary>
    public interface IDepartmentDal : NFramework.DBTool.Common.IDalBase
    {
        /// <summary>
        /// 新建部门
        /// </summary>
        /// <param name="department">部门实体对象</param>
        /// <returns>返回处理后的部门实体对象</returns>
        Department Add(Department department);
        /// <summary>
        /// 新建部门
        /// </summary>
        /// <param name="department">部门实体对象</param>
        /// <param name="tran">事务对象</param>
        /// <returns>返回处理后的部门实体对象</returns>
        Department Add(Department department, ICTransaction tran);
        /// <summary>
        /// 批量新建部门
        /// </summary>
        /// <param name="departmentList">部门实体对象集合</param>
        /// <returns>返回处理后的部门实体对象集合</returns>
        IList<Department> Add(IList<Department> departmentList);
        /// <summary>
        /// 批量新建部门
        /// </summary>
        /// <param name="departmentList">部门实体对象集合</param>
        /// <param name="tran">事务对象</param>
        /// <returns>返回处理后的部门实体对象集合</returns>
        IList<Department> Add(IList<Department> departmentList, ICTransaction tran);
        /// <summary>
        /// 查询部门数量
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <returns>返回查询到的数量</returns>
        long Count(DepartmentSearcher departmentSearcher);
        /// <summary>
        /// 查询部门数量
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="tran">事务对象</param>
        /// <returns>返回查询到的数量</returns>
        long Count(DepartmentSearcher departmentSearcher, ICTransaction tran);
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        void Delete(string departmentId);
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="tran">中间事务对象</param>
        void Delete(string departmentId, ICTransaction tran);
        /// <summary>
        /// 根据指定条件删除部门
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        void Delete(DepartmentSearcher departmentSearcher);
        /// <summary>
        /// 根据指定条件删除部门
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="tran">中间事务对象</param>
        void Delete(DepartmentSearcher departmentSearcher, ICTransaction tran);
        /// <summary>
        /// 查找指定ID的部门
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>返回部门实体对象</returns>
        Department FindSingle(string departmentId);
        /// <summary>
        /// 查找指定ID的部门
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回部门实体对象</returns>
        Department FindSingle(string departmentId, ICTransaction tran);
        /// <summary>
        /// 查找指定条件的部门集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <returns>返回部门实体对象集合</returns>
        IList<Department> FindList(DepartmentSearcher departmentSearcher);
        /// <summary>
        /// 查找指定条件的部门集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回部门实体对象集合</returns>
        IList<Department> FindList(DepartmentSearcher departmentSearcher, ICTransaction tran);
        /// <summary>
        /// 查找指定条件的部门分页集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        PageList<Department> FindList(DepartmentSearcher departmentSearcher, Pager pager);
        /// <summary>
        /// 查找指定条件的部门分页集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        PageList<Department> FindList(DepartmentSearcher departmentSearcher, Pager pager, ICTransaction tran);
        /// <summary>
        /// 根据指定条件查找部门集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <returns>返回一个DataTable对象</returns>
        DataTable FindDataTable(DepartmentSearcher departmentSearcher);
        /// <summary>
        /// 根据指定条件查找部门集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回一个DataTable对象</returns>
        DataTable FindDataTable(DepartmentSearcher departmentSearcher, ICTransaction tran);
        /// <summary>
        /// 根据指定条件查找部门分页集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        PageDataTable FindDataTable(DepartmentSearcher departmentSearcher, Pager pager);
        /// <summary>
        /// 根据指定条件查找部门分页集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        PageDataTable FindDataTable(DepartmentSearcher departmentSearcher, Pager pager, ICTransaction tran);
        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="department">部门实体对象</param>
        void Update(Department department);
        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="department">部门实体对象</param>
        /// <param name="tran">中间事务对象</param>
        void Update(Department department, ICTransaction tran);
        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="departmentList">部门实体对象</param>
        void Update(IList<Department> departmentList);
        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="departmentList">部门实体对象</param>
        /// <param name="tran">中间事务对象</param>
        void Update(IList<Department> departmentList, ICTransaction tran);
    }
}
