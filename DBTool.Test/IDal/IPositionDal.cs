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
    /// 职位数据接口
    /// </summary>
    public interface IPositionDal : NFramework.DBTool.Common.IDalBase
    {
        /// <summary>
        /// 新建职位
        /// </summary>
        /// <param name="position">职位实体对象</param>
        /// <returns>返回处理后的职位实体对象</returns>
        Position Add(Position position);
        /// <summary>
        /// 新建职位
        /// </summary>
        /// <param name="position">职位实体对象</param>
        /// <param name="tran">事务对象</param>
        /// <returns>返回处理后的职位实体对象</returns>
        Position Add(Position position, ICTransaction tran);
        /// <summary>
        /// 批量新建职位
        /// </summary>
        /// <param name="positionList">职位实体对象集合</param>
        /// <returns>返回处理后的职位实体对象集合</returns>
        IList<Position> Add(IList<Position> positionList);
        /// <summary>
        /// 批量新建职位
        /// </summary>
        /// <param name="positionList">职位实体对象集合</param>
        /// <param name="tran">事务对象</param>
        /// <returns>返回处理后的职位实体对象集合</returns>
        IList<Position> Add(IList<Position> positionList, ICTransaction tran);
        /// <summary>
        /// 查询职位数量
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <returns>返回查询到的数量</returns>
        long Count(PositionSearcher positionSearcher);
        /// <summary>
        /// 查询职位数量
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="tran">事务对象</param>
        /// <returns>返回查询到的数量</returns>
        long Count(PositionSearcher positionSearcher, ICTransaction tran);
        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="positionId">职位ID</param>
        void Delete(string positionId);
        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="positionId">职位ID</param>
        /// <param name="tran">中间事务对象</param>
        void Delete(string positionId, ICTransaction tran);
        /// <summary>
        /// 根据指定条件删除职位
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        void Delete(PositionSearcher positionSearcher);
        /// <summary>
        /// 根据指定条件删除职位
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="tran">中间事务对象</param>
        void Delete(PositionSearcher positionSearcher, ICTransaction tran);
        /// <summary>
        /// 查找指定ID的职位
        /// </summary>
        /// <param name="positionId">职位ID</param>
        /// <returns>返回职位实体对象</returns>
        Position FindSingle(string positionId);
        /// <summary>
        /// 查找指定ID的职位
        /// </summary>
        /// <param name="positionId">职位ID</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回职位实体对象</returns>
        Position FindSingle(string positionId, ICTransaction tran);
        /// <summary>
        /// 查找指定条件的职位集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <returns>返回职位实体对象集合</returns>
        IList<Position> FindList(PositionSearcher positionSearcher);
        /// <summary>
        /// 查找指定条件的职位集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回职位实体对象集合</returns>
        IList<Position> FindList(PositionSearcher positionSearcher, ICTransaction tran);
        /// <summary>
        /// 查找指定条件的职位分页集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        PageList<Position> FindList(PositionSearcher positionSearcher, Pager pager);
        /// <summary>
        /// 查找指定条件的职位分页集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的对象集合</returns>
        PageList<Position> FindList(PositionSearcher positionSearcher, Pager pager, ICTransaction tran);
        /// <summary>
        /// 根据指定条件查找职位集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <returns>返回一个DataTable对象</returns>
        DataTable FindDataTable(PositionSearcher positionSearcher);
        /// <summary>
        /// 根据指定条件查找职位集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回一个DataTable对象</returns>
        DataTable FindDataTable(PositionSearcher positionSearcher, ICTransaction tran);
        /// <summary>
        /// 根据指定条件查找职位分页集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        PageDataTable FindDataTable(PositionSearcher positionSearcher, Pager pager);
        /// <summary>
        /// 根据指定条件查找职位分页集合
        /// </summary>
        /// <param name="positionSearcher">职位查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        PageDataTable FindDataTable(PositionSearcher positionSearcher, Pager pager, ICTransaction tran);
        /// <summary>
        /// 更新职位数据
        /// </summary>
        /// <param name="position">职位实体对象</param>
        void Update(Position position);
        /// <summary>
        /// 更新职位数据
        /// </summary>
        /// <param name="position">职位实体对象</param>
        /// <param name="tran">中间事务对象</param>
        void Update(Position position, ICTransaction tran);
        /// <summary>
        /// 更新职位数据
        /// </summary>
        /// <param name="positionList">职位实体对象</param>
        void Update(IList<Position> positionList);
        /// <summary>
        /// 更新职位数据
        /// </summary>
        /// <param name="positionList">职位实体对象</param>
        /// <param name="tran">中间事务对象</param>
        void Update(IList<Position> positionList, ICTransaction tran);
    }
}
