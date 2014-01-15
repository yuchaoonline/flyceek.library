using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindProperty.Lib.Common
{
    public class LinkHelper
    {
        public static string HouseList(string posType)
        {
            string url = string.Empty;
            switch (posType.ToUpper().Trim())
            {
                case "S":
                    url = ConfigInfo.HouseListSale;
                    break;
                case "R":
                    url = ConfigInfo.HouseListRent;
                    break;
            }
            return ToAbsoluteUrl(url);
        }
        public static string SaleHouseList()
        {
            string url = string.Empty;
            url = ConfigInfo.HouseListSale;
            return ToAbsoluteUrl(url);
        }
        public static string RentHouseList()
        {
            string url = string.Empty;
            url = ConfigInfo.HouseListRent;
            return ToAbsoluteUrl(url);
        }

        public static string HouseView()
        {
            string url = ConfigInfo.HouseInfoView;

            return ToAbsoluteUrl(url);
        }
        public static string AgentInfo()
        {
            string url = ConfigInfo.AgentInfo;

            return ToAbsoluteUrl(url);
        }
        public static string AgentList()
        {
            string url = ConfigInfo.AgentList;

            return ToAbsoluteUrl(url);
        }

        public static string AgentNetrust()
        {
            string url = ConfigInfo.AgentNetrust;

            return ToAbsoluteUrl(url);
        }

        public static string MainUrl()
        {
            return ConfigInfo.MainUrl;
        }

        public static string CurrentMainUrl()
        {
            return ConfigInfo.CurrentMainUrl;
        }

        private static string ToAbsoluteUrl(string url)
        {
            return "~/" + url;
        }
    }
}