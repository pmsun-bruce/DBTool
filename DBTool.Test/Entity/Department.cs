namespace NFramework.DBTool.Test.Entity
{
    #region Reference

    using System;
    using System.Collections;
    using System.Collections.Generic;

    #endregion

	/// <summary>
	/// ����
	/// </summary>
	[Serializable]
    public class Department : NFramework.DBTool.Common.IRVersion
    {

        #region Fields & Properties
        
        /// <summary>
        /// ����ID
        /// </summary>
        private string departmentId;
        /// <summary>
        /// ����ID
        /// </summary>
        public virtual string DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value; }
        } 

        /// <summary>
        /// ���ű��
        /// </summary>
        private string departmentCode;
        /// <summary>
        /// ���ű��
        /// </summary>
        public virtual string DepartmentCode
        {
            get { return departmentCode; }
            set { departmentCode = value; }
        } 

        /// <summary>
        /// ������˾ID
        /// </summary>
        private string companyId;
        /// <summary>
        /// ������˾ID
        /// </summary>
        public virtual string CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        } 

        /// <summary>
        /// ��������
        /// </summary>
        private string name;
        /// <summary>
        /// ��������
        /// </summary>
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
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
        /// ����״̬
        /// </summary>
        private int status;
        /// <summary>
        /// ����״̬
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
        /// ��ǰ������˾����
        /// </summary>
        private Company currCompany;
        /// <summary>
        /// ��ǰ������˾����
        /// </summary>
        public virtual Company CurrCompany
        {
            get { return currCompany; }
            set { currCompany = value; }
        }


        /// <summary>
        /// ����������Ա�����󼯺�
        /// </summary>
        private ISet<Employee> employeeList;
        /// <summary>
        /// ����������Ա�����󼯺�
        /// </summary>
        public virtual ISet<Employee> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; }
        }

        /// <summary>
        /// ����������ְλ���󼯺�
        /// </summary>
        private ISet<Position> positionList;
        /// <summary>
        /// ����������ְλ���󼯺�
        /// </summary>
        public virtual ISet<Position> PositionList
        {
            get { return positionList; }
            set { positionList = value; }
        }

        #endregion

        #region Public Constuctors

        /// <summary>
        /// ���캯�����޲�
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
		/// ���캯������ʼ������
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
