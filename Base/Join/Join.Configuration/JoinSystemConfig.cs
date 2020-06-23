using System.Configuration;

namespace Join.Configuration
{
    /// <summary>
    /// Join 系统配置
    /// </summary>
    public class JoinSystemConfig : ConfigurationSection
    {
        #region $A.单例
        private static JoinSystemConfig _Instance;
        public static JoinSystemConfig X
        {
            get
            {
                if (_Instance == null)
                    _Instance = (JoinSystemConfig)ConfigurationManager.GetSection("joinSystem");
                return _Instance;
            }
        }
        #endregion

        #region B.基本属性[Title]
        /// <summary>
        /// 系统名称[XX信息管理系统]
        /// </summary>
        [ConfigurationProperty("title", IsRequired = true)]
        public string Title
        {
            get { return (string)base["title"]; }
            set { base["title"] = value; }
        }
        #endregion

        #region B.基本属性[VersionNo]
        /// <summary>
        /// 版本号
        /// </summary>
        [ConfigurationProperty("version", IsRequired = true)]
        public string VersionNo
        {
            get { return (string)base["version"]; }
            set { base["version"] = value; }
        }
        #endregion


        #region C.子属性[MySql]
        [ConfigurationProperty("mysql", IsRequired = false)]
        public MySqlConfigElement MySql
        {
            get { return (MySqlConfigElement)base["mysql"]; }
        }
        #endregion

    }
}
