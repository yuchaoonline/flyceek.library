using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common
{
    public class StringFormat
    {
        public static string Price(decimal? price)
        {
            string result=string.Empty;
            if(price.HasValue)
            {
                if(price.Value >=10000*10000)
                {
                    result=string.Format("{0:#.##}",price.Value/(Decimal)(10000*10000));
                }
                else if (price.Value >= 10000)
                {
                    result=string.Format("{0:#.##}",price.Value/(Decimal)10000);
                }
                else
                {
                    result = string.Format("{0:#.##}", price.Value);
                }
            }
            return result;
        }

        public static string Price(int? price)
        {
            string result = string.Empty;
            if (price.HasValue)
            {
                if (price.Value >= 10000 * 10000)
                {
                    result = string.Format("{0:#.##}", price.Value / (Decimal)(10000 * 10000));
                }
                else if (price.Value >= 10000)
                {
                    result = string.Format("{0:#.##}", price.Value / (Decimal)10000);
                }
                else
                {
                    result = string.Format("{0:#.##}", price.Value);
                }
            }
            return result;
        }

        public static string Price(decimal? price,decimal? rental,string posType)
        {
            string result=string.Empty;
            switch(posType.ToUpper().Trim())
            {
                case "S":
                    if(price.HasValue)
                    {
                        if(price >=10000*10000)
                        {
                            result = String.Format("{0:#.##}", price.Value / (Decimal)(10000 * 10000));
                        }
                        else if(price >=10000)
                        {
                            result = String.Format("{0:#.##}", price.Value / (Decimal)10000);
                        }
                        else
                        {
                            result = String.Format("{0:#.##}", price.Value);
                        }
                    }
                    break;
                case "R":
                    if(rental.HasValue)
                    {
                        result=String.Format("{0:#.##}",rental);
                    }
                    break;
            }
            return result;
        }

        public static string PriceUnit(decimal price)
        {
            string result=string.Empty;
            if(price >=10000*10000)
            {
                result="亿";
            }
            else if(price >=10000)
            {
                result="万";
            }
            else
            {
                result = "元";
            }
            return result;
        }

        public static string PriceUnit(string posType)
        {
            string result=string.Empty;
            switch(posType.ToUpper().Trim())
            {
                case "S":
                    result="万";
                    break;
                case "R":
                    result = "元";
                    break;
            }
            return result;
        }

        public static string PriceUnit(Decimal? price,Decimal? rental,string posType)
        {
            string result=string.Empty;
            switch(posType.ToUpper().Trim())
            {
                case "S":
                    if(price.HasValue)
                    {
                        if(price.Value >=10000*10000)
                        {
                            result="亿";
                        }
                        else if(price.Value >=10000)
                        {
                            result="万";
                        }
                        else
                        {
                            result="元";
                        }
                    }
                    break;
                case "R":
                    result = "元";
                    break;
            }
            return result;
        }

        public static string Price(int price)
        {
            return String.Format("{0:#,###}", price);
        }


        public static string Price(Decimal price)
        {
            return String.Format("{0:#,###}", price); 
        }

        public static string Size(Decimal price)
        {
            return string.Format("{0:#.##}", price);
        }

        public static string DateTime(DateTime? date)
        {
            if(date.HasValue)
            {
                return date.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                return " -- ";
            }
        }

        public static string HTMLInnterText(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(ldquo);", "“", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(rdquo);", "”", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(mdash);", "—", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(middot);", "·", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = System.Web.HttpUtility.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }

        public static int HouseAge(DateTime? DateTimeStr)
        {
            int intTime = -1;
            if (DateTimeStr.HasValue)
            {
                DateTime datetime1 = DateTimeStr.Value;
                DateTime datetime2 = System.DateTime.Now;
                TimeSpan timespan = datetime2 - datetime1;
                intTime = Convert.ToInt32(timespan.Days / 365);
            }
            return intTime;
        }

        public static string HouseAgeStr(DateTime? DateTimeStr)
        {
            int intTime = HouseAge(DateTimeStr);
            if (intTime > -1)
            {
                if (intTime > 0)
                    return string.Format("{0}年", intTime.ToString());
                else
                {
                    return string.Format("一年内");
                }
            }
            else
            {
                return string.Format("--");
            }
        }

        public static string NullStrShow(string str)
        {
            return string.IsNullOrEmpty(str) ? "---" : str;
        }

        public static string EagleMemberTypeString(string type)
        {
            string str = string.Empty;
            //GE 金鹰 SE银鹰  GL 金狮  SL 银狮
            switch (type.ToUpper())
            {
                case "GE":
                    str="金鹰";
                    break;
                case "SE":
                    str="银鹰";
                     break; 
                case "GL":
                    str="金狮";
                    break;
                case "SL":
                    str = "银狮";
                    break;
            }
           
            return str;
        }

        public static string MinMaxPriceDisplay(decimal? min, decimal? max,bool isSale)
        {
            if (isSale)
            {
                min = min / 10000;
                max = max / 10000;
            }

            return MinMaxDecimalDisplay(min, max, "以下", "以上", (isSale?"万":"元"), false, "", "");
        }

        public static string MinMaxSizeDsplay(decimal? min, decimal? max)
        {
            return MinMaxDecimalDisplay(min, max, "以下", "以上", "㎡", false, "", "");
        }

        public static string MinMaxBedRoomDsplay(decimal? min, decimal? max,string transStr)
        {
            //return MinMaxDecimalDisplay(min, max, "以下", "及以上", "", false, transStr, transStr);
            return MinMaxDecimalDisplay(min, max, "以下", "及以上", "室", false, transStr, transStr);
        }

        public static string MinMaxDecimalDisplay(decimal? min, decimal? max, string minStr, string maxStr, string showUnit,bool isPerShow,
            string minTransStr,string maxTransStr)
        {
            if (string.IsNullOrEmpty(minTransStr))
            {
                minTransStr = min.ToString();
            }
            if (string.IsNullOrEmpty(maxTransStr))
            {
                maxTransStr = max.ToString();
            }

            string str = string.Empty;
            if (min.HasValue && max.HasValue)
            {
                if (min.Value == max.Value)
                {
                    str = minTransStr + showUnit;
                }
                else
                {
                    if (min.HasValue && max.HasValue && min.Value > 0 && max.Value > 0)
                    {
                        str = minTransStr + "-" + maxTransStr + showUnit;
                    }
                    else
                    {
                        if ((!min.HasValue || min.Value == 0) && (max.HasValue && max.Value > 0))
                        {
                            str = (isPerShow ? minStr + maxTransStr + showUnit : maxTransStr + showUnit + minStr);
                        }
                        if ((!max.HasValue || max.Value == 0) && (min.HasValue && min.Value > 0))
                        {
                            str = (isPerShow ? maxStr + minTransStr + showUnit : minTransStr + showUnit + maxStr);
                        }
                    }
                }
            }

            return str;
        }
    }
}
