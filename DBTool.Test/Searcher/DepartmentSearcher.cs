namespace NFramework.DBTool.Test.Searcher
{
    #region Reference

    using System;
    using System.Collections;

    using NFramework.DBTool.QueryTool;

    #endregion

    /// <summary>
    /// 部门
    /// </summary>
    [Serializable]
	public class DepartmentSearcher : NFramework.DBTool.QueryTool.Searcher
    {

        #region Fields & Properties
        
        /// <summary>
        /// 部门ID
        /// </summary>
        private SearchColumn departmentId;
        /// <summary>
        /// 部门ID
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
        /// 部门编号
        /// </summary>
        private SearchColumn departmentCode;
        /// <summary>
        /// 部门编号
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
        /// 所属公司ID
        /// </summary>
        private SearchColumn companyId;
        /// <summary>
        /// 所属公司ID
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
        /// 部门名称
        /// </summary>
        private SearchColumn name;
        /// <summary>
        /// 部门名称
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
        /// 部门状态
        /// </summary>
        private SearchColumn status;
        /// <summary>
        /// 部门状态
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
        /// 当前所属公司对象
        /// </summary>
        private CompanySearcher currCompany;
        /// <summary>
        /// 当前所属公司对象
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
        /// 构造函数，无参
        /// </summary>
        public DepartmentSearcher()
		{
            this.TableName = "Department";
        }
		
		#endregion
	}
}
