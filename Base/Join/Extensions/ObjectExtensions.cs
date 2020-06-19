using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Join
{
    /// <summary>
    /// 对象扩展
    /// </summary>
    public static class ObjectExtensions
    {
        #region X.成员方法[AsArray]
        /// <summary>
        /// 单一对象转数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T[] AsArray<T>(this T obj)
        {
            return new T[] { obj };
        }

        public static List<T> AsList<T>(this T obj)
        {
            return obj.AsArray().ToList();
        }
        #endregion

        #region X.成员方法[WriteProperty]
        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="target">对象实例</param>
        /// <param name="propName">属性名</param>
        /// <param name="value">属性值</param>
        public static object WriteProperty(this object target, string propName, object value)
        {
            var p = target.GetType().GetProperty(propName);
            if (p != null)
                p.SetValue(target, value, null);
            return target;
        }
        #endregion
    }
}
