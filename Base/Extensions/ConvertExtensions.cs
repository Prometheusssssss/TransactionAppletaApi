using System;
using System.Data;

namespace Join
{
    /// <summary>
    /// 转换拓展
    /// </summary>
    public static class ConvertExtensions
    {
        #region X.成员方法[ToBoolean]
        /// <summary>
        /// 转Boolean
        /// </summary>
        public static bool? ToBoolean(this object input)
        {
            var s = Convert.ToString(input);
            if (string.IsNullOrEmpty(s))
                return null;

            Boolean result;
            var success = Boolean.TryParse(s, out result);
            if (success)
                return result;
            return null;
        }

        /// <summary>
        /// 转Boolean
        /// </summary>
        public static bool ToBoolean(this object input, bool nullValue)
        {
            if (null == input)
                return nullValue;
            var result = input.ToBoolean();
            if (result == null)
                return nullValue;
            return result.Value;
        }

        /// <summary>
        /// 判断是否可转为Boolean
        /// </summary>
        public static bool CanBoolean(this object input)
        {
            if (null == input)
                return false;
            var result = input.ToBoolean();
            return result != null;
        }
        #endregion

        #region X.成员方法[ToDateTime]
        /// <summary>
        /// 转日期
        /// </summary>
        public static DateTime? ToDateTime(this object input)
        {
            var s = Convert.ToString(input);
            if (string.IsNullOrEmpty(s))
                return null;

            DateTime result;
            var success = DateTime.TryParse(s, out result);
            if (success)
                return result;
            return null;
        }

        /// <summary>
        /// 转日期
        /// </summary>
        public static DateTime ToDateTime(this object input, DateTime nullValue)
        {
            if (null == input)
                return nullValue;
            var result = input.ToDateTime();
            if (result == null)
                return nullValue;
            return result.Value;
        }

        /// <summary>
        /// 判断是否可转为DateTime
        /// </summary>
        public static bool CanDateTime(this object input)
        {
            if (null == input)
                return false;
            var result = input.ToDateTime();
            return result != null;
        }

        /// <summary>
        /// 判断是否为DateTime(Null 或 Max 或 Min)
        /// </summary>
        public static bool IsDateTimeEmpty(this object input)
        {
            var min = DateTime.MinValue;
            var max = DateTime.MaxValue;
            if (null == input)
                return false;
            var result = input.ToDateTime();
            if (result == null || result.Value == min || result.Value == max)
                return true;
            return false;
        }
        #endregion

        #region X.成员方法[ToInt16]
        /// <summary>
        /// 转Int16
        /// </summary>
        public static short? ToInt16(this object input)
        {
            var s = Convert.ToString(input);
            if (string.IsNullOrEmpty(s))
                return null;

            short result;
            var success = short.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out result);
            if (success)
                return result;
            return null;
        }

        /// <summary>
        /// 转Int16
        /// </summary>
        public static int ToInt16(this object input, short nullValue)
        {
            if (null == input)
                return nullValue;
            var result = input.ToInt16();
            if (result == null)
                return nullValue;
            return result.Value;
        }

        /// <summary>
        /// 判断是否可转为Int16
        /// </summary>
        public static bool CanInt16(this object input)
        {
            if (null == input)
                return false;
            var result = input.ToInt16();
            return result != null;
        }

        /// <summary>
        /// 判断是否为Int16（0）
        /// </summary>
        public static bool IsInt16Zero(this object input)
        {
            short zero = 0;
            if (null == input)
                return false;
            var result = input.ToInt16(zero);
            return result == zero;
        }
        #endregion

        #region X.成员方法[ToInt32]
        /// <summary>
        /// 转Int32
        /// </summary>
        public static int? ToInt32(this object input)
        {
            var s = Convert.ToString(input);
            if (string.IsNullOrEmpty(s))
                return null;

            Int32 result;
            var success = Int32.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out result);
            if (success)
                return result;
            return null;
        }

        /// <summary>
        /// 转Int32
        /// </summary>
        public static int ToInt32(this object input, int nullValue)
        {
            if (null == input)
                return nullValue;
            var result = input.ToInt32();
            if (result == null)
                return nullValue;
            return result.Value;
        }

        /// <summary>
        /// 判断是否可转为Int32
        /// </summary>
        public static bool CanInt32(this object input)
        {
            if (null == input)
                return false;
            var result = input.ToInt32();
            return result != null;
        }

        /// <summary>
        /// 判断是否为Int32（0）
        /// </summary>
        public static bool IsInt32Zero(this object input)
        {
            int zero = 0;
            if (null == input)
                return false;
            var result = input.ToInt32(zero);
            return result == zero;
        }
        #endregion

        #region X.成员方法[ToInt64]
        /// <summary>
        /// 转Int64
        /// </summary>
        public static long? ToInt64(this object input)
        {
            var s = Convert.ToString(input);
            if (string.IsNullOrEmpty(s))
                return null;

            long result;
            var success = long.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out result);
            if (success)
                return result;
            return null;
        }

        /// <summary>
        /// 转Int64
        /// </summary>
        public static long ToInt64(this object input, long nullValue)
        {
            if (null == input)
                return nullValue;
            var result = input.ToInt64();
            if (result == null)
                return nullValue;
            return result.Value;
        }

        /// <summary>
        /// 判断是否可转为Int64
        /// </summary>
        public static bool CanInt64(this object input)
        {
            if (null == input)
                return false;
            var result = input.ToInt64();
            return result != null;
        }

        /// <summary>
        /// 判断是否为Int64（0）
        /// </summary>
        public static bool IsInt64Zero(this object input)
        {
            int zero = 0;
            if (null == input)
                return false;
            var result = input.ToInt64(zero);
            return result == zero;
        }
        #endregion

        #region X.成员方法[ToSingle]
        /// <summary>
        /// 转单精度小数Single
        /// </summary>
        public static float? ToSingle(this object input)
        {
            var s = Convert.ToString(input);
            if (string.IsNullOrEmpty(s))
                return null;
            float result;
            var success = float.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out result);
            if (success)
                return result;
            return null;
        }

        /// <summary>
        /// 转单精度小数Single
        /// </summary>
        public static float ToSingle(this object input, float nullValue)
        {
            if (null == input)
                return nullValue;
            var result = input.ToSingle();
            if (result == null)
                return nullValue;
            return result.Value;
        }

        /// <summary>
        /// 判断是否为Single
        /// </summary>
        public static bool CanSingle(this object input)
        {
            if (null == input)
                return false;
            var result = input.ToSingle();
            return result != null;
        }
        #endregion

        #region X.成员方法[ToDecimal]
        /// <summary>
        /// 转小数Decimal
        /// </summary>
        public static decimal? ToDecimal(this object input)
        {
            var s = Convert.ToString(input);
            if (string.IsNullOrEmpty(s))
                return null;

            Decimal result;
            var success = Decimal.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out result);
            if (success)
                return result;
            return null;
        }

        /// <summary>
        /// 转小数
        /// </summary>
        public static decimal ToDecimal(this object input, decimal nullValue)
        {
            if (null == input)
                return nullValue;
            var result = input.ToDecimal();
            if (result == null)
                return nullValue;
            return result.Value;
        }

        /// <summary>
        /// 判断是否为Decimal
        /// </summary>
        public static bool CanDecimal(this object input)
        {
            if (null == input)
                return false;
            var result = input.ToDecimal();
            return result != null;
        }

        /// <summary>
        /// 判断是否为Decimal(0m)
        /// </summary>
        public static bool IsDecimalZero(this object input)
        {
            var zero = decimal.Zero;
            if (null == input)
                return false;
            var result = input.ToDecimal(zero);
            return result == zero;
        }
        #endregion

        #region X.成员方法[ToDouble]
        /// <summary>
        /// 转双精度小数Double
        /// </summary>
        public static double? ToDouble(this object input)
        {
            var s = Convert.ToString(input);
            if (string.IsNullOrEmpty(s))
                return null;

            Double result;
            var success = Double.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out result);
            if (success)
                return result;
            return null;
        }

        /// <summary>
        /// 转双精度小数Double
        /// </summary>
        public static double ToDouble(this object input, double nullValue)
        {
            if (null == input)
                return nullValue;
            var result = input.ToDouble();
            if (result == null)
                return nullValue;
            return result.Value;
        }

        /// <summary>
        /// 判断是否为Double
        /// </summary>
        public static bool CanDouble(this object input)
        {
            if (null == input)
                return false;
            var result = input.ToDouble();
            return result != null;
        }

        /// <summary>
        /// 判断是否为Double(0.0)
        /// </summary>
        public static bool IsDoubleZero(this object input)
        {
            double zero = 0.0;
            if (null == input)
                return false;
            var result = input.ToDouble(zero);
            return result == zero;
        }
        #endregion

        #region X.成员方法[CanNumeric]
        /// <summary>
        /// Determines whether the specified
        /// value can be converted to a valid number.
        /// 判断给定值是否可转成数值
        /// </summary>
        public static bool CanNumeric(this object value)
        {
            double dbl;
            return double.TryParse(value.ToString(), System.Globalization.NumberStyles.Any,
              System.Globalization.NumberFormatInfo.InvariantInfo, out dbl);
        }
        #endregion

        #region X.成员方法[ToEnum]
        /// <summary>
        /// 转枚举
        /// </summary>
        public static T? ToEnum<T>(this object input)
           where T : struct
        {
            var s = Convert.ToString(input);
            if (string.IsNullOrEmpty(s))
                return null;

            T result;
            var success = Enum.TryParse(s, true, out result);
            if (success)
                return result;
            return null;
        }

        /// <summary>
        /// 转枚举
        /// </summary>
        public static T ToEnum<T>(this object input, T nullValue)
         where T : struct
        {
            if (null == input)
                return nullValue;
            var result = input.ToEnum<T>();
            if (result == null)
                return nullValue;
            return result.Value;
        }

        /// <summary>
        /// 判断是否为Enum
        /// </summary>
        public static bool CanEnum<T>(this object input)
            where T : struct
        {
            if (null == input)
                return false;
            var result = input.ToEnum<T>();
            return result != null;
        }
        #endregion

        #region X.成员方法[ToN(Guid)]
        public static string ToN(this Guid id)
        {
            return id.ToString("N");
        }
        #endregion

        #region X.成员方法[ToD(Guid)]
        public static string ToD(this Guid id)
        {
            return id.ToString("D");
        }
        #endregion

        #region X.成员方法[ToGuid]
        /// <summary>
        /// 转Guid
        /// </summary>
        public static Guid? ToGuid(this object input)
        {
            var s = Convert.ToString(input);
            if (string.IsNullOrEmpty(s))
                return null;

            Guid result;
            var success = Guid.TryParse(s, out result);
            if (success)
                return result;
            return null;
        }

        /// <summary>
        /// 转Guid
        /// </summary>
        /// <param name="input">Guid格式字符串</param>
        public static Guid ToGuid(this object input, Guid nullValue)
        {
            if (null == input)
                return nullValue;
            var result = input.ToGuid();
            if (result == null)
                return nullValue;
            return result.Value;
        }

        /// <summary>
        /// 判断是否为Guid
        /// </summary>
        public static bool CanGuid(this object input)
        {
            if (null == input)
                return false;
            var result = input.ToGuid();
            return result != null;
        }

        /// <summary>
        /// 判断是否为Guid空
        /// </summary>
        public static bool IsGuidEmpty(this object input)
        {
            if (null == input)
                return false;
            var result = input.ToGuid(Guid.Empty);
            return result == Guid.Empty;
        }
        #endregion

        #region X.成员方法[IsString]
        public static bool IsString(this object input, string value)
        {
            if (null == input || value == null)
                return false;
            return input.ToString() == value;
        }
        #endregion

        #region X.成员方法[ToSqlValue]
        public static object ToSqlValue(this object input, SqlDbType dbType)
        {
            if (input == null)
                return DBNull.Value;
            var value = Convert.ToString(input);
            switch (dbType)
            {
                case SqlDbType.BigInt:
                    return Convert.ToInt64(value);
                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return System.Convert.FromBase64String(value);
                case SqlDbType.Bit:
                    return Convert.ToBoolean(value);
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                case SqlDbType.SmallDateTime:
                    return Convert.ToDateTime(value);
                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return Convert.ToDecimal(value);
                case SqlDbType.Float:
                    return Convert.ToDouble(value);
                case SqlDbType.Int:
                    return Convert.ToInt32(value);
                case SqlDbType.Real:
                    return Convert.ToSingle(value);
                case SqlDbType.SmallInt:
                    return Convert.ToInt16(value);
                case SqlDbType.TinyInt:
                    return Convert.ToByte(value);
                case SqlDbType.UniqueIdentifier:
                    return value.ToGuid();
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                    return value;
                default:
                    return value;
            }
        }
        #endregion

        
    }
}
