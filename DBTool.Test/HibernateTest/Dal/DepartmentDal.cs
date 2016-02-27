using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NFramework.DBTool.Common;
using NFramework.DBTool.QueryTool;
using NFramework.DBTool.QueryTool.Hibernate;
using NFramework.ExceptionTool;
using NFramework.ObjectTool;
using NHibernate;

using NFramework.DBTool.Test.Entity;
using NFramework.DBTool.Test.IDal;
using NFramework.DBTool.Test.Searcher;

namespace NFramework.DBTool.Test.HibernateTest.Dal
{
    /// <summary>
    /// 使用Hibernate的部门数据操作类
    /// </summary>
    public class DepartmentDal : HibernateDalBase, IDepartmentDal
    {
        #region Public Methods

        /// <summary>
        /// 新建部门
        /// </summary>
        /// <param name="department">部门实体对象</param>
        /// <returns>返回处理后的部门实体对象</returns>
        public Department Add(Department department)
        {
            return this.Add(department, null);
        }

        /// <summary>
        /// 新建部门
        /// </summary>
        /// <param name="department">部门实体对象</param>
        /// <param name="tran">事务对象</param>
        /// <returns>返回处理后的部门实体对象</returns>
        public Department Add(Department department, ICTransaction tran)
        {
            IList<Department> departmentList = new List<Department>();
            departmentList.Add(department);
            departmentList = this.Add(departmentList, tran);
            return departmentList == null ? null : departmentList[0];
        }

        /// <summary>
        /// 批量新建部门
        /// </summary>
        /// <param name="departmentList">部门实体对象集合</param>
        /// <returns>返回处理后的部门实体对象集合</returns>
        public IList<Department> Add(IList<Department> departmentList)
        {
            return this.Add(departmentList, null);
        }

        /// <summary>
        /// 批量新建部门
        /// </summary>
        /// <param name="departmentList">部门实体对象集合</param>
        /// <param name="tran">事务对象</param>
        /// <returns>返回处理后的部门实体对象集合</returns>
        public IList<Department> Add(IList<Department> departmentList, ICTransaction tran)
        {
            IList<Department> newDepartmentList = new List<Department>();
            HibernateTransaction hTran = null;
            ISession session = null;
            ITransaction ihTran = null;

            if (tran != null)
            {
                hTran = (HibernateTransaction)tran;
                session = hTran.CurrentSession;
            }
            else
            {
                session = this.CurrentHibernateConfig.GetCurrentSession();
                ihTran = session.BeginTransaction();
            }

            try
            {
                bool isPass = true;

                if (departmentList != null)
                {
                    Department newDepartment = null;

                    foreach (Department department in departmentList)
                    {
                        newDepartment = HibernateHelper.AddObject<Department>(session, department);
                        newDepartmentList.Add(newDepartment);

                        if (string.IsNullOrEmpty(newDepartment.DepartmentId) && isPass)
                        {
                            isPass = false;
                        }
                    }
                }

                if (ihTran != null)
                {
                    if (isPass)
                    {
                        ihTran.Commit();
                    }
                    else
                    {
                        ihTran.Rollback();
                    }

                    HibernateHelper.FlushSession(session);
                }
            }
            catch
            {
                if (ihTran != null)
                {
                    ihTran.Rollback();
                    HibernateHelper.FlushSession(session);
                }

                throw;
            }

            return newDepartmentList;
        }

        /// <summary>
        /// 查询部门数量
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count(DepartmentSearcher departmentSearcher)
        {
            return this.Count(departmentSearcher, null);
        }

        /// <summary>
        /// 查询部门数量
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="tran">事务对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count(DepartmentSearcher departmentSearcher, ICTransaction tran)
        {
            DepartmentSearcher querySearcher = null;
            HibernateTransaction hTran = null;
            ISession session = null;
            HQLQueryParser queryParser = new HQLQueryParser();
            StringBuilder query = new StringBuilder();

            query.Append("SELECT ");
            query.Append("  COUNT(*) ");
            query.Append("FROM ");
            query.Append("  Department D ");

            if (departmentSearcher != null)
            {
                querySearcher = (DepartmentSearcher)departmentSearcher.Clone();
                querySearcher.TableName = "D";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.Append("LEFT JOIN ");
                    query.Append("  D.CurrCompany C ");
                }
            }

            queryParser.SearcherParse(querySearcher);

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                query.Append("WHERE ");
                query.Append("  " + queryParser.ConditionString);
            }

            if (tran != null)
            {
                hTran = (HibernateTransaction)tran;
                session = hTran.CurrentSession;
            }
            else
            {
                session = this.CurrentHibernateConfig.GetCurrentSession();
            }

            long count = HibernateHelper.FindUniqueObjectByHQL<long>(session, query.ToString(), queryParser.ParamCollection);

            return count;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        public void Delete(string departmentId)
        {
            this.Delete(departmentId, null);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="tran">中间事务对象</param>
        public void Delete(string departmentId, ICTransaction tran)
        {
            HibernateTransaction hTran = null;
            ISession session = null;

            if (tran != null)
            {
                hTran = (HibernateTransaction)tran;
                session = hTran.CurrentSession;
            }
            else
            {
                session = this.CurrentHibernateConfig.GetCurrentSession();
            }

            Department department = HibernateHelper.FindObjectById<Department>(session, departmentId);
            HibernateHelper.DeleteObject<Department>(session, department);
        }

        /// <summary>
        /// 根据指定条件删除部门
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        public void Delete(DepartmentSearcher departmentSearcher)
        {
            this.Delete(departmentSearcher, null);
        }

        /// <summary>
        /// 根据指定条件删除部门
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Delete(DepartmentSearcher departmentSearcher, ICTransaction tran)
        {
            DepartmentSearcher querySearcher = null;
            HibernateTransaction hTran = null;
            ISession session = null;
            HQLQueryParser queryParser = new HQLQueryParser();
            StringBuilder query = new StringBuilder();

            query.Append("DELETE FROM ");
            query.Append("  Department  ");
            query.Append("WHERE ");
            query.Append("  DepartmentId IN (");
            query.Append("    SELECT ");
            query.Append("      D.DepartmentId ");
            query.Append("    FROM ");
            query.Append("      Department D ");

            if (departmentSearcher != null)
            {
                querySearcher = (DepartmentSearcher)departmentSearcher.Clone();
                querySearcher.TableName = "D";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.Append("LEFT JOIN ");
                    query.Append("  D.CurrCompany C ");
                }
            }

            queryParser.SearcherParse(querySearcher);

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                query.Append("WHERE ");
                query.Append("  " + queryParser.ConditionString);
            }

            query.Append(")");

            if (tran != null)
            {
                hTran = (HibernateTransaction)tran;
                session = hTran.CurrentSession;
            }
            else
            {
                session = this.CurrentHibernateConfig.GetCurrentSession();
            }

            int effectCount = HibernateHelper.DeleteObjectByHQL(session, query.ToString(), queryParser.ParamCollection);
        }

        /// <summary>
        /// 查找指定ID的部门
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>返回部门实体对象</returns>
        public Department FindSingle(string departmentId)
        {
            return this.FindSingle(departmentId, null);
        }

        /// <summary>
        /// 查找指定ID的部门
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回部门实体对象</returns>
        public Department FindSingle(string departmentId, ICTransaction tran)
        {
            HibernateTransaction hTran = null;
            ISession session = null;

            if (tran != null)
            {
                hTran = (HibernateTransaction)tran;
                session = hTran.CurrentSession;
            }
            else
            {
                session = this.CurrentHibernateConfig.GetCurrentSession();
            }

            return HibernateHelper.FindObjectById<Department>(session, departmentId);
        }

        /// <summary>
        /// 查找指定条件的部门集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <returns>返回部门实体对象集合</returns>
        public IList<Department> FindList(DepartmentSearcher departmentSearcher)
        {
            PageList<Department> pageList = this.FindList(departmentSearcher, null, null);
            return pageList == null ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查找指定条件的部门集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回部门实体对象集合</returns>
        public IList<Department> FindList(DepartmentSearcher departmentSearcher, ICTransaction tran)
        {
            PageList<Department> pageList = this.FindList(departmentSearcher, null, tran);
            return pageList == null ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查找指定条件的部门分页集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public PageList<Department> FindList(DepartmentSearcher departmentSearcher, Pager pager)
        {
            return this.FindList(departmentSearcher, pager, null);
        }

        /// <summary>
        /// 查找指定条件的部门分页集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public PageList<Department> FindList(DepartmentSearcher departmentSearcher, Pager pager, ICTransaction tran)
        {
            DepartmentSearcher querySearcher = null;
            HibernateTransaction hTran = null;
            ISession session = null;
            PageList<Department> pList = new PageList<Department>();
            HQLQueryParser queryParser = new HQLQueryParser();
            StringBuilder query = new StringBuilder();

            query.Append("SELECT ");
            query.Append(" D ");
            query.Append("FROM ");
            query.Append("  Department D ");

            if (departmentSearcher != null)
            {
                querySearcher = (DepartmentSearcher)departmentSearcher.Clone();
                querySearcher.TableName = "D";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.Append("LEFT JOIN ");
                    query.Append("  D.CurrCompany C ");
                }
            }

            queryParser.SearcherParse(querySearcher);

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                query.Append("WHERE ");
                query.Append("  " + queryParser.ConditionString);
            }

            if (!string.IsNullOrEmpty(queryParser.SortString))
            {
                query.Append("ORDER BY ");
                query.Append("  " + queryParser.SortString);
            }

            if (tran != null)
            {
                hTran = (HibernateTransaction)tran;
                session = hTran.CurrentSession;
                pList.TotalCount = this.Count(querySearcher, tran);
            }
            else
            {
                session = this.CurrentHibernateConfig.GetCurrentSession();
                pList.TotalCount = this.Count(querySearcher);
            }

            if (pager != null)
            {
                pList.RecordList = HibernateHelper.FindObjectListByHQL<Department>(session, query.ToString(), queryParser.ParamCollection, pager);
            }
            else
            {
                pList.RecordList = HibernateHelper.FindObjectListByHQL<Department>(session, query.ToString(), queryParser.ParamCollection);
            }

            return pList;
        }

        /// <summary>
        /// 根据指定条件查找部门集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public DataTable FindDataTable(DepartmentSearcher departmentSearcher)
        {
            PageDataTable pageDataTable = this.FindDataTable(departmentSearcher, null, null);
            return pageDataTable == null ? null : pageDataTable.RecordList;
        }

        /// <summary>
        /// 根据指定条件查找部门集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public DataTable FindDataTable(DepartmentSearcher departmentSearcher, ICTransaction tran)
        {
            PageDataTable pageDataTable = this.FindDataTable(departmentSearcher, null, tran);
            return pageDataTable == null ? null : pageDataTable.RecordList;
        }

        /// <summary>
        /// 根据指定条件查找部门分页集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public PageDataTable FindDataTable(DepartmentSearcher departmentSearcher, Pager pager)
        {
            return this.FindDataTable(departmentSearcher, pager, null);
        }

        /// <summary>
        /// 根据指定条件查找部门分页集合
        /// </summary>
        /// <param name="departmentSearcher">部门查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public PageDataTable FindDataTable(DepartmentSearcher departmentSearcher, Pager pager, ICTransaction tran)
        {
            PageDataTable pDataTable = null;
            PageList<Department> departmentList = this.FindList(departmentSearcher, pager, tran);

            if (departmentList != null)
            {
                pDataTable.PageIndex = departmentList.PageIndex;
                pDataTable.TotalCount = departmentList.TotalCount;
                pDataTable.RecordList = new DataTable("Department");
                pDataTable.RecordList.Columns.AddRange(new DataColumn[] { 
                    new DataColumn("DepartmentCode", typeof(String)),
                    new DataColumn("DepartmentId", typeof(String)),
                    new DataColumn("CompanyId", typeof(String)),
                    new DataColumn("CreaterId", typeof(String)),
                    new DataColumn("CreateTime", typeof(DateTime)),
                    new DataColumn("Name", typeof(String)),
                    new DataColumn("RVersion", typeof(Int32)),
                    new DataColumn("Status", typeof(Int32)),
                    new DataColumn("UpdateTime", typeof(DateTime)),
                    new DataColumn("UpdatorId", typeof(String))
                });

                foreach (Department department in departmentList.RecordList)
                {
                    pDataTable.RecordList.Rows.Add(
                        department.DepartmentCode,
                        department.DepartmentId,
                        department.CurrCompany.CompanyId,
                        department.CreaterId,
                        department.CreateTime,
                        department.Name,
                        department.RVersion,
                        department.Status,
                        department.UpdateTime,
                        department.UpdatorId
                    );
                }
            }

            return pDataTable;
        }

        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="department">部门实体对象</param>
        public void Update(Department department)
        {
            this.Update(department, null);
        }

        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="department">部门实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Update(Department department, ICTransaction tran)
        {
            IList<Department> departmentList = new List<Department>();
            departmentList.Add(department);
            this.Update(departmentList, tran);
        }

        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="departmentList">部门实体对象</param>
        public void Update(IList<Department> departmentList)
        {
            this.Update(departmentList, null);
        }

        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="departmentList">部门实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Update(IList<Department> departmentList, ICTransaction tran)
        {
            HibernateTransaction hTran = null;
            ISession session = null;
            ITransaction ihTran = null;

            if (tran != null)
            {
                hTran = (HibernateTransaction)tran;
                session = hTran.CurrentSession;
            }
            else
            {
                session = this.CurrentHibernateConfig.GetCurrentSession();
                ihTran = session.BeginTransaction();
            }

            try
            {
                if (departmentList != null)
                {
                    foreach (Department department in departmentList)
                    {
                        HibernateHelper.UpdateObject<Department>(session, department);
                    }

                    if (ihTran != null)
                    {
                        ihTran.Commit();
                    }
                }

                HibernateHelper.FlushSession(session);
            }
            catch
            {
                if (ihTran != null)
                {
                    ihTran.Rollback();
                }

                throw;
            }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dalFactory">传入当前指定的DalFactory对象</param>
        public DepartmentDal(HibernateDalFactoryBase dalFactory)
            : base(dalFactory)
        {

        }

        #endregion
    }
}
