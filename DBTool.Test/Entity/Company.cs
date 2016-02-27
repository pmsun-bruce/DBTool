namespace NFramework.DBTool.Test.Entity
{
    #region Reference

    using System;
    using System.Collections;
    using System.Collections.Generic;

    #endregion

    /// <summary>
	///	公司
	/// </summary>
	[Serializable]
	public class Company : NFramework.DBTool.Common.IRVersion
	{
		#region Fields & Properties
		
        /// <summary>
        /// 公司ID
        /// </summary>
        private string companyId;
        /// <summary>
        /// 公司ID
        /// </summary>
        public virtual string CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }

        /// <summary>
        /// 公司编号
        /// </summary>
        private string companyCode;
        /// <summary>
        /// 公司编号
        /// </summary>
        public virtual string CompanyCode
        {
            get { return companyCode; }
            set { companyCode = value; }
        } 

        /// <summary>
        /// 公司名称
        /// </summary>
        private string name;
        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        } 

        /// <summary>
        /// 公司地址
        /// </summary>
        private string address;
        /// <summary>
        /// 公司地址
        /// </summary>
        public virtual string Address
        {
            get { return address; }
            set { address = value; }
        } 

        /// <summary>
        /// 上级公司ID
        /// </summary>
        private string parentCompanyId;
        /// <summary>
        /// 上级公司ID
        /// </summary>
        public virtual string ParentCompanyId
        {
            get { return parentCompanyId; }
            set { parentCompanyId = value; }
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
        /// 公司状态
        /// </summary>
        private int status;
        /// <summary>
        /// 公司状态
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
        /// 上级公司
        /// </summary>
        private Company parentCompany;
        /// <summary>
        /// 上级公司
        /// </summary>
        public virtual Company ParentCompany
        {
            get{ return parentCompany; }
            set{ parentCompany = value; }
        }


        /// <summary>
        /// 所有子公司对象集合
        /// </summary>
        private ISet<Company> subCompanyList;
        /// <summary>
        /// 所有子公司对象集合
        /// </summary>
        public virtual ISet<Company> SubCompanyList
        {
            get { return subCompanyList; }
            set { subCompanyList = value; }
        }

        /// <summary>
        /// 公司下所有部门对象集合
        /// </summary>
        private ISet<Department> departmentList;
        /// <summary>
        /// 公司下所有部门对象集合
        /// </summary>
        public virtual ISet<Department> DepartmentList
        {
            get { return departmentList; }
            set { departmentList = value; }
        }

        /// <summary>
        /// 公司下所有员工对象集合
        /// </summary>
        private ISet<Employee> employeeList;
        /// <summary>
        /// 公司下所有员工对象集合
        /// </summary>
        public virtual ISet<Employee> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; }
        }

        /// <summary>
        /// 公司下所有职位对象集合
        /// </summary>
        private ISet<Position> positionList;
        /// <summary>
        /// 公司下所有职位对象集合
        /// </summary>
        public virtual ISet<Position> PositionList
        {
            get { return positionList; }
            set { positionList = value; }
        }

        #endregion

        #region Public Constuctors

        /// <summary>
        /// 构造函数，无参数
        /// </summary>
        public Company()
		{
			this.companyId = String.Empty; 
			this.companyCode = String.Empty; 
			this.name = String.Empty; 
			this.address = String.Empty; 
			this.parentCompanyId = null;
            this.rVersion = 0; 
			this.status = 0; 
			this.createrId = String.Empty; 
			this.createTime = DateTime.Now; 
			this.updatorId = String.Empty; 
			this.updateTime = DateTime.Now;

            this.parentCompany = null;
            this.subCompanyList = new HashSet<Company>();
            this.departmentList = new HashSet<Department>();
            this.employeeList = new HashSet<Employee>();
            this.positionList = new HashSet<Position>();
        }
		
		/// <summary>
		/// 构造函数，带初始属性参数
		/// </summary>
		public Company(
			string companyCode,
            string name,
            string address,
            string parentCompanyId, 
			int rVersion, 
			int status, 
			string createrId, 
			DateTime createTime, 
			string updatorId, 
			DateTime updateTime)
			: this()
		{
			this.companyCode = companyCode;
			this.name = name;
			this.address = String.Empty;
			this.parentCompanyId = null;
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
