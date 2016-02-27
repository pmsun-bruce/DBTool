namespace NFramework.DBTool.Test.Searcher
{
    #region Reference

    using System;
    using System.Collections;

    using NFramework.DBTool.QueryTool;

    #endregion

    /// <summary>
	///	公司查询对象
	/// </summary>
	[Serializable]
	public class CompanySearcher : NFramework.DBTool.QueryTool.Searcher
	{
		#region Fields & Properties
		
        /// <summary>
        /// 公司ID
        /// </summary>
        private SearchColumn companyId;
        /// <summary>
        /// 公司ID
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
        /// 公司编号
        /// </summary>
        private SearchColumn companyCode;
        /// <summary>
        /// 公司编号
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
        /// 公司名称
        /// </summary>
        private SearchColumn name;
        /// <summary>
        /// 公司名称
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
        /// 公司地址
        /// </summary>
        private SearchColumn address;
        /// <summary>
        /// 公司地址
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
        /// 上级公司ID
        /// </summary>
        private SearchColumn parentCompanyId;
        /// <summary>
        /// 上级公司ID
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
        /// 记录版本
        /// </summary>
        private SearchColumn rVersion;
        /// <summary>
        /// 记录版本
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
        /// 公司状态
        /// </summary>
        private SearchColumn status;
        /// <summary>
        /// 公司状态
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
        /// 创建人ID
        /// </summary>
        private SearchColumn createrId;
        /// <summary>
        /// 创建人ID
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
        /// 创建时间
        /// </summary>
        private SearchColumn createTime;
        /// <summary>
        /// 创建时间
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
        /// 最后更新人ID
        /// </summary>
        private SearchColumn updatorId;
        /// <summary>
        /// 最后更新人ID
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
        /// 最后更新时间
        /// </summary>
        private SearchColumn updateTime;
        /// <summary>
        /// 最后更新时间
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
        /// 上级公司
        /// </summary>
        private CompanySearcher parentCompany;
        /// <summary>
        /// 上级公司
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
        /// 构造函数，无参数
        /// </summary>
        public CompanySearcher()
		{
            this.TableName = "Company";
        }
		
		#endregion
	}
}
