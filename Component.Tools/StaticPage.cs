using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace Component.Tools
{
    public class StaticPage
    {
        /// <summary>
        /// 读取静态页模板
        /// </summary>
        /// <param name="strTmplPath">模板路径</param>
        /// <returns>模板内容</returns>
        public string Re_html(string strTmplPath)
        {
            string str = string.Empty;
            Encoding code = Encoding.UTF8;

            try
            {
                StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(strTmplPath),code);
                str = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return str;
        }

        public void Splitpage(string html, int pagesize, string strFileName, int CategoryId = 1)
        {

        }
        public void SimplePage(string html)
        {

        }
    }
}
