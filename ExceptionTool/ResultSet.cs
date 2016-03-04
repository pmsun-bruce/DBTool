using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NFramework.ExceptionTool
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ResultSet
    {
        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private IList<ResponseMessage> messages;

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public ResultCode ResultCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public IList<ResponseMessage> Messages
        {
            get
            {
                if (this.messages == null)
                {
                    this.messages = new List<ResponseMessage>();
                }

                return this.messages;
            }
        }

        #endregion
    }
}
