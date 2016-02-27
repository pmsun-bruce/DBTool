namespace NFramework.DBTool.Test.Searcher
{
    #region Reference

    using System;
    using System.Collections;

    using NFramework.DBTool.QueryTool;

    #endregion

    /// <summary>
    ///	Ա��
    /// </summary>
    [Serializable]
	public sealed class EmployeeSearcher : NFramework.DBTool.QueryTool.Searcher
    {

        #region Fields & Properties
        
        /// <summary>
        /// Ա��ID
        /// </summary>
        private SearchColumn employeeId;
        /// <summary>
        /// Ա��ID
        /// </summary>
        public SearchColumn EmployeeId
        {
            get
            {
                if (this.employeeId == null)
                {
                    this.employeeId = new SearchColumn(this, "EmployeeId");
                    this.ConditionColumnList.Add(this.employeeId);
                }

                return this.employeeId;
            }
        } 
        /// <summary>
        /// Ա�����
        /// </summary>
        private SearchColumn employeeCode;
        /// <summary>
        /// Ա�����
        /// </summary>
        public SearchColumn EmployeeCode
        {
            get
            {
                if (this.employeeCode == null)
                {
                    this.employeeCode = new SearchColumn(this, "EmployeeCode");
                    this.ConditionColumnList.Add(this.employeeCode);
                }

                return this.employeeCode;
            }
        }

        /// <summary>
        /// Ա������
        /// </summary>
        private SearchColumn name;
        /// <summary>
        /// Ա������
        /// </summary>
        public SearchColumn Name
        {
            get
            {
                if (this.name == null)
                {
                    this.name = new SearchColumn(this, "Name");
                    this.ConditionColumnList.Add(this.name);
                }

                return this.name;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private SearchColumn birthday;
        /// <summary>
        /// ����
        /// </summary>
        public SearchColumn Birthday
        {
            get
            {
                if (this.birthday == null)
                {
                    this.birthday = new SearchColumn(this, "Birthday");
                    this.ConditionColumnList.Add(this.birthday);
                }

                return this.birthday;
            }
        }

        /// <summary>
        /// �Ա�
        /// </summary>
        private SearchColumn sex;
        /// <summary>
        /// �Ա�
        /// </summary>
        public SearchColumn Sex
        {
            get
            {
                if (this.sex == null)
                {
                    this.sex = new SearchColumn(this, "Sex");
                    this.ConditionColumnList.Add(this.sex);
                }

                return this.sex;
            }
        }

        /// <summary>
        /// ���ڹ�˾ID
        /// </summary>
        private SearchColumn companyId;
        /// <summary>
        /// ���ڹ�˾ID
        /// </summary>
        public SearchColumn CompanyId
        {
            get
            {
                if (this.companyId == null)
                {
                    this.companyId = new SearchColumn(this, "CompanyId");
                    this.ConditionColumnList.Add(this.companyId);
                }

                return this.companyId;
            }
        }

        /// <summary>
        /// ���ڲ���ID
        /// </summary>
        private SearchColumn departmentId;
        /// <summary>
        /// ���ڲ���ID
        /// </summary>
        public SearchColumn DepartmentId
        {
            get
            {
                if (this.departmentId == null)
                {
                    this.departmentId = new SearchColumn(this, "DepartmentId");
                    this.ConditionColumnList.Add(this.departmentId);
                }

                return this.departmentId;
            }
        }

        /// <summary>
        /// ����ְλID
        /// </summary>
        private SearchColumn positionId;
        /// <summary>
        /// ����ְλID
        /// </summary>
        public SearchColumn PositionId
        {
            get
            {
                if (this.positionId == null)
                {
                    this.positionId = new SearchColumn(this, "PositionId");
                    this.ConditionColumnList.Add(this.positionId);
                }

                return this.positionId;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private SearchColumn rand;
        /// <summary>
        /// ����
        /// </summary>
        public SearchColumn Rand
        {
            get
            {
                if (this.rand == null)
                {
                    this.rand = new SearchColumn(this, "Rand");
                    this.ConditionColumnList.Add(this.rand);
                }

                return this.rand;
            }
        }

        /// <summary>
        /// ��¼�汾
        /// </summary>
        private SearchColumn rVersion;
        /// <summary>
        /// ��¼�汾
        /// </summary>
        public SearchColumn RVersion
        {
            get
            {
                if (this.rVersion == null)
                {
                    this.rVersion = new SearchColumn(this, "RVersion");
                    this.ConditionColumnList.Add(this.rVersion);
                }

                return this.rVersion;
            }
        }

        /// <summary>
        /// Ա��״̬
        /// </summary>
        private SearchColumn status;
        /// <summary>
        /// Ա��״̬
        /// </summary>
        public SearchColumn Status
        {
            get
            {
                if (this.status == null)
                {
                    this.status = new SearchColumn(this, "Status");
                    this.ConditionColumnList.Add(this.status);
                }

                return this.status;
            }
        }

        /// <summary>
        /// ������ID
        /// </summary>
        private SearchColumn createrId;
        /// <summary>
        /// ������ID
        /// </summary>
        public SearchColumn CreaterId
        {
            get
            {
                if (this.createrId == null)
                {
                    this.createrId = new SearchColumn(this, "CreaterId");
                    this.ConditionColumnList.Add(this.createrId);
                }

                return this.createrId;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private SearchColumn createTime;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public SearchColumn CreateTime
        {
            get
            {
                if (this.createTime == null)
                {
                    this.createTime = new SearchColumn(this, "CreateTime");
                    this.ConditionColumnList.Add(this.createTime);
                }

                return this.createTime;
            }
        }

        /// <summary>
        /// ��������ID
        /// </summary>
        private SearchColumn updatorId;
        /// <summary>
        /// ��������ID
        /// </summary>
        public SearchColumn UpdatorId
        {
            get
            {
                if (this.updatorId == null)
                {
                    this.updatorId = new SearchColumn(this, "UpdatorId");
                    this.ConditionColumnList.Add(this.updatorId);
                }

                return this.updatorId;
            }
        }

        /// <summary>
        /// ������ʱ��
        /// </summary>
        private SearchColumn updateTime;
        /// <summary>
        /// ������ʱ��
        /// </summary>
        public SearchColumn UpdateTime
        {
            get
            {
                if (this.updateTime == null)
                {
                    this.updateTime = new SearchColumn(this, "UpdateTime");
                    this.ConditionColumnList.Add(this.updateTime);
                }

                return this.updateTime;
            }
        }

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        private SearchColumn startWorkDate;
        /// <summary>
        /// ��ʼ����ʱ��
        /// </summary>
        public SearchColumn StartWorkDate
        {
            get
            {
                if (this.startWorkDate == null)
                {
                    this.startWorkDate = new SearchColumn(this, "StartWorkDate");
                    this.ConditionColumnList.Add(this.startWorkDate);
                }

                return this.startWorkDate;
            }
        }

        /// <summary>
        /// ��ְ����
        /// </summary>
        private SearchColumn joinDate;
        /// <summary>
        /// ��ְ����
        /// </summary>
        public SearchColumn JoinDate
        {
            get
            {
                if (this.joinDate == null)
                {
                    this.joinDate = new SearchColumn(this, "JoinDate");
                    this.ConditionColumnList.Add(this.joinDate);
                }

                return this.joinDate;
            }
        }

        /// <summary>
        /// ������˾����
        /// </summary>
        private CompanySearcher currCompany;
        /// <summary>
        /// ������˾����
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
                    this.RelationSearcherList.Add(this.currCompany);
                }
            }
        }

        /// <summary>
        /// �������Ŷ���
        /// </summary>
        private DepartmentSearcher currDepartment;
        /// <summary>
        /// �������Ŷ���
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
                    this.RelationSearcherList.Add(this.currDepartment);
                }
            }
        }

        /// <summary>
        /// ����ְλ����
        /// </summary>
        private PositionSearcher currPosition;
        /// <summary>
        /// ����ְλ����
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
                    this.RelationSearcherList.Add(this.currPosition);
                }
            }
        }

        #endregion

        #region Public Constuctors

        /// <summary>
        /// ���캯�����޲�
        /// </summary>
        public EmployeeSearcher()
		{
            this.TableName = "Employee"; 
		}
		
		#endregion
	}
}
