﻿namespace NFramework.ObjectTool
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
            StringBuilder xmlStr = new StringBuilder();
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            XmlWriter xmlWriter = XmlWriter.Create(xmlStr);
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
            StringReader strReader = new StringReader(serXml);
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            T desObj = (T)xmlSer.Deserialize(strReader);

            return desObj;
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

            using (MemoryStream stream = new MemoryStream())
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
            FieldInfo[] fields = desObject.GetType().GetFields();

            foreach (FieldInfo field in fields)
            {
                field.SetValue(desObject, field.GetValue(srcObject));
            }

            PropertyInfo[] properties = desObject.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                PropertyInfo srcProp = srcObject.GetType().GetProperty(property.Name);
                property.SetValue(desObject, srcProp.GetValue(srcObject, null), null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IList<PropertyInfo> GetProperties<T>(T obj)
        {
            return obj.GetType().GetProperties();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IList<FieldInfo> GetFields<T>(T obj)
        {
            return obj.GetType().GetFields();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IList<PropertyInfo> GetProperties(object obj)
        {
            return obj.GetType().GetProperties();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IList<FieldInfo> GetFields(object obj)
        {
            return obj.GetType().GetFields();
        }

        #endregion
    }
}
