namespace NFramework.DBTool.QueryTool
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Data;

    #endregion

    /// <summary>
    /// 查询对象的字段对象
    /// </summary>
    [Serializable]
    public class SearchColumn
    {
        #region Fields & Properties

        /// <summary>
        /// 字段名称
        /// </summary>
        private string columnName;
        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName
        {
            get
            {
                return this.columnName;
            }

            set
            {
                this.columnName = value;
            }
        }

        /// <summary>
        /// 自定义条件字符串
        /// </summary>
        private string conditionString;
        /// <summary>
        /// 自定义条件字符串
        /// </summary>
        public string ConditionString
        {
            get
            {
                return this.conditionString;
            }
            set
            {
                this.conditionString = value;
            }
        }

        /// <summary>
        /// 排序条件的先后
        /// </summary>
        private int sortIndex;
        /// <summary>
        /// 排序条件的先后
        /// </summary>
        public int SortIndex
        {
            get
            {
                return this.sortIndex;
            }
            set
            {
                this.sortIndex = value;
            }
        }

        /// <summary>
        /// 排序的条件，升序或降序
        /// </summary>
        private SortOrder sortOrder;
        /// <summary>
        /// 排序的条件，升序或降序
        /// </summary>
        public SortOrder SortOrder
        {
            get
            {
                return this.sortOrder;
            }
            set
            {
                this.sortOrder = value;
            }
        }

        /// <summary>
        /// 自定义排序字符串，如可以使用一些SQL函数转换字段类型，再排序
        /// </summary>
        private string sortString;
        /// <summary>
        /// 自定义排序字符串，如可以使用一些SQL函数转换字段类型，再排序
        /// </summary>
        public string SortString
        {
            get
            {
                return string.IsNullOrEmpty(this.sortString) ? "" : this.sortString.Replace(SQLPlaceholder.ColName, this.ColumnName).Replace(SQLPlaceholder.TableName, this.CurrentSearcher != null ? this.CurrentSearcher.TableName : "");
            }
            set
            {
                this.sortString = value;
            }
        }

        /// <summary>
        /// 条件集合
        /// 可以给一个字段赋多个不同的条件
        /// </summary>
        private List<Condition> conditionCollection;
        /// <summary>
        /// 条件集合
        /// 可以给一个字段赋多个不同的条件
        /// </summary>
        public List<Condition> ConditionCollection
        {
            get
            {
                if (this.conditionCollection == null)
                {
                    this.conditionCollection = new List<Condition>();
                }

                return this.conditionCollection;
            }
        }

        /// <summary>
        /// 数据库字段类型
        /// </summary>
        private DbType dbType = DbType.Object;
        /// <summary>
        /// 数据库字段类型
        /// </summary>
        public DbType DBType
        {
            get
            {
                return this.dbType;
            }
            set
            {
                this.dbType = value;
            }
        }

        /// <summary>
        /// 当前字段是否显示查询结果
        /// </summary>
        private bool isGet = true;
        /// <summary>
        /// 当前字段是否显示查询结果
        /// </summary>
        public bool IsGet
        {
            get
            {
                return this.isGet;
            }
            set
            {
                this.isGet = value;
            }
        }

        /// <summary>
        /// 包含当前字段的查询对象
        /// 用于后期拼接查询字符串时拼接表名
        /// 此为只读属性
        /// </summary>
        private Searcher currentSearcher;
        /// <summary>
        /// 包含当前字段的查询对象
        /// 用于后期拼接查询字符串时拼接表名
        /// 此为只读属性
        /// </summary>
        public Searcher CurrentSearcher
        {
            get
            {
                return this.currentSearcher;
            }
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// 添加条件到条件集合中
        /// </summary>
        /// <param name="con">条件对象</param>
        public void AddCondition(Condition con)
        {
            con.SearchColumn = this;
            this.ConditionCollection.Add(con);
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="searcher">该字段所在的查询对象</param>
        /// <param name="columnName">字段名称</param>
        public SearchColumn(Searcher searcher, string columnName)
        {
            this.sortOrder = SortOrder.None;
            this.sortIndex = 1;
            this.columnName = columnName;
            this.currentSearcher = searcher;
        }

        #endregion
    }
}
