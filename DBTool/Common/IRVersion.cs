namespace NFramework.DBTool.Common
{
    #region Reference

    

    #endregion

    /// <summary>
    /// 数据记录的版本接口
    /// </summary>
    public interface IRVersion
    {
        /// <summary>
        /// 记录数据的版本号
        /// </summary>
        int RVersion { get; set; }
    }
}
