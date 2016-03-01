namespace NFramework.DBTool.Common
{
    /// <summary>
    /// 事务接口对象,用于屏蔽使用Hibernate或不同数据库所带来的差异
    /// </summary>
    public interface ICTransaction
    {
        /// <summary>
        /// 创建并开始一个事务
        /// </summary>
        /// <param name="dalFactory">传入一个具体的DalFactory对象，DalFactory对象含有数据库连接等信息</param>
        void Begin(IDalFactoryBase dalFactory);

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollBack();
    }
}
