namespace NFramework.DBTool.Test.Entity
{
    #region Reference

    using System;
    using System.Collections;
    using System.Collections.Generic;

    #endregion

	/// <summary>
	/// ְλ
	/// </summary>
	[Serializable]
    public class Position : NFramework.DBTool.Common.IRVersion
	{
		#region Fields & Properties

        /// <summary>
        /// ְλID
        /// </summary>
        private string positionId;
        /// <summary>
        /// ְλID
        /// </summary>
        public virtual string PositionId
        {
            get { return positionId; }
            set { positionId = value; }
        } 

        /// <summary>
        /// ְλ���
        /// </summary>
        private string positionCode;
        /// <summary>
        /// ְλ���
        /// </summary>
        public virtual string PositionCode
        {
            get { return positionCode; }
            set { positionCode = value; }
        } 

        /// <summary>
        /// ���ڹ�˾ID
        /// </summary>
        private string companyId;
        /// <summary>
        /// ���ڹ�˾ID
        /// </summary>
        public virtual string CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }

        /// <summary>
        /// ���ڲ���ID
        /// </summary>
        private string departmentId;
        /// <summary>
        /// ���ڲ���ID
        /// </summary>
        public virtual string DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value; }
        } 

        /// <summary>
        /// ְλ����
        /// </summary>
        private string name;
        /// <summary>
        /// ְλ����
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
        /// ְλ״̬
        /// </summary>
        private int status;
        /// <summary>
        /// ְλ״̬
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
        /// ������˾����
        /// </summary>
        private Company currCompany;
        /// <summary>
        /// ������˾����
        /// </summary>
        public virtual Company CurrCompany
        {
            get { return currCompany; }
            set { currCompany = value; }
        }

        /// <summary>
        /// �������Ŷ���
        /// </summary>
        private Department currDepartment;
        /// <summary>
        /// �������Ŷ���
        /// </summary>
        public virtual Department CurrDepartment
        {
            get { return currDepartment; }
            set { currDepartment = value; }
        }

        /// <summary>
        /// ְλ������Ա�����󼯺�
        /// </summary>
        private ISet<Employee> employeeList;
        /// <summary>
        /// ְλ������Ա�����󼯺�
        /// </summary>
        public virtual ISet<Employee> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; }
        }

        #endregion

        #region Public Constuctors

        /// <summary>
        /// ���캯�����޲�
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
		/// ���캯������ʼ������
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
