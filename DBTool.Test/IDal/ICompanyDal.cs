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
    /// 公司数据接口
    /// </summary>
    public interface ICompanyDal : NFramework.DBTool.Common.IDalBase
    {
        /// <summary>
        /// 新建公司
        /// </summary>
        /// <param name="company">公司实体对象</param>
        /// <returns>返回处理后的公司实体对象</returns>
        Company Add(Company company);
        /// <summary>
        /// 新建公司
        /// </summary>
        /// <param name="company">公司实体对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的公司实体对象</returns>
        Company Add(Company company, ICTransaction tran);
        /// <summary>
        /// 批量新建公司
        /// </summary>
        /// <param name="companyList">公司实体对象集合</param>
        /// <returns>返回处理后的公司实体对象集合</returns>
        IList<Company> Add(IList<Company> companyList);
        /// <summary>
        /// 批量新建公司
        /// </summary>
        /// <param name="companyList">公司实体对象集合</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的公司实体对象集合</returns>
        IList<Company> Add(IList<Company> companyList, ICTransaction tran);
        /// <summary>
        /// 查询公司数量
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <returns>返回查询到的数量</returns>
        long Count(CompanySearcher companySearcher);
        /// <summary>
        /// 查询公司数量
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查询到的数量</returns>
        long Count(CompanySearcher companySearcher, ICTransaction tran);
        /// <summary>
        /// 删除公司
        /// </summary>
        /// <param name="companyId">公司ID</param>
        void Delete(string companyId);
        /// <summary>
        /// 删除公司
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="tran">中间事务对象</param>
        void Delete(string companyId, ICTransaction tran);
        /// <summary>
        /// 根据指定条件删除公司
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        void Delete(CompanySearcher companySearcher);
        /// <summary>
        /// 根据指定条件删除公司
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="tran">中间事务对象</param>
        void Delete(CompanySearcher companySearcher, ICTransaction tran);
        /// <summary>
        /// 查找指定ID的公司
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns>返回公司实体对象</returns>
        Company FindSingle(string companyId);
        /// <summary>
        /// 查找指定ID的公司
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回公司实体对象</returns>
        Company FindSingle(string companyId, ICTransaction tran);
        /// <summary>
        /// 查找指定条件的公司集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <returns>返回公司实体对象集合</returns>
        IList<Company> FindList(CompanySearcher companySearcher);
        /// <summary>
        /// 查找指定条件的公司集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回公司实体对象集合</returns>
        IList<Company> FindList(CompanySearcher companySearcher, ICTransaction tran);
        /// <summary>
        /// 查找指定条件的公司分页集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        PageList<Company> FindList(CompanySearcher companySearcher, Pager pager);
        /// <summary>
        /// 查找指定条件的公司分页集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        PageList<Company> FindList(CompanySearcher companySearcher, Pager pager, ICTransaction tran);
        /// <summary>
        /// 根据指定条件查找公司集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <returns>返回一个DataTable对象</returns>
        DataTable FindDataTable(CompanySearcher companySearcher);
        /// <summary>
        /// 根据指定条件查找公司集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回一个DataTable对象</returns>
        DataTable FindDataTable(CompanySearcher companySearcher, ICTransaction tran);
        /// <summary>
        /// 根据指定条件查找公司分页集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        PageDataTable FindDataTable(CompanySearcher companySearcher, Pager pager);
        /// <summary>
        /// 根据指定条件查找公司分页集合
        /// </summary>
        /// <param name="companySearcher">公司查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        PageDataTable FindDataTable(CompanySearcher companySearcher, Pager pager, ICTransaction tran);
        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="company">公司实体对象</param>
        void Update(Company company);
        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="company">公司实体对象</param>
        /// <param name="tran">中间事务对象</param>
        void Update(Company company, ICTransaction tran);
        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="companyList">公司实体对象</param>
        void Update(IList<Company> companyList);
        /// <summary>
        /// 更新公司数据
        /// </summary>
        /// <param name="companyList">公司实体对象</param>
        /// <param name="tran">中间事务对象</param>
        void Update(IList<Company> companyList, ICTransaction tran);
    }
}
