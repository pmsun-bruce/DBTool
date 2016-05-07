namespace NFramework.DBTool.Common
{
    #region Reference

    using System;

    #endregion

    /// <summary>
    /// 主键生成器，用于通用主键生成方法定义
    /// </summary>
    public class KeyGenerator
    {
        /// <summary>
        /// 生成一个GUID，去除了-，并全部转为小写
        /// </summary>
        /// <returns>返回一个去除了-并全部转为小写的GUID字符串</returns>
        public static string GenNewGuidKey()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
