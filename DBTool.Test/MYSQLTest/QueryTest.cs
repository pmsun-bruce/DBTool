using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NFramework.DBTool.Common;
using NFramework.DBTool.QueryTool;
using NFramework.DBTool.QueryTool.Mssql;
using NFramework.ExceptionTool;

using NFramework.DBTool.Test.Entity;
using NFramework.DBTool.Test.Handler;
using NFramework.DBTool.Test.IDal;
using NFramework.DBTool.Test.MYSQLTest.Dal;
using NFramework.DBTool.Test.Searcher;
using NFramework.ObjectTool;
using MySql.Data.MySqlClient;

namespace NFramework.DBTool.Test.MYSQLTest
{
    /// <summary>
    /// QueryTest 的摘要说明
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
            log4net.ILog logger = log4net.LogManager.GetLogger("DBTool MYSQL Test");
            logger.Debug("Start Test");

            string connectionString = @"Database=DBToolSampleDB;Data Source=localhost;User Id=devuser;Password=1qazxsw2";
            IDalFactory dalFactory = new DalFactory(connectionString);
            NFramework.DBTool.Test.IDal.DalManager.Load(dalFactory);
        }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
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
                    department.CompanyId = company.CompanyId;
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
                        position.CompanyId = company.CompanyId;
                        position.DepartmentId = department.DepartmentId;
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
                            employee.CompanyId = company.CompanyId;
                            employee.DepartmentId = department.DepartmentId;
                            employee.PositionId = position.PositionId;
                            employee.CreaterId = "00000000000";
                            employee.UpdatorId = "00000000000";
                            employee.RVersion = 1;
                            employee.Status = 1;
                            employee.Birthday = Convert.ToDateTime("19" + rand.Next(70, 90).ToString() + "-" + rand.Next(1, 9).ToString() + "-" + rand.Next(1, 28).ToString());
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

                            if (QueryTest.EmployeeIdList.Count < 10 && !dName.Equals("GM") && !pName.Equals("GM"))
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
            bool isSucc = employeeList == null ? false : (employeeList.Count > 1 ? true : false);
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
            subQuery.Append("LEFT JOIN ");
            subQuery.Append("  Company CSub ON(CSub.CompanyId=DSub.CompanyId) ");
            subQuery.Append("WHERE ");
            subQuery.Append("  CSub.CompanyCode = '10401' ");
            empSearcher.DepartmentId.SQLIn(subQuery.ToString());
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
            subQuery.Append("LEFT JOIN ");
            subQuery.Append("  Company CSub ON(CSub.CompanyId=DSub.CompanyId) ");
            subQuery.Append("WHERE ");
            subQuery.Append("  CSub.CompanyCode = '10401' ");
            empSearcher.DepartmentId.ConditionString = "LOWER(" + SQLPlaceholder.ColName + ")";
            empSearcher.DepartmentId.SQLIn(subQuery.ToString());
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
            subQuery.Append("LEFT JOIN ");
            subQuery.Append("  Company CSub ON(CSub.CompanyId=DSub.CompanyId) ");
            subQuery.Append("WHERE ");
            subQuery.Append("  CSub.CompanyCode = '10401' ");
            empSearcher.DepartmentId.SQLNotIn(subQuery.ToString());
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
            subQuery.Append("LEFT JOIN ");
            subQuery.Append("  Company CSub ON(CSub.CompanyId=DSub.CompanyId) ");
            subQuery.Append("WHERE ");
            subQuery.Append("  CSub.CompanyCode = '10401' ");
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
            subQuery.Append("  DSub.CompanyId ");
            subQuery.Append("FROM ");
            subQuery.Append("  Department DSub ");
            subQuery.Append("WHERE ");
            subQuery.Append("  DSub.DepartmentCode = '2104010005' ");
            empSearcher.CompanyId.SQLEqual(subQuery.ToString());
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
            subQuery.Append("  CSub.CompanyCode ");
            subQuery.Append("FROM ");
            subQuery.Append("  Department DSub ");
            subQuery.Append("LEFT JOIN ");
            subQuery.Append("  Company CSub ON(CSub.CompanyId=DSub.CompanyId) ");
            subQuery.Append("WHERE ");
            subQuery.Append("  DSub.DepartmentCode = '2104010005' ");
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
            subQuery.Append("  DSub.CompanyId ");
            subQuery.Append("FROM ");
            subQuery.Append("  Department DSub ");
            subQuery.Append("WHERE ");
            subQuery.Append("  DSub.DepartmentCode = '2104010005' ");
            empSearcher.CompanyId.SQLNotEqual(subQuery.ToString());
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
            subQuery.Append("  CSub.CompanyCode ");
            subQuery.Append("FROM ");
            subQuery.Append("  Department DSub ");
            subQuery.Append("LEFT JOIN ");
            subQuery.Append("  Company CSub ON(CSub.CompanyId=DSub.CompanyId) ");
            subQuery.Append("WHERE ");
            subQuery.Append("  DSub.DepartmentCode = '2104010005' ");
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
            int allCount = 9;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// 使用ConditionGroup的GroupIndex进行分组
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByConditionGroupTest1()
        {
            EmployeeSearcher employeeSearcher = new EmployeeSearcher();
            employeeSearcher.CurrCompany = new CompanySearcher();

            Group.And(Group.Or(employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[4]), employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[5]),
                               Group.Or(employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[0]), employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[1])),
                               Group.Or(employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[2]), employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[3]))),
                      Group.Or(employeeSearcher.CurrCompany.CompanyCode.Equal("10201"), employeeSearcher.CurrCompany.CompanyCode.Equal("10301"),
                               Group.Or(employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[6]), employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[7]))));

            IList<Employee> employeeList = OrgHandler.FindEmployeeList(employeeSearcher);
            int allCount = 6;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
        }

        /// <summary>
        /// 使用ConditionGroup的构造函数进行嵌套分组
        /// </summary>
        [TestMethod()]
        public void FindEmployeeByConditionGroupTest2()
        {
            EmployeeSearcher employeeSearcher = new EmployeeSearcher();
            employeeSearcher.CurrCompany = new CompanySearcher();

            Group.And(Group.Or(employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[4]), employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[5]),
                               Group.Or(employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[0]), employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[1])),
                               Group.Or(employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[2]), employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[3]))),
                      Group.Or(employeeSearcher.CurrCompany.CompanyCode.Equal("10201"), employeeSearcher.CurrCompany.CompanyCode.Equal("10301"),
                               Group.Or(employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[6]), employeeSearcher.EmployeeCode.Equal(QueryTest.EmployeeCodeList[7]))));

            IList<Employee> employeeList = OrgHandler.FindEmployeeList(employeeSearcher);
            int allCount = 6;
            Assert.AreEqual(allCount, employeeList == null ? 0 : employeeList.Count);
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
            Assert.IsTrue(employeeList[0].CompanyId.Equals(employeeList[1].CompanyId));
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
            existEmployee.CurrCompany = OrgHandler.FindCompanyById(existEmployee.CompanyId);
            Position newPosition = new Position();
            newPosition.CompanyId = existEmployee.CompanyId;
            newPosition.DepartmentId = existEmployee.DepartmentId;
            newPosition.PositionCode = "3" + existEmployee.CurrCompany.CompanyCode + string.Format("{0:0000}", "12");
            newPosition.CreaterId = "00000000000";
            newPosition.UpdatorId = "00000000000";
            newPosition.RVersion = 1;
            newPosition.Status = 1;
            OrgHandler.AddPosition(newPosition, tran);

            Employee newEmployee = new Employee();
            newEmployee.Name = "Emp" + Guid.NewGuid().ToString().ToLower().Replace("-", "").Substring(0, 5);
            newEmployee.EmployeeCode = "5" + existEmployee.CurrCompany.CompanyCode + string.Format("{0:000000}", "157");
            newEmployee.CompanyId = existEmployee.CompanyId;
            newEmployee.DepartmentId = existEmployee.DepartmentId;
            newEmployee.PositionId = newPosition.PositionId;
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
            newEmployee.CompanyId = existEmployee.CompanyId;
            newEmployee.DepartmentId = existEmployee.DepartmentId;
            newEmployee.PositionId = newPosition.PositionId;
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

            tran.Commit();

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
            existEmployee.CurrCompany = OrgHandler.FindCompanyById(existEmployee.CompanyId);
            Position newPosition = new Position();
            newPosition.CompanyId = existEmployee.CompanyId;
            newPosition.DepartmentId = existEmployee.DepartmentId;
            newPosition.PositionCode = "3" + existEmployee.CurrCompany.CompanyCode + string.Format("{0:0000}", "13");
            newPosition.CreaterId = "00000000000";
            newPosition.UpdatorId = "00000000000";
            newPosition.RVersion = 1;
            newPosition.Status = 1;
            OrgHandler.AddPosition(newPosition, tran);

            Employee newEmployee = new Employee();
            newEmployee.Name = "Emp" + Guid.NewGuid().ToString().ToLower().Replace("-", "").Substring(0, 5);
            newEmployee.EmployeeCode = "5" + existEmployee.CurrCompany.CompanyCode + string.Format("{0:000000}", "159");
            newEmployee.CompanyId = existEmployee.CompanyId;
            newEmployee.DepartmentId = existEmployee.DepartmentId;
            newEmployee.PositionId = newPosition.PositionId;
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
            newEmployee.CompanyId = existEmployee.CompanyId;
            newEmployee.DepartmentId = existEmployee.DepartmentId;
            newEmployee.PositionId = newPosition.PositionId;
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

            tran.Rollback();

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

        private TableInfo FillTable()
        {
            TableInfo tableInfo = new TableInfo();
            tableInfo.TableName = "TestTbl1";
            tableInfo.Remarks = "测试表1";

            ColumnInfo colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col1";
            colInfo.IsPK = true;
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "主键字段1";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col11";
            colInfo.IsPK = true;
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "主键字段11";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col2";
            colInfo.IsUnique = true;
            colInfo.UniqueConstraintName = "Col2";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "唯一字段1";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col3";
            colInfo.IsUnique = true;
            colInfo.UniqueConstraintName = "Col34";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "唯一字段34";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col4";
            colInfo.IsUnique = true;
            colInfo.UniqueConstraintName = "Col34";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "唯一字段34";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col5";
            colInfo.DBType = DbType.DateTime;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段5";
            colInfo.DefaultValue = "1753-01-01";
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col6";
            colInfo.DBType = DbType.Int32;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段6";
            colInfo.DefaultValue = 100;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col7";
            colInfo.DBType = DbType.Decimal;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段7";
            colInfo.DefaultValue = 100.32;
            colInfo.Precision = 10;
            colInfo.Scale = 2;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col99";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段99";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            return tableInfo;
        }

        static TableInfo tableInfo = null;

        [TestMethod]
        public void CreateTableTest()
        {
            tableInfo = FillTable();
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.DropTable(tableInfo.TableName);
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.CreateTable(tableInfo);
        }

        [TestMethod]
        public void EditTableTest()
        {
            tableInfo.Remarks = tableInfo.Remarks + "22";
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.EditTable(tableInfo, tableInfo);
        }

        [TestMethod]
        public void AddColumnTest()
        {
            // 增加一般字段
            ColumnInfo colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col8";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段8";
            colInfo.MaxLength = 40;
            colInfo.DefaultValue = "1134";
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.AddColumn(tableInfo, colInfo);
            tableInfo.Columns.Add(colInfo);

            // 已存在字段
            try
            {
                colInfo = new ColumnInfo();
                colInfo.ColumnName = "Col2";
                colInfo.IsUnique = true;
                colInfo.UniqueConstraintName = "Col2";
                colInfo.DBType = DbType.AnsiString;
                colInfo.CurrTable = tableInfo;
                colInfo.Remarks = "唯一字段1";
                colInfo.MaxLength = 40;

                NFramework.DBTool.Test.IDal.DalManager.DalFactory.AddColumn(tableInfo, colInfo);
                Assert.IsTrue(false);
            }
            catch(Exception ex)
            {
                Assert.IsTrue(true);
            }

            // 增加同组唯一字段
            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col9";
            colInfo.IsUnique = true;
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段9";
            colInfo.MaxLength = 40;
            colInfo.DefaultValue = "1134";
            colInfo.UniqueConstraintName = "Col34";
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.AddColumn(tableInfo, colInfo);
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col10";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段10";
            colInfo.MaxLength = 40;
            colInfo.DefaultValue = "1010";
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.AddColumn(tableInfo, colInfo);
            tableInfo.Columns.Add(colInfo);

            // 增加主键字段
            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col111";
            colInfo.IsPK = true;
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "主键字段111";
            colInfo.MaxLength = 40;
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.AddColumn(tableInfo, colInfo);
            tableInfo.Columns.Add(colInfo);
        }

        [TestMethod]
        public void EditColumnTest()
        {
            // 修改备注
            ColumnInfo oldColInfo = tableInfo.Columns.FirstOrDefault<ColumnInfo>(ci => ci.ColumnName.Equals("Col8"));
            ColumnInfo newColInfo = oldColInfo;
            oldColInfo = ObjectFactory.Clone<ColumnInfo>(newColInfo);
            newColInfo.Remarks = "字段89999111";
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.EditColumn(tableInfo, oldColInfo, newColInfo);

            // 修改默认值
            oldColInfo.Remarks = "字段89999111";
            newColInfo.Remarks = "字段89999222";
            newColInfo.DefaultValue = "1000";
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.EditColumn(tableInfo, oldColInfo, newColInfo);

            // 增加主键
            oldColInfo = ObjectFactory.Clone<ColumnInfo>(newColInfo);
            newColInfo.IsPK = true;
            newColInfo.IsNullable = true;
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.EditColumn(tableInfo, oldColInfo, newColInfo);

            // 增加唯一项
            oldColInfo = tableInfo.Columns.FirstOrDefault<ColumnInfo>(ci => ci.ColumnName.Equals("Col10"));
            newColInfo = oldColInfo;
            oldColInfo = ObjectFactory.Clone<ColumnInfo>(newColInfo);
            newColInfo.IsUnique = true;
            newColInfo.UniqueConstraintName = "Col34";
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.EditColumn(tableInfo, oldColInfo, newColInfo);

            // 修改列名
            oldColInfo = tableInfo.Columns.FirstOrDefault<ColumnInfo>(ci => ci.ColumnName.Equals("Col5"));
            newColInfo = oldColInfo;
            oldColInfo = ObjectFactory.Clone<ColumnInfo>(newColInfo);
            newColInfo.ColumnName = "Col55";
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.EditColumn(tableInfo, oldColInfo, newColInfo);

            // 修改长度
            oldColInfo = tableInfo.Columns.FirstOrDefault<ColumnInfo>(ci => ci.ColumnName.Equals("Col99"));
            newColInfo = oldColInfo;
            oldColInfo = ObjectFactory.Clone<ColumnInfo>(newColInfo);
            newColInfo.MaxLength = 100;
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.EditColumn(tableInfo, oldColInfo, newColInfo);

            // 修改类型
            oldColInfo = ObjectFactory.Clone<ColumnInfo>(newColInfo);
            newColInfo.DBType = DbType.Int32;
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.EditColumn(tableInfo, oldColInfo, newColInfo);

            // 修改列名和类型
            oldColInfo = ObjectFactory.Clone<ColumnInfo>(newColInfo);
            newColInfo.ColumnName = "Col555";
            newColInfo.DBType = DbType.String;
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.EditColumn(tableInfo, oldColInfo, newColInfo);

            // 删除唯一性
            oldColInfo = tableInfo.Columns.FirstOrDefault<ColumnInfo>(ci => ci.ColumnName.Equals("Col3"));
            newColInfo = oldColInfo;
            oldColInfo = ObjectFactory.Clone<ColumnInfo>(newColInfo);
            newColInfo.IsUnique = false;
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.EditColumn(tableInfo, oldColInfo, newColInfo);

            // 增加唯一性
            oldColInfo = ObjectFactory.Clone<ColumnInfo>(newColInfo);
            newColInfo.IsUnique = true;
            newColInfo.UniqueConstraintName = "Col34";
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.EditColumn(tableInfo, oldColInfo, newColInfo);

            // 修改类型和唯一性
            oldColInfo = newColInfo;
            newColInfo = ObjectFactory.Clone<ColumnInfo>(oldColInfo);
            newColInfo.IsUnique = false;
            newColInfo.ColumnName = "Col33";
            NFramework.DBTool.Test.IDal.DalManager.DalFactory.EditColumn(tableInfo, oldColInfo, newColInfo);
            oldColInfo.IsUnique = false;
            oldColInfo.ColumnName = "Col33";
            oldColInfo.UniqueConstraintName = string.Empty;
        }

        [TestMethod]
        public void DropColumnTest()
        {
            ColumnInfo colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col9";
            colInfo.IsUnique = true;
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段9";
            colInfo.MaxLength = 40;
            colInfo.DefaultValue = "1134";
            colInfo.UniqueConstraintName = "Col34";

            NFramework.DBTool.Test.IDal.DalManager.DalFactory.DropColumn(tableInfo, colInfo);
        }

        private TableInfo FillTable2()
        {
            TableInfo tableInfo = new TableInfo();
            tableInfo.TableName = "TestTbl2";
            tableInfo.Remarks = "测试表2";

            ColumnInfo colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col1";
            colInfo.IsPK = true;
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "主键字段1";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col11";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段11";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col2";
            colInfo.IsUnique = true;
            colInfo.UniqueConstraintName = "Col2";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "唯一字段1";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col3";
            colInfo.IsUnique = true;
            colInfo.UniqueConstraintName = "Col34";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "唯一字段34";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col4";
            colInfo.IsUnique = true;
            colInfo.UniqueConstraintName = "Col34";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "唯一字段34";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col5";
            colInfo.DBType = DbType.DateTime;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段5";
            colInfo.DefaultValue = "1753-01-01";
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col6";
            colInfo.DBType = DbType.Int32;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段6";
            colInfo.DefaultValue = 100;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col7";
            colInfo.DBType = DbType.Decimal;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段7";
            colInfo.DefaultValue = 100.32;
            colInfo.Precision = 10;
            colInfo.Scale = 2;
            tableInfo.Columns.Add(colInfo);

            return tableInfo;
        }

        private TableInfo FillTable3()
        {
            TableInfo tableInfo = new TableInfo();
            tableInfo.TableName = "TestTbl3";
            tableInfo.Remarks = "测试表3";

            ColumnInfo colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col1";
            colInfo.IsPK = true;
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "主键字段1";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col11";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段11";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col2";
            colInfo.IsUnique = true;
            colInfo.UniqueConstraintName = "Col2";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "唯一字段1";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col3";
            colInfo.IsUnique = true;
            colInfo.UniqueConstraintName = "Col34";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "唯一字段34";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col4";
            colInfo.IsUnique = true;
            colInfo.UniqueConstraintName = "Col34";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "唯一字段34";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col5";
            colInfo.DBType = DbType.DateTime;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段5";
            colInfo.DefaultValue = "1753-01-01";
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col6";
            colInfo.DBType = DbType.Int32;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段6";
            colInfo.DefaultValue = 100;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col7";
            colInfo.DBType = DbType.Decimal;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段7";
            colInfo.DefaultValue = 100.32;
            colInfo.Precision = 10;
            colInfo.Scale = 2;
            tableInfo.Columns.Add(colInfo);

            return tableInfo;
        }

        private TableInfo FillTable4()
        {
            TableInfo tableInfo = new TableInfo();
            tableInfo.TableName = "TestTbl4";
            tableInfo.Remarks = "测试表4";

            ColumnInfo colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col1";
            colInfo.IsPK = true;
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "主键字段1";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "FKCol1";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "外键1";
            colInfo.MaxLength = 40;
            colInfo.IsFK = true;
            colInfo.RefTableName = "TestTbl2";
            colInfo.RefColumnName = "Col1";
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "FKCol2";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "外键2";
            colInfo.MaxLength = 40;
            colInfo.IsFK = true;
            colInfo.RefTableName = "TestTbl3";
            colInfo.RefColumnName = "Col1";
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col3";
            colInfo.IsUnique = true;
            colInfo.UniqueConstraintName = "Col34";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "唯一字段34";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col4";
            colInfo.IsUnique = true;
            colInfo.UniqueConstraintName = "Col34";
            colInfo.DBType = DbType.AnsiString;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "唯一字段34";
            colInfo.MaxLength = 40;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col5";
            colInfo.DBType = DbType.DateTime;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段5";
            colInfo.DefaultValue = "1753-01-01";
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col6";
            colInfo.DBType = DbType.Int32;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段6";
            colInfo.DefaultValue = 100;
            tableInfo.Columns.Add(colInfo);

            colInfo = new ColumnInfo();
            colInfo.ColumnName = "Col7";
            colInfo.DBType = DbType.Decimal;
            colInfo.CurrTable = tableInfo;
            colInfo.Remarks = "字段7";
            colInfo.DefaultValue = 100.32;
            colInfo.Precision = 10;
            colInfo.Scale = 2;
            tableInfo.Columns.Add(colInfo);

            return tableInfo;
        }

        [TestMethod]
        public void AddFKTest()
        {
            TableInfo fkTableInfo = FillTable4();
            TableInfo refTableInfo1 = FillTable2();
            TableInfo refTableInfo2 = FillTable3();
            DalManager.DalFactory.DropTable(fkTableInfo.TableName);
            DalManager.DalFactory.DropTable(refTableInfo1.TableName);
            DalManager.DalFactory.DropTable(refTableInfo2.TableName);
            DalManager.DalFactory.CreateTable(refTableInfo1);
            DalManager.DalFactory.CreateTable(refTableInfo2);
            DalManager.DalFactory.CreateTable(fkTableInfo);
            //ColumnInfo fkColumnInfo = fkTableInfo.Columns.FirstOrDefault<ColumnInfo>(ci=>ci.ColumnName.Equals("FKCol1"));
            //DalManager.DalFactory.AddFK(fkTableInfo, refTableInfo1.TableName, fkColumnInfo, "Col1");
            //fkColumnInfo = fkTableInfo.Columns.FirstOrDefault<ColumnInfo>(ci => ci.ColumnName.Equals("FKCol2"));
            //DalManager.DalFactory.AddFK(fkTableInfo, refTableInfo2.TableName, fkColumnInfo, "Col1");
        }

        [TestMethod]
        public void DropFKTest()
        {
            TableInfo fkTableInfo = FillTable4();
            TableInfo refTableInfo1 = FillTable2();
            TableInfo refTableInfo2 = FillTable3();
            ColumnInfo fkColumnInfo1 = fkTableInfo.Columns.FirstOrDefault<ColumnInfo>(ci => ci.ColumnName.Equals("FKCol1"));
            ColumnInfo fkColumnInfo2 = fkTableInfo.Columns.FirstOrDefault<ColumnInfo>(ci => ci.ColumnName.Equals("FKCol2"));
            DalManager.DalFactory.DropColumn(fkTableInfo, fkColumnInfo1);
            //DalManager.DalFactory.DropColumn(fkTableInfo, fkColumnInfo2);
            //DalManager.DalFactory.DropFK(fkTableInfo, fkColumnInfo1, "Col1");
            //DalManager.DalFactory.DropFK(fkTableInfo, fkColumnInfo2, "Col1");
        }
    }
}
