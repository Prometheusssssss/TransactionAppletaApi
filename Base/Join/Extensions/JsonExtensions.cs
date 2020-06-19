using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    result += de.Key + "='" + de.Value + "'";
                    //if (i < jobj.Count) result += " AND ";
                }
            }
            return result;
        }
        #endregion
    }
}