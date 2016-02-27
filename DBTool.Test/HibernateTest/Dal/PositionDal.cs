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
    /// 使用Hibernate的职位数据操作类
    /// </summary>
    public class PositionDal : HibernateDalBase, IPositionDal
    {
        #region Public Methods

        /// <summary>
        /// 新建职位
        /// </summary>
        /// <param name="position">职位实体对象</param>
        /// <returns>返回处理后的职位实体对象</returns>
        public Position Add(Position position)
        {
            return this.Add(position, null);
        }

        /// <summary>
        /// 新建职位
        /// </summary>
        /// <param name="position">职位实体对象</param>
        /// <param name="tran">事务对象</param>
        /// <returns>返回处理后的职位实体对象</returns>
        public Position Add(Position position, ICTransaction tran)
        {
            IList<Position> positionList = new List<Position>();
            positionList.Add(position);
            positionList = this.Add(positionList, tran);
            return positionList == null ? null : positionList[0];
        }

        /// <summary>
        /// 批量新建职位
        /// </summary>
        /// <param name="positionList">职位实体对象集合</param>
        /// <returns>返回处理后的职位实体对象集合</returns>
        public IList<Position> Add(IList<Position> positionList)
        {
            return this.Add(positionList, null);
        }

        /// <summary>
        /// 批量新建职位
        /// </summary>
        /// <param name="positionList">职位实体对象集合</param>
        /// <param name="tran">事务对象</param>
        /// <returns>返回处理后的职位实体对象集合</returns>
        public IList<Position> Add(IList<Position> positionList, ICTransaction tran)
        {
            IList<Position> newPositionList = new List<Position>();
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

                if (positionList != null)
                {
                    Position newPosition = null;

                    foreach (Position position in positionList)
                    {
                        newPosition = HibernateHelper.AddObject<Position>(session, position);
                        newPositionList.Add(newPosition);

                        if (string.IsNullOrEmpty(newPosition.PositionId) && isPass)
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

            return newPositionList;
        }

        /// <summary>
        /// 查询职位数量
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count(PositionSearcher positionSearcher)
        {
            return this.Count(positionSearcher, null);
        }

        /// <summary>
        /// 查询职位数量
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="tran">事务对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count(PositionSearcher positionSearcher, ICTransaction tran)
        {
            PositionSearcher querySearcher = null;
            HibernateTransaction hTran = null;
            ISession session = null;
            HQLQueryParser queryParser = new HQLQueryParser();
            StringBuilder query = new StringBuilder();

            query.Append("SELECT ");
            query.Append("  COUNT(*) ");
            query.Append("FROM ");
            query.Append("  Position P ");

            if (positionSearcher != null)
            {
                querySearcher = (PositionSearcher)positionSearcher.Clone();
                querySearcher.TableName = "P";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.Append("LEFT JOIN ");
                    query.Append("  P.CurrCompany C ");
                }

                if (querySearcher.CurrDepartment != null)
                {
                    querySearcher.CurrDepartment.TableName = "D";
                    query.Append("LEFT JOIN ");
                    query.Append("  P.CurrDepartment D ");
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
        /// 删除职位
        /// </summary>
        /// <param name="positionId">职位ID</param>
        public void Delete(string positionId)
        {
            this.Delete(positionId, null);
        }

        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="positionId">职位ID</param>
        /// <param name="tran">中间事务对象</param>
        public void Delete(string positionId, ICTransaction tran)
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

            Position position = HibernateHelper.FindObjectById<Position>(session, positionId);
            HibernateHelper.DeleteObject<Position>(session, position);
        }

        /// <summary>
        /// 根据指定条件删除职位
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        public void Delete(PositionSearcher positionSearcher)
        {
            this.Delete(positionSearcher, null);
        }

        /// <summary>
        /// 根据指定条件删除职位
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Delete(PositionSearcher positionSearcher, ICTransaction tran)
        {
            PositionSearcher querySearcher = null;
            HibernateTransaction hTran = null;
            ISession session = null;
            HQLQueryParser queryParser = new HQLQueryParser();
            StringBuilder query = new StringBuilder();

            query.Append("DELETE FROM ");
            query.Append("  Position  ");
            query.Append("WHERE ");
            query.Append("  PositionId IN (");
            query.Append("    SELECT ");
            query.Append("      P.PositionId ");
            query.Append("    FROM ");
            query.Append("      Position P ");

            if (positionSearcher != null)
            {
                querySearcher = (PositionSearcher)positionSearcher.Clone();
                querySearcher.TableName = "P";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.Append("LEFT JOIN ");
                    query.Append("  P.CurrCompany C ");
                }

                if (querySearcher.CurrDepartment != null)
                {
                    querySearcher.CurrDepartment.TableName = "D";
                    query.Append("LEFT JOIN ");
                    query.Append("  P.CurrDepartment D ");
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
        /// 查找指定ID的职位
        /// </summary>
        /// <param name="positionId">职位ID</param>
        /// <returns>返回职位实体对象</returns>
        public Position FindSingle(string positionId)
        {
            return this.FindSingle(positionId, null);
        }

        /// <summary>
        /// 查找指定ID的职位
        /// </summary>
        /// <param name="positionId">职位ID</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回职位实体对象</returns>
        public Position FindSingle(string positionId, ICTransaction tran)
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

            return HibernateHelper.FindObjectById<Position>(session, positionId);
        }

        /// <summary>
        /// 查找指定条件的职位集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <returns>返回职位实体对象集合</returns>
        public IList<Position> FindList(PositionSearcher positionSearcher)
        {
            PageList<Position> pageList = this.FindList(positionSearcher, null, null);
            return pageList == null ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查找指定条件的职位集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回职位实体对象集合</returns>
        public IList<Position> FindList(PositionSearcher positionSearcher, ICTransaction tran)
        {
            PageList<Position> pageList = this.FindList(positionSearcher, null, tran);
            return pageList == null ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查找指定条件的职位分页集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public PageList<Position> FindList(PositionSearcher positionSearcher, Pager pager)
        {
            return this.FindList(positionSearcher, pager, null);
        }

        /// <summary>
        /// 查找指定条件的职位分页集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        public PageList<Position> FindList(PositionSearcher positionSearcher, Pager pager, ICTransaction tran)
        {
            PositionSearcher querySearcher = null;
            HibernateTransaction hTran = null;
            ISession session = null;
            PageList<Position> pList = new PageList<Position>();
            HQLQueryParser queryParser = new HQLQueryParser();
            StringBuilder query = new StringBuilder();

            query.Append("SELECT ");
            query.Append(" P ");
            query.Append("FROM ");
            query.Append("  Position P ");

            if (positionSearcher != null)
            {
                querySearcher = (PositionSearcher)positionSearcher.Clone();
                querySearcher.TableName = "P";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.Append("LEFT JOIN ");
                    query.Append("  P.CurrCompany C ");
                }

                if (querySearcher.CurrDepartment != null)
                {
                    querySearcher.CurrDepartment.TableName = "D";
                    query.Append("LEFT JOIN ");
                    query.Append("  P.CurrDepartment D ");
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
                pList.RecordList = HibernateHelper.FindObjectListByHQL<Position>(session, query.ToString(), queryParser.ParamCollection, pager);
            }
            else
            {
                pList.RecordList = HibernateHelper.FindObjectListByHQL<Position>(session, query.ToString(), queryParser.ParamCollection);
            }

            return pList;
        }

        /// <summary>
        /// 根据指定条件查找职位集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public DataTable FindDataTable(PositionSearcher positionSearcher)
        {
            PageDataTable pageDataTable = this.FindDataTable(positionSearcher, null, null);
            return pageDataTable == null ? null : pageDataTable.RecordList;
        }

        /// <summary>
        /// 根据指定条件查找职位集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回一个DataTable对象</returns>
        public DataTable FindDataTable(PositionSearcher positionSearcher, ICTransaction tran)
        {
            PageDataTable pageDataTable = this.FindDataTable(positionSearcher, null, tran);
            return pageDataTable == null ? null : pageDataTable.RecordList;
        }

        /// <summary>
        /// 根据指定条件查找职位分页集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public PageDataTable FindDataTable(PositionSearcher positionSearcher, Pager pager)
        {
            return this.FindDataTable(positionSearcher, pager, null);
        }

        /// <summary>
        /// 根据指定条件查找职位分页集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        public PageDataTable FindDataTable(PositionSearcher positionSearcher, Pager pager, ICTransaction tran)
        {
            PageDataTable pDataTable = null;
            PageList<Position> positionList = this.FindList(positionSearcher, pager, tran);

            if (positionList != null)
            {
                pDataTable.PageIndex = positionList.PageIndex;
                pDataTable.TotalCount = positionList.TotalCount;
                pDataTable.RecordList = new DataTable("Position");
                pDataTable.RecordList.Columns.AddRange(new DataColumn[] { 
                    new DataColumn("PositionCode", typeof(String)),
                    new DataColumn("PositionId", typeof(String)),
                    new DataColumn("CompanyId", typeof(String)),
                    new DataColumn("DepartmentId", typeof(String)),
                    new DataColumn("CreaterId", typeof(String)),
                    new DataColumn("CreateTime", typeof(DateTime)),
                    new DataColumn("Name", typeof(String)),
                    new DataColumn("RVersion", typeof(Int32)),
                    new DataColumn("Status", typeof(Int32)),
                    new DataColumn("UpdateTime", typeof(DateTime)),
                    new DataColumn("UpdatorId", typeof(String))
                });

                foreach (Position position in positionList.RecordList)
                {
                    pDataTable.RecordList.Rows.Add(
                        position.PositionCode,
                        position.PositionId,
                        position.CurrCompany.CompanyId,
                        position.CurrDepartment.DepartmentId,
                        position.CreaterId,
                        position.CreateTime,
                        position.Name,
                        position.RVersion,
                        position.Status,
                        position.UpdateTime,
                        position.UpdatorId
                    );
                }
            }

            return pDataTable;
        }

        /// <summary>
        /// 更新职位数据
        /// </summary>
        /// <param name="position">职位实体对象</param>
        public void Update(Position position)
        {
            this.Update(position, null);
        }

        /// <summary>
        /// 更新职位数据
        /// </summary>
        /// <param name="position">职位实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Update(Position position, ICTransaction tran)
        {
            IList<Position> positionList = new List<Position>();
            positionList.Add(position);
            this.Update(positionList, tran);
        }

        /// <summary>
        /// 更新职位数据
        /// </summary>
        /// <param name="positionList">职位实体对象</param>
        public void Update(IList<Position> positionList)
        {
            this.Update(positionList, null);
        }

        /// <summary>
        /// 更新职位数据
        /// </summary>
        /// <param name="positionList">职位实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Update(IList<Position> positionList, ICTransaction tran)
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
                if (positionList != null)
                {
                    foreach (Position position in positionList)
                    {
                        HibernateHelper.UpdateObject<Position>(session, position);
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
        public PositionDal(HibernateDalFactoryBase dalFactory)
            : base(dalFactory)
        {

        }

        #endregion
    }
}
