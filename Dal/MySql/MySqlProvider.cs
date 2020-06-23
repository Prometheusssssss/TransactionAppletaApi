using Join.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Join.Dal
{
    /// <summary>
    /// MySql提供器 用于操作MySql
    /// </summary>
    public class MySqlProvider : IDisposable
    {
        #region $A.成员变量
        /// <summary>
        /// MySql 配置节点
        /// </summary>
        private static readonly MySqlConfigElement MySqlConf
            = JoinSystemConfig.X.MySql;
        private bool InTrans = false;
        #endregion

        #region B.基本属性[Conn]
        public MySqlTransaction Trans { get; private set; }
        public MySqlConnection Conn { get; private set; }
        #endregion

        #region B.基本属性[Parameters]
        public List<DbParameter> Parameters { get; private set; }
        #endregion

        #region F.工厂方法
        /// <summary>
        /// 获取新提供器
        /// </summary>
        /// <returns></returns>
        public static MySqlProvider X(MySqlTransaction trans = null)
        {
            return new MySqlProvider(trans);
        }
        #endregion

        #region K.构造方法
        private MySqlProvider(MySqlTransaction trans = null)
        {
            if (trans != null)
            {
                this.Trans = trans;
                this.Conn = trans.Connection;
                InTrans = true;
            }
            else
            {
                var connString = MySqlConf.ConnString();

                try
                {
                    this.Conn = new MySqlConnection(connString);
                    Conn.Open();
                }
                catch
                {
                    MySqlConnection.ClearAllPools();
                    this.Conn = new MySqlConnection(connString);
                    Conn.Open();
                }
            }

            this.Parameters = new List<DbParameter>();

        }
        #endregion

        #region I.IDisposable
        public void Close()
        {
            if (!InTrans)
                this.Conn.Close();
        }
        public void Dispose()
        {
            if (!InTrans)
                this.Conn.Dispose();
        }
        #endregion

        #region X.成员方法[LoadParam]
        /// <summary>
        /// 加载参数
        /// </summary>
        /// <param name="ps">参数s</param>
        public MySqlProvider LoadParam(DbParameter[] ps)
        {
            this.Parameters.AddRange(ps);
            return this;
        }
        #endregion

        #region X.成员方法[ParamIn]
        /// <summary>
        /// 生成传入参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public MySqlProvider ParamIn<T>(string name, T value)
        {
            var param = new MySqlParameter(name, value)
            {
                Direction = ParameterDirection.Input
            };

            this.Parameters.Add(param);
            return this;
        }

        /// <summary>
        /// 生成传入参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="type">参数类型</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public MySqlProvider ParamIn(string name, MySqlDbType type, object value)
        {
            var param = new MySqlParameter(name, type)
            {
                Direction = ParameterDirection.Input
            };

            if (type == MySqlDbType.JSON)
                value = value.ToJsonString();

            param.Value = value;
            this.Parameters.Add(param);
            return this;
        }
        #endregion

        #region X.成员方法[ExecuteStoredProcedure]
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public DataSet ExecuteStoredProcedure(string spName)
        {
            var cmd = Conn.CreateCommand();
            cmd.CommandText = spName;
            cmd.CommandType = CommandType.StoredProcedure;
            //if (this.InTrans) cmd.Transaction = this.Trans;
            Parameters.ForEach(de => cmd.Parameters.Add(de));

            var da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        #endregion

        #region X.成员方法[ExecuteSqlCommand]
        public DataSet ExecuteSqlCommand(string commandText)
        {
            var cmd = Conn.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.Text;
            if (this.InTrans) cmd.Transaction = this.Trans;
            Parameters.ForEach(de => cmd.Parameters.Add(de));

            var da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        #endregion

        #region X.成员方法[ExecuteSqlCommand]
        public int ExecuteCommand(string commandText)
        {
            var cmd = Conn.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.Text;
            if (this.InTrans) cmd.Transaction = this.Trans;
            Parameters.ForEach(de => cmd.Parameters.Add(de));
            return cmd.ExecuteNonQuery();
        }
        #endregion

        #region X.成员方法[ExecScalar]
        public object ExecuteScalar(string commandText)
        {
            var cmd = Conn.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.Text;
            if (this.InTrans) cmd.Transaction = this.Trans;
            Parameters.ForEach(de => cmd.Parameters.Add(de));
            return cmd.ExecuteScalar();
        }
        #endregion

        #region X.成员方法[UpdateRows]
        public int UpdateRows(DataRow[] rows, string tablename)
        {
            var kids = String.Join(",", rows.Select(r => r["KID"].ToInt32(0)));
            var selSql = "SELECT * FROM " + tablename + " where KID in (" + kids + ")";
            var da = new MySqlDataAdapter(selSql, this.Conn);
            var cb = new MySqlCommandBuilder(da);
            var dt = new DataTable();

            da.Fill(dt);
            dt.PrimaryKey = new DataColumn[] { dt.Columns["KID"] };

            foreach (var row in rows)
            {
                var dr = dt.Rows.Find(row["KID"]);
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    dr[i] = row[i];
                }
            }
            if (this.InTrans)
            {
                if (da.UpdateCommand != null)
                    da.UpdateCommand.Transaction = this.Trans;
            }
            return da.Update(dt);
        }
        #endregion
    }
}