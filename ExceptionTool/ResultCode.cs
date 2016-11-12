namespace NFramework.ExceptionTool
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion

    /// <summary>
    /// 结果码枚举
    /// </summary>
    public enum ResultCode : int
    {
        /// <summary>
        /// 失败
        /// </summary>
        Failed = -1,
        /// <summary>
        /// 成功
        /// </summary>
        Successfully = 200,

        #region DB Code

        /// <summary>
        /// 有外键关联错误
        /// </summary>
        FKError = 547,
        /// <summary>
        /// 唯一键错误
        /// </summary>
        UQError = 2627,
        /// <summary>
        /// 版本已更改
        /// </summary>
        VersionChanged = 110001,
        /// <summary>
        /// 没有数据被更新
        /// </summary>
        NoDataUpdate = 110002,
        /// <summary>
        /// 没有数据被删除
        /// </summary>
        NoDataDelete = 110003,
        /// <summary>
        /// 未添加任何数据
        /// </summary>
        NoDataInsert = 110004,
        /// <summary>
        /// 查找的数据不存在
        /// </summary>
        NoDataExists = 110005,
        
        #endregion

        #region Validate Code

        ValidError = 210001,

        #endregion

        #region Sys Error

        Warning = 100,

        Error = 300

        #endregion
    }
}
