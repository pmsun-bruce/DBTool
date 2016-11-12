namespace NFramework.DBTool.Test.Searcher
{
    #region Reference

    using System;
    using System.Collections;

    using NFramework.DBTool.QueryTool;
    using System.Data;

    #endregion

    /// <summary>
    ///	员工
    /// </summary>
    [Serializable]
	public sealed class EmployeeSearcher : NFramework.DBTool.QueryTool.Searcher
    {

        #region Fields & Properties
        
        /// <summary>
        /// 员工ID
        /// </summary>
        private SearchColumn employeeId;
        /// <summary>
        /// 员工ID
        /// </summary>
        public SearchColumn EmployeeId
        {
            get
            {
                if (this.employeeId == null)
                {
                    this.employeeId = new SearchColumn(this, "EmployeeId", DbType.AnsiString);
                    this.ConditionColumnList.Add(this.employeeId);
                }

                return this.employeeId;
            }
        } 
        /// <summary>
        /// 员工编号
        /// </summary>
        private SearchColumn employeeCode;
        /// <summary>
        /// 员工编号
        /// </summary>
        public SearchColumn EmployeeCode
        {
            get
            {
                if (this.employeeCode == null)
                {
                    this.employeeCode = new SearchColumn(this, "EmployeeCode", DbType.AnsiString);
                    this.ConditionColumnList.Add(this.employeeCode);
                }

                return this.employeeCode;
            }
        }

        /// <summary>
        /// 员工姓名
        /// </summary>
        private SearchColumn name;
        /// <summary>
        /// 员工姓名
        /// </summary>
        public SearchColumn Name
        {
            get
            {
                if (this.name == null)
                {
                    this.name = new SearchColumn(this, "Name", DbType.String);
                    this.ConditionColumnList.Add(this.name);
                }

                return this.name;
            }
        }

        /// <summary>
        /// 生日
        /// </summary>
        private SearchColumn birthday;
        /// <summary>
        /// 生日
        /// </summary>
        public SearchColumn Birthday
        {
            get
            {
                if (this.birthday == null)
                {
                    this.birthday = new SearchColumn(this, "Birthday", DbType.DateTime);
                    this.ConditionColumnList.Add(this.birthday);
                }

                return this.birthday;
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        private SearchColumn sex;
        /// <summary>
        /// 性别
        /// </summary>
        public SearchColumn Sex
        {
            get
            {
                if (this.sex == null)
                {
                    this.sex = new SearchColumn(this, "Sex", DbType.Int32);
                    this.ConditionColumnList.Add(this.sex);
                }

                return this.sex;
            }
        }

        /// <summary>
        /// 所在公司ID
        /// </summary>
        private SearchColumn companyId;
        /// <summary>
        /// 所在公司ID
        /// </summary>
        public SearchColumn CompanyId
        {
            get
            {
                if (this.companyId == null)
                {
                    this.companyId = new SearchColumn(this, "CompanyId", DbType.AnsiString);
                    this.ConditionColumnList.Add(this.companyId);
                }

                return this.companyId;
            }
        }

        /// <summary>
        /// 所在部门ID
        /// </summary>
        private SearchColumn departmentId;
        /// <summary>
        /// 所在部门ID
        /// </summary>
        public SearchColumn DepartmentId
        {
            get
            {
                if (this.departmentId == null)
                {
                    this.departmentId = new SearchColumn(this, "DepartmentId", DbType.AnsiString);
                    this.ConditionColumnList.Add(this.departmentId);
                }

                return this.departmentId;
            }
        }

        /// <summary>
        /// 所属职位ID
        /// </summary>
        private SearchColumn positionId;
        /// <summary>
        /// 所属职位ID
        /// </summary>
        public SearchColumn PositionId
        {
            get
            {
                if (this.positionId == null)
                {
                    this.positionId = new SearchColumn(this, "PositionId", DbType.AnsiString);
                    this.ConditionColumnList.Add(this.positionId);
                }

                return this.positionId;
            }
        }

        /// <summary>
        /// 级别
        /// </summary>
        private SearchColumn rand;
        /// <summary>
        /// 级别
        /// </summary>
        public SearchColumn Rand
        {
            get
            {
                if (this.rand == null)
                {
                    this.rand = new SearchColumn(this, "Rand", DbType.Int32);
                    this.ConditionColumnList.Add(this.rand);
                }

                return this.rand;
            }
        }

        /// <summary>
        /// 记录版本
        /// </summary>
        private SearchColumn rVersion;
        /// <summary>
        /// 记录版本
        /// </summary>
        public SearchColumn RVersion
        {
            get
            {
                if (this.rVersion == null)
                {
                    this.rVersion = new SearchColumn(this, "RVersion", DbType.Int32);
                    this.ConditionColumnList.Add(this.rVersion);
                }

                return this.rVersion;
            }
        }

        /// <summary>
        /// 员工状态
        /// </summary>
        private SearchColumn status;
        /// <summary>
        /// 员工状态
        /// </summary>
        public SearchColumn Status
        {
            get
            {
                if (this.status == null)
                {
                    this.status = new SearchColumn(this, "Status", DbType.Int32);
                    this.ConditionColumnList.Add(this.status);
                }

                return this.status;
            }
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        private SearchColumn createrId;
        /// <summary>
        /// 创建人ID
        /// </summary>
        public SearchColumn CreaterId
        {
            get
            {
                if (this.createrId == null)
                {
                    this.createrId = new SearchColumn(this, "CreaterId", DbType.AnsiString);
                    this.ConditionColumnList.Add(this.createrId);
                }

                return this.createrId;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        private SearchColumn createTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public SearchColumn CreateTime
        {
            get
            {
                if (this.createTime == null)
                {
                    this.createTime = new SearchColumn(this, "CreateTime", DbType.DateTime);
                    this.ConditionColumnList.Add(this.createTime);
                }

                return this.createTime;
            }
        }

        /// <summary>
        /// 最后更新人ID
        /// </summary>
        private SearchColumn updatorId;
        /// <summary>
        /// 最后更新人ID
        /// </summary>
        public SearchColumn UpdatorId
        {
            get
            {
                if (this.updatorId == null)
                {
                    this.updatorId = new SearchColumn(this, "UpdatorId", DbType.AnsiString);
                    this.ConditionColumnList.Add(this.updatorId);
                }

                return this.updatorId;
            }
        }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        private SearchColumn updateTime;
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public SearchColumn UpdateTime
        {
            get
            {
                if (this.updateTime == null)
                {
                    this.updateTime = new SearchColumn(this, "UpdateTime", DbType.DateTime);
                    this.ConditionColumnList.Add(this.updateTime);
                }

                return this.updateTime;
            }
        }

        /// <summary>
        /// 开始工作日期
        /// </summary>
        private SearchColumn startWorkDate;
        /// <summary>
        /// 开始工作时间
        /// </summary>
        public SearchColumn StartWorkDate
        {
            get
            {
                if (this.startWorkDate == null)
                {
                    this.startWorkDate = new SearchColumn(this, "StartWorkDate", DbType.DateTime);
                    this.ConditionColumnList.Add(this.startWorkDate);
                }

                return this.startWorkDate;
            }
        }

        /// <summary>
        /// 入职日期
        /// </summary>
        private SearchColumn joinDate;
        /// <summary>
        /// 入职日期
        /// </summary>
        public SearchColumn JoinDate
        {
            get
            {
                if (this.joinDate == null)
                {
                    this.joinDate = new SearchColumn(this, "JoinDate", DbType.DateTime);
                    this.ConditionColumnList.Add(this.joinDate);
                }

                return this.joinDate;
            }
        }

        /// <summary>
        /// 所属公司对象
        /// </summary>
        private CompanySearcher currCompany;
        /// <summary>
        /// 所属公司对象
        /// </summary>
        public CompanySearcher CurrCompany
        {
            get
            {
                return this.currCompany;
            }
            set
            {
                if (this.currCompany != null)
                {
                    this.RelationSearcherList.Remove(this.currCompany);
                }

                this.currCompany = value;

                if (this.currCompany != null)
                {
                    this.currCompany.ConditionMaxIndex = this.ConditionMaxIndex;
                    this.RelationSearcherList.Add(this.currCompany);
                }
            }
        }

        /// <summary>
        /// 所属部门对象
        /// </summary>
        private DepartmentSearcher currDepartment;
        /// <summary>
        /// 所属部门对象
        /// </summary>
        public DepartmentSearcher CurrDepartment
        {
            get
            {
                return this.currDepartment;
            }
            set
            {
                if (this.currDepartment != null)
                {
                    this.RelationSearcherList.Remove(this.currDepartment);
                }

                this.currDepartment = value;

                if (this.currDepartment != null)
                {
                    this.currDepartment.ConditionMaxIndex = this.ConditionMaxIndex;
                    this.RelationSearcherList.Add(this.currDepartment);
                }
            }
        }

        /// <summary>
        /// 所属职位对象
        /// </summary>
        private PositionSearcher currPosition;
        /// <summary>
        /// 所属职位对象
        /// </summary>
        public PositionSearcher CurrPosition
        {
            get
            {
                return this.currPosition;
            }
            set
            {
                if (this.currPosition != null)
                {
                    this.RelationSearcherList.Remove(this.currPosition);
                }

                this.currPosition = value;

                if (this.currPosition != null)
                {
                    this.currPosition.ConditionMaxIndex = this.ConditionMaxIndex;
                    this.RelationSearcherList.Add(this.currPosition);
                }
            }
        }

        #endregion

        #region Public Constuctors

        /// <summary>
        /// 构造函数，无参
        /// </summary>
        public EmployeeSearcher()
            : base("Employee")
		{
		}
		
		#endregion
	}
}
