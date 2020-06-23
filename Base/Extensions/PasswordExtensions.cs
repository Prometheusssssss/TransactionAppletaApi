using System;
using System.Text;

namespace Join
{
    /// <summary>
    /// 密码扩展
    /// </summary>
    public static class PasswordExtensions
    {
        #region X.成员方法[EncodeWithSalt]
        /// <summary>
        /// 加密(不可逆)
        /// </summary>
        /// <param name="pwd">明码</param>
        /// <param name="salt">指定Salt</param>
        public static string EncodeWithSalt(this string pwd, string salt)
        {
            var bIn = Encoding.Unicode.GetBytes(pwd);
            var bSalt = Convert.FromBase64String(salt);
            var bAll = new byte[bSalt.Length + bIn.Length];

            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);

            using (var s = new System.Security.Cryptography.SHA1Managed())
            {
                var bRlt = s.ComputeHash(bAll);
                var result = Convert.ToBase64String(bRlt);
                return result;
            }
        }
        #endregion

        #region X.成员方法[GenerateSalt]
        /// <summary>
        /// 获取随机Salt
        /// </summary>
        public static string GenerateSalt(this object obj)
        {
            using (var s = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                var buf = new byte[16];
                s.GetBytes(buf);
                var result = Convert.ToBase64String(buf);
                return result;
            }
        }
        #endregion
    }
}