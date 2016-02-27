using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NFramework.DBTool.QueryTool.Hibernate;

namespace NFramework.DBTool.Test.IDal
{
    /// <summary>
    /// Dal管理类
    /// </summary>
    public class DalManager
    {
        /// <summary>
        /// Dal工厂类
        /// </summary>
        private static IDalFactory dalFactory;
        /// <summary>
        /// Dal工厂类，只读属性
        /// </summary>
        public static IDalFactory DalFactory
        {
            get
            {
                return DalManager.dalFactory;
            }
        }

        /// <summary>
        /// 加载Dal工厂
        /// </summary>
        /// <param name="dalFactory">实际运用的Dal工厂类</param>
        public static void Load(IDalFactory dalFactory)
        {
            DalManager.dalFactory = dalFactory;
        }
    }
}
