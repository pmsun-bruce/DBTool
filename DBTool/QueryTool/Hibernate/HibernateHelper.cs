namespace NFramework.DBTool.QueryTool.Hibernate
{
    #region Reference

    using System;
    using System.Collections;
    using System.Collections.Generic;

    using NHibernate;
    using NFramework.DBTool.Common;
    using NFramework.DBTool.QueryTool;

    #endregion

    /// <summary>
    /// Hibernate操作帮助对象
    /// </summary>
    public class HibernateHelper
    {
        #region Public Methods

        #region Session Methods

        /// <summary>
        /// 清理Session
        /// </summary>
        /// <param name="session">需要清理的Session对象</param>
        public static void ClearSession(ISession session)
        {
            session.Clear();
        }

        /// <summary>
        /// 关闭Session
        /// </summary>
        /// <param name="session">需要关闭的Session对象</param>
        public static void CloseSession(ISession session)
        {
            if (session == null)
            {
                return;
            }

            session.Close();
        }

        /// <summary>
        /// 刷新Session
        /// </summary>
        /// <param name="session">需要刷新的Session对象</param>
        public static void FlushSession(ISession session)
        {
            if (session != null)
            {
                session.Flush();
            }
        }

        #endregion

        #region Object Operation Methods

        /// <summary>
        /// 从Session中移除加载的对象
        /// </summary>
        /// <param name="session">当前使用的Session对象</param>
        /// <param name="obj">需要移除的对象</param>
        public static void RemoveObject(ISession session, object obj)
        {
            session.Evict(obj);
        }

        /// <summary>
        /// 刷新Session中的对象
        /// </summary>
        /// <param name="session">当前使用的Session对象</param>
        /// <param name="obj">需要刷新的对象</param>
        public static void RefreshObject(ISession session, object obj)
        {
            session.Refresh(obj);
        }

        #region Add Object Methods

        /// <summary>
        /// 新建数据操作
        /// </summary>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="obj">需要新建的对象</param>
        /// <returns>返回执行结果对象</returns>
        public static object AddObject(ISession session, object obj)
        {
            session.Save(obj);
            return obj;
        }

        /// <summary>
        /// 批量新建数据操作
        /// </summary>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="objList">需要新建的对象列表</param>
        /// <returns>返回执行结果对象</returns>
        public static IList AddObject(ISession session, IList objList)
        {
            if (objList != null)
            {
                foreach (object obj in objList)
                {
                    session.Save(obj);
                }
            }

            return objList;
        }

        /// <summary>
        /// 新建数据操作
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="obj">需要新建的对象</param>
        /// <returns>返回执行结果对象</returns>
        public static T AddObject<T>(ISession session, T obj)
        {
            session.Save(obj);
            return obj;
        }

        /// <summary>
        /// 新建数据操作
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="objList">需要新建的对象列表</param>
        /// <returns>返回执行结果对象</returns>
        public static IList<T> AddObject<T>(ISession session, IList<T> objList)
        {
            if (objList != null)
            {
                foreach (object obj in objList)
                {
                    session.Save(obj);
                }
            }

            return objList;
        }

        #endregion

        #region Find Object Methods

        /// <summary>
        /// 根据ID获取数据对象
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="id">数据的ID</param>
        /// <returns>返回指定类型的数据对象</returns>
        public static T FindObjectById<T>(ISession session, object id)
        {
            return session.Get<T>(id);
        }

        /// <summary>
        /// 获取指定对象的所有数据对象
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <returns>返回指定类型的数据对象集合</returns>
        public static IList<T> FindObjectList<T>(ISession session) where T : class
        {
            ICriteria c = session.CreateCriteria<T>();
            return c.List<T>();
        }

        #endregion

        #region Update Object Methods

        /// <summary>
        /// 更新指定的数据对象
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="newObj">需要更新的数据对象</param>
        /// <returns>返回执行结果对象</returns>
        public static T UpdateObject<T>(ISession session, T newObj)
        {
            session.Update(newObj);
            return newObj;
        }

        /// <summary>
        /// 批量更新数据对象
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="newObjList"></param>
        /// <returns>返回执行结果对象</returns>
        public static IList<T> UpdateObject<T>(ISession session, IList<T> newObjList)
        {
            if (newObjList != null)
            {
                foreach (T obj in newObjList)
                {
                    HibernateHelper.UpdateObject<T>(session, obj);
                }
            }

            return newObjList;
        }
        
        #endregion

        #region Delete Methods

        /// <summary>
        /// 删除数据对象
        /// </summary>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="obj">需要删除的数据对象</param>
        /// <returns>返回执行结果对象</returns>
        public static void DeleteObject(ISession session, object obj)
        {
            session.Delete(obj);
        }

        /// <summary>
        /// 删除数据对象
        /// </summary>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="objList">需要删除的数据对象</param>
        /// <returns>返回执行结果对象</returns>
        public static void DeleteObject(ISession session, IList objList)
        {
            if (objList != null)
            {
                foreach (object obj in objList)
                {
                    session.Delete(obj);
                }
            }
        }

        /// <summary>
        /// 删除数据对象
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="obj">需要删除的数据对象</param>
        /// <returns>返回执行结果对象</returns>
        public static void DeleteObject<T>(ISession session, T obj)
        {
            session.Delete(obj);
        }

        /// <summary>
        /// 删除数据对象
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="objList">需要删除的数据对象列表</param>
        /// <returns>返回执行结果对象</returns>
        public static void DeleteObject<T>(ISession session, IList<T> objList)
        {
            if (objList != null)
            {
                foreach (object obj in objList)
                {
                    session.Delete(obj);
                }
            }
        }

        #endregion

        #endregion

        #region HQL Methods

        /// <summary>
        /// 通过HQL进行数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="hql">需要执行的HQL查询语句</param>
        /// <returns>返回查询到的对象集合</returns>
        public static IList<T> FindObjectListByHQL<T>(ISession session, string hql)
        {
            IQuery query = session.CreateQuery(hql);
            return query.List<T>();
        }

        /// <summary>
        /// 通过HQL进行数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="hql">需要执行的HQL查询语句</param>
        /// <param name="dbParamCollection">查询中的参数集合</param>
        /// <returns>返回查询到的对象集合</returns>
        public static IList<T> FindObjectListByHQL<T>(ISession session, string hql, DBParamCollection dbParamCollection)
        {
            IQuery query = session.CreateQuery(hql);
            Console.WriteLine("Parse Params Start:" + DateTime.Now.ToString("mm:ss fff"));
            HibernateHelper.FillQueryParams(query, dbParamCollection);
            Console.WriteLine("Parse Params End:" + DateTime.Now.ToString("mm:ss fff"));
            return query.List<T>();
        }

        /// <summary>
        /// 通过HQL进行数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="hql">需要执行的HQL查询语句</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查询到的对象集合</returns>
        public static IList<T> FindObjectListByHQL<T>(ISession session, string hql, Pager pager)
        {
            IQuery query = session.CreateQuery(hql);
            query.SetFirstResult(pager.StartRecord);
            query.SetMaxResults(pager.PageSize);
            return query.List<T>();
        }

        /// <summary>
        /// 通过HQL进行数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="hql">需要执行的HQL查询语句</param>
        /// <param name="dbParamCollection">查询中的参数集合</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查询到的对象集合</returns>
        public static IList<T> FindObjectListByHQL<T>(ISession session, string hql, DBParamCollection dbParamCollection, Pager pager)
        {
            IQuery query = session.CreateQuery(hql);
            HibernateHelper.FillQueryParams(query, dbParamCollection);
            query.SetFirstResult(pager.StartRecord);
            query.SetMaxResults(pager.PageSize);
            return query.List<T>();
        }

        /// <summary>
        /// 通过HQL进行数据对象的查询，查询出单个数据对象
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="hql">需要执行的HQL查询语句</param>
        /// <returns>返回查询到的数据对象</returns>
        public static T FindUniqueObjectByHQL<T>(ISession session, string hql)
        {
            return HibernateHelper.FindUniqueObjectByHQL<T>(session, hql, null);
        }

        /// <summary>
        /// 通过HQL进行数据对象的查询，查询出单个数据对象
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="hql">需要执行的HQL查询语句</param>
        /// <param name="dbParamCollection">查询中的参数集合</param>
        /// <returns>返回查询到的数据对象</returns>
        public static T FindUniqueObjectByHQL<T>(ISession session, string hql, DBParamCollection dbParamCollection)
        {
            IQuery query = session.CreateQuery(hql);
            HibernateHelper.FillQueryParams(query, dbParamCollection);
            return query.UniqueResult<T>();
        }

        /// <summary>
        /// 通过HQL进行数据的删除
        /// </summary>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="hql">需要执行的HQL的删除语句</param>
        /// <returns>返回删除的结果</returns>
        public static int DeleteObjectByHQL(ISession session, string hql)
        {
            return HibernateHelper.DeleteObjectByHQL(session, hql, null);
        }

        /// <summary>
        /// 通过HQL进行数据的删除
        /// </summary>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="hql">需要执行的HQL的删除语句</param>
        /// <param name="dbParamCollection">查询中的参数集合</param>
        /// <returns>返回删除的结果</returns>
        public static int DeleteObjectByHQL(ISession session, string hql, DBParamCollection dbParamCollection)
        {
            IQuery query = session.CreateQuery(hql);
            HibernateHelper.FillQueryParams(query, dbParamCollection);
            int count = query.ExecuteUpdate();
            return count;
        }

        #endregion

        #region SQL Methods

        /// <summary>
        /// 通过SQL进行数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="sql">需要执行的SQL查询语句</param>
        /// <param name="alias">需要进行填充的数据对象别名</param>
        /// <param name="entityClass">需要填充的数据对象类型</param>
        /// <returns>返回查询到的数据对象集合</returns>
        public static IList<T> FindObjectListBySQL<T>(ISession session, string sql, string alias, Type entityClass)
        {
            return HibernateHelper.FindObjectListBySQL<T>(session, sql, null, alias, entityClass, null);
        }

        /// <summary>
        /// 通过SQL进行数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="sql">需要执行的SQL查询语句</param>
        /// <param name="alias">需要进行填充的数据对象别名</param>
        /// <param name="entityClass">需要填充的数据对象类型</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查询到的数据对象集合</returns>
        public static IList<T> FindObjectListBySQL<T>(ISession session, string sql, string alias, Type entityClass, Pager pager)
        {
            return HibernateHelper.FindObjectListBySQL<T>(session, sql, null, alias, entityClass, pager);
        }

        /// <summary>
        /// 通过SQL进行数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="sql">需要执行的SQL查询语句</param>
        /// <param name="dbParamCollection">查询中的参数集合</param>
        /// <param name="alias">需要进行填充的数据对象别名</param>
        /// <param name="entityClass">需要填充的数据对象类型</param>
        /// <returns>返回查询到的数据对象集合</returns>
        public static IList<T> FindObjectListBySQL<T>(ISession session, string sql, DBParamCollection dbParamCollection, string alias, Type entityClass)
        {
            return HibernateHelper.FindObjectListBySQL<T>(session, sql, dbParamCollection, alias, entityClass, null);
        }

        /// <summary>
        /// 通过SQL进行数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="sql">需要执行的SQL查询语句</param>
        /// <param name="dbParamCollection">查询中的参数集合</param>
        /// <param name="alias">需要进行填充的数据对象别名</param>
        /// <param name="entityClass">需要填充的数据对象类型</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查询到的数据对象集合</returns>
        public static IList<T> FindObjectListBySQL<T>(ISession session, string sql, DBParamCollection dbParamCollection, string alias, Type entityClass, Pager pager)
        {
            IQuery query = session.CreateSQLQuery(sql).AddEntity(alias, entityClass);
            HibernateHelper.FillQueryParams(query, dbParamCollection);

            if (pager != null)
            {
                query.SetFirstResult(pager.StartRecord);
                query.SetMaxResults(pager.PageSize);
            }

            return query.List<T>();
        }

        /// <summary>
        /// 通过SQL进行数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="sql">需要执行的SQL查询语句</param>
        /// <returns>返回查询到的数据对象集合</returns>
        public static IList<T> FindObjectListBySQL<T>(ISession session, string sql)
        {
            return HibernateHelper.FindObjectListBySQL<T>(session, sql, (DBParamCollection)null, (Pager)null);
        }

        /// <summary>
        /// 通过SQL进行数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="sql">需要执行的SQL查询语句</param>
        /// <param name="dbParamCollection">查询中的参数集合</param>
        /// <returns>返回查询到的数据对象集合</returns>
        public static IList<T> FindObjectListBySQL<T>(ISession session, string sql, DBParamCollection dbParamCollection)
        {
            return HibernateHelper.FindObjectListBySQL<T>(session, sql, dbParamCollection, null);
        }

        /// <summary>
        /// 通过SQL进行数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="sql">需要执行的SQL查询语句</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查询到的数据对象集合</returns>
        public static IList<T> FindObjectListBySQL<T>(ISession session, string sql, Pager pager)
        {
            return HibernateHelper.FindObjectListBySQL<T>(session, sql, null, pager);
        }

        /// <summary>
        /// 通过SQL进行数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="sql">需要执行的SQL查询语句</param>
        /// <param name="dbParamCollection">查询中的参数集合</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查询到的数据对象集合</returns>
        public static IList<T> FindObjectListBySQL<T>(ISession session, string sql, DBParamCollection dbParamCollection, Pager pager)
        {
            IQuery query = session.CreateSQLQuery(sql);

            if (dbParamCollection != null)
            {
                HibernateHelper.FillQueryParams(query, dbParamCollection);
            }

            if (pager != null)
            {
                query.SetFirstResult(pager.StartRecord);
                query.SetMaxResults(pager.PageSize);
            }

            return query.List<T>();
        }

        /// <summary>
        /// 通过SQL进行单个数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="sql">需要执行的SQL查询语句</param>
        /// <returns>返回查询到的数据对象</returns>
        public static T FindUniqueObjectBySQL<T>(ISession session, string sql)
        {
            return HibernateHelper.FindUniqueObjectBySQL<T>(session, sql, null);
        }

        /// <summary>
        /// 通过SQL进行单个数据对象的查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="sql">需要执行的SQL查询语句</param>
        /// <param name="dbParamCollection">查询中的参数集合</param>
        /// <returns>返回查询到的数据对象</returns>
        public static T FindUniqueObjectBySQL<T>(ISession session, string sql, DBParamCollection dbParamCollection)
        {
            IQuery query = session.CreateSQLQuery(sql);
            HibernateHelper.FillQueryParams(query, dbParamCollection);

            return query.UniqueResult<T>();
        }

        /// <summary>
        /// 通过SQL进行数据对象的删除
        /// </summary>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="sql">需要执行的SQL查询语句</param>
        /// <returns>返回删除结果对象</returns>
        public static int DeleteObjectBySQL(ISession session, string sql)
        {
            return HibernateHelper.DeleteObjectBySQL(session, sql, null);
        }

        /// <summary>
        /// 通过SQL进行数据对象的删除
        /// </summary>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="sql">需要执行的SQL查询语句</param>
        /// <param name="dbParamCollection">查询中的参数集合</param>
        /// <returns>返回删除结果对象</returns>
        public static int DeleteObjectBySQL(ISession session, string sql, DBParamCollection dbParamCollection)
        {
            IQuery query = session.CreateSQLQuery(sql);

            if (dbParamCollection != null)
            {
                HibernateHelper.FillQueryParams(query, dbParamCollection);
            }
            
            int count = query.ExecuteUpdate();
            return count;
        }

        #endregion

        #region Execute Name Query

        /// <summary>
        /// 通过配置的SQL名称执行SQL查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="queryName">配置的查询名称</param>
        /// <param name="dbParamCollection">查询中的参数集合</param>
        /// <returns>返回查询到的数据对象集合</returns>
        public static IList<T> ExecuteNameQuery<T>(ISession session, string queryName, DBParamCollection dbParamCollection)
        {
            HibernateHelper.FlushSession(session);
            IQuery query = session.GetNamedQuery(queryName);
            HibernateHelper.FillQueryParams(query, dbParamCollection);

            return query.List<T>();
        }

        /// <summary>
        /// 通过配置的SQL名称执行SQL查询
        /// </summary>
        /// <typeparam name="T">泛型类型，由用户定义</typeparam>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="queryName">配置的查询名称</param>
        /// <returns>返回查询到的数据对象集合</returns>
        public static IList<T> ExecuteNameQuery<T>(ISession session, string queryName)
        {
            return HibernateHelper.ExecuteNameQuery<T>(session, queryName, null);
        }

        /// <summary>
        /// 通过配置的SQL名称执行SQL查询
        /// </summary>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="queryName">配置的查询名称</param>
        /// <param name="dbParamCollection">查询中的参数集合</param>
        /// <returns>返回查询到的数据对象集合</returns>
        public static IList ExecuteNameQuery(ISession session, string queryName, DBParamCollection dbParamCollection)
        {
            HibernateHelper.FlushSession(session);
            IQuery query = session.GetNamedQuery(queryName);
            HibernateHelper.FillQueryParams(query, dbParamCollection);

            return query.List();
        }

        /// <summary>
        /// 通过配置的SQL名称执行SQL查询
        /// </summary>
        /// <param name="session">需要执行操作的Session</param>
        /// <param name="queryName">配置的查询名称</param>
        /// <returns>返回查询到的数据对象集合</returns>
        public static IList ExecuteNameQuery(ISession session, string queryName)
        {
            return HibernateHelper.ExecuteNameQuery(session, queryName, null);
        }

        #endregion

        #region Param Methods

        /// <summary>
        /// 将参数填充的Hibernate的查询对象中
        /// </summary>
        /// <param name="query">Hibernate查询对象</param>
        /// <param name="dbParamCollection">查询中的参数集合</param>
        public static void FillQueryParams(IQuery query, DBParamCollection dbParamCollection)
        {
            if (dbParamCollection == null) return;

            foreach (DBParam param in dbParamCollection)
            {
                if (param is IEnumerable)
                {
                    query.SetParameterList(param.ParameterName, (IList)param.Value);
                }
                else
                {
                    query.SetParameter(param.ParameterName, param.Value);
                }
            }
        }

        #endregion

        #endregion

        #region Private Constructor

        /// <summary>
        /// 私有构造函数，不能实例化对象
        /// </summary>
        private HibernateHelper()
        {

        }

        #endregion
    }
}
