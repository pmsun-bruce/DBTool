namespace NFramework.DBTool.QueryTool.Mssql
{
    #region Reference

    using System;
    using System.Text;

    #endregion

    /// <summary>
    /// SQL的查询语句解析对象，用于分析Searcher对象并拼接查询语句
    /// </summary>
    public class MssqlQueryParser : QueryParser
    {
        #region Protected Methods

        #region Condition Parse

        /// <summary>
        /// Between条件解析
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string BetweenParse(BetweenCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" BETWEEN ");

            if (condition.StartValue is SearchColumn)
            {
                SearchColumn tmpColumn = (SearchColumn)condition.StartValue;
                string startValueColName = tmpColumn.CurrentSearcher.TableName + "." + tmpColumn.ColumnName;
                conditionStr.Append(startValueColName);
            }
            else
            {
                string startParamName = "P" + Guid.NewGuid().ToString().ToLower().Replace("-", "");
                conditionStr.Append("@" + startParamName);
                this.ParamCollection.Add(new DBParam(startParamName, condition.StartValue));
            }

            conditionStr.Append(" AND ");

            if (condition.EndValue is SearchColumn)
            {
                SearchColumn tmpColumn = (SearchColumn)condition.EndValue;
                string endValueColName = tmpColumn.CurrentSearcher.TableName + "." + tmpColumn.ColumnName;
                conditionStr.Append(endValueColName);
            }
            else
            {
                string endParamName = "P" + Guid.NewGuid().ToString().ToLower().Replace("-", "");
                conditionStr.Append("@" + endParamName);
                this.ParamCollection.Add(new DBParam(endParamName, condition.EndValue));
            }

            conditionStr.Append(" ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// 等于条件解析；如果条件值为NULL或空字符串时则判断字段的空字符串或NULL值
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string EqualParse(EqualCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" = ");

            if (condition.ParamValue is SearchColumn)
            {
                SearchColumn tmpColumn = (SearchColumn)condition.ParamValue;
                string valueColName = tmpColumn.CurrentSearcher.TableName + "." + tmpColumn.ColumnName;
                conditionStr.Append(valueColName);
            }
            else
            {
                if (condition.ParamValue != null && !condition.ParamValue.ToString().Equals(string.Empty))
                {
                    string paramName = "P" + Guid.NewGuid().ToString().ToLower().Replace("-", "");
                    conditionStr.Append("@" + paramName);
                    ParamCollection.Add(new DBParam(paramName, condition.ParamValue));
                }
                else
                {
                    conditionStr.Insert(0, " (");
                    conditionStr.Append("'' ");
                    conditionStr.Append(" OR ");
                    conditionStr.Append(queryColName);
                    conditionStr.Append(" IS NULL) ");
                }
            }

            conditionStr.Append(" ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// 不等于条件解析；如果条件的值为空，则字段的空字符串和NULL值都排除
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string NotEqualParse(NotEqualCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" <> ");

            if (condition.ParamValue is SearchColumn)
            {
                SearchColumn tmpColumn = (SearchColumn)condition.ParamValue;
                string valueColName = tmpColumn.CurrentSearcher.TableName + "." + tmpColumn.ColumnName;
                conditionStr.Append(valueColName);
            }
            else
            {
                if (condition.ParamValue != null && !string.IsNullOrEmpty(condition.ParamValue.ToString()))
                {
                    string paramName = "P" + Guid.NewGuid().ToString().ToLower().Replace("-", "");
                    conditionStr.Append("@" + paramName);
                    ParamCollection.Add(new DBParam(paramName, condition.ParamValue));
                }
                else
                {
                    conditionStr.Insert(0, " (");
                    conditionStr.Append("'' ");
                    conditionStr.Append(" OR ");
                    conditionStr.Append(queryColName);
                    conditionStr.Append(" IS NOT NULL) ");
                }
            }

            conditionStr.Append(" ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// 大于等于条件解析
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string LargeEqualParse(LargeEqualCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" >= ");

            if (condition.ParamValue is SearchColumn)
            {
                SearchColumn tmpColumn = (SearchColumn)condition.ParamValue;
                string valueColName = tmpColumn.CurrentSearcher.TableName + "." + tmpColumn.ColumnName;
                conditionStr.Append(valueColName);
            }
            else
            {
                string paramName = "P" + Guid.NewGuid().ToString().ToLower().Replace("-", "");
                conditionStr.Append("@" + paramName);
                ParamCollection.Add(new DBParam(paramName, condition.ParamValue));
            }

            conditionStr.Append(" ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// 大于条件解析
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string LargeThanParse(LargeThanCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" > ");

            if (condition.ParamValue is SearchColumn)
            {
                SearchColumn tmpColumn = (SearchColumn)condition.ParamValue;
                string valueColName = tmpColumn.CurrentSearcher.TableName + "." + tmpColumn.ColumnName;
                conditionStr.Append(valueColName);
            }
            else
            {
                string paramName = "P" + Guid.NewGuid().ToString().ToLower().Replace("-", "");
                conditionStr.Append("@" + paramName);
                ParamCollection.Add(new DBParam(paramName, condition.ParamValue));
            }

            conditionStr.Append(" ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// 小于等于条件解析
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string LessEqualParse(LessEqualCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" <= ");

            if (condition.ParamValue is SearchColumn)
            {
                SearchColumn tmpColumn = (SearchColumn)condition.ParamValue;
                string valueColName = tmpColumn.CurrentSearcher.TableName + "." + tmpColumn.ColumnName;
                conditionStr.Append(valueColName);
            }
            else
            {
                string paramName = "P" + Guid.NewGuid().ToString().ToLower().Replace("-", "");
                conditionStr.Append("@" + paramName);
                ParamCollection.Add(new DBParam(paramName, condition.ParamValue));
            }

            conditionStr.Append(" ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// 小于条件解析
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string LessThanParse(LessThanCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" < ");

            if (condition.ParamValue is SearchColumn)
            {
                SearchColumn tmpColumn = (SearchColumn)condition.ParamValue;
                string valueColName = tmpColumn.CurrentSearcher.TableName + "." + tmpColumn.ColumnName;
                conditionStr.Append(valueColName);
            }
            else
            {
                string paramName = "P" + Guid.NewGuid().ToString().ToLower().Replace("-", "");
                conditionStr.Append("@" + paramName);
                ParamCollection.Add(new DBParam(paramName, condition.ParamValue));
            }

            conditionStr.Append(" ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// LIKE条件解析
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string LikeParse(LikeCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" LIKE ");

            if (condition.ParamValue is SearchColumn)
            {
                SearchColumn tmpColumn = (SearchColumn)condition.ParamValue;
                string valueColName = tmpColumn.CurrentSearcher.TableName + "." + tmpColumn.ColumnName;
                conditionStr.Append("'%' +");
                conditionStr.Append(valueColName);
                conditionStr.Append("+ '%'");
            }
            else
            {
                string paramName = "P" + Guid.NewGuid().ToString().ToLower().Replace("-", "");
                conditionStr.Append("'%' +");
                conditionStr.Append("@" + paramName);
                conditionStr.Append("+ '%'");
                ParamCollection.Add(new DBParam(paramName, condition.ParamValue, System.Data.DbType.String));
            }

            conditionStr.Append(" ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// Not Like条件解析
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string NotLikeParse(NotLikeCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" NOT LIKE ");

            if (condition.ParamValue is SearchColumn)
            {
                SearchColumn tmpColumn = (SearchColumn)condition.ParamValue;
                string valueColName = tmpColumn.CurrentSearcher.TableName + "." + tmpColumn.ColumnName;
                conditionStr.Append("'%' +");
                conditionStr.Append(valueColName);
                conditionStr.Append("+ '%'");
            }
            else
            {
                string paramName = "P" + Guid.NewGuid().ToString().ToLower().Replace("-", "");
                conditionStr.Append("'%' +");
                conditionStr.Append("@" + paramName);
                conditionStr.Append("+ '%'");
                this.ParamCollection.Add(new DBParam(paramName, condition.ParamValue, System.Data.DbType.String));
            }

            conditionStr.Append(" ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// IN条件解析
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string InParse(InCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" IN ( ");

            string paramName = string.Empty;
            int i = 0;

            foreach (object pvalue in condition.ParamValueList)
            {
                if (i != 0)
                {
                    conditionStr.Append(",");
                }

                if (pvalue is SearchColumn)
                {
                    SearchColumn tmpColumn = (SearchColumn)pvalue;
                    string valueColName = tmpColumn.CurrentSearcher.TableName + "." + tmpColumn.ColumnName;
                    conditionStr.Append(valueColName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(pvalue.ToString()))
                    {
                        paramName = "P" + Guid.NewGuid().ToString().ToLower().Replace("-", "");
                        conditionStr.Append("@" + paramName);

                        ParamCollection.Add(new DBParam(paramName, pvalue));
                    }
                    else
                    {
                        conditionStr.Append("''");
                    }
                }

                i++;
            }

            conditionStr.Append(") ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// NOT IN条件解析
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string NotInParse(NotInCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" NOT IN ( ");

            string paramName = string.Empty;
            int i = 0;

            foreach (object pvalue in condition.ParamValueList)
            {
                if (i != 0)
                {
                    conditionStr.Append(",");
                }

                if (pvalue is SearchColumn)
                {
                    SearchColumn tmpColumn = (SearchColumn)pvalue;
                    string valueColName = tmpColumn.CurrentSearcher.TableName + "." + tmpColumn.ColumnName;
                    conditionStr.Append(valueColName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(pvalue.ToString()))
                    {
                        paramName = "P" + Guid.NewGuid().ToString().ToLower().Replace("-", "");
                        conditionStr.Append("@" + paramName);

                        ParamCollection.Add(new DBParam(paramName, pvalue));
                    }
                    else
                    {
                        conditionStr.Append("''");
                    }
                }

                i++;
            }

            conditionStr.Append(") ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// 值包含在一段子查询中的条件解析，子查询返回的必须是单列数据
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string SQLInParse(SQLInCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" IN ( ");
            conditionStr.Append(condition.SQL);
            conditionStr.Append(") ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// 值不包含在一段子查询中的条件解析，子查询返回的必须是单列数据
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string NotSQLInParse(SQLNotInCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" NOT IN ( ");
            conditionStr.Append(condition.SQL);
            conditionStr.Append(") ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// 值等于一段子查询返回的值的条件解析，子查询返回的必须是单值
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string SQLEqualParse(SQLEqualCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" = (");
            conditionStr.Append(condition.SQL);
            conditionStr.Append(") ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// 值不等于一段子查询返回的值的条件解析，子查询返回的必须是单值
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string NotSQLEqualParse(SQLNotEqualCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" <> (");
            conditionStr.Append(condition.SQL);
            conditionStr.Append(") ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// IS NULL条件解析
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string IsNullParse(IsNullCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" IS Null ");

            return conditionStr.ToString();
        }

        /// <summary>
        /// IS NOT NULL条件解析
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回解析后的条件字符串</returns>
        protected override string IsNotNullParse(IsNotNullCondition condition)
        {
            StringBuilder conditionStr = new StringBuilder(" ");
            string queryColName = condition.SearchColumn.CurrentSearcher.TableName + "." + condition.SearchColumn.ColumnName;

            if (string.IsNullOrEmpty(condition.SearchColumn.ConditionString))
            {
                conditionStr.Append(queryColName);
            }
            else
            {
                conditionStr.Append(condition.SearchColumn.ConditionString.Replace(SQLPlaceholder.ColName, queryColName));
            }

            conditionStr.Append(" IS NOT NULL ");

            return conditionStr.ToString();
        }

        #endregion

        #endregion
    }
}
