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
    /// 使用Hibernate的公司数据操作类
    /// </summary>
    public class CompanyDal : HibernateDalBase,ICompanyDal
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
            IList<Company> companyList = new List<Company>();
            companyList.Add(company);
            companyList = this.Add(companyList, tran);
            return companyList == null ? null : companyList[0];
        }

        /// <summary>
        /// 批量新建公司
        /// </summary>
        /// <param name="companyList">公司实体对象集合</param>
        /// <returns>返回处理后的公司实体对象集合</returns>
        public IList<Company> Add(IList<Company> companyList)
        {
            return this.Add(companyList, null);
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

                if (companyList != null)
                {
                    Company newCompany= null;

                    foreach (Company company in companyList)
                    {
                        newCompany = HibernateHelper.AddObject<Company>(session, company);
                        newCompanyList.Add(newCompany);

                        if (string.IsNullOrEmpty(newCompany.CompanyId) && isPass)
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
            CompanySearcher querySearcher = null;
            HibernateTransaction hTran = null;
            ISession session = null;
            HQLQueryParser queryParser = new HQLQueryParser();
            StringBuilder query = new StringBuilder();

            query.Append("SELECT ");
            query.Append("  COUNT(*) ");
            query.Append("FROM ");
            query.Append("  Company C ");

            if (companySearcher != null)
            {
                querySearcher = (CompanySearcher)companySearcher.Clone();
                querySearcher.TableName = "C";
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

            Company company = HibernateHelper.FindObjectById<Company>(session, companyId);
            HibernateHelper.DeleteObject<Company>(session, company);
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
            CompanySearcher querySearcher = null;
            HibernateTransaction hTran = null;
            ISession session = null;
            HQLQueryParser queryParser = new HQLQueryParser();
            StringBuilder query = new StringBuilder();

            query.AppendLine("DELETE FROM ");
            query.AppendLine("   Company C ");

            if (companySearcher != null)
            {
                querySearcher = (CompanySearcher)companySearcher.Clone();
                querySearcher.TableName = "C";
            }

            queryParser.SearcherParse(querySearcher);

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                query.AppendLine("WHERE ");
                query.AppendLine("   " + queryParser.ConditionString);
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

            int effectCount = HibernateHelper.DeleteObjectByHQL(session, query.ToString(), queryParser.ParamCollection);
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

            return HibernateHelper.FindObjectById<Company>(session, companyId);
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
            CompanySearcher querySearcher = null;
            HibernateTransaction hTran = null;
            ISession session = null;
            PageList<Company> pList = new PageList<Company>();
            HQLQueryParser queryParser = new HQLQueryParser();
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT ");
            query.AppendLine("   C ");
            query.AppendLine("FROM ");
            query.AppendLine("   Company C ");

            if (companySearcher != null)
            {
                querySearcher = (CompanySearcher)companySearcher.Clone();
                querySearcher.TableName = "C";
            }

            queryParser.SearcherParse(querySearcher);

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                query.AppendLine("WHERE ");
                query.AppendLine("   " + queryParser.ConditionString);
            }

            if (!string.IsNullOrEmpty(queryParser.SortString))
            {
                query.AppendLine("ORDER BY ");
                query.AppendLine("   " + queryParser.SortString);
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
                pList.RecordList = HibernateHelper.FindObjectListByHQL<Company>(session, query.ToString(), queryParser.ParamCollection, pager);
            }
            else
            {
                pList.RecordList = HibernateHelper.FindObjectListByHQL<Company>(session, query.ToString(), queryParser.ParamCollection);
            }

            return pList;
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
            PageDataTable pDataTable = null;
            PageList<Company> companyList = this.FindList(companySearcher, pager, tran);

            if (companyList != null)
            {
                pDataTable.PageIndex = companyList.PageIndex;
                pDataTable.TotalCount = companyList.TotalCount;
                pDataTable.RecordList = new DataTable("Company");
                pDataTable.RecordList.Columns.AddRange(new DataColumn[] { 
                    new DataColumn("Address", typeof(String)),
                    new DataColumn("CompanyCode", typeof(String)),
                    new DataColumn("CompanyId", typeof(String)),
                    new DataColumn("CreaterId", typeof(String)),
                    new DataColumn("CreateTime", typeof(DateTime)),
                    new DataColumn("Name", typeof(String)),
                    new DataColumn("ParentCompanyId", typeof(String)),
                    new DataColumn("RVersion", typeof(Int32)),
                    new DataColumn("Status", typeof(Int32)),
                    new DataColumn("UpdateTime", typeof(DateTime)),
                    new DataColumn("UpdatorId", typeof(String))
                });

                foreach (Company company in companyList.RecordList)
                {
                    pDataTable.RecordList.Rows.Add(
                        company.Address,
                        company.CompanyCode,
                        company.CompanyId,
                        company.CreaterId,
                        company.CreateTime,
                        company.Name,
                        company.ParentCompany == null ? null : company.ParentCompany.CompanyId,
                        company.RVersion,
                        company.Status,
                        company.UpdateTime,
                        company.UpdatorId
                    );
                }
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
            IList<Company> companyList = new List<Company>();
            companyList.Add(company);
            this.Update(companyList, tran);
        }

        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="companyList">公司实体对象</param>
        public void Update(IList<Company> companyList)
        {
            this.Update(companyList, null);
        }

        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="companyList">公司实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Update(IList<Company> companyList, ICTransaction tran)
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
                if (companyList != null)
                {
                    foreach (Company company in companyList)
                    {
                        HibernateHelper.UpdateObject<Company>(session, company);
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
        public CompanyDal(HibernateDalFactoryBase dalFactory)
            : base(dalFactory)
        {

        }

        #endregion
    }
}
