using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GininDev.Common.DataTools.Helpers
{
    public class ConvertData
    {
        public static int GetMonth(string sB, string sE)
        {
            int month = 0;
            DateTime dtbegin = Convert.ToDateTime(sB); //起始时间
            DateTime dtend = Convert.ToDateTime(sE); //结束时间
            if ((dtend.Year - dtbegin.Year) == 0)
            {
                month = dtend.Month - dtbegin.Month;
            }
            if ((dtend.Year - dtbegin.Year) < 1) return month;

            if (dtend.Month - dtbegin.Month < 0)
            {
                month = (dtend.Year - dtbegin.Year - 1)*12 + (12 - dtbegin.Month) + dtend.Month + 1;
            }
            else
            {
                month = (dtend.Year - dtbegin.Year)*12 + dtend.Month - dtbegin.Month + 1;
            }
            return month;
        }

        public static double[] ToDoubleArray(List<object> muls, int iMaxYear)
        {
            var arPars = new List<double>();
            for (int i = 0; i < iMaxYear; i++)
            {
                if ((muls == null) || muls.Count <= i)
                {
                    arPars.Add(0);
                }
                else
                {
                    arPars.Add(ToDouble(muls[i]));
                }
            }
            return arPars.ToArray();
        }

        public static bool ToBool(object obj)
        {
            return ToBool(obj, false);
        }

        private static bool ToBool(object obj, bool bDefault)
        {
            bool num2 = false;
            try
            {
                if ((obj == null) || (string.IsNullOrEmpty(ToString(obj))))
                {
                    return bDefault;
                }
                num2 = bool.Parse(obj.ToString().Trim());
            }
            catch (Exception ex)
            {
            }
            return num2;
        }

        public static DateTime ToDateTime(object obj)
        {
            return ToDateTime(obj, DateTime.MinValue);
        }

        private static DateTime ToDateTime(object obj, DateTime dDefault)
        {
            DateTime num2 = DateTime.MinValue;
            try
            {
                if ((obj == null) || (string.IsNullOrEmpty(ToString(obj))))
                {
                    return dDefault;
                }
                num2 = DateTime.Parse(obj.ToString().Trim());
            }
            catch
            {
            }
            return num2;
        }

        public static decimal ToDecimal(object obj)
        {
            return ToDecimal(obj, 0M);
        }

        public static decimal ToDecimal(object obj, decimal dDefault)
        {
            decimal num2 = 0;
            try
            {
                if ((obj == null) || (string.IsNullOrEmpty(ToString(obj))))
                {
                    return dDefault;
                }
                num2 = decimal.Parse(obj.ToString().Trim(), CultureInfo.CurrentCulture);
            }
            catch
            {
            }
            return num2;
        }

        public static double ToDouble(object obj)
        {
            return ToDouble(obj, 0.0);
        }

        public static double ToDouble(object obj, double dDefault)
        {
            double num2 = 0;
            try
            {
                if ((obj == null) || (string.IsNullOrEmpty(ToString(obj))))
                {
                    return dDefault;
                }
                num2 = double.Parse(obj.ToString().Trim(), CultureInfo.CurrentCulture);
            }
            catch
            {
            }
            return num2;
        }

        public static string ToString(object obj)
        {
            string sResult = string.Empty;
            if (obj != null)
            {
                sResult = obj.ToString();
                if (!string.IsNullOrEmpty(sResult))
                {
                    sResult = sResult.TrimStart();
                    sResult = sResult.TrimEnd();
                }
            }
            return sResult;
        }

        public static string EUrlEncode(string str)
        {
            var sb = new StringBuilder();
            //byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            byte[] byStr = Encoding.Default.GetBytes(str);
            foreach (byte t in byStr)
            {
                sb.Append(@"%" + Convert.ToString(t, 16));
            }

            return (sb.ToString());
        }

        public static Int32 ToInt32(string _iS)
        {
            return ToInt32(_iS, 0);
        }

        public static Int32 ToInt32(string _iS, Int32 iDefault)
        {
            int num2 = 0;
            try
            {
                if (_iS == null)
                {
                    return iDefault;
                }
                num2 = Int32.Parse(_iS.Trim(), CultureInfo.CurrentCulture);
            }
            catch
            {
            }
            return num2;
        }
    }
}