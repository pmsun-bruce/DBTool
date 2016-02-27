namespace NFramework.DBTool.Test.Searcher
{
    #region Reference

    using System;
    using System.Collections;

    using NFramework.DBTool.QueryTool;

    #endregion

    /// <summary>
	///	��˾��ѯ����
	/// </summary>
	[Serializable]
	public class CompanySearcher : NFramework.DBTool.QueryTool.Searcher
	{
		#region Fields & Properties
		
        /// <summary>
        /// ��˾ID
        /// </summary>
        private SearchColumn companyId;
        /// <summary>
        /// ��˾ID
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
        /// ��˾���
        /// </summary>
        private SearchColumn companyCode;
        /// <summary>
        /// ��˾���
        /// </summary>
        public SearchColumn CompanyCode
        {
            get
            {
                if (this.companyCode == null)
                {
                    this.companyCode = new SearchColumn(this, "CompanyCode");
                    this.ConditionColumnList.Add(this.companyCode);
                }

                return this.companyCode;
            }
        } 

        /// <summary>
        /// ��˾����
        /// </summary>
        private SearchColumn name;
        /// <summary>
        /// ��˾����
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
        /// ��˾��ַ
        /// </summary>
        private SearchColumn address;
        /// <summary>
        /// ��˾��ַ
        /// </summary>
        public SearchColumn Address
        {
            get
            {
                if (this.address == null)
                {
                    this.address = new SearchColumn(this, "Address");
                    this.ConditionColumnList.Add(this.address);
                }

                return this.name;
            }
        } 

        /// <summary>
        /// �ϼ���˾ID
        /// </summary>
        private SearchColumn parentCompanyId;
        /// <summary>
        /// �ϼ���˾ID
        /// </summary>
        public SearchColumn ParentCompanyId
        {
            get
            {
                if (this.parentCompanyId == null)
                {
                    this.parentCompanyId = new SearchColumn(this, "ParentCompanyId");
                    this.ConditionColumnList.Add(this.parentCompanyId);
                }

                return this.parentCompanyId;
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
        /// ��˾״̬
        /// </summary>
        private SearchColumn status;
        /// <summary>
        /// ��˾״̬
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
        /// �ϼ���˾
        /// </summary>
        private CompanySearcher parentCompany;
        /// <summary>
        /// �ϼ���˾
        /// </summary>
        public CompanySearcher ParentCompany
        {
            get
            {
                return this.parentCompany;
            }
            set
            {
                if (this.parentCompany != null)
                {
                    this.RelationSearcherList.Remove(this.parentCompany);
                }

                this.parentCompany = value;

                if (this.parentCompany != null)
                {
                    this.RelationSearcherList.Add(this.parentCompany);
                }
            }
        }
        
        #endregion

        #region Public Constuctors

        /// <summary>
        /// ���캯�����޲���
        /// </summary>
        public CompanySearcher()
		{
            this.TableName = "Company";
        }
		
		#endregion
	}
}
