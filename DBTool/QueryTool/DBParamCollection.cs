namespace NFramework.DBTool.QueryTool
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Data.Common;

    #endregion

    /// <summary>
    /// ���ݿ��������
    /// </summary>
    [Serializable]
	public class DBParamCollection : List<DbParameter>
    {
        #region Public Methods

        /// <summary>
        /// �ϲ�DBParamCollection����
        /// </summary>
        /// <param name="dbParamCollection">���ϲ���DBParamCollection����</param>
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
        /// ���캯��
        /// </summary>
        public DBParamCollection()
        {

        }

        #endregion
    }
}