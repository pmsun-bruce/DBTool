namespace NFramework.DBTool.Test.Handler
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NFramework.DBTool.Common;
    using NFramework.DBTool.QueryTool;
    using NFramework.DBTool.QueryTool.Hibernate;
    using NFramework.ExceptionTool;

    using NFramework.DBTool.Test.Entity;
    using NFramework.DBTool.Test.IDal;
    using NFramework.DBTool.Test.Searcher;
    using System.Data;

    #endregion

    /// <summary>
    /// 组织结构业务逻辑类
    /// </summary>
    public class OrgHandler
    {
        #region Dal Fields & Properties

        /// <summary>
        /// 公司数据操作类
        /// </summary>
        private static ICompanyDal companyDal;
        /// <summary>
        /// 公司数据操作类
        /// </summary>
        protected static ICompanyDal CompanyDal
        {
            get
            {
                if (companyDal == null)
                {
                    companyDal = DalManager.DalFactory.CreateCompanyDal();
                }

                return companyDal;
            }
        }

        /// <summary>
        /// 部门数据操作类
        /// </summary>
        private static IDepartmentDal departmentDal;
        /// <summary>
        /// 部门数据操作类
        /// </summary>
        protected static IDepartmentDal DepartmentDal
        {
            get
            {
                if (departmentDal == null)
                {
                    departmentDal = DalManager.DalFactory.CreateDepartmentDal();
                }

                return departmentDal;
            }
        }

        /// <summary>
        /// 职位数据操作类
        /// </summary>
        private static IPositionDal positionDal;
        /// <summary>
        /// 职位数据操作类
        /// </summary>
        protected static IPositionDal PositionDal
        {
            get
            {
                if (positionDal == null)
                {
                    positionDal = DalManager.DalFactory.CreatePositionDal();
                }

                return positionDal;
            }
        }

        /// <summary>
        /// 员工数据操作类
        /// </summary>
        private static IEmployeeDal employeeDal;
        /// <summary>
        /// 员工数据操作类
        /// </summary>
        protected static IEmployeeDal EmployeeDal
        {
            get
            {
                if (employeeDal == null)
                {
                    employeeDal = DalManager.DalFactory.CreateEmployeeDal();
                }

                return employeeDal;
            }
        }

        #endregion

        #region Company Methods

        /// <summary>
        /// 新建公司
        /// </summary>
        /// <param name="company">公司实体对象</param>
        /// <returns>返回处理后的公司实体对象</returns>
        public static Company AddCompany(Company company)
        {
            return CompanyDal.Add(company);
        }

        /// <summary>
        /// 新建公司
        /// </summary>
        /// <param name="company">公司实体对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的公司实体对象</returns>
        public static Company AddCompany(Company company, ICTransaction tran)
        {
            return CompanyDal.Add(company, tran);
        }

        /// <summary>
        /// 批量新建公司
        /// </summary>
        /// <param name="companyList">公司实体对象集合</param>
        /// <returns>返回处理后的公司实体对象集合</returns>
        public static IList<Company> AddCompany(IList<Company> companyList)
        {
            return CompanyDal.Add(companyList);
        }

        /// <summary>
        /// 批量新建公司
        /// </summary>
        /// <param name="companyList">公司实体对象集合</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的公司实体对象集合</returns>
        public static IList<Company> AddCompany(IList<Company> companyList, ICTransaction tran)
        {
            return CompanyDal.Add(companyList, tran);
        }

        /// <summary>
        /// 查询公司数量
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <returns>返回查询到的数量</returns>
        public static long CountCompany(CompanySearcher companySearcher)
        {
            long count = CompanyDal.Count(companySearcher);
            return count;
        }

        /// <summary>
        /// 查询公司数量
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查询到的数量</returns>
        public static long CountCompany(CompanySearcher companySearcher, ICTransaction tran)
        {
            long count = CompanyDal.Count(companySearcher, tran);
            return count;
        }

        /// <summary>
        /// 删除公司
        /// </summary>
        /// <param name="companyId">公司ID</param>
        public static void DeleteCompany(string companyId)
        {
            CompanyDal.Delete(companyId);
        }

        /// <summary>
        /// 删除公司
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="tran">中间事务对象</param>
        public static void DeleteCompany(string companyId, ICTransaction tran)
        {
            CompanyDal.Delete(companyId, tran);
        }

        /// <summary>
        /// 根据指定条件删除公司
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        public static void DeleteCompany(CompanySearcher companySearcher)
        {
            CompanyDal.Delete(companySearcher);
        }

        /// <summary>
        /// 根据指定条件删除公司
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="tran">中间事务对象</param>
        public static void DeleteCompany(CompanySearcher companySearcher, ICTransaction tran)
        {
            CompanyDal.Delete(companySearcher, tran);
        }

        /// <summary>
        /// 查找指定ID的公司
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns>返回公司实体对象</returns>
        public static Company FindCompanyById(string companyId)
        {
            return CompanyDal.FindSingle(companyId);
        }

        /// <summary>
        /// 查找指定ID的公司
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回公司实体对象</returns>
        public static Company FindCompanyById(string companyId, ICTransaction tran)
        {
            return CompanyDal.FindSingle(companyId, tran);
        }

        /// <summary>
        /// 查找指定条件的公司集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <returns>返回公司实体对象集合</returns>
        public static IList<Company> FindCompanyList(CompanySearcher companySearcher)
        {
            return CompanyDal.FindList(companySearcher);
        }

        /// <summary>
        /// 查找指定条件的公司集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回公司实体对象集合</returns>
        public static IList<Company> FindCompanyList(CompanySearcher companySearcher, ICTransaction tran)
        {
            return CompanyDal.FindList(companySearcher, tran);
        }

        /// <summary>
        /// 查找指定条件的公司分页集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public static PageList<Company> FindCompanyList(CompanySearcher companySearcher, Pager pager)
        {
            return CompanyDal.FindList(companySearcher, pager);
        }

        /// <summary>
        /// 查找指定条件的公司分页集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public static PageList<Company> FindCompanyList(CompanySearcher companySearcher, Pager pager, ICTransaction tran)
        {
            return CompanyDal.FindList(companySearcher, pager, tran);
        }

        /// <summary>
        /// 根据指定条件查找公司集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public static DataTable FindCompanyDataTable(CompanySearcher companySearcher)
        {
            return CompanyDal.FindDataTable(companySearcher);
        }

        /// <summary>
        /// 根据指定条件查找公司集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public static DataTable FindCompanyDataTable(CompanySearcher companySearcher, ICTransaction tran)
        {
            return CompanyDal.FindDataTable(companySearcher, tran);
        }

        /// <summary>
        /// 根据指定条件查找公司分页集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public static PageDataTable FindCompanyDataTable(CompanySearcher companySearcher, Pager pager)
        {
            return CompanyDal.FindDataTable(companySearcher, pager);
        }

        /// <summary>
        /// 根据指定条件查找公司分页集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public static PageDataTable FindCompanyDataTable(CompanySearcher companySearcher, Pager pager, ICTransaction tran)
        {
            return CompanyDal.FindDataTable(companySearcher, pager, tran);
        }

        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="company">公司实体对象</param>
        public static void UpdateCompany(Company company)
        {
            CompanyDal.Update(company);
        }

        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="company">公司实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public static void UpdateCompany(Company company, ICTransaction tran)
        {
            CompanyDal.Update(company, tran);
        }

        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="companyList">公司实体对象</param>
        public static void UpdateCompany(IList<Company> companyList)
        {
            CompanyDal.Update(companyList);
        }

        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="companyList">公司实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public static void UpdateCompany(IList<Company> companyList, ICTransaction tran)
        {
            CompanyDal.Update(companyList, tran);
        }

        #endregion

        #region Department Methods

        /// <summary>
        /// 新建部门
        /// </summary>
        /// <param name="department">部门实体对象</param>
        /// <returns>返回处理后的部门实体对象</returns>
        public static Department AddDepartment(Department department)
        {
            return DepartmentDal.Add(department);
        }

        /// <summary>
        /// 新建部门
        /// </summary>
        /// <param name="department">部门实体对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的部门实体对象</returns>
        public static Department AddDepartment(Department department, ICTransaction tran)
        {
            return DepartmentDal.Add(department, tran);
        }

        /// <summary>
        /// 批量新建部门
        /// </summary>
        /// <param name="departmentList">部门实体对象集合</param>
        /// <returns>返回处理后的部门实体对象集合</returns>
        public static IList<Department> AddDepartment(IList<Department> departmentList)
        {
            return DepartmentDal.Add(departmentList);
        }

        /// <summary>
        /// 批量新建部门
        /// </summary>
        /// <param name="departmentList">部门实体对象集合</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的部门实体对象集合</returns>
        public static IList<Department> AddDepartment(IList<Department> departmentList, ICTransaction tran)
        {
            return DepartmentDal.Add(departmentList, tran);
        }

        /// <summary>
        /// 查询部门数量
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <returns>返回查询到的数量</returns>
        public static long CountDepartment(DepartmentSearcher departmentSearcher)
        {
            long count = DepartmentDal.Count(departmentSearcher);
            return count;
        }

        /// <summary>
        /// 查询部门数量
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查询到的数量</returns>
        public static long CountDepartment(DepartmentSearcher departmentSearcher, ICTransaction tran)
        {
            long count = DepartmentDal.Count(departmentSearcher, tran);
            return count;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        public static void DeleteDepartment(string departmentId)
        {
            DepartmentDal.Delete(departmentId);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="tran">中间事务对象</param>
        public static void DeleteDepartment(string departmentId, ICTransaction tran)
        {
            DepartmentDal.Delete(departmentId, tran);
        }

        /// <summary>
        /// 根据指定条件删除部门
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        public static void DeleteDepartment(DepartmentSearcher departmentSearcher)
        {
            DepartmentDal.Delete(departmentSearcher);
        }

        /// <summary>
        /// 根据指定条件删除部门
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="tran">中间事务对象</param>
        public static void DeleteDepartment(DepartmentSearcher departmentSearcher, ICTransaction tran)
        {
            DepartmentDal.Delete(departmentSearcher, tran);
        }

        /// <summary>
        /// 查找指定ID的部门
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>返回部门实体对象</returns>
        public static Department FindDepartmentById(string departmentId)
        {
            return DepartmentDal.FindSingle(departmentId);
        }

        /// <summary>
        /// 查找指定ID的部门
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回部门实体对象</returns>
        public static Department FindDepartmentById(string departmentId, ICTransaction tran)
        {
            return DepartmentDal.FindSingle(departmentId, tran);
        }

        /// <summary>
        /// 查找指定条件的部门集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <returns>返回部门实体对象集合</returns>
        public static IList<Department> FindDepartmentList(DepartmentSearcher departmentSearcher)
        {
            return DepartmentDal.FindList(departmentSearcher);
        }

        /// <summary>
        /// 查找指定条件的部门集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回部门实体对象集合</returns>
        public static IList<Department> FindDepartmentList(DepartmentSearcher departmentSearcher, ICTransaction tran)
        {
            return DepartmentDal.FindList(departmentSearcher, tran);
        }

        /// <summary>
        /// 查找指定条件的部门分页集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public static PageList<Department> FindDepartmentList(DepartmentSearcher departmentSearcher, Pager pager)
        {
            return DepartmentDal.FindList(departmentSearcher, pager);
        }

        /// <summary>
        /// 查找指定条件的部门分页集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public static PageList<Department> FindDepartmentList(DepartmentSearcher departmentSearcher, Pager pager, ICTransaction tran)
        {
            return DepartmentDal.FindList(departmentSearcher, pager, tran);
        }

        /// <summary>
        /// 根据指定条件查找部门集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public static DataTable FindDepartmentDataTable(DepartmentSearcher departmentSearcher)
        {
            return DepartmentDal.FindDataTable(departmentSearcher);
        }

        /// <summary>
        /// 根据指定条件查找部门集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public static DataTable FindDepartmentDataTable(DepartmentSearcher departmentSearcher, ICTransaction tran)
        {
            return DepartmentDal.FindDataTable(departmentSearcher, tran);
        }

        /// <summary>
        /// 根据指定条件查找部门分页集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public static PageDataTable FindDepartmentDataTable(DepartmentSearcher departmentSearcher, Pager pager)
        {
            return DepartmentDal.FindDataTable(departmentSearcher, pager);
        }

        /// <summary>
        /// 根据指定条件查找部门分页集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public static PageDataTable FindDepartmentDataTable(DepartmentSearcher departmentSearcher, Pager pager, ICTransaction tran)
        {
            return DepartmentDal.FindDataTable(departmentSearcher, pager, tran);
        }

        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="department">部门实体对象</param>
        public static void UpdateDepartment(Department department)
        {
            DepartmentDal.Update(department);
        }

        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="department">部门实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public static void UpdateDepartment(Department department, ICTransaction tran)
        {
            DepartmentDal.Update(department, tran);
        }

        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="departmentList">部门实体对象</param>
        public static void UpdateDepartment(IList<Department> departmentList)
        {
            DepartmentDal.Update(departmentList);
        }

        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="departmentList">部门实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public static void UpdateDepartment(IList<Department> departmentList, ICTransaction tran)
        {
            DepartmentDal.Update(departmentList, tran);
        }

        #endregion

        #region Position Methods

        /// <summary>
        /// 新建职位
        /// </summary>
        /// <param name="position">职位实体对象</param>
        /// <returns>返回处理后的职位实体对象</returns>
        public static Position AddPosition(Position position)
        {
            return PositionDal.Add(position);
        }

        /// <summary>
        /// 新建职位
        /// </summary>
        /// <param name="position">职位实体对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的职位实体对象</returns>
        public static Position AddPosition(Position position, ICTransaction tran)
        {
            return PositionDal.Add(position, tran);
        }

        /// <summary>
        /// 批量新建职位
        /// </summary>
        /// <param name="positionList">职位实体对象集合</param>
        /// <returns>返回处理后的职位实体对象集合</returns>
        public static IList<Position> AddPosition(IList<Position> positionList)
        {
            return PositionDal.Add(positionList);
        }

        /// <summary>
        /// 批量新建职位
        /// </summary>
        /// <param name="positionList">职位实体对象集合</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的职位实体对象集合</returns>
        public static IList<Position> AddPosition(IList<Position> positionList, ICTransaction tran)
        {
            return PositionDal.Add(positionList, tran);
        }

        /// <summary>
        /// 查询职位数量
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <returns>返回查询到的数量</returns>
        public static long CountPosition(PositionSearcher positionSearcher)
        {
            long count = PositionDal.Count(positionSearcher);
            return count;
        }

        /// <summary>
        /// 查询职位数量
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查询到的数量</returns>
        public static long CountPosition(PositionSearcher positionSearcher, ICTransaction tran)
        {
            long count = PositionDal.Count(positionSearcher, tran);
            return count;
        }

        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="positionId">职位ID</param>
        public static void DeletePosition(string positionId)
        {
            PositionDal.Delete(positionId);
        }

        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="positionId">职位ID</param>
        /// <param name="tran">中间事务对象</param>
        public static void DeletePosition(string positionId, ICTransaction tran)
        {
            PositionDal.Delete(positionId, tran);
        }

        /// <summary>
        /// 根据指定条件删除职位
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        public static void DeletePosition(PositionSearcher positionSearcher)
        {
            PositionDal.Delete(positionSearcher);
        }

        /// <summary>
        /// 根据指定条件删除职位
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="tran">中间事务对象</param>
        public static void DeletePosition(PositionSearcher positionSearcher, ICTransaction tran)
        {
            PositionDal.Delete(positionSearcher, tran);
        }

        /// <summary>
        /// 查找指定ID的职位
        /// </summary>
        /// <param name="positionId">职位ID</param>
        /// <returns>返回职位实体对象</returns>
        public static Position FindPositionById(string positionId)
        {
            return PositionDal.FindSingle(positionId);
        }

        /// <summary>
        /// 查找指定ID的职位
        /// </summary>
        /// <param name="positionId">职位ID</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回职位实体对象</returns>
        public static Position FindPositionById(string positionId, ICTransaction tran)
        {
            return PositionDal.FindSingle(positionId, tran);
        }

        /// <summary>
        /// 查找指定条件的职位集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <returns>返回职位实体对象集合</returns>
        public static IList<Position> FindPositionList(PositionSearcher positionSearcher)
        {
            return PositionDal.FindList(positionSearcher);
        }

        /// <summary>
        /// 查找指定条件的职位集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回职位实体对象集合</returns>
        public static IList<Position> FindPositionList(PositionSearcher positionSearcher, ICTransaction tran)
        {
            return PositionDal.FindList(positionSearcher, tran);
        }

        /// <summary>
        /// 查找指定条件的职位分页集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public static PageList<Position> FindPositionList(PositionSearcher positionSearcher, Pager pager)
        {
            return PositionDal.FindList(positionSearcher, pager);
        }

        /// <summary>
        /// 查找指定条件的职位分页集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public static PageList<Position> FindPositionList(PositionSearcher positionSearcher, Pager pager, ICTransaction tran)
        {
            return PositionDal.FindList(positionSearcher, pager, tran);
        }

        /// <summary>
        /// 根据指定条件查找职位集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public static DataTable FindPositionDataTable(PositionSearcher positionSearcher)
        {
            return PositionDal.FindDataTable(positionSearcher);
        }

        /// <summary>
        /// 根据指定条件查找职位集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public static DataTable FindPositionDataTable(PositionSearcher positionSearcher, ICTransaction tran)
        {
            return PositionDal.FindDataTable(positionSearcher, tran);
        }

        /// <summary>
        /// 根据指定条件查找职位分页集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public static PageDataTable FindPositionDataTable(PositionSearcher positionSearcher, Pager pager)
        {
            return PositionDal.FindDataTable(positionSearcher, pager);
        }

        /// <summary>
        /// 根据指定条件查找职位分页集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public static PageDataTable FindPositionDataTable(PositionSearcher positionSearcher, Pager pager, ICTransaction tran)
        {
            return PositionDal.FindDataTable(positionSearcher, pager, tran);
        }

        /// <summary>
        /// 更新职位数据
        /// </summary>
        /// <param name="position">职位实体对象</param>
        public static void UpdatePosition(Position position)
        {
            PositionDal.Update(position);
        }

        /// <summary>
        /// 更新职位数据
        /// </summary>
        /// <param name="position">职位实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public static void UpdatePosition(Position position, ICTransaction tran)
        {
            PositionDal.Update(position, tran);
        }

        /// <summary>
        /// 更新职位数据
        /// </summary>
        /// <param name="positionList">职位实体对象</param>
        public static void UpdatePosition(IList<Position> positionList)
        {
            PositionDal.Update(positionList);
        }

        /// <summary>
        /// 更新职位数据
        /// </summary>
        /// <param name="positionList">职位实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public static void UpdatePosition(IList<Position> positionList, ICTransaction tran)
        {
            PositionDal.Update(positionList, tran);
        }

        #endregion

        #region Employee Methods

        /// <summary>
        /// 新建员工
        /// </summary>
        /// <param name="employee">员工实体对象</param>
        /// <returns>返回处理后的员工实体对象</returns>
        public static Employee AddEmployee(Employee employee)
        {
            return EmployeeDal.Add(employee);
        }

        /// <summary>
        /// 新建员工
        /// </summary>
        /// <param name="employee">员工实体对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的员工实体对象</returns>
        public static Employee AddEmployee(Employee employee, ICTransaction tran)
        {
            return EmployeeDal.Add(employee, tran);
        }

        /// <summary>
        /// 批量新建员工
        /// </summary>
        /// <param name="employeeList">员工实体对象集合</param>
        /// <returns>返回处理后的员工实体对象集合</returns>
        public static IList<Employee> AddEmployee(IList<Employee> employeeList)
        {
            return EmployeeDal.Add(employeeList);
        }

        /// <summary>
        /// 批量新建员工
        /// </summary>
        /// <param name="employeeList">员工实体对象集合</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的员工实体对象集合</returns>
        public static IList<Employee> AddEmployee(IList<Employee> employeeList, ICTransaction tran)
        {
            return EmployeeDal.Add(employeeList, tran);
        }

        /// <summary>
        /// 查询员工数量
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <returns>返回查询到的数量</returns>
        public static long CountEmployee(EmployeeSearcher employeeSearcher)
        {
            long count = EmployeeDal.Count(employeeSearcher);
            return count;
        }

        /// <summary>
        /// 查询员工数量
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查询到的数量</returns>
        public static long CountEmployee(EmployeeSearcher employeeSearcher, ICTransaction tran)
        {
            long count = EmployeeDal.Count(employeeSearcher, tran);
            return count;
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        public static void DeleteEmployee(string employeeId)
        {
            EmployeeDal.Delete(employeeId);
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="tran">中间事务对象</param>
        public static void DeleteEmployee(string employeeId, ICTransaction tran)
        {
            EmployeeDal.Delete(employeeId, tran);
        }

        /// <summary>
        /// 根据指定条件删除员工
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        public static void DeleteEmployee(EmployeeSearcher employeeSearcher)
        {
            EmployeeDal.Delete(employeeSearcher);
        }

        /// <summary>
        /// 根据指定条件删除员工
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="tran">中间事务对象</param>
        public static void DeleteEmployee(EmployeeSearcher employeeSearcher, ICTransaction tran)
        {
            EmployeeDal.Delete(employeeSearcher, tran);
        }

        /// <summary>
        /// 查找指定ID的员工
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>返回员工实体对象</returns>
        public static Employee FindEmployeeById(string employeeId)
        {
            return EmployeeDal.FindSingle(employeeId);
        }

        /// <summary>
        /// 查找指定ID的员工
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回员工实体对象</returns>
        public static Employee FindEmployeeById(string employeeId, ICTransaction tran)
        {
            return EmployeeDal.FindSingle(employeeId, tran);
        }

        /// <summary>
        /// 查找指定条件的员工集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <returns>返回员工实体对象集合</returns>
        public static IList<Employee> FindEmployeeList(EmployeeSearcher employeeSearcher)
        {
            return EmployeeDal.FindList(employeeSearcher);
        }

        /// <summary>
        /// 查找指定条件的员工集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回员工实体对象集合</returns>
        public static IList<Employee> FindEmployeeList(EmployeeSearcher employeeSearcher, ICTransaction tran)
        {
            return EmployeeDal.FindList(employeeSearcher, tran);
        }

        /// <summary>
        /// 查找指定条件的员工分页集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public static PageList<Employee> FindEmployeeList(EmployeeSearcher employeeSearcher, Pager pager)
        {
            return EmployeeDal.FindList(employeeSearcher, pager);
        }

        /// <summary>
        /// 查找指定条件的员工分页集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public static PageList<Employee> FindEmployeeList(EmployeeSearcher employeeSearcher, Pager pager, ICTransaction tran)
        {
            return EmployeeDal.FindList(employeeSearcher, pager, tran);
        }

        /// <summary>
        /// 根据指定条件查找员工集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public static DataTable FindEmployeeDataTable(EmployeeSearcher employeeSearcher)
        {
            return EmployeeDal.FindDataTable(employeeSearcher);
        }

        /// <summary>
        /// 根据指定条件查找员工集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public static DataTable FindEmployeeDataTable(EmployeeSearcher employeeSearcher, ICTransaction tran)
        {
            return EmployeeDal.FindDataTable(employeeSearcher, tran);
        }

        /// <summary>
        /// 根据指定条件查找员工分页集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public static PageDataTable FindEmployeeDataTable(EmployeeSearcher employeeSearcher, Pager pager)
        {
            return EmployeeDal.FindDataTable(employeeSearcher, pager);
        }

        /// <summary>
        /// 根据指定条件查找员工分页集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public static PageDataTable FindEmployeeDataTable(EmployeeSearcher employeeSearcher, Pager pager, ICTransaction tran)
        {
            return EmployeeDal.FindDataTable(employeeSearcher, pager, tran);
        }

        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employee">员工实体对象</param>
        public static void UpdateEmployee(Employee employee)
        {
            EmployeeDal.Update(employee);
        }

        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employee">员工实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public static void UpdateEmployee(Employee employee, ICTransaction tran)
        {
            EmployeeDal.Update(employee, tran);
        }

        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employeeList">员工实体对象</param>
        public static void UpdateEmployee(IList<Employee> employeeList)
        {
            EmployeeDal.Update(employeeList);
        }

        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employeeList">员工实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public static void UpdateEmployee(IList<Employee> employeeList, ICTransaction tran)
        {
            EmployeeDal.Update(employeeList, tran);
        }

        #endregion
    }
}
