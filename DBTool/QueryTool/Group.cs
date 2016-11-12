using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFramework.DBTool.QueryTool
{
    public class Group
    {
        public static ConditionGroup And(ICondition condition1, ICondition condition2, params ICondition[] conditions)
        {
            return AddCondition(ConditionRelation.And, condition1, condition2, conditions);
        }

        public static ConditionGroup Or(ICondition condition1, ICondition condition2, params ICondition[] conditions)
        {
            return AddCondition(ConditionRelation.Or, condition1, condition2, conditions);
        }

        private static ConditionGroup AddCondition(ConditionRelation relation, ICondition condition1, ICondition condition2, params ICondition[] conditions)
        {
            ConditionGroup newGroup = new ConditionGroup();
            List<ICondition> conditionList = new List<ICondition>();
            conditionList.Add(condition1);
            conditionList.Add(condition2);

            if (conditions != null)
            {
                conditionList.AddRange(conditions);
            }

            Condition paramCondition = null;
            ConditionGroup conditionGroup = null;
            int groupIndex = 1;
            int conditionIndex = 1;

            foreach (ICondition con in conditionList)
            {
                if (con is ConditionGroup)
                {
                    conditionGroup = (ConditionGroup)con;
                    conditionGroup.GroupIndex = groupIndex;
                    conditionGroup.GroupRelation = relation;
                    newGroup.AddSubGroup(conditionGroup);
                    groupIndex++;
                }
                else if (con is Condition)
                {
                    paramCondition = (Condition)con;
                    paramCondition.Index = conditionIndex;
                    paramCondition.Group = newGroup;
                    paramCondition.Relation = relation;
                    conditionIndex++;
                }
            }

            return newGroup;
        }
    }
}
