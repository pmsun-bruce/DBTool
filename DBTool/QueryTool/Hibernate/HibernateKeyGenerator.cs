namespace NFramework.DBTool.QueryTool.Hibernate
{
    #region Reference
    
    using NHibernate.Engine;
    using NHibernate.Id;

    #endregion

    /// <summary>
    /// Hibernate的通用主键生成对象
    /// </summary>
    public class HibernateKeyGenerator : IIdentifierGenerator
    {
        /// <summary>
        /// 主键生成方法
        /// </summary>
        /// <param name="session">需要生成主键的Session</param>
        /// <param name="obj">需要生成主键的对象</param>
        /// <returns>返回主键</returns>
        public object Generate(ISessionImplementor session, object obj)
        {
            return NFramework.DBTool.Common.KeyGenerator.GenNewGuidKey();
        }
    }
}
