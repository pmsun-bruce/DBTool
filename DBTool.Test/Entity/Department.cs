namespace NFramework.DBTool.Test.Entity
{
    #region Reference

    using System;
    using System.Collections;
    using System.Collections.Generic;

    #endregion

	/// <summary>
	/// 部门
	/// </summary>
	[Serializable]
    public class Department : NFramework.DBTool.Common.IRVersion
    {

        #region Fields & Properties
        
        /// <summary>
        /// 部门ID
        /// </summary>
        private string departmentId;
        /// <summary>
        /// 部门ID
        /// </summary>
        public virtual string DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value; }
        } 

        /// <summary>
        /// 部门编号
        /// </summary>
        private string departmentCode;
        /// <summary>
        /// 部门编号
        /// </summary>
        public virtual string DepartmentCode
        {
            get { return departmentCode; }
            set { departmentCode = value; }
        } 

        /// <summary>
        /// 所属公司ID
        /// </summary>
        private string companyId;
        /// <summary>
        /// 所属公司ID
        /// </summary>
        public virtual string CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        } 

        /// <summary>
        /// 部门名称
        /// </summary>
        private string name;
        /// <summary>
        /// 部门名称
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
        /// 部门状态
        /// </summary>
        private int status;
        /// <summary>
        /// 部门状态
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
        /// 当前所属公司对象
        /// </summary>
        private Company currCompany;
        /// <summary>
        /// 当前所属公司对象
        /// </summary>
        public virtual Company CurrCompany
        {
            get { return currCompany; }
            set { currCompany = value; }
        }


        /// <summary>
        /// 部门下所有员工对象集合
        /// </summary>
        private ISet<Employee> employeeList;
        /// <summary>
        /// 部门下所有员工对象集合
        /// </summary>
        public virtual ISet<Employee> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; }
        }

        /// <summary>
        /// 部门下所有职位对象集合
        /// </summary>
        private ISet<Position> positionList;
        /// <summary>
        /// 部门下所有职位对象集合
        /// </summary>
        public virtual ISet<Position> PositionList
        {
            get { return positionList; }
            set { positionList = value; }
        }

        #endregion

        #region Public Constuctors

        /// <summary>
        /// 构造函数，无参
        /// </summary>
        public Department()
		{
			this.departmentId = String.Empty; 
			this.departmentCode = String.Empty; 
			this.companyId = String.Empty; 
			this.name = String.Empty; 
			this.rVersion = 0; 
			this.status = 0; 
			this.createrId = String.Empty; 
			this.createTime = DateTime.Now; 
			this.updatorId = String.Empty; 
			this.updateTime = DateTime.Now;

            this.employeeList = new HashSet<Employee>();
            this.positionList = new HashSet<Position>();
        }
		
		/// <summary>
		/// 构造函数，初始化属性
		/// </summary>
		public Department(
			string departmentCode, 
			string companyId, 
			string name, 
			int rVersion, 
			int status, 
			string createrId, 
			DateTime createTime, 
			string updatorId, 
			DateTime updateTime)
			: this()
		{
			this.departmentCode = departmentCode;
			this.companyId = companyId;
			this.name = name;
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
