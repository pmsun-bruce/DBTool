namespace NFramework.ObjectTool
{
    #region

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml.Serialization;
    using System.Xml;

    #endregion

    /// <summary>
    /// 通用对象工厂，用于正反序列化和对象的克隆赋值等操作
    /// </summary>
    public class ObjectFactory
    {
        #region Public Static Methods

        /// <summary>
        /// 序列化一个对象为XML数据
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="source">被序列化的对象</param>
        /// <returns>返回序列化后的XML数据</returns>
        public static string SerializeToXML<T>(T source)
        {
            var xmlStr = new StringBuilder();
            var xmlSer = new XmlSerializer(typeof(T));
            var xmlWriter = XmlWriter.Create(xmlStr);

            xmlSer.Serialize(xmlWriter, source);
            
            return xmlStr.ToString();
        }

        /// <summary>
        /// 从XML序列化字符串反序列化为一个对象
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="serXml">需要反序列化的XML数据</param>
        /// <returns>反序列化成功返回对象</returns>
        public static T DeserializeFromXML<T>(string serXml)
        {
            var strReader = new StringReader(serXml);
            var xmlSer = new XmlSerializer(typeof(T));

            return (T)xmlSer.Deserialize(strReader);
        }

        /// <summary>
        /// 深层clone一个对象
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="source">被clone的对象</param>
        /// <returns>克隆成功返回指定的对象</returns>
        public static T Clone<T>(T source)
        {
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);

                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// 从一个对象复制所有字段和属性值到指定对象中
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="srcObject">被复制的对象</param>
        /// <param name="desObject">目标对象</param>
        public static void Copy<T>(T srcObject, T desObject)
        {
            var fields = desObject.GetType().GetFields();

            foreach (var field in fields)
            {
                field.SetValue(desObject, field.GetValue(srcObject));
            }

            var propertys = desObject.GetType().GetProperties();
            PropertyInfo srcProp = null;

            foreach (var property in propertys)
            {
                srcProp = srcObject.GetType().GetProperty(property.Name);
                property.SetValue(desObject, srcProp.GetValue(srcObject, null), null);
            }
        }

        #endregion
    }
}
