namespace NFramework.DBTool.Test.Searcher
{
    #region Reference

    using System;
    using System.Collections;

    using NFramework.DBTool.QueryTool;
    using System.Data;

    #endregion

    /// <summary>
    /// 职位
    /// </summary>
    [Serializable]
	public sealed class PositionSearcher : NFramework.DBTool.QueryTool.Searcher
    {
		#region Fields & Properties

        /// <summary>
        /// 职位ID
        /// </summary>
        private SearchColumn positionId;
        /// <summary>
        /// 职位ID
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
        /// 职位编号
        /// </summary>
        private SearchColumn positionCode;
        /// <summary>
        /// 职位编号
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
        /// 所在公司ID
        /// </summary>
        private SearchColumn companyId;
        /// <summary>
        /// 所在公司ID
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
        /// 所在部门ID
        /// </summary>
        private SearchColumn departmentId;
        /// <summary>
        /// 所在部门ID
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
        /// 职位名称
        /// </summary>
        private SearchColumn name;
        /// <summary>
        /// 职位名称
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
                    this.rVersion = new SearchColumn(this, "RVersion", DbType.Int32);
                    this.ConditionColumnList.Add(this.rVersion);
                }

                return this.rVersion;
            }
        }

        /// <summary>
        /// 职位状态
        /// </summary>
        private SearchColumn status;
        /// <summary>
        /// 职位状态
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
                    this.createrId = new SearchColumn(this, "CreaterId", DbType.AnsiString);
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
                    this.createTime = new SearchColumn(this, "CreateTime", DbType.DateTime);
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
                    this.updatorId = new SearchColumn(this, "UpdatorId", DbType.AnsiString);
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
                    this.updateTime = new SearchColumn(this, "UpdateTime", DbType.DateTime);
                    this.ConditionColumnList.Add(this.updateTime);
                }

                return this.updateTime;
            }
        }

        /// <summary>
        /// 所属公司对象
        /// </summary>
        private CompanySearcher currCompany;
        /// <summary>
        /// 所属公司对象
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
        /// 所属部门对象
        /// </summary>
        private DepartmentSearcher currDepartment;
        /// <summary>
        /// 所属部门对象
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
        /// 构造函数，无参
        /// </summary>
        public PositionSearcher() : base("Position")
		{
        }
		
		#endregion
	}
}
