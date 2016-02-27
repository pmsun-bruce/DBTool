﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFramework.ExceptionTool
{
    /// <summary>
    /// 消息反馈对象
    /// </summary>
    public class ResponseMessage
    {
        #region Private Fields

        /// <summary>
        /// 反馈消息名称，如果是多语言，该名称可对应多语言Resource中的键值
        /// </summary>
        private string name;
        /// <summary>
        /// 反馈的消息内容，如果是多语言情况下，该内容主要用于保留反馈信息。避免多语言修改而反馈信息含义不明。
        /// </summary>
        private string content;

        #endregion

        #region Public Properties

        /// <summary>
        /// 反馈消息名称，如果是多语言，该名称可对应多语言Resource中的键值
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// 反馈的消息内容，如果是多语言情况下，该内容主要用于保留反馈信息。避免多语言修改而反馈信息含义不明。
        /// </summary>
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }

        #endregion

        #region Public Constructor

        /// <summary>
        /// 
        /// </summary>
        public ResponseMessage()
        {

        }

        #endregion
    }
}