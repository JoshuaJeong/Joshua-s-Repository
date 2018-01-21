using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;

namespace xave.web.generator.helper.Util
{
    internal static class CommonExtension
    {
        //private static DateTime dt;
        private static string dateTimeFormat = "yyyy-MM-dd";
        private static CultureInfo defaultCultureInfo = new CultureInfo("en-US");
        //private static Regex urlPattern = new Regex(@"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex urlPattern = new Regex(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static string[] dateTimeFormats = new string[] 
        { 
            "yyyyMMddHHmmss.f",
            "yyyyMMddHHmmss.fz",
            "yyyyMMddHHmmss.fzz",
            "yyyyMMddHHmmss.fzzz",            
            "yyyyMMddHHmmss.ff",
            "yyyyMMddHHmmss.ffz",
            "yyyyMMddHHmmss.ffzz",
            "yyyyMMddHHmmss.ffzzz",

            "yyyyMMddHHmmssfff",
            "yyyyMMddHHmmss.fff",
            "yyyyMMddHHmmss.fffz",
            "yyyyMMddHHmmss.fffzz",
            "yyyyMMddHHmmss.fffzzz",
            "yyyyMMddHHmmss.ffff",
            "yyyyMMddHHmmss.ffffz",
            "yyyyMMddHHmmss.ffffzz",
            "yyyyMMddHHmmss.ffffzzz",

            "yyyyMMddHHmmss", 
            "yyyyMMddHHmm",
            "yyyyMMddHH",
            "yyyyMMdd",
            "yyyyMM",
            "yyyy",

            "yyyy-MM-dd",
            "yyyy-MM-dd HH:mm:ss",
            "dd-MM-yyyy HH:mm:ss",
            "yyyy-MM-dd HH:mm:ss tt",
            "yyyy/MM/dd",
            "yyyy/MM/dd HH:mm:ss",
            "dd/MM/yyyy HH:mm:ss",
            "yyyy/MM/dd HH:mm:ss tt",
            "dd-MM-yyyy",
            "dd/MM/yyyy",
            "MM/dd/yyyy",
            "MM/dd/yyyy HH:mm:ss",
            "MM/dd/yyyy HH:mm:ss tt",
            "M/d/yyyy",
            "M/d/yyyy HH:mm:ss",
            "M/d/yyyy HH:mm:ss tt",
            "M/d/yyyy h:mm:ss tt",
            "M/d/yyyy h:mm tt",
            "MM/dd/yyyy hh:mm:ss",
            "M/d/yyyy h:mm:ss",
            "M/d/yyyy hh:mm tt",
            "M/d/yyyy hh tt",
            "M/d/yyyy h:mm",
            "M/d/yyyy h:mm",
            "MM/dd/yyyy hh:mm",
            "M/dd/yyyy hh:mm",
        };

        /// <summary>
        /// Enum 객체의 DescriptionAttribute.Description 값을 반환합니다.
        /// </summary>
        /// <param name="enumValue">DescriptionAttribute을 반환할 Enum 개체 입니다.</param>
        /// <returns>Enum 개체의 DescriptionAttribute.Description 값입니다</returns>
        internal static string GetDescription(this object enumValue)
        {
            string defDesc = string.Empty;
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            if (null != fi)
            {
                object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return defDesc;
        }

        internal static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            //throw new ArgumentException("Not found.", "description");
            return default(T);
        }


        /// <summary>
        /// 해당 문자열의 URL 형식 여부를 판별합니다.
        /// </summary>
        /// <param name="param">URL 형식 여부를 판단한 문자열 입니다.</param>
        /// <returns>해당 문자열이 URL 형식이면 true, 그렇지 않은 경우는 false를 반환합니다.</returns>
        internal static bool IsURL(this string param)
        {
            if (string.IsNullOrEmpty(param))
                return false;
            else if (urlPattern.IsMatch(param))
                return true;
            else
                return false;
        }

        internal static long GetLinesCount(this string s) // string 행수 반환
        {
            long count = 0;
            int position = 0;
            while ((position = s.IndexOf('\n', position)) != -1)
            {
                count++;
                position++;
            }
            return count;
        }

        /// <summary>
        /// System.DateTime 형식으로 변환가능한 문자열의 포맷을 변경합니다.
        /// </summary>
        /// <param name="value">하나 이상의 날짜와 시간을 포함하는 변환할 문자열입니다.</param>
        /// <param name="format">value에 허용되는 서식입니다. 기본값은 'yyyy-MM-dd' 입니다</param>
        /// <returns>변환이 성공한 경우 서식을 적용한 문자열을 출력하고, 변환에 실패한 경우 해당 문자열 그대로 출력합니다.</returns>        
        internal static string StringToDatetime(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string retobj = value.Trim();
                DateTime dt;
                if (DateTime.TryParseExact(retobj, dateTimeFormats, defaultCultureInfo, DateTimeStyles.AssumeLocal, out dt))
                {
                    if (retobj.Length < 9)
                    {
                        return dt.ToString(dateTimeFormat);
                    }
                    else
                    {
                        return dt.ToString("yyyy-MM-dd HH:mm");
                    }
                }
                else
                {
                    return value;
                }
            }
            else return string.Empty;
        }

        internal static string StringToDatetime(this string value, string format = null)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value.Trim();
                string formatValue = !string.IsNullOrEmpty(format) ? format : "yyyy-MM-dd";
                DateTime dt;

                //if (DateTime.TryParseExact(value, dateTimeFormats, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out dt))
                if (DateTime.TryParseExact(value, dateTimeFormats, defaultCultureInfo, DateTimeStyles.AssumeLocal, out dt))
                {
                    if (value.Length < 9)
                    {
                        return dt.ToString(formatValue);
                    }
                    else
                    {
                        return dt.ToString("yyyy-MM-dd HH:mm");
                    }
                }
                else
                {
                    return value;
                }
            }
            else return string.Empty;
        }

        internal static string CreateRandomDigits(int length)
        {
            var random = new Random();
            string value = string.Empty;
            for (int i = 0; i < length; i++)
            {
                if (i == 0)
                {
                    value = String.Concat(value, random.Next(1, 10).ToString());
                }
                else
                {
                    value = String.Concat(value, random.Next(10).ToString());
                }
            }
            return value;
        }

        internal static T[] ConvertListToArray<T>(this object param)
        {
            if (param != null && param.GetType().IsGenericType)
            {
                List<T> obj = param as List<T>;
                return obj.ToArray();
            }
            return null;
        }
    }
}
