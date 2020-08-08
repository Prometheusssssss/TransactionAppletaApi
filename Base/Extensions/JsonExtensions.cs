using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace Join
{
    [JsonArray]
    public class KV<TKey, TValue>
        : Dictionary<TKey, TValue>
    {
        public KV(Dictionary<TKey, TValue> source)
            : base(source)
        {

        }
    }
    /// <summary>
    /// Json 扩展
    /// </summary>
    public static class JsonExtensions
    {
        public static readonly IsoDateTimeConverter DATE_CONVERTER = new IsoDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
        };

        #region X.成员方法[ToJsonString]
        /// <summary>
        /// 根据指定日期格式JSON序列化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj)
        {
            if (obj is string)
                return obj as string;
            else if (obj is JToken j)
            {
                return j.ToString(Formatting.None, DATE_CONVERTER);
            }
            else
            {
                return JsonConvert.SerializeObject(obj, Formatting.None, DATE_CONVERTER);
            }
        }
        #endregion

        #region X.成员方法[ToJToken]
        public static JToken ToJToken(this object obj)
        {
            if (obj is JToken)
                return obj as JToken;
            else if (obj is string)
                return JToken.Parse(obj as string);
            else
                return JToken.FromObject(obj);
        }
        #endregion

        #region X.成员方法[ToJArray]
        public static JArray ToJArray(this object obj)
        {
            if (obj is JArray)
                return obj as JArray;
            else if (obj is string)
                return JArray.Parse(obj as string);
            else
                return JArray.FromObject(obj);
        }
        #endregion

        #region X.成员方法[ToKV]
        public static KV<TKey, TValue> ToKV<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
        {
            var dict = source.ToDictionary(keySelector, valueSelector);
            return new KV<TKey, TValue>(dict);
        }
        public static KV<TKey, TValue> ToKV<TKey, TValue>(this Dictionary<TKey, TValue> source)
        {
            return new KV<TKey, TValue>(source);
        }
        #endregion

        #region X.成员方法[FromJsonObject]
        /// <summary>
        /// 根据指定日期格式JSON反序列化成对象
        /// </summary>
        public static T FromJsonObject<T>(this string json)
        {
            if (string.IsNullOrEmpty(json))
                return default(T);
            return JsonConvert.DeserializeObject<T>(json, DATE_CONVERTER);
        }
        #endregion

        #region X.成员方法[FromJsonObject]
        /// <summary>
        /// 根据指定类型T target反序列化JSON字符成对象
        /// </summary>
        public static T FromJsonObject<T>(this T target, string json)
        {
            if (string.IsNullOrEmpty(json))
                return default(T);
            return JsonConvert.DeserializeObject<T>(json, DATE_CONVERTER);
        }
        #endregion

        #region X.成员方法[FromJsonDictionary]
        /// <summary>
        /// JSON反序列化成Dictionary
        /// </summary>
        public static Dictionary<TKey, TValue> FromJsonDictionary<TKey, TValue>(this string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(json);
        }
        #endregion

        #region X.成员方法[AsDynamic]
        /// <summary>
        /// 转成动态对象
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static dynamic AsDynamic(this JToken target)
        {
            dynamic slot = target;
            return slot;
        }
        #endregion

        #region X.成员方法[ToOrderBySql]
        /// <summary>
        /// 转排序
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string ToOrderBySql(this JToken target)
        {
            var result = string.Empty;
            var jobj = target as JObject;
            //var i = 0;
            foreach (var de in jobj)
            {
                //i++;
                if (de.Key != "TEMPLATE_NAME")
                {
                    if (!string.IsNullOrEmpty(result))
                    {
                        result += " AND ";
                    }
                    if (de.Value.ToString() == "")
                        result += de.Key + " IS NULL";
                    else
                        result += de.Key + "='" + de.Value + "'";
                    //if (i < jobj.Count) result += " AND ";
                }
            }
            return result;
        }
        #endregion

        #region X.成员方法[ToWhereSql]
        /// <summary>
        /// 转WHERE条件
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string ToWhereSql(this JToken target)
        {
            var result = string.Empty;
            var jobj = target as JObject;
            //var i = 0;
            foreach (var de in jobj)
            {
                //i++;
                if (de.Key != "TEMPLATE_NAME")
                {
                    if (!string.IsNullOrEmpty(result))
                    {
                        result += " AND ";
                    }
                    if (de.Value.ToString() == "")
                        result += de.Key + " IS NULL";
                    else
                        result += de.Key + "='" + de.Value + "'";
                    //if (i < jobj.Count) result += " AND ";
                }
            }
            return result;
        }
        #endregion

        #region X.成员方法[ToFilterSql]
        /// <summary>
        /// 转WHERE条件
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string ToFilterSql(this JToken target)
        {
            var filters = target.AsDynamic();
            var result = string.Empty;
            var sql = "";
            for (int i = 0; i < filters.Count; i++)
            {
                var data = filters[i];
                //
                string fieldName = data.fieldName;
                string type = data.type;
                string compared = data.compared;
                string filterValue = data.filterValue;
                if (!string.IsNullOrWhiteSpace(filterValue))
                {
                    switch (type)
                    {
                        case "string":
                            if (compared == "like")
                            {
                                string valueSql = "";
                                //判断filedName是否为数组
                                var nameList = fieldName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int j = 0; j < nameList.Count(); j++)
                                {
                                    var item = nameList[j];
                                    if (j == nameList.Count() - 1)
                                        valueSql = valueSql.AddSql(item + " like '%" + filterValue + "%'", false);
                                    else
                                        valueSql = valueSql.AddSql(item + " like '%" + filterValue + "%' or ", false);

                                }
                                sql = sql.AddSql("(" + valueSql + ")", true);
                            }
                            break;
                        case "date":
                            sql = sql.AddSql(fieldName + compared + "'" + filterValue + "'", true);
                            break;
                        default:
                            break;
                    }
                }
            }
            return sql;
        }
        #endregion

        #region X.成员方法[AddSql]
        /// <summary>
        /// 转WHERE条件
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string AddSql(this string str, string append, bool isAppendAnd)
        {
            var result = "";
            if (!string.IsNullOrEmpty(str))
            {
                if (isAppendAnd)
                    str += " and ";
                result = str + append;
            }
            else
                result = append;
            return result;
        }
        #endregion

        #region X.成员方法[AppendSql]
        /// <summary>
        /// 转CU条件
        /// </summary>
        public static string AppendSql(this string str, string append, string appChar, bool isAppend)
        {
            var result = "";
            if (!string.IsNullOrEmpty(str))
            {
                if (isAppend)
                    str += ",";
                result = str + appChar + append + appChar;
            }
            else
            {
                result = appChar + append + appChar;
            }
            return result;
        }
        #endregion

        #region X.成员方法[GetValue]
        /// <summary>
        /// 获取字典值
        /// </summary>
        public static string GetValue(this Dictionary<string, object> str, string key)
        {
            var result = "";
            foreach (var item in str)
            {
                if (item.Key == key)
                    result = item.Value.ToString();
            }
            return result;
        }
        #endregion

        #region X.成员方法[JsonToDictionary]
        /// <summary>
        /// json转换dic
        /// </summary>
        public static Dictionary<string, object> JsonToDictionary(this string str)
        {
            //实例化JavaScriptSerializer类的新实例
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                return jss.Deserialize<Dictionary<string, object>>(str);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}