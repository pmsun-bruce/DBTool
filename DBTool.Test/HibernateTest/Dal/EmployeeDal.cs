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
    /// 使用Hibernate的员工数据操作类
    /// </summary>
    public class EmployeeDal : HibernateDalBase, IEmployeeDal
    {
        #region Public Methods

        /// <summary>
        /// 新建员工
        /// </summary>
        /// <param name="employee">员工实体对象</param>
        /// <returns>返回处理后的员工实体对象</returns>
        public Employee Add(Employee employee)
        {
            return this.Add(employee, null);
        }

        /// <summary>
        /// 新建员工
        /// </summary>
        /// <param name="employee">员工实体对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的员工实体对象</returns>
        public Employee Add(Employee employee, ICTransaction tran)
        {
            IList<Employee> employeeList = new List<Employee>();
            employeeList.Add(employee);
            employeeList = this.Add(employeeList, tran);
            return employeeList == null ? null : employeeList[0];
        }

        /// <summary>
        /// 批量新建员工
        /// </summary>
        /// <param name="employeeList">员工实体对象集合</param>
        /// <returns>返回处理后的员工实体对象集合</returns>
        public IList<Employee> Add(IList<Employee> employeeList)
        {
            return this.Add(employeeList, null);
        }

        /// <summary>
        /// 批量新建员工
        /// </summary>
        /// <param name="employeeList">员工实体对象集合</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的员工实体对象集合</returns>
        public IList<Employee> Add(IList<Employee> employeeList, ICTransaction tran)
        {
            IList<Employee> newEmployeeList = new List<Employee>();
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

                if (employeeList != null)
                {
                    Employee newEmployee = null;

                    foreach (Employee employee in employeeList)
                    {
                        newEmployee = HibernateHelper.AddObject<Employee>(session, employee);
                        newEmployeeList.Add(newEmployee);

                        if (string.IsNullOrEmpty(newEmployee.EmployeeId) && isPass)
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
            catch(Exception ex)
            {
                if (ihTran != null)
                {
                    ihTran.Rollback();
                    HibernateHelper.FlushSession(session);
                }

                throw new Exception(ex.Message, ex);
            }

            return newEmployeeList;
        }

        /// <summary>
        /// 查询员工数量
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count(EmployeeSearcher employeeSearcher)
        {
            return this.Count(employeeSearcher, null);
        }

        /// <summary>
        /// 查询员工数量
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count(EmployeeSearcher employeeSearcher, ICTransaction tran)
        {
            EmployeeSearcher querySearcher = null;
            HibernateTransaction hTran = null;
            ISession session = null;
            HQLQueryParser queryParser = new HQLQueryParser();
            StringBuilder query = new StringBuilder();

            query.Append("SELECT ");
            query.Append("  COUNT(*) ");
            query.Append("FROM ");
            query.Append("  Employee E ");

            if (employeeSearcher != null)
            {
                querySearcher = (EmployeeSearcher)employeeSearcher.Clone();
                querySearcher.TableName = "E";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.Append("LEFT JOIN ");
                    query.Append("  E.CurrCompany C ");
                }

                if (querySearcher.CurrDepartment != null)
                {
                    querySearcher.CurrDepartment.TableName = "D";
                    query.Append("LEFT JOIN ");
                    query.Append("  E.CurrDepartment D ");
                }

                if (querySearcher.CurrPosition != null)
                {
                    querySearcher.CurrPosition.TableName = "P";
                    query.Append("LEFT JOIN ");
                    query.Append("  E.CurrPosition P ");
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
        /// 删除员工
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        public void Delete(string employeeId)
        {
            this.Delete(employeeId, null);
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="tran">中间事务对象</param>
        public void Delete(string employeeId, ICTransaction tran)
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

            Employee employee = HibernateHelper.FindObjectById<Employee>(session, employeeId);
            HibernateHelper.DeleteObject<Employee>(session, employee);
        }

        /// <summary>
        /// 根据指定条件删除员工
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        public void Delete(EmployeeSearcher employeeSearcher)
        {
            this.Delete(employeeSearcher, null);
        }

        /// <summary>
        /// 根据指定条件删除员工
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Delete(EmployeeSearcher employeeSearcher, ICTransaction tran)
        {
            EmployeeSearcher querySearcher = null;
            HibernateTransaction hTran = null;
            ISession session = null;
            HQLQueryParser queryParser = new HQLQueryParser();
            StringBuilder query = new StringBuilder();

            query.Append("DELETE FROM ");
            query.Append("  Employee  ");
            query.Append("WHERE ");
            query.Append("  EmployeeId IN (");
            query.Append("    SELECT ");
            query.Append("      E.EmployeeId ");
            query.Append("    FROM ");
            query.Append("      Employee E ");

            if (employeeSearcher != null)
            {
                querySearcher = (EmployeeSearcher)employeeSearcher.Clone();
                querySearcher.TableName = "E";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.Append("LEFT JOIN ");
                    query.Append("  E.CurrCompany C ");
                }

                if (querySearcher.CurrDepartment != null)
                {
                    querySearcher.CurrDepartment.TableName = "D";
                    query.Append("LEFT JOIN ");
                    query.Append("  E.CurrDepartment D ");
                }

                if (querySearcher.CurrPosition != null)
                {
                    querySearcher.CurrPosition.TableName = "P";
                    query.Append("LEFT JOIN ");
                    query.Append("  E.CurrPosition P ");
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
        /// 查找指定ID的员工
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>返回员工实体对象</returns>
        public Employee FindSingle(string employeeId)
        {
            return this.FindSingle(employeeId, null);
        }

        /// <summary>
        /// 查找指定ID的员工
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回员工实体对象</returns>
        public Employee FindSingle(string employeeId, ICTransaction tran)
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

            return HibernateHelper.FindObjectById<Employee>(session, employeeId);
        }

        /// <summary>
        /// 查找指定条件的员工集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <returns>返回员工实体对象集合</returns>
        public IList<Employee> FindList(EmployeeSearcher employeeSearcher)
        {
            PageList<Employee> pageList = this.FindList(employeeSearcher, null, null);
            return pageList == null ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查找指定条件的员工集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回员工实体对象集合</returns>
        public IList<Employee> FindList(EmployeeSearcher employeeSearcher, ICTransaction tran)
        {
            PageList<Employee> pageList = this.FindList(employeeSearcher, null, tran);
            return pageList == null ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查找指定条件的员工分页集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public PageList<Employee> FindList(EmployeeSearcher employeeSearcher, Pager pager)
        {
            return this.FindList(employeeSearcher, pager, null);
        }

        /// <summary>
        /// 查找指定条件的员工分页集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public PageList<Employee> FindList(EmployeeSearcher employeeSearcher, Pager pager, ICTransaction tran)
        {
            EmployeeSearcher querySearcher = null;
            HibernateTransaction hTran = null;
            ISession session = null;
            PageList<Employee> pList = new PageList<Employee>();
            HQLQueryParser queryParser = new HQLQueryParser();
            StringBuilder query = new StringBuilder();

            query.Append("SELECT ");
            query.Append(" E ");
            query.Append("FROM ");
            query.Append("  Employee E ");

            if (employeeSearcher != null)
            {
                querySearcher = (EmployeeSearcher)employeeSearcher.Clone();
                querySearcher.TableName = "E";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.Append("LEFT JOIN ");
                    query.Append("  E.CurrCompany C ");
                }

                if (querySearcher.CurrDepartment != null)
                {
                    querySearcher.CurrDepartment.TableName = "D";
                    query.Append("LEFT JOIN ");
                    query.Append("  E.CurrDepartment D ");
                }

                if (querySearcher.CurrPosition != null)
                {
                    querySearcher.CurrPosition.TableName = "P";
                    query.Append("LEFT JOIN ");
                    query.Append("  E.CurrPosition P ");
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
                pList.RecordList = HibernateHelper.FindObjectListByHQL<Employee>(session, query.ToString(), queryParser.ParamCollection, pager);
            }
            else
            {
                pList.RecordList = HibernateHelper.FindObjectListByHQL<Employee>(session, query.ToString(), queryParser.ParamCollection);
            }

            return pList;
        }

        /// <summary>
        /// 根据指定条件查找员工集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public DataTable FindDataTable(EmployeeSearcher employeeSearcher)
        {
            PageDataTable pageDataTable = this.FindDataTable(employeeSearcher, null, null);
            return pageDataTable == null ? null : pageDataTable.RecordList;
        }

        /// <summary>
        /// 根据指定条件查找员工集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public DataTable FindDataTable(EmployeeSearcher employeeSearcher, ICTransaction tran)
        {
            PageDataTable pageDataTable = this.FindDataTable(employeeSearcher, null, tran);
            return pageDataTable == null ? null : pageDataTable.RecordList;
        }

        /// <summary>
        /// 根据指定条件查找员工分页集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public PageDataTable FindDataTable(EmployeeSearcher employeeSearcher, Pager pager)
        {
            return this.FindDataTable(employeeSearcher, pager, null);
        }

        /// <summary>
        /// 根据指定条件查找员工分页集合
        /// </summary>
        /// <param name="employeeSearcher">员工查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public PageDataTable FindDataTable(EmployeeSearcher employeeSearcher, Pager pager, ICTransaction tran)
        {

            PageDataTable pDataTable = null;
            PageList<Employee> employeeList = this.FindList(employeeSearcher, pager, tran);

            if (employeeList != null)
            {
                pDataTable.PageIndex = employeeList.PageIndex;
                pDataTable.TotalCount = employeeList.TotalCount;
                pDataTable.RecordList = new DataTable("Employee");
                pDataTable.RecordList.Columns.AddRange(new DataColumn[] { 
                    new DataColumn("EmployeeCode", typeof(String)),
                    new DataColumn("EmployeeId", typeof(String)),
                    new DataColumn("CompanyId", typeof(String)),
                    new DataColumn("DepartmentId", typeof(String)),
                    new DataColumn("PositionId", typeof(String)),
                    new DataColumn("CreaterId", typeof(String)),
                    new DataColumn("CreateTime", typeof(DateTime)),
                    new DataColumn("Name", typeof(String)),
                    new DataColumn("RVersion", typeof(Int32)),
                    new DataColumn("Status", typeof(Int32)),
                    new DataColumn("UpdateTime", typeof(DateTime)),
                    new DataColumn("UpdatorId", typeof(String)),
                    new DataColumn("Birthday", typeof(DateTime)),
                    new DataColumn("Sex", typeof(Int32)),
                    new DataColumn("Rand", typeof(Int32)),
                    new DataColumn("StartWorkDate", typeof(DateTime)),
                    new DataColumn("JoinDate", typeof(DateTime))
                });

                foreach (Employee employee in employeeList.RecordList)
                {
                    pDataTable.RecordList.Rows.Add(
                        employee.EmployeeCode,
                        employee.EmployeeId,
                        employee.CurrCompany.CompanyId,
                        employee.CurrDepartment.DepartmentId,
                        employee.CurrPosition.PositionId,
                        employee.CreaterId,
                        employee.CreateTime,
                        employee.Name,
                        employee.RVersion,
                        employee.Status,
                        employee.UpdateTime,
                        employee.UpdatorId,
                        employee.Birthday,
                        employee.Sex,
                        employee.Rand,
                        employee.StartWorkDate,
                        employee.JoinDate
                    );
                }
            }

            return pDataTable;
        }

        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employee">员工实体对象</param>
        public void Update(Employee employee)
        {
            this.Update(employee, null);
        }

        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employee">员工实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Update(Employee employee, ICTransaction tran)
        {
            IList<Employee> employeeList = new List<Employee>();
            employeeList.Add(employee);
            this.Update(employeeList, tran);
        }

        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employeeList">员工实体对象</param>
        public void Update(IList<Employee> employeeList)
        {
            this.Update(employeeList, null);
        }

        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employeeList">员工实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Update(IList<Employee> employeeList, ICTransaction tran)
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
                if (employeeList != null)
                {
                    foreach (Employee employee in employeeList)
                    {
                        HibernateHelper.UpdateObject<Employee>(session, employee);
                    }

                    if (ihTran != null)
                    {
                        ihTran.Commit();
                    }
                }
                
                HibernateHelper.FlushSession(session);
            }
            catch(Exception ex)
            {
                if (ihTran != null)
                {
                    ihTran.Rollback();
                }

                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dalFactory">传入当前指定的DalFactory对象</param>
        public EmployeeDal(HibernateDalFactoryBase dalFactory)
            : base(dalFactory)
        {

        }

        #endregion
    }
}
