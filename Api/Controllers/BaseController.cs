using Join;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace TransactionAppletaApi
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BaseController : ApiController
    {
        #region $A.常量
        protected const string KEY_PV = "pv";
        #endregion

        #region A.成员变量
        private Dictionary<string, string> Headers;
        #endregion

        #region B.基本属性
        /// <summary>
        /// 当前用户
        /// </summary>
        protected LoginUser LoginUser { get; private set; }
        #endregion

        #region L.加载方法
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="controllerContext"></param>
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            var headers = controllerContext.Request.Headers;
            this.Headers = headers.Where(de => de.Key.StartsWith("jt-")).ToDictionary(de => de.Key.ToLower(), de => de.Value.FirstOrDefault());
            if (this.Headers.Count() > 0)
            {
                this.LoginUser = new LoginUser()
                {
                    Id = Convert.ToInt32(this.GetHeader(AjaxHeader.ID)),
                    Cid = Convert.ToInt32(this.GetHeader(AjaxHeader.CID)),
                    Code = this.GetHeader(AjaxHeader.CODE),
                    Token = this.GetHeader(AjaxHeader.TOKEN)
                };
            }
        }
        #endregion

        #region X.成员方法[GetHeader]
        /// <summary>
        /// 获取头附加数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetHeader(string key)
        {
            key = key.ToLower();
            if (this.Headers.ContainsKey(key))
                return this.Headers[key];
            return string.Empty;
        }
        #endregion

        #region X.成员方法[TryReturn]
        /// <summary>
        /// 尝试加载
        /// </summary>
        /// <param name="doFunc">执行</param>
        protected IHttpActionResult TryReturn<T>(Func<T> doFunc)
        {
            try
            {
                return Ok(doFunc());
            }
            //catch (BizException ex)
            //{
            //    var obj = new { Msg = ex.Message, Obj = ex.Biz };
            //    return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
            //}
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
