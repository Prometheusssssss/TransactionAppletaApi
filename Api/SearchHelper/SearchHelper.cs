using System.Data;

namespace TransactionAppletaApi
{
    /// <summary>
    /// 查询帮助器
    /// </summary>
    public static class SearchHelper
    {
        #region X.成员方法[SearchTable]
        public static DataSet SearchTable(string tableName, string whereSql, int page = -1, int limit = -1)
        {
            using (var x = Join.Dal.MySqlProvider.X())
            {
                if (whereSql == "")
                    whereSql = "1=1";
                //查询分账明细表已分账数据
                var sql = string.Format(@"SELECT * from {0} where {1}", tableName, whereSql);
                //分页
                if (page != -1 && limit != -1)
                {
                    var take = (page - 1) * limit;
                    sql += "limit " + take + "," + limit;
                }
                var dt = x.ExecuteSqlCommand(sql);
                return dt;
            }
        }
        #endregion
    }
}
