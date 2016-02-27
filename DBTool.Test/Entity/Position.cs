namespace NFramework.DBTool.Test.Entity
{
    #region Reference

    using System;
    using System.Collections;
    using System.Collections.Generic;

    #endregion

	/// <summary>
	/// 职位
	/// </summary>
	[Serializable]
    public class Position : NFramework.DBTool.Common.IRVersion
	{
		#region Fields & Properties

        /// <summary>
        /// 职位ID
        /// </summary>
        private string positionId;
        /// <summary>
        /// 职位ID
        /// </summary>
        public virtual string PositionId
        {
            get { return positionId; }
            set { positionId = value; }
        } 

        /// <summary>
        /// 职位编号
        /// </summary>
        private string positionCode;
        /// <summary>
        /// 职位编号
        /// </summary>
        public virtual string PositionCode
        {
            get { return positionCode; }
            set { positionCode = value; }
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
        /// 职位名称
        /// </summary>
        private string name;
        /// <summary>
        /// 职位名称
        /// </summary>
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
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
        /// 职位状态
        /// </summary>
        private int status;
        /// <summary>
        /// 职位状态
        /// </summary>
        public virtual int Status
        {
            get { return status; }
            set { status = value; }
        } 

        /// <summary>
        /// 创建者ID
        /// </summary>
        private string createrId;
        /// <summary>
        /// 创建者ID
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
        /// 职位下所有员工对象集合
        /// </summary>
        private ISet<Employee> employeeList;
        /// <summary>
        /// 职位下所有员工对象集合
        /// </summary>
        public virtual ISet<Employee> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; }
        }

        #endregion

        #region Public Constuctors

        /// <summary>
        /// 构造函数，无参
        /// </summary>
        public Position()
		{
			this.positionId = String.Empty; 
			this.positionCode = String.Empty; 
			this.companyId = String.Empty;
            this.departmentId = String.Empty;
            this.name = String.Empty; 
			this.rVersion = 0; 
			this.status = 0; 
			this.createrId = String.Empty; 
			this.createTime = DateTime.Now; 
			this.updatorId = String.Empty; 
			this.updateTime = DateTime.Now;

            this.employeeList = new HashSet<Employee>();
        }
		
		/// <summary>
		/// 构造函数，初始化属性
		/// </summary>
		public Position(
			string positionCode, 
			string companyId,
			string departmentId, 
			int rVersion, 
			string createrId, 
			DateTime createTime, 
			string updatorId,
            DateTime updateTime)
			: this()
		{
			this.positionCode = positionCode;
			this.companyId = companyId;
			this.departmentId = departmentId;
			this.name = String.Empty;
			this.rVersion = rVersion;
			this.status = 0;
			this.createrId = createrId;
			this.createTime = createTime;
			this.updatorId = updatorId;
			this.updateTime = updateTime;
		}

		#endregion
	}
}
