namespace NFramework.DBTool.Common
{
    #region Reference

    

    #endregion

    /// <summary>
    /// 数据操作基本接口，用于如中间事务(ICTransaction)等的参数传递
    /// </summary>
    public interface IDalBase
    {
        /// <summary>
        /// 创建当前数据接口的工厂类
        /// </summary>
        IDalFactoryBase DalFactory { get; set; }
    }
}
