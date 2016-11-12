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
    /// 使用MSSQL原生SQL语句的职位数据操作类
    /// </summary>
    public class PositionDal : MssqlDalBase, IPositionDal
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
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的职位实体对象</returns>
        public Position Add(Position position, ICTransaction tran)
        {
            if (position == null)
            {
                throw new ArgumentNullException("position");
            }

            position.PositionId = KeyGenerator.GenNewGuidKey();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"INSERT INTO ");
            query.AppendLine(@"   [Position] ( ");
            query.AppendLine(@"      [PositionId] ");
            query.AppendLine(@"     ,[PositionCode] ");
            query.AppendLine(@"     ,[CompanyId] ");
            query.AppendLine(@"     ,[DepartmentId] ");
            query.AppendLine(@"     ,[Name] ");
            query.AppendLine(@"     ,[RVersion] ");
            query.AppendLine(@"     ,[Status] ");
            query.AppendLine(@"     ,[CreaterId] ");
            query.AppendLine(@"     ,[CreateTime] ");
            query.AppendLine(@"     ,[UpdatorId] ");
            query.AppendLine(@"     ,[UpdateTime] ");
            query.AppendLine(@"   ) ");
            query.AppendLine(@"VALUES ( ");
            query.AppendLine(@"      @PositionId ");
            query.AppendLine(@"     ,@PositionCode ");
            query.AppendLine(@"     ,@CompanyId ");
            query.AppendLine(@"     ,@DepartmentId ");
            query.AppendLine(@"     ,@Name ");
            query.AppendLine(@"     ,@RVersion ");
            query.AppendLine(@"     ,@Status ");
            query.AppendLine(@"     ,@CreaterId ");
            query.AppendLine(@"     ,@CreateTime ");
            query.AppendLine(@"     ,@UpdatorId ");
            query.AppendLine(@"     ,@UpdateTime ");
            query.AppendLine(@"); ");

            DBParamCollection<DBParam> paramCollection = new DBParamCollection<DBParam>();
            paramCollection.Add(new DBParam("@PositionId", position.PositionId, DbType.String, 40));
            paramCollection.Add(new DBParam("@PositionCode", position.PositionCode, DbType.String, 13));
            paramCollection.Add(new DBParam("@CompanyId", position.CompanyId, DbType.String, 40));
            paramCollection.Add(new DBParam("@DepartmentId", position.DepartmentId, DbType.String, 40));
            paramCollection.Add(new DBParam("@Name", position.Name, DbType.String, 100));
            paramCollection.Add(new DBParam("@RVersion", position.RVersion, DbType.Int32));
            paramCollection.Add(new DBParam("@Status", position.Status, DbType.Int32));
            paramCollection.Add(new DBParam("@CreaterId", position.CreaterId, DbType.String, 40));
            paramCollection.Add(new DBParam("@CreateTime", position.CreateTime, DbType.DateTime));
            paramCollection.Add(new DBParam("@UpdatorId", position.UpdatorId, DbType.String, 40));
            paramCollection.Add(new DBParam("@UpdateTime", position.UpdateTime, DbType.DateTime));

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
                    position.PositionId = string.Empty;
                    throw new ResponseException((int)ResultCode.NoDataInsert, position.PositionCode);
                }
            }
            catch(Exception ex)
            {
                position.PositionId = string.Empty;
                throw new Exception(ex.Message, ex);
            }

            return position;
        }

        /// <summary>
        /// 批量新建职位
        /// </summary>
        /// <param name="positionList">职位实体对象集合</param>
        /// <returns>返回处理后的职位实体对象集合</returns>
        public IList<Position> Add(IList<Position> positionList)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();
            IList<Position> newPositionList = new List<Position>();
            Position newPosition = null;
            bool allSucc = true;

            try
            {
                if (positionList != null)
                {
                    foreach (Position position in positionList)
                    {
                        if (allSucc)
                        {
                            newPosition = this.Add(position, tran);
                        }

                        if (string.IsNullOrEmpty(newPosition.PositionId))
                        {
                            allSucc = false;
                        }

                        newPositionList.Add(newPosition);
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

            return newPositionList;
        }

        /// <summary>
        /// 批量新建职位
        /// </summary>
        /// <param name="positionList">职位实体对象集合</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回处理后的职位实体对象集合</returns>
        public IList<Position> Add(IList<Position> positionList, ICTransaction tran)
        {
            IList<Position> newPositionList = new List<Position>();
            Position newPosition = null;
            bool allSucc = true;

            try
            {
                if (positionList != null)
                {
                    foreach (Position position in positionList)
                    {
                        if (allSucc)
                        {
                            newPosition = this.Add(position, tran);

                            if (string.IsNullOrEmpty(newPosition.PositionId))
                            {
                                allSucc = false;

                                foreach (Position nPosition in newPositionList)
                                {
                                    nPosition.PositionId = string.Empty;
                                }
                            }
                        }

                        newPositionList.Add(newPosition);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
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
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count(PositionSearcher positionSearcher, ICTransaction tran)
        {
            object count = 0;
            long result = 0;
            PositionSearcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"SELECT ");
            query.AppendLine(@"   COUNT(P.PositionId) ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"   Position P ");

            if (positionSearcher != null)
            {
                querySearcher = (PositionSearcher)positionSearcher.Clone();
                querySearcher.TableName = "P";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"   [Company] C ON(C.CompanyId = P.CompanyId) ");
                }

                if (querySearcher.CurrDepartment != null)
                {
                    querySearcher.CurrDepartment.TableName = "D";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"   [Department] D ON(D.DepartmentId = P.DepartmentId) ");
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
            PositionSearcher querySearcher = new PositionSearcher();
            querySearcher.PositionId.Equal(positionId);
            this.Delete(querySearcher, tran);
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
            int effectCount = 0;
            PositionSearcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            StringBuilder query = new StringBuilder();

            query.AppendLine(@"DELETE FROM ");
            query.AppendLine(@"   Position ");
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"   PositionId IN (");
            query.AppendLine(@"      SELECT ");
            query.AppendLine(@"         P.PositionId ");
            query.AppendLine(@"      FROM ");
            query.AppendLine(@"         Position P ");

            if (positionSearcher != null)
            {
                querySearcher = (PositionSearcher)positionSearcher.Clone();
                querySearcher.TableName = "P";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"   [Company] C ON(C.CompanyId = P.CompanyId) ");
                }

                if (querySearcher.CurrDepartment != null)
                {
                    querySearcher.CurrDepartment.TableName = "D";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"   [Department] D ON(D.DepartmentId = P.DepartmentId) ");
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
            PositionSearcher positionSearcher = new PositionSearcher();
            positionSearcher.PositionId.Equal(positionId);
            IList<Position> positionList = this.FindList(positionSearcher);
            return (positionList == null || positionList.Count == 0) ? null : positionList[0];
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
            PageList<Position> resultList = new PageList<Position>();
            PageDataTable pageDataTable = this.FindDataTable(positionSearcher, pager, tran);
            Position ele = null;

            if (pageDataTable != null)
            {
                resultList = new PageList<Position>();
                resultList.PageIndex = pageDataTable.PageIndex;
                resultList.TotalCount = pageDataTable.TotalCount;

                if (pageDataTable.RecordList != null && pageDataTable.RecordList.Rows.Count > 0)
                {
                    foreach (DataRow aRow in pageDataTable.RecordList.Rows)
                    {
                        ele = new Position();

                        if (!(aRow["PositionId"] is DBNull))
                        {
                            ele.PositionId = aRow["PositionId"].ToString();
                        }

                        if (!(aRow["CompanyId"] is DBNull))
                        {
                            ele.CompanyId = aRow["CompanyId"].ToString();
                        }

                        if (!(aRow["DepartmentId"] is DBNull))
                        {
                            ele.DepartmentId = aRow["DepartmentId"].ToString();
                        }

                        if (!(aRow["PositionCode"] is DBNull))
                        {
                            ele.PositionCode = aRow["PositionCode"].ToString();
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
            PositionSearcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            PageDataTable pDataTable = new PageDataTable();
            DataSet resultSet = null;
            StringBuilder query = new StringBuilder();
            StringBuilder joinQuery = new StringBuilder();
            StringBuilder conditionQuery = new StringBuilder();
            StringBuilder sortQuery = new StringBuilder();

            if (positionSearcher != null)
            {
                querySearcher = (PositionSearcher)positionSearcher.Clone();
                querySearcher.TableName = "P";

                if (querySearcher.CurrCompany != null)
                {
                    querySearcher.CurrCompany.TableName = "C";
                    joinQuery.AppendLine(@"LEFT JOIN ");
                    joinQuery.AppendLine(@"   [Company] C ON(C.CompanyId = P.CompanyId) ");
                }

                if (querySearcher.CurrDepartment != null)
                {
                    querySearcher.CurrDepartment.TableName = "D";
                    joinQuery.AppendLine(@"LEFT JOIN ");
                    joinQuery.AppendLine(@"   [Department] D ON(D.DepartmentId = P.DepartmentId) ");
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
            query.AppendLine(@"   P.[PositionId]");
            query.AppendLine(@"  ,P.[PositionCode]");
            query.AppendLine(@"  ,P.[CompanyId]");
            query.AppendLine(@"  ,P.[DepartmentId]");
            query.AppendLine(@"  ,P.[Name]");
            query.AppendLine(@"  ,P.[RVersion]");
            query.AppendLine(@"  ,P.[Status]");
            query.AppendLine(@"  ,P.[CreaterId]");
            query.AppendLine(@"  ,P.[CreateTime]");
            query.AppendLine(@"  ,P.[UpdatorId]");
            query.AppendLine(@"  ,P.[UpdateTime]");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"   [Position] P ");
            query.AppendLine(joinQuery.ToString());
            query.AppendLine(conditionQuery.ToString());
            query.AppendLine(sortQuery.ToString());
            query.AppendLine(@"; ");

            if (tran != null)
            {
                DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;

                if (pager != null)
                {
                    resultSet = MssqlHelper.ExecuteDataSet(dbTran, CommandType.Text, query.ToString(), pager, "Position", queryParser.ParamCollection);
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
                    resultSet = MssqlHelper.ExecuteDataSet(this.CurrentConnectionString, CommandType.Text, query.ToString(), pager, "Position", queryParser.ParamCollection);
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

                pDataTable.TotalCount = this.Count(positionSearcher);
                pDataTable.RecordList = resultSet.Tables[0];
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
            Position oldPosition = this.FindSingle(position.PositionId, tran);
            int updateColCount = 0;

            if (position == null)
            {
                throw new ArgumentException("position");
            }

            if (oldPosition == null)
            {
                throw new ResponseException((int)ResultCode.NoDataExists, position.PositionCode);
            }

            if (position.RVersion != oldPosition.RVersion)
            {
                throw new ResponseException((int)ResultCode.VersionChanged, oldPosition.RVersion.ToString());
            }

            StringBuilder query = new StringBuilder();
            query.AppendLine(@"UPDATE ");
            query.AppendLine(@"   [Position] ");
            query.AppendLine(@"SET ");
            query.AppendLine(@"   [PositionId] = @PositionId ");

            if ((!string.IsNullOrEmpty(position.PositionCode) && !position.PositionCode.Equals(oldPosition.PositionCode))
                || (!string.IsNullOrEmpty(oldPosition.PositionCode) && !oldPosition.PositionCode.Equals(position.PositionCode)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[PositionCode] = @PositionCode ");
            }

            if ((!string.IsNullOrEmpty(position.CompanyId) && !position.CompanyId.Equals(oldPosition.CompanyId))
                || (!string.IsNullOrEmpty(oldPosition.CompanyId) && !oldPosition.CompanyId.Equals(position.CompanyId)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[CompanyId] = @CompanyId ");
            }

            if ((!string.IsNullOrEmpty(position.DepartmentId) && !position.DepartmentId.Equals(oldPosition.DepartmentId))
                || (!string.IsNullOrEmpty(oldPosition.DepartmentId) && !oldPosition.DepartmentId.Equals(position.DepartmentId)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[DepartmentId] = @DepartmentId ");
            }

            if ((!string.IsNullOrEmpty(position.Name) && !position.Name.Equals(oldPosition.Name))
                || (!string.IsNullOrEmpty(oldPosition.Name) && !oldPosition.Name.Equals(position.Name)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[Name] = @Name ");
            }

            if (position.Status != oldPosition.Status)
            {
                updateColCount++;
                query.AppendLine(@"  ,[Status] = @Status ");
            }

            if ((!string.IsNullOrEmpty(position.PositionCode) && !position.PositionCode.Equals(oldPosition.PositionCode))
                || (!string.IsNullOrEmpty(oldPosition.PositionCode) && !oldPosition.PositionCode.Equals(position.PositionCode)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[CreaterId] = @CreaterId ");
            }

            if (oldPosition.CreateTime.CompareTo(position.CreateTime) != 0 && position.CreateTime != DateTime.MinValue)
            {
                updateColCount++;
                query.AppendLine(@"  ,[CreateTime] = @CreateTime ");
            }

            if ((!string.IsNullOrEmpty(position.PositionCode) && !position.PositionCode.Equals(oldPosition.PositionCode))
                || (!string.IsNullOrEmpty(oldPosition.PositionCode) && !oldPosition.PositionCode.Equals(position.PositionCode)))
            {
                updateColCount++;
                query.AppendLine(@"  ,[UpdatorId] = @UpdatorId ");
            }

            if (oldPosition.UpdateTime.CompareTo(position.UpdateTime) != 0 && position.UpdateTime != DateTime.MinValue)
            {
                updateColCount++;
                query.AppendLine(@"  ,[UpdateTime] = @UpdateTime ");
            }

            query.AppendLine(@"  ,[RVersion] = @RVersion ");
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"   [PositionId] = @PositionId ");

            if (updateColCount == 0)
            {
                return;
            }

            position.UpdateTime = DateTime.Now;
            DBParamCollection<DBParam> paramCollection = new DBParamCollection<DBParam>();
            paramCollection.Add(new DBParam("@PositionId", position.PositionId, DbType.String, 40));
            paramCollection.Add(new DBParam("@PositionCode", position.PositionCode, DbType.String, 13));
            paramCollection.Add(new DBParam("@CompanyId", position.CompanyId, DbType.String, 40));
            paramCollection.Add(new DBParam("@DepartmentId", position.DepartmentId, DbType.String, 40));
            paramCollection.Add(new DBParam("@Name", position.Name, DbType.String, 100));
            paramCollection.Add(new DBParam("@RVersion", position.RVersion, DbType.Int32));
            paramCollection.Add(new DBParam("@Status", position.Status, DbType.Int32));
            paramCollection.Add(new DBParam("@CreaterId", position.CreaterId, DbType.String, 40));
            paramCollection.Add(new DBParam("@CreateTime", position.CreateTime, DbType.DateTime));
            paramCollection.Add(new DBParam("@UpdatorId", position.UpdatorId, DbType.String, 40));
            paramCollection.Add(new DBParam("@UpdateTime", position.UpdateTime, DbType.DateTime));

            try
            {
                int effectCount = 0;

                if (position != null)
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
                    throw new ResponseException((int)ResultCode.NoDataUpdate, position.PositionCode);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新职位数据
        /// </summary>
        /// <param name="positionList">职位实体对象</param>
        public void Update(IList<Position> positionList)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                if (positionList != null)
                {
                    foreach (Position position in positionList)
                    {
                        this.Update(position, tran);
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
        /// 更新职位数据
        /// </summary>
        /// <param name="positionList">职位实体对象</param>
        /// <param name="tran">中间事务对象</param>
        public void Update(IList<Position> positionList, ICTransaction tran)
        {
            try
            {
                if (positionList != null)
                {
                    foreach (Position position in positionList)
                    {
                        this.Update(position, tran);
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
        public PositionDal(MssqlDalFactoryBase dalFactory)
            : base(dalFactory)
        {

        }

        #endregion
    }
}
