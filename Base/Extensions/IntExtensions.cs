using System.Collections.Generic;

namespace Join
{
    /// <summary>
    /// 整数拓展
    /// </summary>
    public static class IntExtensions
    {
        #region X.成员方法[ToIncreaseList]
        /// <summary>
        /// 根据起始值,最大值,间隔值,生成递增数值数组
        /// </summary>
        /// <param name="target"></param>
        /// <param name="max"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static List<int> ToIncreaseList(this int target, int max, int step)
        {
            var result = new List<int>();
            for (var i = target; i <= max; i += step)
                result.Add(i);
            return result;
        }
        #endregion

        #region X.成员方法[ToDecreaseList]
        /// <summary>
        /// 根据起始值,最小值,间隔值,生成递减数值数组
        /// </summary>
        /// <param name="target"></param>
        /// <param name="min"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static List<int> ToDecreaseList(this int target, int min, int step)
        {
            var result = new List<int>();
            for (var i = target; i >= min; i -= step)
                result.Add(i);
            return result;
        }
        #endregion
    }
}
