namespace NFramework.DBTool.QueryTool
{
    #region Reference

    using System;
    using System.Collections.Generic;
    
    using NFramework.ObjectTool;

    #endregion

    /// <summary>
    /// 查询对象的基类
    /// </summary>
    [Serializable]
    public class Searcher
    {
        #region Fields & Properties

        /// <summary>
        /// 该查询对象所对应的表名;在多表查询时将设置成别名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 有条件的字段集合
        /// </summary>
        public IList<SearchColumn> ConditionColumnList { get; private set; }

        /// <summary>
        /// 有关联的查询对象集合
        /// </summary>
        public IList<Searcher> RelationSearcherList { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 克隆一份Searcher对象
        /// </summary>
        /// <returns></returns>
        public virtual Searcher Clone()
        {
            return ObjectFactory.Clone(this);
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public Searcher()
        {
            this.RelationSearcherList = new List<Searcher>();
            this.ConditionColumnList = new List<SearchColumn>();
            this.TableName = string.Empty;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName">该查询对象所对应的表名</param>
        public Searcher(string tableName) : this()
        {
            this.TableName = tableName;
        }

        #endregion
    }
}
