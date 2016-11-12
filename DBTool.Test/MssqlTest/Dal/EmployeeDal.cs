using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using NFramework.DBTool.Common;
using NFramework.ExceptionTool;
using NFramework.DBTool.QueryTool;
using NFramework.DBTool.QueryTool.Mssql;

using NFramework.DBTool.Test.Entity;
using NFramework.DBTool.Test.IDal;
using NFramework.DBTool.Test.Searcher;
using System.Data.SqlClient;

namespace NFramework.DBTool.Test.MSSQLTest.Dal
{
    /// <summary>
    /// 使用MSSQL原生SQL语句的员工数据操作类
    /// </summary>
    public class EmployeeDal : MssqlDalBase, IEmployeeDal
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
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }

            employee.EmployeeId = KeyGenerator.GenNewGuidKey();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"INSERT INTO ");
            query.AppendLine(@"  [Employee] ( ");
            query.AppendLine(@"     [EmployeeId] ");
            query.AppendLine(@"    ,[EmployeeCode] ");
            query.AppendLine(@"    ,[Name] ");
            query.AppendLine(@"    ,[Birthday] ");
            query.AppendLine(@"    ,[Sex] ");
            query.AppendLine(@"    ,[CompanyId] ");
            query.AppendLine(@"    ,[DepartmentId] ");
            query.AppendLine(@"    ,[PositionId] ");
            query.AppendLine(@"    ,[Rand] ");
            query.AppendLine(@"    ,[RVersion] ");
            query.AppendLine(@"    ,[Status] ");
            query.AppendLine(@"    ,[CreaterId] ");
            query.AppendLine(@"    ,[CreateTime] ");
            query.AppendLine(@"    ,[UpdatorId] ");
            query.AppendLine(@"    ,[UpdateTime] ");
            query.AppendLine(@"    ,[StartWorkDate] ");
            query.AppendLine(@"    ,[JoinDate] ");
            query.AppendLine(@"  ) ");
            query.AppendLine(@"VALUES (");
            query.AppendLine(@"     @EmployeeId ");
            query.AppendLine(@"    ,@EmployeeCode ");
            query.AppendLine(@"    ,@Name ");
            query.AppendLine(@"    ,@Birthday ");
            query.AppendLine(@"    ,@Sex ");
            query.AppendLine(@"    ,@CompanyId ");
            query.AppendLine(@"    ,@DepartmentId ");
            query.AppendLine(@"    ,@PositionId ");
            query.AppendLine(@"    ,@Rand ");
            query.AppendLine(@"    ,@RVersion ");
            query.AppendLine(@"    ,@Status ");
            query.AppendLine(@"    ,@CreaterId ");
            query.AppendLine(@"    ,@CreateTime ");
            query.AppendLine(@"    ,@UpdatorId ");
            query.AppendLine(@"    ,@UpdateTime ");
            query.AppendLine(@"    ,@StartWorkDate ");
            query.AppendLine(@"    ,@JoinDate ");
            query.AppendLine(@"); ");

            DBParamCollection<DBParam> paramCollection = new DBParamCollection<DBParam>();
            paramCollection.Add(new DBParam("@EmployeeId", employee.EmployeeId, DbType.String, 40));
            paramCollection.Add(new DBParam("@EmployeeCode", employee.EmployeeCode, DbType.String, 15));
            paramCollection.Add(new DBParam("@Name", employee.Name, DbType.String, 50));
            paramCollection.Add(new DBParam("@Birthday", employee.Birthday, DbType.DateTime));
            paramCollection.Add(new DBParam("@Sex", employee.Sex, DbType.Int32));
            paramCollection.Add(new DBParam("@CompanyId", employee.CompanyId, DbType.String, 40));
            paramCollection.Add(new DBParam("@DepartmentId", employee.DepartmentId, DbType.String, 40));
            paramCollection.Add(new DBParam("@PositionId", employee.PositionId, DbType.String, 40));
            paramCollection.Add(new DBParam("@Rand", employee.Rand, DbType.Int32));
            paramCollection.Add(new DBParam("@RVersion", employee.RVersion, DbType.Int32));
            paramCollection.Add(new DBParam("@Status", employee.Status, DbType.Int32));
            paramCollection.Add(new DBParam("@CreaterId", employee.CreaterId, DbType.String, 40));
            paramCollection.Add(new DBParam("@CreateTime", employee.CreateTime, DbType.DateTime));
            paramCollection.Add(new DBParam("@UpdatorId", employee.UpdatorId, DbType.String, 40));
            paramCollection.Add(new DBParam("@UpdateTime", employee.UpdateTime, DbType.DateTime));
            paramCollection.Add(new DBParam("@StartWorkDate", employee.StartWorkDate, DbType.DateTime));
            paramCollection.Add(new DBParam("@JoinDate", employee.JoinDate, DbType.DateTime));

            try
            {
                int effectCount = 0;

                if (tran != null)
                {
                    DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;
                    effectCount = MssqlHelper.ExecuteNonQuery(dbTran, CommandType.Text, query.ToString(), paramCollection);
                }
                else
                {
                    effectCount = MssqlHelper.ExecuteNonQuery(this.CurrentConnectionString, CommandType.Text, query.ToString(), paramCollection);
                }

                if (effectCount == 0)
                {
                    employee.EmployeeId = string.Empty;
                    throw new ResponseException((int)ResultCode.NoDataInsert, employee.EmployeeCode);
                }
            }
            catch(Exception ex)
            {
                employee.EmployeeId = string.Empty;
                throw new Exception(ex.Message, ex);
            }

            return employee;
        }

        /// <summary>
        /// 批量新建员工
        /// </summary>
        /// <param name="employeeList">员工实体对象集合</param>
        /// <returns>返回处理后的员工实体对象集合</returns>
        public IList<Employee> Add(IList<Employee> employeeList)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();
            IList<Employee> newEmployeeList = new List<Employee>();
            Employee newEmployee = null;
            bool allSucc = true;

            try
            {
                if (employeeList != null)
                {
                    foreach (Employee employee in employeeList)
                    {
                        if (allSucc)
                        {
                            newEmployee = this.Add(employee, tran);
                        }

                        if (string.IsNullOrEmpty(newEmployee.EmployeeId))
                        {
                            allSucc = false;
                        }

                        newEmployeeList.Add(newEmployee);
                    }

                    if (allSucc)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        tran.Rollback();
                    }
                }
            }
            catch(Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message, ex);
            }

            return newEmployeeList;
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
            Employee newEmployee = null;
            bool allSucc = true;

            try
            {
                if (employeeList != null)
                {
                    foreach (Employee employee in employeeList)
                    {
                        if (allSucc)
                        {
                            newEmployee = this.Add(employee, tran);

                            if (string.IsNullOrEmpty(newEmployee.EmployeeId))
                            {
                                allSucc = false;

                                foreach (Employee nEmployee in newEmployeeList)
                                {
                                    nEmployee.EmployeeId = string.Empty;
                                }
                            }
                        }

                        newEmployeeList.Add(newEmployee);
                    }
                }
            }
            catch(Exception ex)
            {
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
            object count = 0;
            long result = 0;
            EmployeeSearcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"SELECT ");
            query.AppendLine(@"   COUNT(E.EmployeeId) ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"   Employee E ");

            if (employeeSearcher != null)
            {
                querySearcher = (EmployeeSearcher)employeeSearcher.Clone();
                querySearcher.TableName = "E";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"   [Company] C ON(C.CompanyId = E.CompanyId) ");
                }

                if (querySearcher.CurrDepartment != null)
                {
                    querySearcher.CurrDepartment.TableName = "D";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"   [Department] D ON(D.DepartmentId = E.DepartmentId) ");
                }

                if (querySearcher.CurrPosition != null)
                {
                    querySearcher.CurrPosition.TableName = "P";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"   [Position] P ON(P.PositionId = E.PositionId) ");
                }
            }

            queryParser.SearcherParse(querySearcher);

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                query.AppendLine(@"WHERE ");
                query.AppendLine(@"   " + queryParser.ConditionString);
            }

            if (tran != null)
            {
                DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;
                count = MssqlHelper.ExecuteScalar(dbTran, CommandType.Text, query.ToString(), queryParser.ParamCollection);
            }
            else
            {
                count = MssqlHelper.ExecuteScalar(this.CurrentConnectionString, CommandType.Text, query.ToString(), queryParser.ParamCollection);
            }

            return long.TryParse(count.ToString(), out result) ? result : 0;
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
            EmployeeSearcher querySearcher = new EmployeeSearcher();
            querySearcher.EmployeeId.Equal(employeeId);
            this.Delete(querySearcher, tran);
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
            int effectCount = 0;
            EmployeeSearcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"DELETE FROM ");
            query.AppendLine(@"   Employee ");
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"   EmployeeId IN (");
            query.AppendLine(@"      SELECT ");
            query.AppendLine(@"         E.EmployeeId ");
            query.AppendLine(@"      FROM ");
            query.AppendLine(@"         Employee E ");

            if (employeeSearcher != null)
            {
                querySearcher = (EmployeeSearcher)employeeSearcher.Clone();
                querySearcher.TableName = "E";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"   [Company] C ON(C.CompanyId = E.CompanyId) ");
                }

                if (querySearcher.CurrDepartment != null)
                {
                    querySearcher.CurrDepartment.TableName = "D";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"   [Department] D ON(D.DepartmentId = E.DepartmentId) ");
                }

                if (querySearcher.CurrPosition != null)
                {
                    querySearcher.CurrPosition.TableName = "P";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"   [Position] P ON(P.PositionId = E.PositionId) ");
                }
            }

            queryParser.SearcherParse(querySearcher);

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                query.AppendLine(@"WHERE ");
                query.AppendLine(@"   " + queryParser.ConditionString);
            }

            query.AppendLine(@"); ");

            try
            {
                if (tran != null)
                {
                    DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;
                    effectCount = MssqlHelper.ExecuteNonQuery(dbTran, CommandType.Text, query.ToString(), queryParser.ParamCollection);
                }
                else
                {
                    effectCount = MssqlHelper.ExecuteNonQuery(this.CurrentConnectionString, CommandType.Text, query.ToString(), queryParser.ParamCollection);
                }
            }
            catch(SqlException sex)
            {
                if (sex.ErrorCode == (int)ResultCode.FKError)
                {
                    throw new ResponseException((int)ResultCode.FKError, "DELETE Position");
                }
            }
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
            EmployeeSearcher employeeSearcher = new EmployeeSearcher();
            employeeSearcher.EmployeeId.Equal(employeeId);
            IList<Employee> employeeList = this.FindList(employeeSearcher);
            return (employeeList == null || employeeList.Count == 0) ? null : employeeList[0];
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
            PageList<Employee> resultList = new PageList<Employee>();
            PageDataTable pageDataTable = this.FindDataTable(employeeSearcher, pager, tran);
            Employee ele = null;

            if (pageDataTable != null)
            {
                resultList = new PageList<Employee>();
                resultList.PageIndex = pageDataTable.PageIndex;
                resultList.TotalCount = pageDataTable.TotalCount;

                if (pageDataTable.RecordList != null && pageDataTable.RecordList.Rows.Count > 0)
                {
                    foreach (DataRow aRow in pageDataTable.RecordList.Rows)
                    {
                        ele = new Employee();

                        if (!(aRow["EmployeeId"] is DBNull))
                        {
                            ele.EmployeeId = aRow["EmployeeId"].ToString();
                        }

                        if (!(aRow["CompanyId"] is DBNull))
                        {
                            ele.CompanyId = aRow["CompanyId"].ToString();
                        }

                        if (!(aRow["DepartmentId"] is DBNull))
                        {
                            ele.DepartmentId = aRow["DepartmentId"].ToString();
                        }

                        if (!(aRow["PositionId"] is DBNull))
                        {
                            ele.PositionId = aRow["PositionId"].ToString();
                        }

                        if (!(aRow["EmployeeCode"] is DBNull))
                        {
                            ele.EmployeeCode = aRow["EmployeeCode"].ToString();
                        }

                        if (!(aRow["CreaterId"] is DBNull))
                        {
                            ele.CreaterId = aRow["CreaterId"].ToString();
                        }

                        if (!(aRow["CreateTime"] is DBNull))
                        {
                            ele.CreateTime = Convert.ToDateTime(aRow["CreateTime"]);
                        }

                        if (!(aRow["Name"] is DBNull))
                        {
                            ele.Name = aRow["Name"].ToString();
                        }

                        if (!(aRow["RVersion"] is DBNull))
                        {
                            ele.RVersion = Convert.ToInt32(aRow["RVersion"]);
                        }

                        if (!(aRow["Status"] is DBNull))
                        {
                            ele.Status = Convert.ToInt32(aRow["Status"]);
                        }

                        if (!(aRow["UpdateTime"] is DBNull))
                        {
                            ele.UpdateTime = Convert.ToDateTime(aRow["UpdateTime"]);
                        }

                        if (!(aRow["UpdatorId"] is DBNull))
                        {
                            ele.UpdatorId = aRow["UpdatorId"].ToString();
                        }

                        if (!(aRow["Birthday"] is DBNull))
                        {
                            ele.Birthday = Convert.ToDateTime(aRow["Birthday"]);
                        }

                        if (!(aRow["JoinDate"] is DBNull))
                        {
                            ele.JoinDate = Convert.ToDateTime(aRow["JoinDate"]);
                        }

                        if (!(aRow["Rand"] is DBNull))
                        {
                            ele.Rand = Convert.ToInt32(aRow["Rand"]);
                        }

                        if (!(aRow["Sex"] is DBNull))
                        {
                            ele.Sex = Convert.ToInt32(aRow["Sex"]);
                        }

                        if (!(aRow["StartWorkDate"] is DBNull))
                        {
                            ele.StartWorkDate = Convert.ToDateTime(aRow["StartWorkDate"]);
                        }

                        resultList.RecordList.Add(ele);
                    }
                }
            }

            return resultList;
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
            EmployeeSearcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            PageDataTable pDataTable = new PageDataTable();
            DataSet resultSet = null;
            StringBuilder query = new StringBuilder();
            StringBuilder joinQuery = new StringBuilder();
            StringBuilder conditionQuery = new StringBuilder();
            StringBuilder sortQuery = new StringBuilder();

            if (employeeSearcher != null)
            {
                querySearcher = (EmployeeSearcher)employeeSearcher.Clone();
                querySearcher.TableName = "E";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    joinQuery.AppendLine(@"LEFT JOIN ");
                    joinQuery.AppendLine(@"   [Company] C ON(C.CompanyId = E.CompanyId) ");
                }

                if (querySearcher.CurrDepartment != null)
                {
                    querySearcher.CurrDepartment.TableName = "D";
                    joinQuery.AppendLine(@"LEFT JOIN ");
                    joinQuery.AppendLine(@"   [Department] D ON(D.DepartmentId = E.DepartmentId) ");
                }

                if (querySearcher.CurrPosition != null)
                {
                    querySearcher.CurrPosition.TableName = "P";
                    joinQuery.AppendLine(@"LEFT JOIN ");
                    joinQuery.AppendLine(@"   [Position] P ON(P.PositionId = E.PositionId) ");
                }
            }

            queryParser.SearcherParse(querySearcher);

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                conditionQuery.AppendLine(@"WHERE ");
                conditionQuery.AppendLine(@"   " + queryParser.ConditionString);
            }

            if (!string.IsNullOrEmpty(queryParser.SortString))
            {
                sortQuery.AppendLine(@"ORDER BY ");
                sortQuery.AppendLine(@"   " + queryParser.SortString);
            }

            query.AppendLine(@"SELECT ");
            query.AppendLine(@"   E.[EmployeeId] ");
            query.AppendLine(@"  ,E.[EmployeeCode] ");
            query.AppendLine(@"  ,E.[Name] ");
            query.AppendLine(@"  ,E.[Birthday] ");
            query.AppendLine(@"  ,E.[Sex] ");
            query.AppendLine(@"  ,E.[CompanyId] ");
            query.AppendLine(@"  ,E.[DepartmentId] ");
            query.AppendLine(@"  ,E.[PositionId] ");
            query.AppendLine(@"  ,E.[Rand] ");
            query.AppendLine(@"  ,E.[RVersion] ");
            query.AppendLine(@"  ,E.[Status] ");
            query.AppendLine(@"  ,E.[CreaterId] ");
            query.AppendLine(@"  ,E.[CreateTime] ");
            query.AppendLine(@"  ,E.[UpdatorId] ");
            query.AppendLine(@"  ,E.[UpdateTime] ");
            query.AppendLine(@"  ,E.[StartWorkDate] ");
            query.AppendLine(@"  ,E.[JoinDate] ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"  [Employee] E ");
            query.AppendLine(joinQuery.ToString());
            query.AppendLine(conditionQuery.ToString());
            query.AppendLine(sortQuery.ToString());
            query.AppendLine(@"; ");

            if (tran != null)
            {
                DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;

                if (pager != null)
                {
                    resultSet = MssqlHelper.ExecuteDataSet(dbTran, CommandType.Text, query.ToString(), pager, "Employee", queryParser.ParamCollection);
                }
                else
                {
                    resultSet = MssqlHelper.ExecuteDataSet(dbTran, CommandType.Text, query.ToString(), queryParser.ParamCollection);
                }
            }
            else
            {
                if (pager != null)
                {
                    resultSet = MssqlHelper.ExecuteDataSet(this.CurrentConnectionString, CommandType.Text, query.ToString(), pager, "Employee", queryParser.ParamCollection);
                }
                else
                {
                    resultSet = MssqlHelper.ExecuteDataSet(this.CurrentConnectionString, CommandType.Text, query.ToString(), queryParser.ParamCollection);
                }
            }

            if (resultSet != null)
            {
                if (pager != null)
                {
                    pDataTable.PageIndex = pager.CurrentPage;
                }

                pDataTable.TotalCount = this.Count(employeeSearcher, tran);
                pDataTable.RecordList = resultSet.Tables[0];
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
            Employee oldEmployee = this.FindSingle(employee.EmployeeId, tran);
            int updateColCount = 0;

            if (employee == null)
            {
                throw new ArgumentException("employee");
            }

            if (oldEmployee == null)
            {
                throw new ResponseException((int)ResultCode.NoDataExists, employee.EmployeeCode);
            }

            if (employee.RVersion != oldEmployee.RVersion)
            {
                throw new ResponseException((int)ResultCode.VersionChanged, oldEmployee.RVersion.ToString());
            }

            StringBuilder query = new StringBuilder();
            query.AppendLine(@"UPDATE ");
            query.AppendLine(@"   [Employee]");
            query.AppendLine(@"SET ");
            query.AppendLine(@"   [EmployeeId] = @EmployeeId ");
            
            if ((!string.IsNullOrEmpty(employee.EmployeeCode) && !employee.EmployeeCode.Equals(oldEmployee.EmployeeCode))
                || (!string.IsNullOrEmpty(oldEmployee.EmployeeCode) && !oldEmployee.EmployeeCode.Equals(employee.EmployeeCode)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[EmployeeCode] = @EmployeeCode ");
            }

            if ((!string.IsNullOrEmpty(employee.Name) && !employee.Name.Equals(oldEmployee.Name))
                || (!string.IsNullOrEmpty(oldEmployee.Name) && !oldEmployee.Name.Equals(employee.Name)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[Name] = @Name ");
            }

            if (((DateTime)oldEmployee.Birthday).CompareTo(employee.Birthday) != 0 && employee.Birthday != DateTime.MinValue)
            {
                updateColCount++;
                query.AppendLine(@"  ,[Birthday] = @Birthday ");
            }

            if (employee.Sex != oldEmployee.Sex)
            {
                updateColCount++;
                query.AppendLine(@"  ,[Sex] = @Sex ");
            }

            if ((!string.IsNullOrEmpty(employee.CompanyId) && !employee.CompanyId.Equals(oldEmployee.CompanyId))
                || (!string.IsNullOrEmpty(oldEmployee.CompanyId) && !oldEmployee.CompanyId.Equals(employee.CompanyId)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[CompanyId] = @CompanyId ");
            }

            if ((!string.IsNullOrEmpty(employee.DepartmentId) && !employee.DepartmentId.Equals(oldEmployee.DepartmentId))
                || (!string.IsNullOrEmpty(oldEmployee.DepartmentId) && !oldEmployee.DepartmentId.Equals(employee.DepartmentId)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[DepartmentId] = @DepartmentId ");
            }

            if ((!string.IsNullOrEmpty(employee.PositionId) && !employee.PositionId.Equals(oldEmployee.PositionId))
                || (!string.IsNullOrEmpty(oldEmployee.PositionId) && !oldEmployee.PositionId.Equals(employee.PositionId)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[PositionId] = @PositionId ");
            }

            if (employee.Rand != oldEmployee.Rand)
            {
                updateColCount++;
                query.AppendLine(@"  ,[Rand] = @Rand ");
            }

            if (employee.Status != oldEmployee.Status)
            {
                updateColCount++;
                query.AppendLine(@"  ,[Status] = @Status ");
            }

            if ((!string.IsNullOrEmpty(employee.CreaterId) && !employee.CreaterId.Equals(oldEmployee.CreaterId))
                || (!string.IsNullOrEmpty(oldEmployee.CreaterId) && !oldEmployee.CreaterId.Equals(employee.CreaterId)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[CreaterId] = @CreaterId ");
            }

            if (oldEmployee.CreateTime.CompareTo(employee.CreateTime) != 0 && employee.CreateTime != DateTime.MinValue)
            {
                updateColCount++;
                query.AppendLine(@"  ,[CreateTime] = @CreateTime ");
            }

            if ((!string.IsNullOrEmpty(employee.UpdatorId) && !employee.UpdatorId.Equals(oldEmployee.UpdatorId))
                || (!string.IsNullOrEmpty(oldEmployee.UpdatorId) && !oldEmployee.UpdatorId.Equals(employee.UpdatorId)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[UpdatorId] = @UpdatorId ");
            }

            if (oldEmployee.UpdateTime.CompareTo(employee.UpdateTime) != 0 && employee.UpdateTime != DateTime.MinValue)
            {
                updateColCount++;
                query.AppendLine(@"  ,[UpdateTime] = @UpdateTime ");
            }

            if (((DateTime)oldEmployee.StartWorkDate).CompareTo(employee.StartWorkDate) != 0 && employee.StartWorkDate != DateTime.MinValue)
            {
                updateColCount++;
                query.AppendLine(@"  ,[StartWorkDate] = @StartWorkDate ");
            }

            if (((DateTime)oldEmployee.JoinDate).CompareTo(employee.JoinDate) != 0 && employee.JoinDate != DateTime.MinValue)
            {
                updateColCount++;
                query.AppendLine(@"  ,[JoinDate] = @JoinDate ");
            }

            query.AppendLine(@"  ,[RVersion] = @RVersion ");
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"   [EmployeeId] = @EmployeeId ");

            if (updateColCount == 0)
            {
                return;
            }

            employee.UpdateTime = DateTime.Now;
            DBParamCollection<DBParam> paramCollection = new DBParamCollection<DBParam>();
            paramCollection.Add(new DBParam("@EmployeeId", employee.EmployeeId, DbType.String, 40));
            paramCollection.Add(new DBParam("@EmployeeCode", employee.EmployeeCode, DbType.String, 15));
            paramCollection.Add(new DBParam("@Name", employee.Name, DbType.String, 50));
            paramCollection.Add(new DBParam("@Birthday", employee.Birthday, DbType.DateTime));
            paramCollection.Add(new DBParam("@Sex", employee.Sex, DbType.Int32));
            paramCollection.Add(new DBParam("@CompanyId", employee.CompanyId, DbType.String, 40));
            paramCollection.Add(new DBParam("@DepartmentId", employee.DepartmentId, DbType.String, 40));
            paramCollection.Add(new DBParam("@PositionId", employee.PositionId, DbType.String, 40));
            paramCollection.Add(new DBParam("@Rand", employee.Rand, DbType.Int32));
            paramCollection.Add(new DBParam("@RVersion", employee.RVersion, DbType.Int32));
            paramCollection.Add(new DBParam("@Status", employee.Status, DbType.Int32));
            paramCollection.Add(new DBParam("@CreaterId", employee.CreaterId, DbType.String, 40));
            paramCollection.Add(new DBParam("@CreateTime", employee.CreateTime, DbType.DateTime));
            paramCollection.Add(new DBParam("@UpdatorId", employee.UpdatorId, DbType.String, 40));
            paramCollection.Add(new DBParam("@UpdateTime", employee.UpdateTime, DbType.DateTime));
            paramCollection.Add(new DBParam("@StartWorkDate", employee.StartWorkDate, DbType.DateTime));
            paramCollection.Add(new DBParam("@JoinDate", employee.JoinDate, DbType.DateTime));

            try
            {
                int effectCount = 0;

                if (employee != null)
                {
                    if (tran != null)
                    {
                        DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;
                        effectCount = MssqlHelper.ExecuteNonQuery(dbTran, CommandType.Text, query.ToString(), paramCollection);
                    }
                    else
                    {
                        effectCount = MssqlHelper.ExecuteNonQuery(this.CurrentConnectionString, CommandType.Text, query.ToString(), paramCollection);
                    }
                }

                // 抛出一个异常
                if (effectCount == 0)
                {
                    throw new ResponseException((int)ResultCode.NoDataUpdate, employee.EmployeeCode);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employeeList">员工实体对象</param>
        public void Update(IList<Employee> employeeList)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                if (employeeList != null)
                {
                    foreach (Employee employee in employeeList)
                    {
                        this.Update(employee, tran);
                    }

                    tran.Commit();
                }
            }
            catch(Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="employeeList">员工实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Update(IList<Employee> employeeList, ICTransaction tran)
        {
            try
            {
                if (employeeList != null)
                {
                    foreach (Employee employee in employeeList)
                    {
                        this.Update(employee, tran);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dalFactory">传入当前指定的DalFactory对象</param>
        public EmployeeDal(MssqlDalFactoryBase dalFactory)
            : base(dalFactory)
        {

        }

        #endregion
    }
}
