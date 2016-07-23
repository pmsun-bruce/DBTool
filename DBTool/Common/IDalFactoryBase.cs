namespace NFramework.DBTool.Common
{
    #region Reference

    

    #endregion

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

        bool IsExistTable(string tableName);

        bool IsExistTable(string tableName, ICTransaction tran);

        bool IsExistColumn(string tableName, string columnName);

        bool IsExistColumn(string tableName, string columnName, ICTransaction tran);
    }
}
