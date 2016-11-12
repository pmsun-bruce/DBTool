using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NFramework.DBTool.Common
{
    [Serializable]
    public class ColumnInfo
    {
        public string ColumnName
        {
            get;
            set;
        }

        public DbType DBType
        {
            get;
            set;
        }

        public int Precision
        {
            get;
            set;
        }

        public int Scale
        {
            get;
            set;
        }

        private bool isNullable;

        public bool IsNullable
        {
            get
            {
                if(this.IsPK)
                {
                    this.isNullable = false;
                }

                return this.isNullable;
            }
            set
            {
                this.isNullable = value;
            }
        }

        public bool IsUnique
        {
            get;
            set;
        }

        public string UniqueConstraintName
        {
            get;
            set;
        }

        public bool IsPK
        {
            get;
            set;
        }

        public bool IsFK
        {
            get;
            set;
        }

        public string RefTableName
        {
            get;
            set;
        }

        public string RefColumnName
        {
            get;
            set;
        }

        public int MaxLength
        {
            get;
            set;
        }

        public object DefaultValue
        {
            get;
            set;
        }

        public string Remarks
        {
            get;
            set;
        }

        public TableInfo CurrTable
        {
            get;
            set;
        }

        public ColumnInfo()
        {
            this.IsNullable = true;
            this.IsUnique = false;
            this.IsPK = false;
            this.IsFK = false;
            this.MaxLength = 0;
            this.Precision = 0;
            this.Scale = 0;
            this.DBType = DbType.AnsiString;
        }
    }
}
