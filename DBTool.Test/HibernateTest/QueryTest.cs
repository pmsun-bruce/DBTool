using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;

using NFramework.DBTool.Common;
using NFramework.DBTool.QueryTool;
using NFramework.DBTool.QueryTool.Hibernate;
using NFramework.ExceptionTool;

using NFramework.DBTool.Test.Entity;
using NFramework.DBTool.Test.IDal;
using NFramework.DBTool.Test.Handler;
using NFramework.DBTool.Test.HibernateTest.Dal;
using NFramework.DBTool.Test.Searcher;

namespace NFramework.DBTool.Test.HibernateTest
{
    /// <summary>
    /// Hibernate单元测试
    /// </summary>
    [TestClass]
    public class QueryTest
    {
        public QueryTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Test Params

        /// <summary>
        /// Hibernate配置存储对象
        /// </summary>
        private static HibernateConfig hConfig;
        /// <summary>
        /// Hibernate配置存储对象
        /// </summary>
        public static HibernateConfig HConfig
        {
            get
            {
                return hConfig;
            }
            set
            {
                hConfig = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static List<string> employeeIdList;
        /// <summary>
        /// 
        /// </summary>
        public static List<string> EmployeeIdList
        {
            get
            {
                if (employeeIdList == null)
                {
                    employeeIdList = new List<string>();
                }

                return employeeIdList;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static List<string> employeeCodeList;
        /// <summary>
        /// 
        /// </summary>
        public static List<string> EmployeeCodeList
        {
            get
            {
                if (employeeCodeList == null)
                {
                    employeeCodeList = new List<string>();
                }

                return employeeCodeList;
            }
        }

        #endregion

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) 
        {
            employeeIdList = null;

            log4net.Config.XmlConfigurator.Configure();
            log4net.ILog logger = log4net.LogManager.GetLogger("DBTool Hibernate Test");
            logger.Debug("Start Test");
            
            HConfig = new HibernateConfig();
            HConfig.ConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NHibernate.MSSQL.cfg.xml");
            HConfig.AssemblyList.Add("NFramework.DBTool.Test");
            HConfig.Config();
            IDalFactory dalFactory = new DalFactory(HConfig);
            NFramework.DBTool.Test.IDal.DalManager.Load(dalFactory);
        }
        
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        [ClassCleanup()]
        public static void MyClassCleanup() { }
        
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        [TestInitialize()]
        public void MyTestInitialize() { }
        
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        [TestCleanup()]
        public void MyTestCleanup() { }
        
        #endregion

        /// <summary>
        /// 清除所有数据，并重新添加测试数据
        /// Company 5
        /// 1 Company 6 Department
        /// 1 Department 2 Position 1 GM 30 Employee
        /// </summary>
        [TestMethod()]
        public void AddDataTest()
        {
            #region Clear All Data

            OrgHandler.DeleteEmployee(new EmployeeSearcher());
            OrgHandler.DeletePosition(new PositionSearcher());
            OrgHandler.DeleteDepartment(new DepartmentSearcher());
            OrgHandler.DeleteCompany(new CompanySearcher());

            #endregion

            string companyName = " Com. Ltd.";
            Company company = null;
            Department department = null;
            Position position = null;
            Employee employee = null;
            int depCode = 1;
            int posCode = 1;
            int empCode = 1;
            int empCount = 30;
            Random rand = new Random(DateTime.Now.Hour);

            string[][] companyNamePool = new string[][] { 
                new string[]{"ABC","10201"},
                new string[]{"DEF","10301"},
                new string[]{"GHI","10202"},
                new string[]{"JKL","10203"},
                new string[]{"MNO","10401"}
            };

            string[] departmentNamePool = new string[]{
                "GM",
                "Finance",
                "HR",
                "Sales",
                "Marketing",
                "Executive"
            };

            string[] positionNamePool = new string[] { 
                "GM",
                "Specialist"
            };

            long allEmpCount = 780;

            foreach (string[] cName in companyNamePool)
            {
                company = new Company();
                company.Name = cName[0] + companyName;
                company.CompanyCode = cName[1];
                company.CreaterId = "00000000000";
                company.UpdatorId = "00000000000";
                company.RVersion = 1;
                company.Status = 1;
                OrgHandler.AddCompany(company);
                depCode = 1;
                empCode = 1;
                posCode = 1;

                foreach (string dName in departmentNamePool)
                {
                    department = new Department();
                    department.CurrCompany = company;
                    department.Name = dName;
                    department.DepartmentCode = "2" + company.CompanyCode + string.Format("{0:0000}", depCode);
                    department.CreaterId = "00000000000";
                    department.UpdatorId = "00000000000";
                    department.RVersion = 1;
                    department.Status = 1;
                    OrgHandler.AddDepartment(department);

                    foreach (string pName in positionNamePool)
                    {
                        position = new Position();
                        position.CurrCompany = company;
                        position.CurrDepartment = department;
                        position.PositionCode = "3" + company.CompanyCode + string.Format("{0:0000}", posCode);
                        position.CreaterId = "00000000000";
                        position.UpdatorId = "00000000000";
                        position.RVersion = 1;
                        position.Status = 1;

                        if (pName.Equals("GM"))
                        {
                            if (dName.Equals("GM"))
                            {
                                position.Name = pName;
                            }
                            else
                            {
                                position.Name = dName + " " + pName;
                            }

                            empCount = 1;
                        }
                        else
                        {
                            position.Name = dName + " " + pName;

                            empCount = 30;
                        }

                        OrgHandler.AddPosition(position);

                        for (int i = 1; i <= empCount; i++)
                        {
                            employee = new Employee();
                            employee.Name = "Emp" + Guid.NewGuid().ToString().ToLower().Replace("-", "").Substring(0, 5);
                            employee.EmployeeCode = "5" + company.CompanyCode + string.Format("{0:000000}", empCode);
                            employee.CurrCompany = company;
                            employee.CurrDepartment = department;
                            employee.CurrPosition = position;
                            employee.CreaterId = "00000000000";
                            employee.UpdatorId = "00000000000";
                            employee.RVersion = 1;
                            employee.Status = 1;
                            employee.Birthday = Convert.ToDateTime("19" + rand.Next(70,90).ToString() + "-" + rand.Next(1, 9).ToString() + "-" + rand.Next(1, 28).ToString());
                            employee.Sex = rand.Next(1, 2);
                            employee.StartWorkDate = Convert.ToDateTime(employee.Birthday).AddYears(20).AddMonths(rand.Next(1, 12));
                            employee.JoinDate = rand.Next(1, 10) > 6 ? employee.StartWorkDate : Convert.ToDateTime(employee.StartWorkDate).AddYears(rand.Next(1, 2)).AddMonths(rand.Next(1, 12));

                            if (dName.Equals("GM"))
                            {
                                employee.Rand = 20;
                            }
                            else if (pName.Equals("GM"))
                            {
                                employee.Rand = rand.Next(15, 19);
                            }
                            else
                            {
                                employee.Rand = rand.Next(5, 14);
                            }

                            OrgHandler.AddEmployee(employee);

                            if(QueryTest.EmployeeIdList.Count < 10 && !dName.Equals("GM") && !pName.Equals("GM"))
                            {
                                QueryTest.EmployeeIdList.Add(employee.EmployeeId);
                                QueryTest.EmployeeCodeList.Add(employee.EmployeeCode);
                            }

                            empCode++;
                        }

                        posCode++;

                        if (dName.Equals("GM"))
                        {
                            break;
                        }
                    }

                    depCode++;
                }
            }

            EmployeeSearcher empSearcher = new EmployeeSearcher();
            long newDataCount = OrgHandler.CountEmployee(empSearcher);
            Assert.AreEqual(allEmpCount, newDataCount);
        }

        /// <summary>
        /// 单记录更新
        /// </summary>
        [TestMethod()]
        public void UpdateBySingleDataTest()
        {
            Employee emp = OrgHandler.FindEmployeeById(QueryTest.EmployeeIdList[0]);
            emp.Birthday = DateTime.Parse("1995-10-11");
            emp.UpdatorId = QueryTest.EmployeeIdList[1];
            OrgHandler.UpdateEmployee(emp);
            HibernateHelper.RemoveObject(HConfig.GetCurrentSession(), emp);
            Employee newEmp = OrgHandler.FindEmployeeById(QueryTest.EmployeeIdList[0]);
            Assert.AreEqual(emp.Birthday, newEmp.Birthday);
        }

        /// <summary>
        /// 多记录更新
        /// </summary>
        [TestMethod()]
        public void UpdateByMultipleDataTest()
        {
            int rand1 = 0;
            int rand2 = 0;
            List<Employee> employeeList = new List<Employee>();
            Employee emp = OrgHandler.FindEmployeeById(QueryTest.EmployeeIdList[0]);
            rand1 = emp.Rand + 1;
            emp.Rand = rand1;
            employeeList.Add(emp);
            emp = OrgHandler.FindEmployeeById(QueryTest.EmployeeIdList[1]);
            rand2 = emp.Rand + 1;
            emp.Rand = rand2;
            employeeList.Add(emp);
            OrgHandler.UpdateEmployee(employeeList);
            HibernateHelper.RemoveObject(HConfig.GetCurrentSession(), employeeList[0]);
            HibernateHelper.RemoveObject(HConfig.GetCurrentSession(), employeeList[1]);
            emp = OrgHandler.FindEmployeeById(QueryTest.EmployeeIdList[0]);
            Assert.AreEqual(rand1, emp.Rand);
            emp = OrgHandler.FindEmployeeById(QueryTest.EmployeeIdList[1]);
            Assert.AreEqual(rand2, emp.Rand);
        }

        /// <summary>
        /// 分页测试
        /// </summary>
        [TestMethod()]
        public void FindEmployeeForPagerTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.CurrPosition = new PositionSearcher();
            empSearcher.CurrPosition.PositionCode.Equal("3102010003");
            Pager pager = new Pager();
            pager.CurrentPage = 2;
            pager.PageSize = 15;
            PageList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher, pager);
            int pageCount = 15;
            int allCount = 30;
            Assert.AreEqual(pageCount, employeeList.RecordList.Count);
            Assert.AreEqual(allCount, employeeList.TotalCount);
        }


        /// <summary>
        /// Between条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByBetweenTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.Birthday.Between(Convert.ToDateTime("1995-10-01"), Convert.ToDateTime("1995-11-01"));
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int exCount = 1;
            Assert.AreEqual(exCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// Between条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByBetweenConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // 这句应该写入Dal进行控制，这里用于测试
            empSearcher.Birthday.ConditionString = "MONTH(" + SQLPlaceholder.ColName + ")";
            empSearcher.Birthday.Between(10, 11);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int exCount = 1;
            Assert.AreEqual(exCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// Between条件，用其他字段作为查询值
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByBetweenColumnTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.JoinDate.Between(empSearcher.StartWorkDate, empSearcher.CreateTime);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            bool isSucc = employeeList == null ? false : (employeeList.Count >= 1 && employeeList.Count <= 780 ? true : false);
            Assert.AreEqual(true, isSucc);
        }

        /// <summary>
        /// Equal条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByEqualTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.CurrPosition = new PositionSearcher();
            empSearcher.CurrPosition.PositionCode.Equal("3102010003");
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 30;
            Assert.AreEqual(allCount, employeeList.Count);
        }

        /// <summary>
        /// Equal条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByEqualCondititonStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // 这句应该写入Dal进行控制，这里用于测试
            empSearcher.Birthday.ConditionString = "MONTH(" + SQLPlaceholder.ColName + ")";
            empSearcher.Birthday.Equal(10);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int exCount = 1;
            Assert.AreEqual(exCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// Equal条件，用其他字段作为查询值
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByEqualColumnTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.StartWorkDate.Equal(empSearcher.JoinDate);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            bool isSucc = employeeList == null ? false : (employeeList.Count > 1  ? true : false);
            Assert.AreEqual(true, isSucc);
        }

        /// <summary>
        /// NotEqual条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByNotEqualTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.CurrPosition = new PositionSearcher();
            empSearcher.CurrPosition.PositionCode.NotEqual("3102010003");
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 750;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// NotEqual条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByNotEqualConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // 这句应该写入Dal进行控制，这里用于测试
            empSearcher.Birthday.ConditionString = "MONTH(" + SQLPlaceholder.ColName + ")";
            empSearcher.Birthday.NotEqual(10);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 779;
            Assert.AreEqual(allCount, 779);
        }

        /// <summary>
        /// NotEqual条件，用其他字段作为查询值
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByNotEqualColumnTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.StartWorkDate.NotEqual(empSearcher.JoinDate);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            bool isSucc = employeeList == null ? false : (employeeList.Count >= 1 ? true : false);
            Assert.AreEqual(true, isSucc);
        }

        /// <summary>
        /// In条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByInTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.EmployeeCode.In(QueryTest.EmployeeCodeList.ToArray());
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 10;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// In条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByInConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // 这句应该写入Dal进行控制，这里用于测试
            empSearcher.EmployeeCode.ConditionString = "LOWER(" + SQLPlaceholder.ColName + ")";
            empSearcher.EmployeeCode.In(QueryTest.EmployeeCodeList.ToArray());
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            Assert.AreEqual(10, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// In条件，用其他字段作为查询值
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByInColumnTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.JoinDate.In(new SearchColumn[] { empSearcher.StartWorkDate });
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            bool isSucc = employeeList == null ? false : (employeeList.Count >= 1 ? true : false);
            Assert.AreEqual(true, isSucc);
        }

        /// <summary>
        /// NotIn条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByNotInTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.EmployeeCode.NotIn(QueryTest.EmployeeCodeList.ToArray());
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 770;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// NotIn条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByNotInConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // 这句应该写入Dal进行控制，这里用于测试
            empSearcher.EmployeeCode.ConditionString = "LOWER(" + SQLPlaceholder.ColName + ")";
            empSearcher.EmployeeCode.NotIn(QueryTest.EmployeeCodeList.ToArray());
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            Assert.AreEqual(770, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// NotIn条件，用其他字段作为查询值
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByNotInColumnTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.JoinDate.NotIn(new SearchColumn[] { empSearcher.StartWorkDate });
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            bool isSucc = employeeList == null ? false : (employeeList.Count >= 1 ? true : false);
            Assert.AreEqual(true, isSucc);
        }

        /// <summary>
        /// LargeEqual条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLargeEqualTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.Rand.LargeEqual(15);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 30;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// LargeEqual条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLargeEqualConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // 这句应该写入Dal进行控制，这里用于测试
            empSearcher.Birthday.ConditionString = "MONTH(" + SQLPlaceholder.ColName + ")";
            empSearcher.Birthday.LargeEqual(10);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 1;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// LargeEqual条件，用其他字段作为查询值
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLargeEqualColumnTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.StartWorkDate.LargeEqual(empSearcher.JoinDate);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            bool isSucc = employeeList == null ? false : (employeeList.Count >= 1 ? true : false);
            Assert.AreEqual(true, isSucc);
        }

        /// <summary>
        /// LargeThan条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLargeThanTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.Rand.LargeThan(14);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 30;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// LargeThan条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLargeThanConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // 这句应该写入Dal进行控制，这里用于测试
            empSearcher.Birthday.ConditionString = "MONTH(" + SQLPlaceholder.ColName + ")";
            empSearcher.Birthday.LargeThan(9);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 1;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// LargeThan条件，用其他字段作为查询值
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLargeThanColumnTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.JoinDate.LargeThan(empSearcher.StartWorkDate);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            bool isSucc = employeeList == null ? false : (employeeList.Count >= 1 ? true : false);
            Assert.AreEqual(true, isSucc);
        }

        /// <summary>
        /// LessEqual条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLessEqualTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.Rand.LessEqual(14);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 750;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// LessEqual条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLessEqualConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // 这句应该写入Dal进行控制，这里用于测试
            empSearcher.Birthday.ConditionString = "MONTH(" + SQLPlaceholder.ColName + ")";
            empSearcher.Birthday.LessEqual(10);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 780;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// LessEqual条件，用其他字段作为查询值
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLessEqualColumnTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.StartWorkDate.LessEqual(empSearcher.JoinDate);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            bool isSucc = employeeList == null ? false : (employeeList.Count >= 1 ? true : false);
            Assert.AreEqual(true, isSucc);
        }

        /// <summary>
        /// LessThan条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLessThanTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.Rand.LessThan(15);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 750;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// LessThan条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLessThanConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // 这句应该写入Dal进行控制，这里用于测试
            empSearcher.Birthday.ConditionString = "MONTH(" + SQLPlaceholder.ColName + ")";
            empSearcher.Birthday.LessThan(10);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 779;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// LessThan条件，用其他字段作为查询值
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLessThanColumnTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.StartWorkDate.LessThan(empSearcher.JoinDate);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            bool isSucc = employeeList == null ? false : (employeeList.Count >= 1 ? true : false);
            Assert.AreEqual(true, isSucc);
        }

        /// <summary>
        /// Like条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLikeTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.Name.Like("Emp");
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 780;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);

            empSearcher = new EmployeeSearcher();
            empSearcher.EmployeeCode.Like("510201");
            employeeList = OrgHandler.FindEmployeeList(empSearcher);
            allCount = 156;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// Like条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLikeConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // 这句应该写入Dal进行控制，这里用于测试
            empSearcher.Name.ConditionString = "LOWER(" + SQLPlaceholder.ColName + ")";
            empSearcher.Name.Like("emp");
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            Assert.AreEqual(780, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// Like条件，用其他字段作为查询值
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByLikeColumnTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.CurrCompany = new CompanySearcher();
            empSearcher.EmployeeCode.Like(empSearcher.CurrCompany.CompanyCode);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 780;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// NotLike条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByNotLikeTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.Name.NotLike("Emp");
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 0;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// NotLike条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByNotLikeConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // 这句应该写入Dal进行控制，这里用于测试
            empSearcher.Name.ConditionString = "LOWER(" + SQLPlaceholder.ColName + ")";
            empSearcher.Name.NotLike("emp");
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 0;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// NotLike条件，用其他字段作为查询值
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByNotLikeColumnTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.CurrCompany = new CompanySearcher();
            empSearcher.EmployeeCode.NotLike(empSearcher.CurrCompany.CompanyCode);
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 0;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// SQLIn条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeBySQLInTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // SQL应该写入Dal进行控制，这里用于测试
            StringBuilder subQuery = new StringBuilder();
            subQuery.Append("SELECT ");
            subQuery.Append("  DSub.DepartmentId ");
            subQuery.Append("FROM ");
            subQuery.Append("  Department DSub ");
            subQuery.Append("WHERE ");
            subQuery.Append("  DSub.CurrCompany.CompanyCode = '10401' ");
            empSearcher.CurrDepartment = new DepartmentSearcher();
            empSearcher.CurrDepartment.DepartmentId.SQLIn(subQuery.ToString());
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 156;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// SQLIn条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeBySQLInConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // SQL应该写入Dal进行控制，这里用于测试
            StringBuilder subQuery = new StringBuilder();
            subQuery.Append("SELECT ");
            subQuery.Append("  DSub.DepartmentId ");
            subQuery.Append("FROM ");
            subQuery.Append("  Department DSub ");
            subQuery.Append("WHERE ");
            subQuery.Append("  DSub.CurrCompany.CompanyCode = '10401' ");
            empSearcher.CurrDepartment = new DepartmentSearcher();
            empSearcher.CurrDepartment.DepartmentId.ConditionString = "LOWER(" + SQLPlaceholder.ColName + ")";
            empSearcher.CurrDepartment.DepartmentId.SQLIn(subQuery.ToString());
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 156;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// SQLNotIn条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeBySQLNotInTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // SQL应该写入Dal进行控制，这里用于测试
            StringBuilder subQuery = new StringBuilder();
            subQuery.Append("SELECT ");
            subQuery.Append("  DSub.DepartmentId ");
            subQuery.Append("FROM ");
            subQuery.Append("  Department DSub ");
            subQuery.Append("WHERE ");
            subQuery.Append("  DSub.CurrCompany.CompanyCode = '10401' ");
            empSearcher.CurrDepartment = new DepartmentSearcher();
            empSearcher.CurrDepartment.DepartmentId.SQLNotIn(subQuery.ToString());
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 624;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// SQLNotIn条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeBySQLNotInConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // SQL应该写入Dal进行控制，这里用于测试
            StringBuilder subQuery = new StringBuilder();
            subQuery.Append("SELECT ");
            subQuery.Append("  DSub.DepartmentId ");
            subQuery.Append("FROM ");
            subQuery.Append("  Department DSub ");
            subQuery.Append("WHERE ");
            subQuery.Append("  DSub.CurrCompany.CompanyCode = '10401' ");
            empSearcher.CurrDepartment = new DepartmentSearcher();
            empSearcher.CurrDepartment.DepartmentId.ConditionString = "LOWER(" + SQLPlaceholder.ColName + ")";
            empSearcher.CurrDepartment.DepartmentId.SQLNotIn(subQuery.ToString());
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 624;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// SQLEqual条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeBySQLEqualTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // SQL应该写入Dal进行控制，这里用于测试
            StringBuilder subQuery = new StringBuilder();
            subQuery.Append("SELECT ");
            subQuery.Append("  DSub.CurrCompany.CompanyId ");
            subQuery.Append("FROM ");
            subQuery.Append("  Department DSub ");
            subQuery.Append("WHERE ");
            subQuery.Append("  DSub.DepartmentCode = '2104010005' ");
            empSearcher.CurrCompany = new CompanySearcher();
            empSearcher.CurrCompany.CompanyId.SQLEqual(subQuery.ToString());
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 156;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// SQLEqual条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeBySQLEqualConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // SQL应该写入Dal进行控制，这里用于测试
            StringBuilder subQuery = new StringBuilder();
            subQuery.Append("SELECT ");
            subQuery.Append("  DSub.CurrCompany.CompanyCode ");
            subQuery.Append("FROM ");
            subQuery.Append("  Department DSub ");
            subQuery.Append("WHERE ");
            subQuery.Append("  DSub.DepartmentCode = '2104010005' ");
            empSearcher.CurrCompany = new CompanySearcher();
            empSearcher.EmployeeCode.ConditionString = "SUBSTRING(" + SQLPlaceholder.ColName + ", 2, 5)";
            empSearcher.EmployeeCode.SQLEqual(subQuery.ToString());
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 156;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// SQLNotEqual条件
        /// </summary>
        [TestMethod()]
        public void FindEmployeeBySQLNotEqualTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // SQL应该写入Dal进行控制，这里用于测试
            StringBuilder subQuery = new StringBuilder();
            subQuery.Append("SELECT ");
            subQuery.Append("  DSub.CurrCompany.CompanyId ");
            subQuery.Append("FROM ");
            subQuery.Append("  Department DSub ");
            subQuery.Append("WHERE ");
            subQuery.Append("  DSub.DepartmentCode = '2104010005' ");
            empSearcher.CurrCompany = new CompanySearcher();
            empSearcher.CurrCompany.CompanyId.SQLNotEqual(subQuery.ToString());
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 624;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// SQLNotEqual条件，给查询的字段加上函数等进行计算或转换后再查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeBySQLNotEqualConditionStringTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            // SQL应该写入Dal进行控制，这里用于测试
            StringBuilder subQuery = new StringBuilder();
            subQuery.Append("SELECT ");
            subQuery.Append("  DSub.CurrCompany.CompanyCode ");
            subQuery.Append("FROM ");
            subQuery.Append("  Department DSub ");
            subQuery.Append("WHERE ");
            subQuery.Append("  DSub.DepartmentCode = '2104010005' ");
            empSearcher.CurrCompany = new CompanySearcher();
            empSearcher.EmployeeCode.ConditionString = "SUBSTRING(" + SQLPlaceholder.ColName + ", 2, 5)";
            empSearcher.EmployeeCode.SQLNotEqual(subQuery.ToString());
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 624;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByConditionsTest()
        {
            EmployeeSearcher empSearcher = new EmployeeSearcher();
            empSearcher.CurrCompany = new CompanySearcher();
            empSearcher.EmployeeCode.Equal(ConditionRelation.Or, QueryTest.EmployeeCodeList[0]);
            empSearcher.EmployeeCode.Equal(ConditionRelation.Or, QueryTest.EmployeeCodeList[1]);
            empSearcher.EmployeeCode.Equal(ConditionRelation.Or, QueryTest.EmployeeCodeList[2]);
            empSearcher.CurrCompany.CompanyCode.Equal(ConditionRelation.Or, "10203");
            empSearcher.Rand.LargeEqual(14);

            IList<Employee> employeeList = OrgHandler.FindEmployeeList(empSearcher);
            int allCount = 158;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// 使用ConditionGroup的GroupIndex进行分组
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByConditionGroupTest1()
        {
            //EmployeeSearcher employeeSearcher = new EmployeeSearcher();
            //Condition condition = null;

            //#region No Group

            //condition = ConditionFactory.Equal(QueryTest.EmployeeCodeList[4]);
            //condition.Relation = ConditionRelation.Or;
            //employeeSearcher.EmployeeCode.AddCondition(condition);

            //condition = ConditionFactory.Equal(QueryTest.EmployeeCodeList[5]);
            //condition.Relation = ConditionRelation.Or;
            //employeeSearcher.EmployeeCode.AddCondition(condition);

            //#endregion

            //#region Group1

            //ConditionGroup group1 = new ConditionGroup();
            //group1.GroupRelation = ConditionRelation.Or;
            //group1.GroupIndex = 1;
            //condition = ConditionFactory.Equal(QueryTest.EmployeeCodeList[0]);
            //condition.Relation = ConditionRelation.Or;
            //condition.Group = group1;
            //employeeSearcher.EmployeeCode.AddCondition(condition);

            //condition = ConditionFactory.Equal(QueryTest.EmployeeCodeList[1]);
            //condition.Relation = ConditionRelation.Or;
            //condition.Group = group1;
            //employeeSearcher.EmployeeCode.AddCondition(condition);

            //#region Sub Group 1 1

            //ConditionGroup subGroup11 = new ConditionGroup();
            //group1.AddSubGroup(subGroup11);
            //subGroup11.GroupRelation = ConditionRelation.And;
            //subGroup11.GroupIndex = 1;
            //condition = ConditionFactory.Equal(QueryTest.EmployeeCodeList[2]);
            //condition.Relation = ConditionRelation.Or;
            //condition.Group = subGroup11;
            //employeeSearcher.EmployeeCode.AddCondition(condition);

            //condition = ConditionFactory.Equal(QueryTest.EmployeeCodeList[3]);
            //condition.Relation = ConditionRelation.Or;
            //condition.Group = subGroup11;
            //employeeSearcher.EmployeeCode.AddCondition(condition);

            //#endregion
            
            //#endregion

            //#region Group2

            //employeeSearcher.CurrCompany = new CompanySearcher();
            //ConditionGroup group2 = new ConditionGroup();
            //group2.GroupRelation = ConditionRelation.Or;
            //group2.GroupIndex = 2;
            //condition = ConditionFactory.Equal("10201");
            //condition.Relation = ConditionRelation.Or;
            //condition.Group = group2;
            //employeeSearcher.CurrCompany.CompanyCode.AddCondition(condition);

            //condition = ConditionFactory.Equal("10301");
            //condition.Relation = ConditionRelation.Or;
            //condition.Group = group2;
            //employeeSearcher.CurrCompany.CompanyCode.AddCondition(condition);

            //#region Sub Group 2 1

            //ConditionGroup subGroup21 = new ConditionGroup();
            //group2.AddSubGroup(subGroup21);
            //subGroup21.GroupRelation = ConditionRelation.Or;
            //subGroup21.GroupIndex = 1;
            //condition = ConditionFactory.Equal(QueryTest.EmployeeCodeList[6]);
            //condition.Relation = ConditionRelation.Or;
            //condition.Group = subGroup21;
            //employeeSearcher.EmployeeCode.AddCondition(condition);

            //condition = ConditionFactory.Equal(QueryTest.EmployeeCodeList[7]);
            //condition.Relation = ConditionRelation.Or;
            //condition.Group = subGroup21;
            //employeeSearcher.EmployeeCode.AddCondition(condition);

            //#endregion

            //#endregion

            //IList<Employee> employeeList = OrgHandler.FindEmployeeList(employeeSearcher);
            //int allCount = 312;
            //Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// 使用ConditionGroup的构造函数进行嵌套分组
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByConditionGroupTest2()
        {
            //EmployeeSearcher employeeSearcher = new EmployeeSearcher();
            //employeeSearcher.CurrCompany = new CompanySearcher();
            //ConditionGroup group = ConditionFactory.Group(ConditionRelation.And, ConditionFactory.Equal(employeeSearcher.EmployeeCode, QueryTest.EmployeeCodeList[0]),
            //                                                                     ConditionFactory.Equal(ConditionRelation.Or, employeeSearcher.EmployeeCode, QueryTest.EmployeeCodeList[1]),
            //                                                                     ConditionFactory.Group(ConditionRelation.Or, ConditionFactory.Equal(ConditionRelation.Or, employeeSearcher.CurrCompany.CompanyCode, "10201"),
            //                                                                                                                  ConditionFactory.Equal(ConditionRelation.Or, employeeSearcher.CurrCompany.CompanyCode, "10301")));

            //IList<Employee> employeeList = OrgHandler.FindEmployeeList(employeeSearcher);
            //int allCount = 312;
            //Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// 单列排序
        /// </summary>
        [TestMethod()]
        public void FindEmployeeBySingleSortTest()
        {
            EmployeeSearcher employeeSearcher = new EmployeeSearcher();
            employeeSearcher.Rand.SortOrder = SortOrder.Desc;
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(employeeSearcher);
            Assert.IsTrue(employeeList[0].Rand >= employeeList[1].Rand);
        }

        /// <summary>
        /// 多列排序
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByMultipleSortTest()
        {
            EmployeeSearcher employeeSearcher = new EmployeeSearcher();
            employeeSearcher.CurrCompany = new CompanySearcher();
            employeeSearcher.CurrCompany.CompanyCode.SortOrder = SortOrder.Desc;
            employeeSearcher.CurrCompany.CompanyCode.SortIndex = 1;
            employeeSearcher.Rand.SortOrder = SortOrder.Desc;
            employeeSearcher.Rand.SortIndex = 2;
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(employeeSearcher);
            Assert.IsTrue(employeeList[0].CurrCompany.CompanyCode.Equals(employeeList[1].CurrCompany.CompanyCode));
            Assert.IsTrue(employeeList[0].Rand >= employeeList[1].Rand);
        }

        /// <summary>
        /// 多列排序，可以对字段使用函数或类型转换后再进行排序
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByMultipleSortStringTest()
        {
            EmployeeSearcher employeeSearcher = new EmployeeSearcher();
            // 这句应该写入Dal进行控制，这里用于测试
            employeeSearcher.EmployeeCode.SortString = "SUBSTRING(" + SQLPlaceholder.ColName + ", 1, 6)";
            employeeSearcher.EmployeeCode.SortOrder = SortOrder.Desc;
            employeeSearcher.EmployeeCode.SortIndex = 1;
            employeeSearcher.Rand.SortOrder = SortOrder.Desc;
            employeeSearcher.Rand.SortIndex = 2;
            IList<Employee> employeeList = OrgHandler.FindEmployeeList(employeeSearcher);
            Assert.IsTrue(employeeList[0].CompanyId.Equals(employeeList[1].CompanyId));
            Assert.IsTrue(employeeList[0].Rand >= employeeList[1].Rand);
        }

        /// <summary>
        /// 使用事务提交操作
        /// </summary>
        [TestMethod()]
        public void TransactionCommitTest()
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            Employee existEmployee = OrgHandler.FindEmployeeById(QueryTest.EmployeeIdList[2]);
            Position newPosition = new Position();
            newPosition.CurrCompany = existEmployee.CurrCompany;
            newPosition.CurrDepartment = existEmployee.CurrDepartment;
            newPosition.PositionCode = "3" + existEmployee.CurrCompany.CompanyCode + string.Format("{0:0000}", "12");
            newPosition.CreaterId = "00000000000";
            newPosition.UpdatorId = "00000000000";
            newPosition.RVersion = 1;
            newPosition.Status = 1;
            OrgHandler.AddPosition(newPosition, tran);

            Employee newEmployee = new Employee();
            newEmployee.Name = "Emp" + Guid.NewGuid().ToString().ToLower().Replace("-", "").Substring(0, 5);
            newEmployee.EmployeeCode = "5" + existEmployee.CurrCompany.CompanyCode + string.Format("{0:000000}", "157");
            newEmployee.CurrCompany = existEmployee.CurrCompany;
            newEmployee.CurrDepartment = existEmployee.CurrDepartment;
            newEmployee.CurrPosition = newPosition;
            newEmployee.CreaterId = "00000000000";
            newEmployee.UpdatorId = "00000000000";
            newEmployee.RVersion = 1;
            newEmployee.Status = 1;
            newEmployee.Birthday = Convert.ToDateTime("1981-11-27");
            newEmployee.Sex = 1;
            newEmployee.StartWorkDate = Convert.ToDateTime("2005-4-1");
            newEmployee.JoinDate = Convert.ToDateTime("2005-4-1");
            newEmployee.Rand = 10;
            OrgHandler.AddEmployee(newEmployee, tran);

            newEmployee = new Employee();
            newEmployee.Name = "Emp" + Guid.NewGuid().ToString().ToLower().Replace("-", "").Substring(0, 5);
            newEmployee.EmployeeCode = "5" + existEmployee.CurrCompany.CompanyCode + string.Format("{0:000000}", "158");
            newEmployee.CurrCompany = existEmployee.CurrCompany;
            newEmployee.CurrDepartment = existEmployee.CurrDepartment;
            newEmployee.CurrPosition = newPosition;
            newEmployee.CreaterId = "00000000000";
            newEmployee.UpdatorId = "00000000000";
            newEmployee.RVersion = 1;
            newEmployee.Status = 1;
            newEmployee.Birthday = Convert.ToDateTime("1981-12-02");
            newEmployee.Sex = 1;
            newEmployee.StartWorkDate = Convert.ToDateTime("2005-4-3");
            newEmployee.JoinDate = Convert.ToDateTime("2005-4-3");
            newEmployee.Rand = 11;
            OrgHandler.AddEmployee(newEmployee, tran);

            ((HibernateTransaction)tran).CurrentTransaction.Commit();

            long allCountPosEx = 56;
            long allCountPos = OrgHandler.CountPosition(new PositionSearcher());
            Assert.AreEqual(allCountPosEx, allCountPos);
            long allCountEmpEx = 782;
            long allCountEmp = OrgHandler.CountEmployee(new EmployeeSearcher());
            Assert.AreEqual(allCountEmpEx, allCountEmp);
        }

        /// <summary>
        /// 使用事务回滚操作
        /// </summary>
        [TestMethod()]
        public void TransactionRollBackTest()
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            Employee existEmployee = OrgHandler.FindEmployeeById(QueryTest.EmployeeIdList[2]);
            Position newPosition = new Position();
            newPosition.CurrCompany = existEmployee.CurrCompany;
            newPosition.CurrDepartment = existEmployee.CurrDepartment;
            newPosition.PositionCode = "3" + existEmployee.CurrCompany.CompanyCode + string.Format("{0:0000}", "13");
            newPosition.CreaterId = "00000000000";
            newPosition.UpdatorId = "00000000000";
            newPosition.RVersion = 1;
            newPosition.Status = 1;
            OrgHandler.AddPosition(newPosition, tran);

            Employee newEmployee = new Employee();
            newEmployee.Name = "Emp" + Guid.NewGuid().ToString().ToLower().Replace("-", "").Substring(0, 5);
            newEmployee.EmployeeCode = "5" + existEmployee.CurrCompany.CompanyCode + string.Format("{0:000000}", "159");
            newEmployee.CurrCompany = existEmployee.CurrCompany;
            newEmployee.CurrDepartment = existEmployee.CurrDepartment;
            newEmployee.CurrPosition = newPosition;
            newEmployee.CreaterId = "00000000000";
            newEmployee.UpdatorId = "00000000000";
            newEmployee.RVersion = 1;
            newEmployee.Status = 1;
            newEmployee.Birthday = Convert.ToDateTime("1981-11-27");
            newEmployee.Sex = 1;
            newEmployee.StartWorkDate = Convert.ToDateTime("2005-4-1");
            newEmployee.JoinDate = Convert.ToDateTime("2005-4-1");
            newEmployee.Rand = 10;
            OrgHandler.AddEmployee(newEmployee, tran);

            newEmployee = new Employee();
            newEmployee.Name = "Emp" + Guid.NewGuid().ToString().ToLower().Replace("-", "").Substring(0, 5);
            newEmployee.EmployeeCode = "5" + existEmployee.CurrCompany.CompanyCode + string.Format("{0:000000}", "160");
            newEmployee.CurrCompany = existEmployee.CurrCompany;
            newEmployee.CurrDepartment = existEmployee.CurrDepartment;
            newEmployee.CurrPosition = newPosition;
            newEmployee.CreaterId = "00000000000";
            newEmployee.UpdatorId = "00000000000";
            newEmployee.RVersion = 1;
            newEmployee.Status = 1;
            newEmployee.Birthday = Convert.ToDateTime("1981-12-02");
            newEmployee.Sex = 1;
            newEmployee.StartWorkDate = Convert.ToDateTime("2005-4-3");
            newEmployee.JoinDate = Convert.ToDateTime("2005-4-3");
            newEmployee.Rand = 11;
            OrgHandler.AddEmployee(newEmployee, tran);

            ((HibernateTransaction)tran).CurrentTransaction.Rollback();

            long allCountPosEx = 56;
            long allCountPos = OrgHandler.CountPosition(new PositionSearcher());
            Assert.AreEqual(allCountPosEx, allCountPos);
            long allCountEmpEx = 782;
            long allCountEmp = OrgHandler.CountEmployee(new EmployeeSearcher());
            Assert.AreEqual(allCountEmpEx, allCountEmp);
        }

        /// <summary>
        /// 根据条件批量更新
        /// </summary>
        [TestMethod()]
        public void UpdateByConditionsDataTest()
        {

        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        [TestMethod()]
        public void DeleteDataByIdTest()
        {
            OrgHandler.DeleteEmployee(QueryTest.EmployeeIdList[8]);
            Employee employee = OrgHandler.FindEmployeeById(QueryTest.EmployeeIdList[8]);
            Assert.IsNull(employee);
        }

        /// <summary>
        /// 根据条件批量删除
        /// </summary>
        [TestMethod()]
        public void DeleteDataByConditionTest()
        {
            EmployeeSearcher employeeSearcher = new EmployeeSearcher();
            employeeSearcher.CurrCompany = new CompanySearcher();
            employeeSearcher.CurrCompany.CompanyCode.Equal("10401");
            OrgHandler.DeleteEmployee(employeeSearcher);

            long count = OrgHandler.CountEmployee(employeeSearcher);
            Assert.AreEqual(0, count);
        }

    }
}
