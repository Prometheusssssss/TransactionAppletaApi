using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;

namespace TransactionAppletaApi
{
    public class UnLockProductRunner
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        public void Do()
        {

            //执行sql
            using (var x = Join.Dal.MySqlProvider.X())
            {
                //查询已锁定并且时间小于当前时间15min的产品，修改状态为上架中
                var unLockTime = DateTime.Now.AddMinutes(-15).ToString("yyyy-MM-dd HH:mm:ss");
                var selectProductSql = "select * from B_PRODUCT_LIST where status='已锁定' and LOCK_TIME <= '" + unLockTime + "' and is_delete=0";
                var selectProductTables = x.ExecuteSqlCommand(selectProductSql);
                foreach (DataRow item in selectProductTables.Tables[0].Rows)
                {
                    //修改状态为上架中
                    var pId = item["KID"].ToString();
                    var updateSql = "update B_PRODUCT_LIST set status='上架中' where kid='" + pId + "'";
                    x.ExecuteSqlCommand(updateSql);
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