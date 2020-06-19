using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WechatRouteApi
{
    /// <summary>
    /// BIZ服务基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BizServiceBase<T>
        where T : BizServiceBase<T>
    {
        #region B.单例
        private static T _X;
        public static T X
        {
            get
            {
                if (_X == null)
                    _X = Activator.CreateInstance<T>();
                return _X;
            }
        }
        #endregion

    }
}