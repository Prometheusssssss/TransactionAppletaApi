using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Join
{
    /// <summary>
    /// 当前登录用户信息
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户所有者ID
        /// </summary>
        public int Cid { get; set; }

        /// <summary>
        /// 用户代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 登录密钥
        /// </summary>
        public string Token { get; set; }
    }
}
