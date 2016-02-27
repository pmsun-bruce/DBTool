1. DBToolSampleDB.sql为测试数据库生成脚本。
2. DBTool.Test/HibernateTest/QueryTest.cs为NHibernate的单元测试。需要修改NHibernate.MSSQL.cfg.xml中的数据库连接
3. DBTool.Test/MSSQLTest/QueryTest.cs为MSSQL的单元测试。需要修改单元测试文件中的MyClassInitialize方法中的connectionString的连接地址。
