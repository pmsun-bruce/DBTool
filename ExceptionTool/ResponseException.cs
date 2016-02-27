namespace NFramework.ExceptionTool
{
    #region Referecne

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion

    /// <summary>
    /// 异常反馈
    /// </summary>
    public class ResponseException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResponseException() : base()
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息字符串</param>
        public ResponseException(string message) : base(message)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hResult">反馈代码</param>
        /// <param name="message">消息字符串</param>
        public ResponseException(int hResult, string message) : base(message)
        {
            this.HResult = hResult;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息字符串</param>
        /// <param name="innerException">内部异常</param>
        public ResponseException(string message, ResponseException innerException) : base(message, innerException)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hResult">反馈代码</param>
        /// <param name="message">消息字符串</param>
        /// <param name="innerException">内部异常</param>
        public ResponseException(int hResult, string message, ResponseException innerException): base(message, innerException)
        {
            this.HResult = hResult;
        }
    }
}
