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
    /// 使用MSSQL原生SQL语句的部门数据操作类
    /// </summary>
    public class DepartmentDal : MssqlDalBase, IDepartmentDal
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
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的部门实体对象</returns>
        public Department Add(Department department, ICTransaction tran)
        {
            if (department == null)
            {
                throw new ArgumentNullException("department");
            }

            department.DepartmentId = KeyGenerator.GenNewGuidKey();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"INSERT INTO ");
            query.AppendLine(@"  [Department] ( ");
            query.AppendLine(@"     [DepartmentId] ");
            query.AppendLine(@"    ,[DepartmentCode] ");
            query.AppendLine(@"    ,[CompanyId]  ");
            query.AppendLine(@"    ,[Name] ");
            query.AppendLine(@"    ,[RVersion] ");
            query.AppendLine(@"    ,[Status] ");
            query.AppendLine(@"    ,[CreaterId] ");
            query.AppendLine(@"    ,[CreateTime] ");
            query.AppendLine(@"    ,[UpdatorId] ");
            query.AppendLine(@"    ,[UpdateTime] ");
            query.AppendLine(@"   )");
            query.AppendLine(@"VALUES (");
            query.AppendLine(@"     @DepartmentId ");
            query.AppendLine(@"    ,@DepartmentCode ");
            query.AppendLine(@"    ,@CompanyId ");
            query.AppendLine(@"    ,@Name ");
            query.AppendLine(@"    ,@RVersion ");
            query.AppendLine(@"    ,@Status ");
            query.AppendLine(@"    ,@CreaterId ");
            query.AppendLine(@"    ,@CreateTime ");
            query.AppendLine(@"    ,@UpdatorId ");
            query.AppendLine(@"    ,@UpdateTime ");
            query.AppendLine(@"); ");

            DBParamCollection<DBParam> paramCollection = new DBParamCollection<DBParam>();
            paramCollection.Add(new DBParam("@DepartmentId", department.DepartmentId, DbType.String, 40));
            paramCollection.Add(new DBParam("@DepartmentCode", department.DepartmentCode, DbType.String, 10));
            paramCollection.Add(new DBParam("@CompanyId", department.CompanyId, DbType.String, 40));
            paramCollection.Add(new DBParam("@Name", department.Name, DbType.String, 100));
            paramCollection.Add(new DBParam("@RVersion", department.RVersion, DbType.Int32));
            paramCollection.Add(new DBParam("@Status", department.Status, DbType.Int32));
            paramCollection.Add(new DBParam("@CreaterId", department.CreaterId, DbType.String, 40));
            paramCollection.Add(new DBParam("@CreateTime", department.CreateTime, DbType.DateTime));
            paramCollection.Add(new DBParam("@UpdatorId", department.UpdatorId, DbType.String, 40));
            paramCollection.Add(new DBParam("@UpdateTime", department.UpdateTime, DbType.DateTime));

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
                    department.DepartmentId = string.Empty;
                    throw new ResponseException((int)ResultCode.NoDataInsert, department.DepartmentCode);
                }
            }
            catch(Exception ex)
            {
                department.DepartmentId = string.Empty;
                throw new Exception(ex.Message, ex);
            }

            return department;
        }

        /// <summary>
        /// 批量新建部门
        /// </summary>
        /// <param name="departmentList">部门实体对象集合</param>
        /// <returns>返回处理后的部门实体对象集合</returns>
        public IList<Department> Add(IList<Department> departmentList)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();
            IList<Department> newDepartmentList = new List<Department>();
            Department newDepartment = null;
            bool allSucc = true;

            try
            {
                if (departmentList != null)
                {
                    foreach (Department department in departmentList)
                    {
                        if (allSucc)
                        {
                            newDepartment = this.Add(department, tran);
                        }

                        if (string.IsNullOrEmpty(newDepartment.DepartmentId))
                        {
                            allSucc = false;
                        }

                        newDepartmentList.Add(newDepartment);
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

            return newDepartmentList;
        }

        /// <summary>
        /// 批量新建部门
        /// </summary>
        /// <param name="departmentList">部门实体对象集合</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的部门实体对象集合</returns>
        public IList<Department> Add(IList<Department> departmentList, ICTransaction tran)
        {
            IList<Department> newDepartmentList = new List<Department>();
            Department newDepartment = null;
            bool allSucc = true;

            try
            {
                if (departmentList != null)
                {
                    foreach (Department department in departmentList)
                    {
                        if (allSucc)
                        {
                            newDepartment = this.Add(department, tran);

                            if (string.IsNullOrEmpty(newDepartment.DepartmentId))
                            {
                                allSucc = false;

                                foreach (Department nDepartment in newDepartmentList)
                                {
                                    nDepartment.DepartmentId = string.Empty;
                                }
                            }
                        }

                        newDepartmentList.Add(newDepartment);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
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
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count(DepartmentSearcher departmentSearcher, ICTransaction tran)
        {
            object count = 0;
            long result = 0;
            DepartmentSearcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"SELECT ");
            query.AppendLine(@"   COUNT(D.DepartmentId) ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"   Department D ");

            if (departmentSearcher != null)
            {
                querySearcher = (DepartmentSearcher)departmentSearcher.Clone();
                querySearcher.TableName = "D";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"   [Company] C ON(C.CompanyId = D.CompanyId) ");
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
            DepartmentSearcher querySearcher = new DepartmentSearcher();
            querySearcher.DepartmentId.Equal(departmentId);
            this.Delete(querySearcher, tran);
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
            int effectCount = 0;
            DepartmentSearcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"DELETE FROM ");
            query.AppendLine(@"   Department ");
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"   DepartmentId IN (");
            query.AppendLine(@"      SELECT ");
            query.AppendLine(@"         D.DepartmentId ");
            query.AppendLine(@"      FROM ");
            query.AppendLine(@"         Department D ");

            if (departmentSearcher != null)
            {
                querySearcher = (DepartmentSearcher)departmentSearcher.Clone();
                querySearcher.TableName = "D";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"   Company C ON(C.CompanyId = D.CompanyId) ");
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
                    throw new ResponseException((int)ResultCode.FKError, "DELETE Department");
                }
            }
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
            DepartmentSearcher departmentSearcher = new DepartmentSearcher();
            departmentSearcher.DepartmentId.Equal(departmentId);
            IList<Department> departmentList = this.FindList(departmentSearcher);
            return (departmentList == null || departmentList.Count == 0) ? null : departmentList[0];
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
            PageList<Department> resultList = new PageList<Department>();
            PageDataTable pageDataTable = this.FindDataTable(departmentSearcher, pager, tran);
            Department ele = null;

            if (pageDataTable != null)
            {
                resultList = new PageList<Department>();
                resultList.PageIndex = pageDataTable.PageIndex;
                resultList.TotalCount = pageDataTable.TotalCount;

                if (pageDataTable.RecordList != null && pageDataTable.RecordList.Rows.Count > 0)
                {
                    foreach (DataRow aRow in pageDataTable.RecordList.Rows)
                    {
                        ele = new Department();

                        if (!(aRow["DepartmentId"] is DBNull))
                        {
                            ele.DepartmentId = aRow["DepartmentId"].ToString();
                        }

                        if (!(aRow["CompanyId"] is DBNull))
                        {
                            ele.CompanyId = aRow["CompanyId"].ToString();
                        }

                        if (!(aRow["DepartmentCode"] is DBNull))
                        {
                            ele.DepartmentCode = aRow["DepartmentCode"].ToString();
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

                        resultList.RecordList.Add(ele);
                    }
                }
            }

            return resultList;
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
            DepartmentSearcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            PageDataTable pDataTable = new PageDataTable();
            DataSet resultSet = null;
            StringBuilder query = new StringBuilder();
            StringBuilder joinQuery = new StringBuilder();
            StringBuilder conditionQuery = new StringBuilder();
            StringBuilder sortQuery = new StringBuilder();

            if (departmentSearcher != null)
            {
                querySearcher = (DepartmentSearcher)departmentSearcher.Clone();
                querySearcher.TableName = "D";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    joinQuery.AppendLine(@"LEFT JOIN ");
                    joinQuery.AppendLine(@"   [Company] C ON(C.CompanyId = D.CompanyId) ");
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
            query.AppendLine(@"   D.[DepartmentId] ");
            query.AppendLine(@"  ,D.[DepartmentCode] ");
            query.AppendLine(@"  ,D.[CompanyId] ");
            query.AppendLine(@"  ,D.[Name] ");
            query.AppendLine(@"  ,D.[RVersion] ");
            query.AppendLine(@"  ,D.[Status] ");
            query.AppendLine(@"  ,D.[CreaterId] ");
            query.AppendLine(@"  ,D.[CreateTime] ");
            query.AppendLine(@"  ,D.[UpdatorId] ");
            query.AppendLine(@"  ,D.[UpdateTime] ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"   [Department] D ");
            query.AppendLine(joinQuery.ToString());
            query.AppendLine(conditionQuery.ToString());
            query.AppendLine(sortQuery.ToString());
            query.AppendLine(@"; ");

            if (tran != null)
            {
                DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;

                if (pager != null)
                {
                    resultSet = MssqlHelper.ExecuteDataSet(dbTran, CommandType.Text, query.ToString(), pager, "Department", queryParser.ParamCollection);
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
                    resultSet = MssqlHelper.ExecuteDataSet(this.CurrentConnectionString, CommandType.Text, query.ToString(), pager, "Department", queryParser.ParamCollection);
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

                pDataTable.TotalCount = this.Count(departmentSearcher, tran);
                pDataTable.RecordList = resultSet.Tables[0];
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
            Department oldDepartment = this.FindSingle(department.DepartmentId, tran);
            int updateColCount = 0;

            if (department == null)
            {
                throw new ArgumentException("department");
            }

            if (oldDepartment == null)
            {
                throw new ResponseException((int)ResultCode.NoDataExists, department.DepartmentCode);
            }

            if (department.RVersion != oldDepartment.RVersion)
            {
                throw new ResponseException((int)ResultCode.VersionChanged, oldDepartment.RVersion.ToString());
            }

            StringBuilder query = new StringBuilder();
            query.AppendLine(@"UPDATE ");
            query.AppendLine(@"   [Department]");
            query.AppendLine(@"SET ");
            query.AppendLine(@"   [DepartmentId] = @DepartmentId ");

            if ((!string.IsNullOrEmpty(department.DepartmentCode) && !department.DepartmentCode.Equals(oldDepartment.DepartmentCode))
                || (!string.IsNullOrEmpty(oldDepartment.DepartmentCode) && !oldDepartment.DepartmentCode.Equals(department.DepartmentCode)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[DepartmentCode] = @DepartmentCode ");
            }

            if ((!string.IsNullOrEmpty(department.CompanyId) && !department.CompanyId.Equals(oldDepartment.CompanyId))
                || (!string.IsNullOrEmpty(oldDepartment.CompanyId) && !oldDepartment.CompanyId.Equals(department.CompanyId)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[CompanyId] = @CompanyId ");
            }

            if ((!string.IsNullOrEmpty(department.Name) && !department.Name.Equals(oldDepartment.Name))
                || (!string.IsNullOrEmpty(oldDepartment.Name) && !oldDepartment.Name.Equals(department.Name)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[Name] = @Name ");
            }

            if (oldDepartment.Status != department.Status)
            {
                updateColCount++;
                query.AppendLine(@"  ,[Status] = @Status ");
            }

            if ((!string.IsNullOrEmpty(department.CreaterId) && !department.CreaterId.Equals(oldDepartment.CreaterId))
                || (!string.IsNullOrEmpty(oldDepartment.CreaterId) && !oldDepartment.CreaterId.Equals(department.CreaterId)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[CreaterId] = @CreaterId ");
            }

            if (oldDepartment.CreateTime.CompareTo(department.CreateTime) != 0 && department.CreateTime != DateTime.MinValue)
            {
                updateColCount++;
                query.AppendLine(@"  ,[CreateTime] = @CreateTime ");
            }

            if ((!string.IsNullOrEmpty(department.UpdatorId) && !department.UpdatorId.Equals(oldDepartment.UpdatorId))
                || (!string.IsNullOrEmpty(oldDepartment.UpdatorId) && !oldDepartment.UpdatorId.Equals(department.UpdatorId)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[UpdatorId] = @UpdatorId ");
            }

            if (oldDepartment.UpdateTime.CompareTo(department.UpdateTime) != 0 && department.UpdateTime != DateTime.MinValue)
            {
                updateColCount++;
                query.AppendLine(@"  ,[UpdateTime] = @UpdateTime ");
            }

            query.AppendLine(@"  ,[RVersion] = @RVersion ");
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"   [DepartmentId] = @DepartmentId ");

            if (updateColCount == 0)
            {
                return;
            }

            department.UpdateTime = DateTime.Now;
            DBParamCollection<DBParam> paramCollection = new DBParamCollection<DBParam>();
            paramCollection.Add(new DBParam("@DepartmentId", department.DepartmentId, DbType.String, 40));
            paramCollection.Add(new DBParam("@DepartmentCode", department.DepartmentCode, DbType.String, 10));
            paramCollection.Add(new DBParam("@CompanyId", department.CompanyId, DbType.String, 40));
            paramCollection.Add(new DBParam("@Name", department.Name, DbType.String, 100));
            paramCollection.Add(new DBParam("@RVersion", department.RVersion, DbType.Int32));
            paramCollection.Add(new DBParam("@Status", department.Status, DbType.Int32));
            paramCollection.Add(new DBParam("@CreaterId", department.CreaterId, DbType.String, 40));
            paramCollection.Add(new DBParam("@CreateTime", department.CreateTime, DbType.DateTime));
            paramCollection.Add(new DBParam("@UpdatorId", department.UpdatorId, DbType.String, 40));
            paramCollection.Add(new DBParam("@UpdateTime", department.UpdateTime, DbType.DateTime));

            try
            {
                int effectCount = 0;

                if (department != null)
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
                    throw new ResponseException((int)ResultCode.NoDataUpdate, department.DepartmentCode);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="departmentList">部门实体对象</param>
        public void Update(IList<Department> departmentList)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                if (departmentList != null)
                {
                    foreach (Department department in departmentList)
                    {
                        this.Update(department, tran);
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
        /// 更新部门数据
        /// </summary>
        /// <param name="departmentList">部门实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Update(IList<Department> departmentList, ICTransaction tran)
        {
            try
            {
                if (departmentList != null)
                {
                    foreach (Department department in departmentList)
                    {
                        this.Update(department, tran);
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
        public DepartmentDal(MssqlDalFactoryBase dalFactory)
            : base(dalFactory)
        {

        }

        #endregion
    }
}
