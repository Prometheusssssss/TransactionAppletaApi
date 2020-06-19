//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="TeaMate Studio">
//     Copyright (c) TeaMate Studio. All rights reserved.
//     Website: http://www.teamate.net/
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Join
{
    /// <summary>
    /// 字符串拓展
    /// </summary>
    public static class StringExtensions
    {
        #region X.成员方法[IsBlank]
        /// <summary>
        /// 是否为空白字符串
        /// </summary>
        /// <param name="target">要判定的字符串</param>
        public static bool IsBlank(this string target)
        {
            return string.IsNullOrEmpty(target) || string.IsNullOrWhiteSpace(target);
        }
        #endregion

        #region X.成员方法[IsEmptyJson]
        /// <summary>
        /// 验证字符串是否不为Null、不为Empty、不为全空格
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmptyJson(this string value)
        {
            return string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value) || value.ToUpper().Equals("NULL") || value.Equals("[]") || value.Equals("{}");
        }
        #endregion

        #region X.成员方法[Appen]
        /// <summary>
        /// 增加行
        /// </summary>
        /// <param name="target">原字符串</param>
        /// <param name="append">增加内容</param>
        public static string AppendLine(this string target, object append)
        {
            target = string.Format("{0}{1}{2}", target, Environment.NewLine, append);
            return target;
        }

        /// <summary>
        /// 增加字符
        /// </summary>
        /// <param name="target">原字符串</param>
        /// <param name="append">增加内容</param>
        public static string AppendText(this string target, object append)
        {
            target = string.Format("{0}{1}", target, append);
            return target;
        }

        /// <summary>
        /// 增加字符
        /// </summary>
        /// <param name="target">原字符串</param>
        /// <param name="append">增加内容</param>
        public static string InsertText(this string target, object append)
        {
            target = string.Format("{0}{1}", append, target);
            return target;
        }
        #endregion

        #region X.成员方法[ContainIn]
        /// <summary>
        /// 是否被包含在字符串中
        /// </summary>
        /// <param name="target">判断的内容</param>
        /// <param name="sentence">被包含在的字符串</param>
        /// <returns></returns>
        public static bool ContainIn(this string target, string sentence)
        {
            return sentence.Contains(target);
        }
        #endregion
    }
}
