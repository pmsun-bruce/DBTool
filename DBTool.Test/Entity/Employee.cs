namespace NFramework.DBTool.Test.Entity
{
    #region Reference

    using System;
    using System.Collections;

    #endregion

	/// <summary>
	///	Ա��
	/// </summary>
	[Serializable]
	public class Employee : NFramework.DBTool.Common.IRVersion
    {

        #region Fields & Properties
        
        /// <summary>
        /// Ա��ID
        /// </summary>
        private string employeeId;
        /// <summary>
        /// Ա��ID
        /// </summary>
        public virtual string EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        } 
        /// <summary>
        /// Ա�����
        /// </summary>
        private string employeeCode;
        /// <summary>
        /// Ա�����
        /// </summary>
        public virtual string EmployeeCode
        {
            get { return employeeCode; }
            set { employeeCode = value; }
        }

        /// <summary>
        /// Ա������
        /// </summary>
        private string name;
        /// <summary>
        /// Ա������
        /// </summary>
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        private DateTime? birthday;
        /// <summary>
        /// ����
        /// </summary>
        public virtual DateTime? Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        /// <summary>
        /// �Ա�
        /// </summary>
        private int sex;
        /// <summary>
        /// �Ա�
        /// </summary>
        public virtual int Sex
        {
            get { return sex; }
            set { sex = value; }
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
        /// ����ְλID
        /// </summary>
        private string positionId;
        /// <summary>
        /// ����ְλID
        /// </summary>
        public virtual string PositionId
        {
            get { return positionId; }
            set { positionId = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        private int rand;
        /// <summary>
        /// ����
        /// </summary>
        public virtual int Rand
        {
            get { return rand; }
            set { rand = value; }
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
        /// Ա��״̬
        /// </summary>
        private int status;
        /// <summary>
        /// Ա��״̬
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
        /// ��ʼ��������
        /// </summary>
        private DateTime? startWorkDate;
        /// <summary>
        /// ��ʼ����ʱ��
        /// </summary>
        public virtual DateTime? StartWorkDate
        {
            get { return startWorkDate; }
            set { startWorkDate = value; }
        }

        /// <summary>
        /// ��ְ����
        /// </summary>
        private DateTime? joinDate;
        /// <summary>
        /// ��ְ����
        /// </summary>
        public virtual DateTime? JoinDate
        {
            get { return joinDate; }
            set { joinDate = value; }
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
        /// ����ְλ����
        /// </summary>
        private Position currPosition;
        /// <summary>
        /// ����ְλ����
        /// </summary>
        public virtual Position CurrPosition
        {
            get { return currPosition; }
            set { currPosition = value; }
        }

        #endregion

        #region Public Constuctors

        /// <summary>
        /// ���캯�����޲�
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
		/// ���캯������ʼ������
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
