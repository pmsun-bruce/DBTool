namespace NFramework.DBTool.Test.Searcher
{
    #region Reference

    using System;
    using System.Collections;

    using NFramework.DBTool.QueryTool;

    #endregion

    /// <summary>
    /// ����
    /// </summary>
    [Serializable]
	public class DepartmentSearcher : NFramework.DBTool.QueryTool.Searcher
    {

        #region Fields & Properties
        
        /// <summary>
        /// ����ID
        /// </summary>
        private SearchColumn departmentId;
        /// <summary>
        /// ����ID
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
        /// ���ű��
        /// </summary>
        private SearchColumn departmentCode;
        /// <summary>
        /// ���ű��
        /// </summary>
        public SearchColumn DepartmentCode
        {
            get
            {
                if (this.departmentCode == null)
                {
                    this.departmentCode = new SearchColumn(this, "DepartmentCode");
                    this.ConditionColumnList.Add(this.departmentCode);
                }

                return this.departmentCode;
            }
        } 

        /// <summary>
        /// ������˾ID
        /// </summary>
        private SearchColumn companyId;
        /// <summary>
        /// ������˾ID
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
        /// ��������
        /// </summary>
        private SearchColumn name;
        /// <summary>
        /// ��������
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
        /// ����״̬
        /// </summary>
        private SearchColumn status;
        /// <summary>
        /// ����״̬
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
        /// ��ǰ������˾����
        /// </summary>
        private CompanySearcher currCompany;
        /// <summary>
        /// ��ǰ������˾����
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
        
        #endregion

        #region Public Constuctors

        /// <summary>
        /// ���캯�����޲�
        /// </summary>
        public DepartmentSearcher()
		{
            this.TableName = "Department";
        }
		
		#endregion
	}
}
