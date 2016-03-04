namespace NFramework.DBTool.QueryTool.Hibernate
{
    #region Reference
    
    using System.Collections.Generic;

    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Context;

    #endregion

    /// <summary>
    /// Hibernate的配置对象
    /// 多数据库可有多个配置对象
    /// 配置对象可以加载Hibernate配置和创建Session
    /// </summary>
    public class HibernateConfig
    {
        #region Fields & Properties

        /// <summary>
        /// 当前配置的名称，用于标识一个配置对象
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 配置文件的完整路径
        /// </summary>
        public string ConfigFile { get; set; }

        /// <summary>
        /// 需要进行映射的HBM文件所在的所有程序集名称的集合
        /// </summary>
        private IList<string> assemblyList;

        /// <summary>
        /// 需要进行映射的HBM文件所在的所有程序集名称的集合
        /// </summary>
        public IList<string> AssemblyList
        {
            get
            {
                if (this.assemblyList == null)
                {
                    this.assemblyList = new List<string>();
                }

                return this.assemblyList;
            }
            set
            {
                this.assemblyList = value;
            }
        }

        /// <summary>
        /// Session工厂
        /// </summary>
        public ISessionFactory SessionFactory { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// 将当前Session工厂绑定到当前上下文中
        /// </summary>
        private void BindContext()
        {
            if (!CurrentSessionContext.HasBind(this.SessionFactory))
            {
                CurrentSessionContext.Bind(this.SessionFactory.OpenSession());
            }
        }

        /// <summary>
        /// 将当前的Session工厂从当前上下文中取消绑定
        /// </summary>
        private void UnBindContext()
        {
            if (CurrentSessionContext.HasBind(this.SessionFactory))
            {
                CurrentSessionContext.Unbind(this.SessionFactory);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取当前的Session
        /// </summary>
        /// <returns></returns>
        public ISession GetCurrentSession()
        {
            this.BindContext();

            var session = this.SessionFactory.GetCurrentSession();

            return this.SessionFactory.GetCurrentSession();
        }

        /// <summary>
        /// 加载Hibernate的配置
        /// </summary>
        public void Config()
        {
            var hbConf = new Configuration();

            hbConf = hbConf.Configure(this.ConfigFile);

            if (this.AssemblyList != null)
            {
                // 循环加载所有的映射对象
                foreach (string assembly in this.AssemblyList)
                {
                    hbConf.AddAssembly(assembly);
                }
            }

            this.SessionFactory = hbConf.BuildSessionFactory();
        }

        #endregion
    }
}
