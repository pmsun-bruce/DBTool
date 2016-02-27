namespace NFramework.DBTool.QueryTool
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Data.Common;

    #endregion

    /// <summary>
    /// 数据库参数集合
    /// </summary>
    [Serializable]
	public class DBParamCollection : List<DbParameter>
    {
        #region Public Methods

        /// <summary>
        /// 合并DBParamCollection集合
        /// </summary>
        /// <param name="dbParamCollection">被合并的DBParamCollection集合</param>
        public void Merge(DBParamCollection dbParamCollection)
        {
            if (dbParamCollection == null)
            {
                return;
            }
            
            this.AddRange(dbParamCollection);
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public DBParamCollection()
        {

        }

        #endregion
    }
}