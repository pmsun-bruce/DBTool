namespace NFramework.DBTool.Test.Entity
{
    #region Reference

    using System;
    using System.Collections;
    using System.Collections.Generic;

    #endregion

    /// <summary>
	///	��˾
	/// </summary>
	[Serializable]
	public class Company : NFramework.DBTool.Common.IRVersion
	{
		#region Fields & Properties
		
        /// <summary>
        /// ��˾ID
        /// </summary>
        private string companyId;
        /// <summary>
        /// ��˾ID
        /// </summary>
        public virtual string CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }

        /// <summary>
        /// ��˾���
        /// </summary>
        private string companyCode;
        /// <summary>
        /// ��˾���
        /// </summary>
        public virtual string CompanyCode
        {
            get { return companyCode; }
            set { companyCode = value; }
        } 

        /// <summary>
        /// ��˾����
        /// </summary>
        private string name;
        /// <summary>
        /// ��˾����
        /// </summary>
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        } 

        /// <summary>
        /// ��˾��ַ
        /// </summary>
        private string address;
        /// <summary>
        /// ��˾��ַ
        /// </summary>
        public virtual string Address
        {
            get { return address; }
            set { address = value; }
        } 

        /// <summary>
        /// �ϼ���˾ID
        /// </summary>
        private string parentCompanyId;
        /// <summary>
        /// �ϼ���˾ID
        /// </summary>
        public virtual string ParentCompanyId
        {
            get { return parentCompanyId; }
            set { parentCompanyId = value; }
        }

        /// <summary>
        /// ��¼�汾
        /// </summary>
        private int rVersion;
        /// <summary>
        /// ��¼�汾
        /// </summary>
        public virtual int RVersion
        {
            get { return rVersion; }
            set { rVersion = value; }
        } 

        /// <summary>
        /// ��˾״̬
        /// </summary>
        private int status;
        /// <summary>
        /// ��˾״̬
        /// </summary>
        public virtual int Status
        {
            get { return status; }
            set { status = value; }
        } 

        /// <summary>
        /// ������ID
        /// </summary>
        private string createrId;
        /// <summary>
        /// ������ID
        /// </summary>
        public virtual string CreaterId
        {
            get { return createrId; }
            set { createrId = value; }
        } 

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime createTime;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public virtual DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        } 

        /// <summary>
        /// ��������ID
        /// </summary>
        private string updatorId;
        /// <summary>
        /// ��������ID
        /// </summary>
        public virtual string UpdatorId
        {
            get { return updatorId; }
            set { updatorId = value; }
        } 

        /// <summary>
        /// ������ʱ��
        /// </summary>
        private DateTime updateTime;
        /// <summary>
        /// ������ʱ��
        /// </summary>
        public virtual DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }

        /// <summary>
        /// �ϼ���˾
        /// </summary>
        private Company parentCompany;
        /// <summary>
        /// �ϼ���˾
        /// </summary>
        public virtual Company ParentCompany
        {
            get{ return parentCompany; }
            set{ parentCompany = value; }
        }


        /// <summary>
        /// �����ӹ�˾���󼯺�
        /// </summary>
        private ISet<Company> subCompanyList;
        /// <summary>
        /// �����ӹ�˾���󼯺�
        /// </summary>
        public virtual ISet<Company> SubCompanyList
        {
            get { return subCompanyList; }
            set { subCompanyList = value; }
        }

        /// <summary>
        /// ��˾�����в��Ŷ��󼯺�
        /// </summary>
        private ISet<Department> departmentList;
        /// <summary>
        /// ��˾�����в��Ŷ��󼯺�
        /// </summary>
        public virtual ISet<Department> DepartmentList
        {
            get { return departmentList; }
            set { departmentList = value; }
        }

        /// <summary>
        /// ��˾������Ա�����󼯺�
        /// </summary>
        private ISet<Employee> employeeList;
        /// <summary>
        /// ��˾������Ա�����󼯺�
        /// </summary>
        public virtual ISet<Employee> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; }
        }

        /// <summary>
        /// ��˾������ְλ���󼯺�
        /// </summary>
        private ISet<Position> positionList;
        /// <summary>
        /// ��˾������ְλ���󼯺�
        /// </summary>
        public virtual ISet<Position> PositionList
        {
            get { return positionList; }
            set { positionList = value; }
        }

        #endregion

        #region Public Constuctors

        /// <summary>
        /// ���캯�����޲���
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
		/// ���캯��������ʼ���Բ���
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
