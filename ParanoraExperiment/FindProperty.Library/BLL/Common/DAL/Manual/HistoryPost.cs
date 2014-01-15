using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FindProperty.Lib.Common;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class HistoryPost:IDAL.IHistoryPost
    {
        private string _cookieName = ConfigInfo.HistoryPostCookieName;

        public List<ViewModel.HistoryPost> SelectHistoryPost()
        {
            List<ViewModel.HistoryPost> list = new List<ViewModel.HistoryPost>();

            HttpCookie hisCookies = HttpContext.Current.Request.Cookies[_cookieName];
            
            if (hisCookies != null)
            {
                string[] hisPostStrAry = hisCookies.Value.Split(',');
                foreach (string hisPost in hisPostStrAry)
                {
                    string[] strAry = hisPost.Split('|');
                    if (strAry.Length > 0 && strAry.Length > 7)
                    {
                        list.Add(new ViewModel.HistoryPost(strAry));
                    }
                }
            }
            return list;
        }

        public List<ViewModel.HistoryPost> SaveHistoryPost(List<ViewModel.HistoryPost> hisPosts)
        {
            if (HttpContext.Current.Response.Cookies[_cookieName] != null)
            {
                HttpContext.Current.Response.Cookies.Remove(_cookieName);
            }

            string value = string.Join(",", hisPosts.Take(5).Select(x => x.ToString()).ToArray());
            HttpCookie hisCookies = new HttpCookie(_cookieName);
            hisCookies.Value = value;
            hisCookies.Expires = DateTime.Now.AddDays(30);
            HttpContext.Current.Response.Cookies.Add(hisCookies);
            return hisPosts;
        }


        public List<ViewModel.HistoryPost> AddHistoryPost(List<ViewModel.HistoryPost> hisPosts, ViewModel.HistoryPost hisPost)
        {
            if (hisPosts.Count >= 5 && hisPosts.Where(x => x.RefNo == hisPost.RefNo).Count() < 1)
            {
                hisPosts = hisPosts.Skip((hisPosts.Count-5)+1).ToList();
            }
            hisPosts.Add(hisPost);
            SaveHistoryPost(hisPosts);
            return hisPosts;
        }
    }
}
