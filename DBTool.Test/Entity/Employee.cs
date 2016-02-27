namespace NFramework.DBTool.Test.Entity
{
    #region Reference

    using System;
    using System.Collections;

    #endregion

	/// <summary>
	///	员工
	/// </summary>
	[Serializable]
	public class Employee : NFramework.DBTool.Common.IRVersion
    {

        #region Fields & Properties
        
        /// <summary>
        /// 员工ID
        /// </summary>
        private string employeeId;
        /// <summary>
        /// 员工ID
        /// </summary>
        public virtual string EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        } 
        /// <summary>
        /// 员工编号
        /// </summary>
        private string employeeCode;
        /// <summary>
        /// 员工编号
        /// </summary>
        public virtual string EmployeeCode
        {
            get { return employeeCode; }
            set { employeeCode = value; }
        }

        /// <summary>
        /// 员工姓名
        /// </summary>
        private string name;
        /// <summary>
        /// 员工姓名
        /// </summary>
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 生日
        /// </summary>
        private DateTime? birthday;
        /// <summary>
        /// 生日
        /// </summary>
        public virtual DateTime? Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        /// <summary>
        /// 性别
        /// </summary>
        private int sex;
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        /// <summary>
        /// 所在公司ID
        /// </summary>
        private string companyId;
        /// <summary>
        /// 所在公司ID
        /// </summary>
        public virtual string CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }

        /// <summary>
        /// 所在部门ID
        /// </summary>
        private string departmentId;
        /// <summary>
        /// 所在部门ID
        /// </summary>
        public virtual string DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value; }
        }

        /// <summary>
        /// 所属职位ID
        /// </summary>
        private string positionId;
        /// <summary>
        /// 所属职位ID
        /// </summary>
        public virtual string PositionId
        {
            get { return positionId; }
            set { positionId = value; }
        }

        /// <summary>
        /// 级别
        /// </summary>
        private int rand;
        /// <summary>
        /// 级别
        /// </summary>
        public virtual int Rand
        {
            get { return rand; }
            set { rand = value; }
        }

        /// <summary>
        /// 记录版本
        /// </summary>
        private int rVersion;
        /// <summary>
        /// 记录版本
        /// </summary>
        public virtual int RVersion
        {
            get { return rVersion; }
            set { rVersion = value; }
        }

        /// <summary>
        /// 员工状态
        /// </summary>
        private int status;
        /// <summary>
        /// 员工状态
        /// </summary>
        public virtual int Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        private string createrId;
        /// <summary>
        /// 创建人ID
        /// </summary>
        public virtual string CreaterId
        {
            get { return createrId; }
            set { createrId = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime createTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 最后更新人ID
        /// </summary>
        private string updatorId;
        /// <summary>
        /// 最后更新人ID
        /// </summary>
        public virtual string UpdatorId
        {
            get { return updatorId; }
            set { updatorId = value; }
        }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        private DateTime updateTime;
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }

        /// <summary>
        /// 开始工作日期
        /// </summary>
        private DateTime? startWorkDate;
        /// <summary>
        /// 开始工作时间
        /// </summary>
        public virtual DateTime? StartWorkDate
        {
            get { return startWorkDate; }
            set { startWorkDate = value; }
        }

        /// <summary>
        /// 入职日期
        /// </summary>
        private DateTime? joinDate;
        /// <summary>
        /// 入职日期
        /// </summary>
        public virtual DateTime? JoinDate
        {
            get { return joinDate; }
            set { joinDate = value; }
        }

        /// <summary>
        /// 所属公司对象
        /// </summary>
        private Company currCompany;
        /// <summary>
        /// 所属公司对象
        /// </summary>
        public virtual Company CurrCompany
        {
            get { return currCompany; }
            set { currCompany = value; }
        }

        /// <summary>
        /// 所属部门对象
        /// </summary>
        private Department currDepartment;
        /// <summary>
        /// 所属部门对象
        /// </summary>
        public virtual Department CurrDepartment
        {
            get { return currDepartment; }
            set { currDepartment = value; }
        }

        /// <summary>
        /// 所属职位对象
        /// </summary>
        private Position currPosition;
        /// <summary>
        /// 所属职位对象
        /// </summary>
        public virtual Position CurrPosition
        {
            get { return currPosition; }
            set { currPosition = value; }
        }

        #endregion

        #region Public Constuctors

        /// <summary>
        /// 构造函数，无参
        /// </summary>
        public Employee()
		{
			this.employeeId = String.Empty; 
			this.employeeCode = String.Empty; 
			this.name = String.Empty; 
			this.birthday = null; 
			this.sex = 0; 
			this.companyId = String.Empty;
            this.departmentId = String.Empty;
            this.positionId = String.Empty;
            this.rand = 0; 
			this.rVersion = 0; 
			this.status = 0; 
			this.createrId = String.Empty; 
			this.createTime = DateTime.Now; 
			this.updatorId = String.Empty; 
			this.updateTime = DateTime.Now; 
		}
		
		/// <summary>
		/// 构造函数，初始化属性
		/// </summary>
		public Employee(
			string employeeCode, 
            string name,
            DateTime birthday,
			int sex, 
			string companyId, 
			string departmentId, 
			string positionId, 
			int rand, 
			int rVersion, 
			int status, 
			string createrId, 
			DateTime createTime, 
			string updatorId, 
			DateTime updateTime)
			: this()
		{
			this.employeeCode = employeeCode;
			this.name = name;
			this.birthday = birthday;
			this.sex = sex;
			this.companyId = companyId;
            this.departmentId = departmentId;
			this.positionId = positionId;
			this.rand = rand;
			this.rVersion = rVersion;
			this.status = status;
			this.createrId = createrId;
			this.createTime = createTime;
			this.updatorId = updatorId;
			this.updateTime = updateTime;
		}

		#endregion
	}
}
