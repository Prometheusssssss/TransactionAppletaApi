using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TransactionAppletaApi.Security
{
    /// <summary>
    /// 标签-需要权限验证的Action和Controller 
    /// </summary>
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        #region X.成员方法[OnActionExcuting(filterContext)]
        /// <summary>
        /// 执行Action前检查权限
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            if (!Skip(filterContext))
            {
                if (filterContext.Request.Headers.Contains(AjaxHeader.TOKEN))
                {
                    var token = filterContext.Request.Headers.GetValues(AjaxHeader.TOKEN).First();
                    //校验token跟本地生成的token的比对

                }
                else
                {
                    filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
            base.OnActionExecuting(filterContext);
        }
        #endregion

        private bool Skip(HttpActionContext filterContext)
        {
            return filterContext.ActionDescriptor.GetCustomAttributes<NoPermissionRequiredAttribute>().Any();
        }

    }

    public class NoPermissionRequiredAttribute : System.Attribute
    {

    }
}