using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;

namespace TransactionAppletaApi
{
    public class UpdateOrderRunner
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        public void Do()
        {
            //执行sql
            using (var x = Join.Dal.MySqlProvider.X())
            {
                var nowDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
                //var nowDate = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd HH:mm:ss");
                //查询订单状态已完成，确认收货时间+3天的单据
                var selectProductSql = "select * from B_ORDER where status='已完成' and CONFIRMED_TIME <= '" + nowDate + "'";
                var selectProductTables = x.ExecuteSqlCommand(selectProductSql);
                foreach (DataRow item in selectProductTables.Tables[0].Rows)
                {
                    //更新关联的流水状态为已结算，并且增加卖家的累计收入和可用资金，减少即将收入
                    var kid = item["KID"].ToString();
                    var price = item["PRICE"].ToString();
                    var sellUserId = item["SELL_USER_ID"].ToString();
                    //更新用户表累计收益金额，可用资金字段，扣除即将收入字段
                    var orderAmount = Math.Round(decimal.Parse(price) * 0.9m, 2);
                    //获取用户余额
                    var selUserSql = "select * from a_user where kid='" + sellUserId + "'";
                    var selUserDt = x.ExecuteSqlCommand(selUserSql);
                    if (selUserDt.Tables[0].Rows.Count > 0)
                    {
                        var row = selUserDt.Tables[0].Rows[0];
                        //用户累计收益
                        var income = decimal.Parse(row["CUMULATIVE_INCOME"].ToString());
                        var incomeResult = income + orderAmount;
                        //账户余额
                        var balance = decimal.Parse(row["BALANCE"].ToString());
                        var balanceResult = balance + orderAmount;
                        //即将收入
                        var upIncome = decimal.Parse(row["UPCOMING_INCOME"].ToString());
                        var upIncomeResult = upIncome - orderAmount;
                        //修改Sql
                        var updateUserSql = "update a_user set BALANCE='" + incomeResult.ToString() + "',CUMULATIVE_INCOME='" + balanceResult.ToString() + "', UPCOMING_INCOME='" + upIncomeResult.ToString() + "' where kid='" + sellUserId + "'";
                        x.ExecuteSqlCommand(updateUserSql);
                        x.Close();
                    }
                    //修改流水状态
                    var updateAccountSql = "update B_ACCOUNT_RECORD set status='已结算' where order_id='" + kid + "'";
                }
            }
        }

        #region 插入消息
        /// <summary>
        /// 插入消息
        /// </summary>
        public void InsertMsg(string theme, string content, string userId, string userName, string userPhone)
        {
            using (var x = Join.Dal.MySqlProvider.X())
            {
                var insertSql = string.Format(@"insert into B_MESSAGE (`THEME`,`STATUS`,`USER_ID`,`USER_NAME`,
                                            `USER_PHONE`,`CONTENT`,`SEND_TIME`,`IS_DELETE`) values ('{0}','{1}','{2}','{3}','{4}'
                                            ,'{5}','{6}',0)", theme, "待发送", userId, userName, userPhone, content
                                            , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                x.ExecuteSqlCommand(insertSql);
            }
        }
        #endregion
    }
}