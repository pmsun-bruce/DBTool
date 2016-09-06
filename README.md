# DBTool

该类库目的是想要解决因查询条件不同而不得不增加新查询方法的问题，至少可以减少这种方法的出现，并且希望保留强类型的优势。通过使用本类库，可以在调用端方便的增加查询条件，而不需要频繁的修改和增加DAO中的方法，并且可以将常规的数据调用方法规范化。

## 说明

1. DBToolSampleDB.sql为测试数据库生成脚本。
1. DBTool.Test/HibernateTest/QueryTest.cs为NHibernate的单元测试。需要修改NHibernate.MSSQL.cfg.xml中的数据库连接。
1. DBTool.Test/MSSQLTest/QueryTest.cs为MSSQL的单元测试。需要修改单元测试文件中的MyClassInitialize方法中的connectionString连接地址。
