using Join;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace TransactionAppletaApi
{
    [RoutePrefix("api/_cud")]
    //[Security.AuthorizationRequired]
    public class CudController : BaseController
    {
        /// <summary>
        /// 插入/更新
        /// http://localhost:64665/api/_cud/createAndUpdate/tableName
        /// </summary>
        [HttpPost]
        [Route("createAndUpdate/{tableName}")]
        public IHttpActionResult CreteoAndUpdateTable(string tablename, [FromBody]JToken json)
        {
            return this.TryReturn<object>(() =>
            {
                try
                {
                    //执行sql
                    using (var x = Join.Dal.MySqlProvider.X())
                    {
                        var dicJson = json.ToJsonString();
                        var dict = dicJson.JsonToDictionary();
                        var kid = dict.GetValue("KID");
                        var sql = "";
                        //new
                        if (kid == "-1")
                        {
                            var ra = new Random();
                            var keys = "";
                            var values = "";
                            if (tablename.ToUpper() != "B_ORDER_MSG_DETAILS" && tablename.ToUpper() != "B_MESSAGE")
                            {
                                keys = "`CODE`,`IS_DELETE`,`CRT_TIME`";
                                values = "'" + DateTime.Now.ToString("yyyyMMddHHmmss") + ra.Next(1000, 9999) + "',0,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                            }
                            else
                            {
                                keys = "`IS_DELETE`,`CRT_TIME`";
                                values = "0,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                            }
                            foreach (var item in dict)
                            {
                                if (item.Key != "KID")
                                {
                                    keys = keys.AppendSql(item.Key, "`", true);
                                    values = values.AppendSql(item.Value.ToString(), "'", true);
                                }
                            }
                            sql = string.Format(@"insert into {0} ({1}) values ({2})", tablename, keys, values);
                        }
                        //update
                        else
                        {
                            var updateSql = "";
                            foreach (var item in dict)
                            {
                                if (item.Key != "KID")
                                {
                                    if (updateSql != "")
                                        updateSql = updateSql + "`" + item.Key + "`='" + item.Value.ToString() + "',";
                                    else
                                        updateSql = "`" + item.Key + "`='" + item.Value.ToString() + "',";
                                }
                            }
                            updateSql = updateSql.Substring(0, updateSql.Length - 1);

                            sql = string.Format(@"update {0} set {1} where kid='{2}'", tablename, updateSql, kid);
                        }
                        var dt = x.ExecuteSqlCommand(sql);
                        ////执行扩展逻辑
                        //switch (tablename)
                        //{
                        //    case "b_order":
                        //        ExcuteInsertOrderEx(kid);
                        //        break;
                        //    default:
                        //        break;
                        //}
                        return new { Table = dt, IS_SUCCESS = true, MSG = "" };
                    }
                }
                catch (Exception ex)
                {
                    return new { Table = "", IS_SUCCESS = false, MSG = ex.Message };
                }
            });
        }

        /// <summary>
        /// 删除
        /// http://localhost:64665/api/_cud/del/tableName
        /// </summary>
        [HttpPost]
        [Route("del/{tableName}")]
        public IHttpActionResult DelTable(string tablename, [FromBody]JToken json)
        {
            return this.TryReturn<object>(() =>
            {
                try
                {
                    var dicJson = json.ToJsonString();
                    var dict = dicJson.JsonToDictionary();
                    var kid = dict.GetValue("KID");

                    //执行扩展逻辑
                    switch (tablename.ToUpper())
                    {
                        case "B_PRODUCT_LIST":
                            var result = ExcuteDelOrderEx(tablename, kid);
                            if (result == false)
                                return new { Table = "", IS_SUCCESS = false, MSG = "产品状态为已售卖，不可删除" };
                            break;
                        default:
                            break;
                    }
                    //执行sql
                    using (var x = Join.Dal.MySqlProvider.X())
                    {
                        var sql = string.Format(@"update {0} set is_delete=1 where KID='{1}'", tablename, kid);
                        var dt = x.ExecuteSqlCommand(sql);

                        return new { Table = dt, IS_SUCCESS = true, MSG = "" };
                    }
                }
                catch (Exception ex)
                {
                    return new { Table = "", IS_SUCCESS = false, MSG = ex.Message };
                }
            });
        }

        #region 成员方法

        #region 生成单据扩展逻辑
        /// <summary>
        /// 生成单据扩展逻辑
        /// </summary>
        /// <param name="kid"></param>
        public void ExcuteInsertOrderEx(string kid)
        {
            //执行
        }
        #endregion

        #region 删除单据扩展逻辑
        /// <summary>
        /// 删除单据扩展逻辑
        /// </summary>
        /// <param name="kid"></param>
        public bool ExcuteDelOrderEx(string tablename, string kid)
        {
            var backResult = true;
            //判断单据状态是否为已售卖
            using (var x = Join.Dal.MySqlProvider.X())
            {
                var sql = string.Format(@"select * from {0} where KID='{1}'", tablename, kid);
                var dt = x.ExecuteSqlCommand(sql);
                var table = dt.Tables[0];
                if (table.Rows.Count > 0)
                {
                    var status = table.Rows[0]["STATUS"].ToString();
                    if (status == "已售卖")
                        backResult = false;
                }
                else
                    backResult = false;
                return backResult;
            }
        }
        #endregion

        #region 插入消息
        /// <summary>
        /// 插入消息
        /// </summary>
        /// <param name="kid"></param>
        public void ExcuteInsertMsg(string theme, string content, int userId)
        {
            //判断单据状态是否为已售卖
            using (var x = Join.Dal.MySqlProvider.X())
            {
                //获取用户信息
                var userModel = SearchByUserId(userId);
                //插入消息

                var sql = string.Format(@"insert B_MESSAGE (`THENE`,`STATUS`,`USER_ID`,`USER_NAME`,`USER_PHONE`,`CONTENT`,`IS_DELETE`,`CRT_TIME`)
                                                    values('{0}','待发送','{1}','{2}','{3}','{4}',0,'{5}')", theme, userId, userModel.userName, userModel.userPhone, content, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                var dt = x.ExecuteSqlCommand(sql);
            }
        }
        #endregion

        #region 根据用户ID查询名称手机号
        /// <summary>
        /// 根据用户ID查询名称手机号
        /// </summary>
        public UserModel SearchByUserId(int userId)
        {
            using (var x = Join.Dal.MySqlProvider.X())
            {
                var userModel = new UserModel();
                var sql = string.Format(@"select * from A_USER where KID='{0}' and is_delete=0", userId);
                var dt = x.ExecuteSqlCommand(sql);
                var table = dt.Tables[0];
                if (table.Rows.Count > 0)
                {
                    userModel.userId = userId;
                    userModel.userName = table.Rows[0]["NAME"].ToString();
                    userModel.userPhone = table.Rows[0]["PHONE"].ToString();
                }
                return userModel;
            }
        }

        public class UserModel
        {
            public int userId { get; set; }
            public string userName { get; set; }
            public string userPhone { get; set; }
        }
        #endregion

        #region 提现单据
        /// <summary>
        /// 提现单据
        /// http://localhost:64665/api/_cud/submitOrder
        /// </summary>
        [HttpPost]
        [Route("submitOrder")]
        public IHttpActionResult submitOrder([FromBody]JToken json)
        {
            return this.TryReturn<object>(() =>
            {
                try
                {
                    var backTable = new DataTable();
                    var jsn = json.AsDynamic();
                    string kid = jsn.KID;
                    string payUserId = jsn.PAY_USER_ID;
                    string payUserName = jsn.PAY_USER_NAME;
                    string payAmount = jsn.PAY_AMOUNT;
                    string orderAmount = jsn.ORDER_AMOUNT;
                    string userId = jsn.USER_ID;
                    string userName = jsn.USER_NAME;
                    string userPhone = jsn.USER_PHONE;
                    string orderCode = jsn.ORDER_CODE;
                    //修改提现单状态
                    using (var x = Join.Dal.MySqlProvider.X())
                    {
                        //修改提现单数据
                        var updateSql = string.Format(@"update  b_withdrawal set PAY_USER_ID='{0}',PAY_USER_NAME='{1}',PAY_AMOUNT='{2}',PAY_TIME='{3}',STATUS='已打款' where kid = '{4}'"
                                                    , payUserId, payUserName, payAmount, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), kid);
                        x.ExecuteSqlCommand(updateSql);
                        //插入流水表数据，修改User表金额
                        InsertAccountRecord(kid, orderCode, orderAmount, "支出", "商品", "已结算", userId, userName, userPhone, payAmount);
                        //插入消息
                        InsertMsg("提现成功提醒", "您的提现申请" + payAmount + "元 已审核通过并支付，请注意查收。", userId, userName, userPhone);
                        //获取返回值
                        var selectSql = "select * from b_withdrawal where is_delete=0 and kid='" + kid + "'";
                        backTable = x.ExecuteSqlCommand(selectSql).Tables[0];
                        x.Close();
                    }
                    return new { Table = backTable, IS_SUCCESS = true, MSG = "" };
                }
                catch (Exception ex)
                {
                    return new { Table = "", IS_SUCCESS = false, MSG = ex.Message };
                }
            });
        }
        #endregion

        #region 单据确认收货
        /// <summary>
        /// 提现单据
        /// http://localhost:64665/api/_cud/confirmedOrder
        /// </summary>
        [HttpPost]
        [Route("confirmedOrder")]
        public IHttpActionResult confirmedOrder([FromBody]JToken json)
        {
            return this.TryReturn<object>(() =>
            {
                try
                {
                    var backTable = new DataTable();
                    var jsn = json.AsDynamic();
                    string kid = jsn.KID;
                    using (var x = Join.Dal.MySqlProvider.X())
                    {
                        //1、更新订单状态为已完成
                        var updateOrderSql = "update b_order set status='已完成',CONFIRMED_TIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where kid ='" + kid + "'";
                        x.ExecuteSqlCommand(updateOrderSql);
                        //查询订单信息
                        var selectOrderSql = "select * from b_order where kid='" + kid + "' and is_delete =0";
                        backTable = x.ExecuteSqlCommand(selectOrderSql).Tables[0];
                        //2、发消息给卖家，消息表插入数据
                        if (backTable.Rows.Count > 0)
                        {
                            var sellUserId = backTable.Rows[0]["SELL_USER_ID"].ToString();
                            var sellUserName = backTable.Rows[0]["SELL_USER_NAME"].ToString();
                            var sellUserPhone = backTable.Rows[0]["SELL_USER_PHONE"].ToString();
                            var productName = backTable.Rows[0]["PRODUCT_NAME"].ToString();
                            InsertMsg("交易完成提醒", "您的宝贝:[" + productName + "]已交易成功。", sellUserId, sellUserName, sellUserPhone);
                            
                            x.Close();
                            return new { Table = "", IS_SUCCESS = false, MSG = "未查询到用户信息" };
                        }
                        x.Close();
                        return new { Table = "", IS_SUCCESS = false, MSG = "未查询到订单信息" };
                    }
                }
                catch (Exception ex)
                {
                    return new { Table = "", IS_SUCCESS = false, MSG = ex.Message };
                }
            });
        }
        #endregion

        #region 生成流水
        /// <summary>
        /// 生成流水
        /// </summary>
        public void InsertAccountRecord(string orderId, string orderCode, string orderAmount, string type, string receiveType, string settStatus, string userId, string userName, string userPhone, string amount)
        {
            using (var x = Join.Dal.MySqlProvider.X())
            {
                var insertSql = string.Format(@"insert into B_ACCOUNT_RECORD (`CODE`,`USER_ID`,`USER_NAME`,`USER_PHONE`,
                                            `TYPE`,`RECEIVE_TYPE`,`SELETTMENT_STATUS`,`SELETTMENT_TIME`,`SELETTMENT_AMOUNT`,
                                            `ORDER_AMOUNT`,`ORDER_ID`,`ORDER_CODE`,`IS_DELETE`,`CRT_TIME`) values ('{0}','{1}','{2}','{3}','{4}'
                                            ,'{5}','{6}','{7}','{8}','{9}','{10}','{11}',0,'{12}')", DateTime.Now.ToString("yyyyMMddHHmmss"),
                                            userId, userName, userPhone, type, receiveType, settStatus, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                            amount, orderAmount, orderId, orderCode, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                x.ExecuteSqlCommand(insertSql);
                //获取用户余额
                var selUserSql = "select * from a_user where kid='" + userId + "'";
                var selUserDt = x.ExecuteSqlCommand(selUserSql);
                var row = selUserDt.Tables[0].Rows[0];
                var balance = decimal.Parse(row["BALANCE"].ToString());
                var payAmount = decimal.Parse(amount);
                //修改用户表金额
                var diffAmount = 0.00m;
                if (type == "支出")
                {
                    diffAmount = balance - payAmount;
                }
                else if (type == "收入")
                {
                    diffAmount = balance + payAmount;
                }
                var updateUserSql = "update a_user set BALANCE='" + diffAmount + "' where kid='" + userId + "'";
                x.ExecuteSqlCommand(updateUserSql);
            }
        }
        #endregion

        #region 插入消息
        /// <summary>
        /// 插入消息
        /// </summary>
        public void InsertMsg(string theme, string content, string userId, string userName, string userPhone)
        {
            using (var x = Join.Dal.MySqlProvider.X())
            {
                var insertSql = string.Format(@"insert into B_MESSAGE (`THEME`,`STATUS`,`USER_ID`,`USER_NAME`,
                                            `USER_PHONE`,`CONTENT`,`SEND_TIME`,`IS_DELETE`,`CRT_TIME`) values ('{0}','{1}','{2}','{3}','{4}'
                                            ,'{5}','{6}',0,'{7}')", theme, "待发送", userId, userName, userPhone, content
                                            , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                x.ExecuteSqlCommand(insertSql);
            }
        }
        #endregion

        #region 生成订单
        /// <summary>
        /// 生成订单
        /// http://localhost:64665/api/_cud/createOrder
        /// </summary>
        [HttpPost]
        [Route("createOrder")]
        public IHttpActionResult CreateOrder([FromBody]JToken json)
        {
            return this.TryReturn<object>(() =>
            {
                try
                {
                    //执行sql
                    using (var x = Join.Dal.MySqlProvider.X())
                    {
                        var dicJson = json.ToJsonString();
                        var dict = dicJson.JsonToDictionary();
                        //获取产品ID 查询产品是否在上架时间
                        var productId = dict.GetValue("PRODUCT_ID");
                        var productName = dict.GetValue("PRODUCT_NAME");
                        var nowDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        var selectProductSql = "select * from B_PRODUCT_LIST where kid='" + productId + "' and OFF_SHELF_TIME > '" + nowDate + "'";
                        var selectProductTables = x.ExecuteSqlCommand(selectProductSql);
                        if (selectProductTables.Tables[0].Rows.Count > 0)
                        {
                            var kid = dict.GetValue("KID");
                            var ra = new Random();
                            var keys = "`CODE`,`IS_DELETE`,`CRT_TIME`";
                            var code = DateTime.Now.ToString("yyyyMMddHHmmss") + ra.Next(1000, 9999);
                            var values = "'" + code + "',0," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                            foreach (var item in dict)
                            {
                                if (item.Key != "KID")
                                {
                                    keys = keys.AppendSql(item.Key, "`", true);
                                    values = values.AppendSql(item.Value.ToString(), "'", true);
                                }
                            }
                            var sql = string.Format(@"insert into {0} ({1}) values ({2})", "b_order", keys, values);
                            x.ExecuteSqlCommand(sql);
                            //查询订单
                            var selectOrderSql = string.Format(@"select * from b_order where code='{0}'", code);
                            var dataTable = x.ExecuteSqlCommand(selectOrderSql);
                            if (dataTable.Tables[0].Rows.Count > 0)
                            {
                                var row = dataTable.Tables[0].Rows[0];
                                //卖家ID
                                string sellUserId = row["SELL_USER_ID"].ToString();
                                //卖家昵称
                                string sellUserName = row["SELL_USER_NAME"].ToString();
                                //卖家手机号
                                string sellUserPhone = row["SELL_USER_PHONE"].ToString();
                                //金额
                                string price = row["PRICE"].ToString();
                                //单据ID
                                string orderId = row["KID"].ToString();

                                //下架产品
                                var updateProductSql = string.Format(@"update B_PRODUCT_LIST set status='已卖出' where kid='{0}'", productId);
                                x.ExecuteSqlCommand(updateProductSql);
                                //插入账户流水收入类型
                                var insertAccountRecordSql = string.Format(@"insert into B_ACCOUNT_RECORD (`CODE`,`USER_ID`,`USER_NAME`,`USER_PHONE`,
                                            `TYPE`,`RECEIVE_TYPE`,`SELETTMENT_STATUS`,`SELETTMENT_TIME`,`SELETTMENT_AMOUNT`,
                                            `ORDER_AMOUNT`,`ORDER_ID`,`ORDER_CODE`,`IS_DELETE`,`CRT_TIME`) values ('{0}','{1}','{2}','{3}','{4}'
                                            ,'{5}','{6}','{7}','{8}','{9}','{10}','{11}',0,'{12}')", DateTime.Now.ToString("yyyyMMddHHmmss"),
                                                   sellUserId, sellUserName, sellUserPhone, "收入", "商品", "待结算", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                   price, price, orderId, code, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                x.ExecuteSqlCommand(insertAccountRecordSql);
                                //修改卖家即将收入增加
                                //获取用户余额
                                var selUserSql = "select * from a_user where kid='" + sellUserId + "'";
                                var selUserDt = x.ExecuteSqlCommand(selUserSql);
                                var userRows = selUserDt.Tables[0].Rows;
                                if (userRows.Count > 0)
                                {
                                    var upIncome = decimal.Parse(userRows[0]["UPCOMING_INCOME"].ToString());
                                    var amount = decimal.Parse(price);
                                    //修改用户表金额
                                    var resultAmount = upIncome + amount;
                                    var updateUserSql = "update a_user set UPCOMING_INCOME='" + resultAmount + "' where kid='" + sellUserId + "'";
                                    x.ExecuteSqlCommand(updateUserSql);
                                }
                                //执行插入消息
                                InsertMsg("发货提醒", "您的宝贝:[" + productName + "]已被拍下,请注意及时发货", sellUserId, sellUserName, sellUserPhone);
                                return new { Table = dataTable, IS_SUCCESS = true, MSG = "" };
                            }
                            else
                            {
                                return new { Table = "", IS_SUCCESS = false, MSG = "订单生成失败" };
                            }
                        }
                        else
                        {
                            return new { Table = "", IS_SUCCESS = false, MSG = "商品已下架" };
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new { Table = "", IS_SUCCESS = false, MSG = ex.Message };
                }
            });
        }
        #endregion

        #endregion

    }
}
