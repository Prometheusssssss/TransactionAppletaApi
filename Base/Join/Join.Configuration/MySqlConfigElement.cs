using System.Configuration;

namespace Join.Configuration
{
    /// <summary>
    /// MySql 配置节点
    /// </summary>
    public class MySqlConfigElement : ConfigurationElement
    {
        #region B.基本属性[Server]
        /// <summary>
        /// 服务器
        /// </summary>
        [ConfigurationProperty("server", IsRequired = true)]
        public string Server
        {
            get { return (string)base["server"]; }
            set { base["server"] = value; }
        }
        #endregion

        #region B.基本属性[Database]
        /// <summary>
        /// 数据库
        /// </summary>
        [ConfigurationProperty("db", IsRequired = true)]
        public string Database
        {
            get { return (string)base["db"]; }
            set { base["db"] = value; }
        }
        #endregion

        #region B.基本属性[Uid]
        /// <summary>
        /// 用户id
        /// </summary>
        [ConfigurationProperty("uid", IsRequired = true)]
        public string Uid
        {
            get { return (string)base["uid"]; }
            set { base["uid"] = value; }
        }
        #endregion

        #region B.基本属性[Password]
        /// <summary>
        /// 密码
        /// </summary>
        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return (string)base["password"]; }
            set { base["password"] = value; }
        }
        #endregion

        #region B.基本属性[SslMode]
        /// <summary>
        /// SSL
        /// </summary>
        [ConfigurationProperty("ssl", IsRequired = true)]
        public string SslMode
        {
            get { return (string)base["ssl"]; }
            set { base["ssl"] = value; }
        }
        #endregion

        #region B.基本属性[Pooling]
        /// <summary>
        /// Pooling
        /// </summary>
        [ConfigurationProperty("pooling", IsRequired = true)]
        public bool Pooling
        {
            get { return (bool)base["pooling"]; }
            set { base["pooling"] = value; }
        }
        #endregion

        #region X.成员方法
        private string _ConnString=null;
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public string ConnString()
        {
            if(_ConnString==null)
                _ConnString= $"Server={Server};Database={Database};Uid={Uid};Pwd={Password};SslMode={SslMode};Pooling={Pooling}";
            return _ConnString;
        }
        #endregion
    }
}
