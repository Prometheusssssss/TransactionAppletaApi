using System;
using System.Data;
using System.Linq;

namespace Join
{
    /// <summary>
    /// DataSet DataTable扩展
    /// </summary>
    public static class DataExtensions
    {
        #region X.成员方法[FirstTable]
        /// <summary>
        /// 获取指定表
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="tableIdx">表索引</param>
        /// <returns></returns>
        public static DataTable FirstTable(this DataSet ds,int tableIdx=0)
        {
            if (ds.Tables.Count <= tableIdx) return null;
            var table = ds.Tables[tableIdx];
            return table;
        }
        #endregion
      
        #region X.成员方法[FirstRow]
        /// <summary>
        /// 获取指定表第一行
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="tableIdx">表索引</param>
        /// <returns></returns>
        public static DataRow FirstRow(this DataSet ds, int tableIdx=0)
        {
            if (ds.Tables.Count<= tableIdx) return null;
            if (ds.Tables[tableIdx].Rows.Count == 0) return null;
            var data = ds.Tables[tableIdx].Rows[0];
            return data;
        }
        #endregion

        #region X.成员方法[Rows]
        /// <summary>
        /// 获取指定表所有行
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="tableIdx">表索引</param>
        /// <returns></returns>
        public static DataRow[] Rows(this DataSet ds, int tableIdx = 0)
        {
            if (ds.Tables.Count <=tableIdx) return null;
            return ds.Tables[tableIdx].Rows.Cast<DataRow>().ToArray();
        }
        #endregion

        #region X.成员方法[Write]
        /// <summary>
        /// 将DataRow写入对象
        /// </summary>
        /// <returns></returns>
        public static T Write<T>(this DataRow row,T target)
        {
            var dt = row.Table;
            var cols = dt.Columns;
            foreach (DataColumn col in cols)
            {
                var value = row[col];
                if (value is DBNull) value = null;

                target.WriteProperty(col.ColumnName, value);
            }
            return target;
        }
        #endregion
    }
}
