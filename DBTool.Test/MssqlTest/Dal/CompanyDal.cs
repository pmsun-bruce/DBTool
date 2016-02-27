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
    /// 使用MSSQL原生SQL语句的公司数据操作类
    /// </summary>
    public class CompanyDal : MssqlDalBase,ICompanyDal
    {
        #region Public Methods

        /// <summary>
        /// 新建公司
        /// </summary>
        /// <param name="company">公司实体对象</param>
        /// <returns>返回处理后的公司实体对象</returns>
        public Company Add(Company company)
        {
            return this.Add(company, null);
        }

        /// <summary>
        /// 新建公司
        /// </summary>
        /// <param name="company">公司实体对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的公司实体对象</returns>
        public Company Add(Company company, ICTransaction tran)
        {
            if (company == null)
            {
                throw new ArgumentNullException("company");
            }

            company.CompanyId = KeyGenerator.GenNewGuidKey();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"INSERT INTO ");
            query.AppendLine(@"  [Company] ( ");
            query.AppendLine(@"     [CompanyId] ");
            query.AppendLine(@"    ,[CompanyCode] ");
            query.AppendLine(@"    ,[Name] ");
            query.AppendLine(@"    ,[Address] ");
            query.AppendLine(@"    ,[ParentCompanyId] ");
            query.AppendLine(@"    ,[RVersion] ");
            query.AppendLine(@"    ,[Status] ");
            query.AppendLine(@"    ,[CreaterId] ");
            query.AppendLine(@"    ,[CreateTime] ");
            query.AppendLine(@"    ,[UpdatorId] ");
            query.AppendLine(@"    ,[UpdateTime] ");
            query.AppendLine(@"  ) ");
            query.AppendLine(@"VALUES ( ");
            query.AppendLine(@"     @CompanyId ");
            query.AppendLine(@"    ,@CompanyCode ");
            query.AppendLine(@"    ,@Name ");
            query.AppendLine(@"    ,@Address ");
            query.AppendLine(@"    ,@ParentCompanyId ");
            query.AppendLine(@"    ,@RVersion ");
            query.AppendLine(@"    ,@Status ");
            query.AppendLine(@"    ,@CreaterId ");
            query.AppendLine(@"    ,@CreateTime ");
            query.AppendLine(@"    ,@UpdatorId ");
            query.AppendLine(@"    ,@UpdateTime ");
            query.AppendLine(@"); ");

            DBParamCollection paramCollection = new DBParamCollection();
            paramCollection.Add(new DBParam("@CompanyId", company.CompanyId, DbType.String, 40));
            paramCollection.Add(new DBParam("@CompanyCode", company.CompanyCode, DbType.String, 5));
            paramCollection.Add(new DBParam("@Name", company.Name, DbType.String, 200));
            paramCollection.Add(new DBParam("@Address", company.Address, DbType.String, 500));
            paramCollection.Add(new DBParam("@ParentCompanyId", company.ParentCompanyId, DbType.String, 40));
            paramCollection.Add(new DBParam("@RVersion", company.RVersion, DbType.Int32));
            paramCollection.Add(new DBParam("@Status", company.Status, DbType.Int32));
            paramCollection.Add(new DBParam("@CreaterId", company.CreaterId, DbType.String, 40));
            paramCollection.Add(new DBParam("@CreateTime", company.CreateTime, DbType.DateTime));
            paramCollection.Add(new DBParam("@UpdatorId", company.UpdatorId, DbType.String, 40));
            paramCollection.Add(new DBParam("@UpdateTime", company.UpdateTime, DbType.DateTime));

            try
            {
                int effectCount = 0;

                if (company != null)
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

                if (effectCount == 0)
                {
                    company.CompanyId = string.Empty;
                    throw new ResponseException((int)ResultCode.NoDataInsert, company.CompanyCode);
                }
            }
            catch
            {
                company.CompanyId = string.Empty;
                throw;
            }

            return company;
        }

        /// <summary>
        /// 批量新建公司
        /// </summary>
        /// <param name="companyList">公司实体对象集合</param>
        /// <returns>返回处理后的公司实体对象集合</returns>
        public IList<Company> Add(IList<Company> companyList)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();
            IList<Company> newCompanyList = new List<Company>();
            Company newCompany = null;
            bool allSucc = true;

            try
            {
                if (companyList != null)
                {
                    foreach (Company company in companyList)
                    {
                        if (allSucc)
                        {
                            newCompany = this.Add(company, tran);
                        }

                        if (string.IsNullOrEmpty(newCompany.CompanyId))
                        {
                            allSucc = false;
                        }

                        newCompanyList.Add(newCompany);
                    }

                    if (allSucc)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        tran.RollBack();
                    }
                }
            }
            catch
            {
                tran.RollBack();
                throw;
            }

            return newCompanyList;
        }

        /// <summary>
        /// 批量新建公司
        /// </summary>
        /// <param name="companyList">公司实体对象集合</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的公司实体对象集合</returns>
        public IList<Company> Add(IList<Company> companyList, ICTransaction tran)
        {
            IList<Company> newCompanyList = new List<Company>();
            Company newCompany = null; 
            bool allSucc = true;
            
            try
            {
                if (companyList != null)
                {
                    foreach (Company company in companyList)
                    {
                        if (allSucc)
                        {
                            newCompany = this.Add(company, tran);

                            if (string.IsNullOrEmpty(newCompany.CompanyId))
                            {
                                allSucc = false;

                                foreach (Company nCompany in newCompanyList)
                                {
                                    nCompany.CompanyId = string.Empty;
                                }
                            }
                        }

                        newCompanyList.Add(newCompany);
                    }
                }
            }
            catch
            {
                throw;
            }

            return newCompanyList;
        }

        /// <summary>
        /// 查询公司数量
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count(CompanySearcher companySearcher)
        {
            return this.Count(companySearcher, null);
        }

        /// <summary>
        /// 查询公司数量
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count(CompanySearcher companySearcher, ICTransaction tran)
        {
            object count = 0;
            long result = 0;
            CompanySearcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"SELECT ");
            query.AppendLine(@"   COUNT(C.CompanyId) ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"   Company C ");

            if (companySearcher != null)
            {
                querySearcher = (CompanySearcher)companySearcher.Clone();
                querySearcher.TableName = "C";
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
        /// 删除公司
        /// </summary>
        /// <param name="companyId">公司ID</param>
        public void Delete(string companyId)
        {
            this.Delete(companyId, null);
        }

        /// <summary>
        /// 删除公司
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="tran">中间事务对象</param>
        public void Delete(string companyId, ICTransaction tran)
        {
            CompanySearcher querySearcher = new CompanySearcher();
            querySearcher.CompanyId.AddCondition(ConditionFactory.Equal(companyId));
            this.Delete(querySearcher, tran);
        }

        /// <summary>
        /// 根据指定条件删除公司
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        public void Delete(CompanySearcher companySearcher)
        {
            this.Delete(companySearcher, null);
        }

        /// <summary>
        /// 根据指定条件删除公司
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Delete(CompanySearcher companySearcher, ICTransaction tran)
        {
            int effectCount = 0;
            CompanySearcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            StringBuilder query = new StringBuilder();

            query.AppendLine(@"DELETE FROM ");
            query.AppendLine(@"   Company ");

            if (companySearcher != null)
            {
                querySearcher = (CompanySearcher)companySearcher.Clone();
                querySearcher.TableName = "C";
            }

            queryParser.SearcherParse(querySearcher);

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                query.AppendLine(@"WHERE ");
                query.AppendLine(@"   " + queryParser.ConditionString);
            }

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
                    throw new ResponseException((int)ResultCode.FKError, "DELETE Company");
                }
            }
        }

        /// <summary>
        /// 查找指定ID的公司
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns>返回公司实体对象</returns>
        public Company FindSingle(string companyId)
        {
            return this.FindSingle(companyId, null);
        }

        /// <summary>
        /// 查找指定ID的公司
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回公司实体对象</returns>
        public Company FindSingle(string companyId, ICTransaction tran)
        {
            CompanySearcher companySearcher = new CompanySearcher();
            companySearcher.CompanyId.AddCondition(ConditionFactory.Equal(companyId));
            IList<Company> companyList = this.FindList(companySearcher);
            return (companyList == null || companyList.Count == 0) ? null : companyList[0];
        }

        /// <summary>
        /// 查找指定条件的公司集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <returns>返回公司实体对象集合</returns>
        public IList<Company> FindList(CompanySearcher companySearcher)
        {
            PageList<Company> pageList = this.FindList(companySearcher, null, null);
            return pageList == null ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查找指定条件的公司集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回公司实体对象集合</returns>
        public IList<Company> FindList(CompanySearcher companySearcher, ICTransaction tran)
        {
            PageList<Company> pageList = this.FindList(companySearcher, null, tran);
            return pageList == null ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查找指定条件的公司分页集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public PageList<Company> FindList(CompanySearcher companySearcher, Pager pager)
        {
            return this.FindList(companySearcher, pager, null);
        }

        /// <summary>
        /// 查找指定条件的公司分页集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public PageList<Company> FindList(CompanySearcher companySearcher, Pager pager, ICTransaction tran)
        {
            PageList<Company> resultList = new PageList<Company>();
            PageDataTable pageDataTable = this.FindDataTable(companySearcher, pager, tran);
            Company ele = null;

            if (pageDataTable != null)
            {
                resultList = new PageList<Company>();
                resultList.PageIndex = pageDataTable.PageIndex;
                resultList.TotalCount = pageDataTable.TotalCount;

                if (pageDataTable.RecordList != null && pageDataTable.RecordList.Rows.Count > 0)
                {
                    foreach (DataRow aRow in pageDataTable.RecordList.Rows)
                    {
                        ele = new Company();

                        if (!(aRow["CompanyId"] is DBNull))
                        {
                            ele.CompanyId = aRow["CompanyId"].ToString();
                        }

                        if (!(aRow["Address"] is DBNull))
                        {
                            ele.Address = aRow["Address"].ToString();
                        }

                        if (!(aRow["CompanyCode"] is DBNull))
                        {
                            ele.CompanyCode = aRow["CompanyCode"].ToString();
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

                        if (!(aRow["ParentCompanyId"] is DBNull))
                        {
                            ele.ParentCompanyId = aRow["ParentCompanyId"].ToString();
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
        /// 根据指定条件查找公司集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public DataTable FindDataTable(CompanySearcher companySearcher)
        {
            PageDataTable pageDataTable = this.FindDataTable(companySearcher, null, null);
            return pageDataTable == null ? null : pageDataTable.RecordList;
        }

        /// <summary>
        /// 根据指定条件查找公司集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public DataTable FindDataTable(CompanySearcher companySearcher, ICTransaction tran)
        {
            PageDataTable pageDataTable = this.FindDataTable(companySearcher, null, tran);
            return pageDataTable == null ? null : pageDataTable.RecordList;
        }

        /// <summary>
        /// 根据指定条件查找公司分页集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public PageDataTable FindDataTable(CompanySearcher companySearcher, Pager pager)
        {
            return this.FindDataTable(companySearcher, pager, null);
        }

        /// <summary>
        /// 根据指定条件查找公司分页集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public PageDataTable FindDataTable(CompanySearcher companySearcher, Pager pager, ICTransaction tran)
        {
            CompanySearcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            PageDataTable pDataTable = new PageDataTable();
            DataSet resultSet = null;
            StringBuilder query = new StringBuilder();
            StringBuilder conditionQuery = new StringBuilder();
            StringBuilder sortQuery = new StringBuilder();
            
            if (companySearcher != null)
            {
                querySearcher = (CompanySearcher)companySearcher.Clone();
                querySearcher.TableName = "C";
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
            query.AppendLine(@"   C.[CompanyId] ");
            query.AppendLine(@"  ,C.[CompanyCode] ");
            query.AppendLine(@"  ,C.[Name] ");
            query.AppendLine(@"  ,C.[Address] ");
            query.AppendLine(@"  ,C.[ParentCompanyId] ");
            query.AppendLine(@"  ,C.[RVersion] ");
            query.AppendLine(@"  ,C.[Status] ");
            query.AppendLine(@"  ,C.[CreaterId] ");
            query.AppendLine(@"  ,C.[CreateTime] ");
            query.AppendLine(@"  ,C.[UpdatorId] ");
            query.AppendLine(@"  ,C.[UpdateTime] ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"   [Company] C ");
            query.AppendLine(conditionQuery.ToString());
            query.AppendLine(sortQuery.ToString());
            query.AppendLine(@"; ");

            if (tran != null)
            {
                DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;
                
                if(pager != null)
                {
                    resultSet = MssqlHelper.ExecuteDataSet(dbTran, CommandType.Text, query.ToString(), pager, "Company", queryParser.ParamCollection);
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
                    resultSet = MssqlHelper.ExecuteDataSet(this.CurrentConnectionString, CommandType.Text, query.ToString(), pager, "Company", queryParser.ParamCollection);
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

                pDataTable.TotalCount = this.Count(companySearcher, tran);
                pDataTable.RecordList = resultSet.Tables[0];
            }

            return pDataTable;
        }

        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="company">公司实体对象</param>
        public void Update(Company company)
        {
            this.Update(company, null);
        }

        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="company">公司实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Update(Company company, ICTransaction tran)
        {
            Company oldCompany = this.FindSingle(company.CompanyId, tran);
            int updateColCount = 0;

            if (company == null)
            {
                throw new ArgumentException("company");
            }

            if (oldCompany == null)
            {
                throw new ResponseException((int)ResultCode.NoDataExists, company.CompanyCode);
            }

            if (company.RVersion != oldCompany.RVersion)
            {
                throw new ResponseException((int)ResultCode.VersionChanged, oldCompany.RVersion.ToString());
            }

            StringBuilder query = new StringBuilder();
            query.AppendLine(@"UPDATE ");
            query.AppendLine(@"   [Company] ");
            query.AppendLine(@"SET ");
            query.AppendLine(@"   [CompanyId] = @CompanyId ");

            if ((!string.IsNullOrEmpty(company.CompanyCode) && !company.CompanyCode.Equals(oldCompany.CompanyCode))
                || (!string.IsNullOrEmpty(oldCompany.CompanyCode) && !oldCompany.CompanyCode.Equals(company.CompanyCode)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[CompanyCode] = @CompanyCode ");
            }

            if ((!string.IsNullOrEmpty(company.Name) && !company.Name.Equals(oldCompany.Name))
                || (!string.IsNullOrEmpty(oldCompany.Name) && !oldCompany.Name.Equals(company.Name)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[Name] = @Name ");
            }

            if ((!string.IsNullOrEmpty(company.Address) && !company.Address.Equals(oldCompany.Address))
                || (!string.IsNullOrEmpty(oldCompany.Address) && !oldCompany.Address.Equals(company.Address)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[Address] = @Address ");
            }

            if ((!string.IsNullOrEmpty(company.ParentCompanyId) && !company.ParentCompanyId.Equals(oldCompany.ParentCompanyId))
                || (!string.IsNullOrEmpty(oldCompany.ParentCompanyId) && !oldCompany.ParentCompanyId.Equals(company.ParentCompanyId)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[ParentCompanyId] = @ParentCompanyId ");
            }

            if (oldCompany.Status != company.Status)
            {
                updateColCount++;
                query.AppendLine(@"  ,[Status] = @Status ");
            }

            if ((!string.IsNullOrEmpty(company.CreaterId) && !company.CreaterId.Equals(oldCompany.CreaterId))
                || (!string.IsNullOrEmpty(oldCompany.CreaterId) && !oldCompany.CreaterId.Equals(company.CreaterId)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[CreaterId] = @CreaterId ");
            }

            if (oldCompany.CreateTime.CompareTo(company.CreateTime) != 0 && company.CreateTime != DateTime.MinValue)
            {
                updateColCount++;
                query.AppendLine(@"  ,[CreateTime] = @CreateTime ");
            }

            if ((!string.IsNullOrEmpty(company.UpdatorId) && !company.UpdatorId.Equals(oldCompany.UpdatorId))
                || (!string.IsNullOrEmpty(oldCompany.UpdatorId) && !oldCompany.UpdatorId.Equals(company.UpdatorId)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[UpdatorId] = @UpdatorId ");
            }

            if (oldCompany.UpdateTime.CompareTo(company.UpdateTime) != 0 && company.UpdateTime != DateTime.MinValue)
            {
                updateColCount++;
                query.AppendLine(@"  ,[UpdateTime] = @UpdateTime ");
            }

            query.AppendLine(@"  ,[RVersion] = @RVersion ");
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"   [CompanyId] = @CompanyId ");

            if (updateColCount == 0)
            {
                return;
            }

            company.UpdateTime = DateTime.Now;
            DBParamCollection paramCollection = new DBParamCollection();
            paramCollection.Add(new DBParam("@CompanyId", company.CompanyId, DbType.String, 40));
            paramCollection.Add(new DBParam("@CompanyCode", company.CompanyCode, DbType.String, 5));
            paramCollection.Add(new DBParam("@Name", company.Name, DbType.String, 200));
            paramCollection.Add(new DBParam("@Address", company.Address, DbType.String, 500));
            paramCollection.Add(new DBParam("@ParentCompanyId", company.ParentCompanyId, DbType.String, 40));
            paramCollection.Add(new DBParam("@RVersion", oldCompany.RVersion + 1, DbType.Int32));
            paramCollection.Add(new DBParam("@Status", company.Status, DbType.Int32));
            paramCollection.Add(new DBParam("@CreaterId", company.CreaterId, DbType.String, 40));
            paramCollection.Add(new DBParam("@CreateTime", company.CreateTime, DbType.DateTime));
            paramCollection.Add(new DBParam("@UpdatorId", company.UpdatorId, DbType.String, 40));
            paramCollection.Add(new DBParam("@UpdateTime", company.UpdateTime, DbType.DateTime));

            try
            {
                int effectCount = 0;

                if (company != null)
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
                    throw new ResponseException((int)ResultCode.NoDataUpdate, company.CompanyCode);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="companyList">公司实体对象</param>
        public void Update(IList<Company> companyList)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                if (companyList != null)
                {
                    foreach (Company company in companyList)
                    {
                        this.Update(company, tran);
                    }

                    tran.Commit();
                }
            }
            catch
            {
                tran.RollBack();
                throw;
            }
        }

        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="companyList">公司实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Update(IList<Company> companyList, ICTransaction tran)
        {
            try
            {
                if (companyList != null)
                {
                    foreach (Company company in companyList)
                    {
                        this.Update(company, tran);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dalFactory">传入当前指定的DalFactory对象</param>
        public CompanyDal(MssqlDalFactoryBase dalFactory)
            : base(dalFactory)
        {

        }

        #endregion
    }
}
