namespace NFramework.DBTool.Common
{
    /// <summary>
    /// Dal工厂基础接口
    /// </summary>
    public interface IDalFactoryBase
    {
        /// <summary>
        /// 创建开启一个中间事务对象
        /// </summary>
        /// <returns>返回中间事务对象</returns>
        ICTransaction BeginTransaction();
    }
}
