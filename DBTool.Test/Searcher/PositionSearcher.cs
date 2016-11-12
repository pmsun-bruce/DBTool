namespace NFramework.DBTool.Test.Searcher
{
    #region Reference

    using System;
    using System.Collections;

    using NFramework.DBTool.QueryTool;
    using System.Data;

    #endregion

    /// <summary>
    /// ְλ
    /// </summary>
    [Serializable]
	public sealed class PositionSearcher : NFramework.DBTool.QueryTool.Searcher
    {
		#region Fields & Properties

        /// <summary>
        /// ְλID
        /// </summary>
        private SearchColumn positionId;
        /// <summary>
        /// ְλID
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
        /// ְλ���
        /// </summary>
        private SearchColumn positionCode;
        /// <summary>
        /// ְλ���
        /// </summary>
        public SearchColumn PositionCode
        {
            get
            {
                if (this.positionCode == null)
                {
                    this.positionCode = new SearchColumn(this, "PositionCode", DbType.AnsiString);
                    this.ConditionColumnList.Add(this.positionCode);
                }

                return this.positionCode;
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
                    this.companyId = new SearchColumn(this, "CompanyId", DbType.AnsiString);
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
                    this.departmentId = new SearchColumn(this, "DepartmentId", DbType.AnsiString);
                    this.ConditionColumnList.Add(this.departmentId);
                }

                return this.departmentId;
            }
        } 

        /// <summary>
        /// ְλ����
        /// </summary>
        private SearchColumn name;
        /// <summary>
        /// ְλ����
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
                    this.rVersion = new SearchColumn(this, "RVersion", DbType.Int32);
                    this.ConditionColumnList.Add(this.rVersion);
                }

                return this.rVersion;
            }
        }

        /// <summary>
        /// ְλ״̬
        /// </summary>
        private SearchColumn status;
        /// <summary>
        /// ְλ״̬
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
                    this.createrId = new SearchColumn(this, "CreaterId", DbType.AnsiString);
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
                    this.createTime = new SearchColumn(this, "CreateTime", DbType.DateTime);
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
                    this.updatorId = new SearchColumn(this, "UpdatorId", DbType.AnsiString);
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
                    this.updateTime = new SearchColumn(this, "UpdateTime", DbType.DateTime);
                    this.ConditionColumnList.Add(this.updateTime);
                }

                return this.updateTime;
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
                    this.currCompany.ConditionMaxIndex = this.ConditionMaxIndex;
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
                    this.currDepartment.ConditionMaxIndex = this.ConditionMaxIndex;
                    this.RelationSearcherList.Add(this.currDepartment);
                }
            }
        }
        
        #endregion

        #region Public Constuctors

        /// <summary>
        /// ���캯�����޲�
        /// </summary>
        public PositionSearcher() : base("Position")
		{
        }
		
		#endregion
	}
}
