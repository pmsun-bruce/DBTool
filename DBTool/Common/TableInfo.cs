using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFramework.DBTool.Common
{
    [Serializable]
    public class TableInfo
    {

        public List<ColumnInfo> Columns
        {
            get;
            set;
        }

        public string TableName
        {
            get;
            set;
        }

        public string Remarks
        {
            get;
            set;
        }

        public TableInfo()
        {
            this.Columns = new List<ColumnInfo>();
        }
    }
}
