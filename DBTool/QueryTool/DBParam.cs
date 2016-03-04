namespace NFramework.DBTool.QueryTool
{
    #region Reference

    using System;
    using System.Data;
    using System.Data.Common;

    #endregion

    /// <summary>
    /// ������������
    /// </summary>
    [Serializable]
    public class DBParam : DbParameter
    {
        #region Fields & Properties

        /// <summary>
        /// ��ȡ�����ò�����������
        /// </summary>
        public override DbType DbType { get; set; }

        /// <summary>
        /// ��ȡ������һ��ֵ����ֵָʾ������ֻ�����롢ֻ�������˫���Ǵ洢���̷���ֵ������
        /// </summary>
        public override ParameterDirection Direction { get; set; }

        /// <summary>
        /// ��ȡ������һ��ֵ����ֵָʾ�����Ƿ���ܿ�ֵ��
        /// </summary>
        public override bool IsNullable { get; set; }

        /// <summary>
        /// ��ȡ������ DbParameter �����ơ�
        /// </summary>
        public override string ParameterName { get; set; }

        /// <summary>
        /// ��ȡ�������������ݵ�����С�����ֽ�Ϊ��λ����
        /// </summary>
        public override int Size { get; set; }

        /// <summary>
        /// ��ȡ������Դ�е����ƣ���Դ��ӳ�䵽 DataSet �����ڼ��ػ򷵻� Value��
        /// </summary>
        public override string SourceColumn { get; set; }

        /// <summary>
        /// ���û��ȡһ��ֵ����ֵָʾԴ���Ƿ����Ϊ null��
        /// ��ʹ�� DbCommandBuilder �ܹ���ȷ��Ϊ����Ϊ null �������� Update ��䡣
        /// </summary>
        public override bool SourceColumnNullMapping { get; set; }

        /// <summary>
        /// ��ȡ�������ڼ��� Value ʱʹ�õ� DataRowVersion��
        /// </summary>
        public override DataRowVersion SourceVersion { get; set; }

        /// <summary>
        /// ��ȡ�����øò�����ֵ��
        /// </summary>
        public override object Value { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// �� DbType ��������Ϊ��ԭʼ���á�
        /// </summary>
        public override void ResetDbType()
        {

        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        public DBParam(string paramName, object paramValue)
        {
            this.ParameterName = paramName;
            this.Value = paramValue;
            this.IsNullable = true;
            this.Direction = ParameterDirection.InputOutput;
            this.DbType = DbType.Object;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        /// <param name="dbType">��������</param>
        public DBParam(string paramName, object paramValue, DbType dbType) : this(paramName, paramValue)
        {
            this.DbType = dbType;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        /// <param name="dbType">��������</param>
        /// <param name="isNullable">�Ƿ�Ϊ��</param>
        public DBParam(string paramName, object paramValue, DbType dbType, bool isNullable) : this(paramName, paramValue, dbType)
        {
            this.IsNullable = isNullable;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        /// <param name="dbType">��������</param>
        /// <param name="size">������С</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size) : this(paramName, paramValue, dbType, true)
        {
            this.Size = size;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        /// <param name="dbType">��������</param>
        /// <param name="size">������С</param>
        /// <param name="isNullable">�Ƿ�Ϊ��</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, bool isNullable) : this(paramName, paramValue, dbType, size)
        {
            this.IsNullable = isNullable;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        /// <param name="dbType">��������</param>
        /// <param name="size">������С</param>
        /// <param name="sourceColumn">Դ�е�����</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, string sourceColumn) : this(paramName, paramValue, dbType, size)
        {
            this.SourceColumn = sourceColumn;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        /// <param name="dbType">��������</param>
        /// <param name="size">������С</param>
        /// <param name="isNullable">�Ƿ�Ϊ��</param>
        /// <param name="sourceColumn">Դ�е�����</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, bool isNullable, string sourceColumn) : this(paramName, paramValue, dbType, size, isNullable)
        {
            this.SourceColumn = sourceColumn;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        /// <param name="dbType">��������</param>
        /// <param name="size">������С</param>
        /// <param name="paramDirection">��������</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, ParameterDirection paramDirection) : this(paramName, paramValue, dbType, size, true)
        {
            this.Direction = paramDirection;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        /// <param name="dbType">��������</param>
        /// <param name="size">������С</param>
        /// <param name="isNullable">�Ƿ�Ϊ��</param>
        /// <param name="paramDirection">��������</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, bool isNullable, ParameterDirection paramDirection) : this(paramName, paramValue, dbType, size, isNullable)
        {
            this.Direction = paramDirection;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        /// <param name="dbType">��������</param>
        /// <param name="size">������С</param>
        /// <param name="paramDirection">��������</param>
        /// <param name="sourceColumn">Դ�е�����</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, string sourceColumn, ParameterDirection paramDirection) : this(paramName, paramValue, dbType, size, true, sourceColumn)
        {
            this.Direction = paramDirection;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        /// <param name="dbType">��������</param>
        /// <param name="size">������С</param>
        /// <param name="isNullable">�Ƿ�Ϊ��</param>
        /// <param name="sourceColumn">Դ�е�����</param>
        /// <param name="paramDirection">��������</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, bool isNullable, string sourceColumn, ParameterDirection paramDirection) : this(paramName, paramValue, dbType, size, isNullable, sourceColumn)
        {
            this.Direction = paramDirection;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        /// <param name="dbType">��������</param>
        /// <param name="size">������С</param>
        /// <param name="isNullable">�Ƿ�Ϊ��</param>
        /// <param name="sourceColumn">Դ�е�����</param>
        /// <param name="paramDirection">��������</param>
        /// <param name="sourceVersion">paramDirection</param>
        public DBParam(string paramName, object paramValue, DbType dbType, int size, bool isNullable, string sourceColumn, ParameterDirection paramDirection, DataRowVersion sourceVersion) : this(paramName, paramValue, dbType, size, isNullable, sourceColumn, paramDirection)
        {
            this.SourceVersion = sourceVersion;
        }

        #endregion

    }
}