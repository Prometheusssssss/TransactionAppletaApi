using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;

namespace TransactionAppletaApi
{
    public class ShelvesProductRunner
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        public void Do()
        {
            //执行sql
            using (var x = Join.Dal.MySqlProvider.X())
            {
                //获取产品ID 查询产品是否在上架时间
                var nowDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //查询状态为上架中并且下架时间已到的商品
                var selectProductSql = "select * from B_PRODUCT_LIST where status='上架中' and OFF_SHELF_TIME < '" + nowDate + "'";
                var selectProductTables = x.ExecuteSqlCommand(selectProductSql);
                foreach (DataRow item in selectProductTables.Tables[0].Rows)
                {
                    //修改状态为下架
                    var pId = item["KID"].ToString();
                    var name = item["NAME"].ToString();
                    var sellUserId = item["SELL_USER_ID"].ToString();
                    var sellUserName = item["SELL_USER_NAME"].ToString();
                    var sellUserPhone = item["SELL_USER_PHONE"].ToString();
                    var updateSql = "update B_PRODUCT_LIST set status='已下架' where kid='" + pId + "'";
                    x.ExecuteSqlCommand(updateSql);
                    //插入消息
                    InsertMsg("下架提醒", "您的宝贝:[" + name + "]已到期自动下架，请重新发布", sellUserId, sellUserName, sellUserPhone);
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