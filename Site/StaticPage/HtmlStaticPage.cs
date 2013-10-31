using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

using Core.Models;
using Site.Models;
using Component.Tools;



namespace Site.StaticPage
{
    public static class HtmlStaticPage
    {
        /// <summary>
        /// 读取静态页模板
        /// </summary>
        /// <param name="strTmplPath">模板路径</param>
        /// <returns>模板内容</returns>
        public static string Re_html(string strTmplPath)
        {
            string str = string.Empty;
            Encoding code = Encoding.UTF8;

            try
            {
                StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(strTmplPath), code);
                str = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return str;
        }

        public static List<Product> Splitpage(this IQueryable<Category> source, string TemplatePath, int pagesize, string savePath)
        {
            List<Category> Categorys = source.OrderByDescending(c => c.StorOrder).ToList();
            int itemId = 1;
            List<Product> pps = new List<Product>();
            string historyPath = HttpContext.Current.Server.MapPath(@"/sunglasses/");
            string pathbase = DateTime.Now.ToString("yyyy-MM-dd") + "/";
            if (Directory.Exists(historyPath))
            {
                Directory.Delete(historyPath,true);
            }
            if (!Directory.Exists(historyPath))
            {
                Directory.CreateDirectory(historyPath + pathbase);
            }
            foreach (var item in Categorys)
            {
                var products = item.Products.OrderByDescending(p => p.AddDate).ToList();
                TotalItemCount = products.Count();
                string CategoryFlieNameBase = "products" + itemId;
                itemId++;
                if (TotalItemCount==0)
                {
                    //类别生成
                    string prs = "<div style='text-align:center;margin-top:30px;'>资料添加中...</div>";
                    StringBuilder categorylist = new StringBuilder();
                    categorylist.Append("<ul class='subnav'>");
                    int CategoryId = 1;
                    foreach (var cate in Categorys)
                    {
                        if (cate.Id == item.Id)
                        {
                            categorylist.Append(string.Format("<li><a href='{0}.html' class='ckd'>{1}</a></li>", "products" + CategoryId, cate.Title));
                        }
                        else
                        {
                            categorylist.Append(string.Format("<li><a href='{0}.html' >{1}</a></li>", "products" + CategoryId, cate.Title));
                        }
                        CategoryId++;
                    }
                    categorylist.Append("</ul>");

                    Encoding code = Encoding.UTF8;
                    string str = Re_html(TemplatePath);
                    str = str.Replace("$fenyebtn", "");//替换列表标签
                    str = str.Replace("$productslist", prs);
                    str = str.Replace("$Category", categorylist.ToString());

                    string CreateFileName = CategoryFlieNameBase + ".html";

                    StreamWriter sw = null;
                    try
                    {
                        sw = new StreamWriter(HttpContext.Current.Server.MapPath(savePath) + CreateFileName, false, code);
                        sw.Write(str);
                        sw.Flush();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    finally
                    {
                        sw.Close();
                    }

                }else if (TotalItemCount > 0)
                {
                    PageCount = (int)Math.Ceiling(TotalItemCount / (double)pagesize);

                    for (PageIndex = 0; PageIndex < PageCount; PageIndex++)
                    {
                        StringBuilder sb = new StringBuilder();//新闻列表
                        StringBuilder prs = new StringBuilder();

                        for (int j = PageIndex * pagesize; j < PageNumber * pagesize; j++)
                        {
                            if (j < TotalItemCount)
                            {
                                string ProUrl = products[j].ProUrl = products[j].SimplePage(Categorys, @"/Template/eyewear.html", @"/sunglasses/" + pathbase);
                                string ProPic = products[j].ProPic;
                                string ProName = products[j].ProName;
                                
                                pps.Add(products[j]);
                                prs.Append(string.Format("<div class='blueidea'><a href='{0}' ><img src='/pic/s_{1}' width='200' height='99' alt='{2}' /><span>{2}</span></a></div>", ProUrl, ProPic, ProName));
                            }
                        }

                        //生成分页
                        if (IsFirstPage)
                        {
                            sb.Append("<a href='javascript:void(0)' class='jp-previous jp-disabled' Enabled='false'>First</a>");
                            sb.Append("<a href='javascript:void(0)'class='jp-previous jp-disabled' Enabled='false'>Previous</a>");
                        }
                        else
                        {
                            string FirstPath = CategoryFlieNameBase + ".html";
                            string PreviousPath = CategoryFlieNameBase + "_" + PageIndex + ".html";
                            if (PageIndex==1)
                            {
                                PreviousPath = CategoryFlieNameBase + ".html"; ;
                            }
                            sb.Append("<a href=" + FirstPath + ">First</a>");
                            sb.Append("<a href=" + PreviousPath + ">Previous</a>");
                        }

                        //分页按钮
                        if (PageIndex<10)
                        {
                            for (int h = 1; h < 10; h++)//每页显示9个分页数字
                            {
                                if (h == PageNumber)
                                {
                                    sb.Append("<a href='javascript:void(0)' class='jp-current' Enabled='false'>" + h + "</a>");
                                }
                                else if (h <= PageCount)
                                {
                                    if (h==1)
                                    {
                                        sb.Append(string.Format("<a href='{0}.html'>{1}</a>", CategoryFlieNameBase, h));
                                    }
                                    else
                                    {
                                        sb.Append(string.Format("<a href='{0}_{1}.html'>{1}</a>", CategoryFlieNameBase, h));
                                    }                                    
                                }
                            }
                        }
                        else
                        {
                            int maxfenye = PageIndex + 5;
                            int minfenye = PageIndex - 5;
                            for (int h = minfenye; h < maxfenye; h++)
                            {
                                if (h == PageNumber)
                                {
                                    sb.Append(string.Format("<a href='{0}_{1}.html' class='jp-current' Enabled='false'>{1}</a>", CategoryFlieNameBase, h));
                                }
                                else if(h<=maxfenye)
                                {
                                    sb.Append(string.Format("<a href='{0}_{1}.html' >{1}</a>", CategoryFlieNameBase, h));
                                }
                            }
                        }
                        if (PageIndex==(PageCount-1) || PageCount==1)//判断如果为最后一页，则不把“下一页”设为超链接
                        {
                            sb.Append("<a href='javascript:void(0)' class='jp-previous jp-disabled' Enabled='false'>Next</a>");
                            sb.Append("<a href='javascript:void(0)' class='jp-previous jp-disabled' Enabled='false'>Last</a>");
                        }
                        else
                        {
                            int NextNumber = PageIndex + 2;

                            sb.Append(string.Format("<a href='{0}_{1}.html' >Next</a>", CategoryFlieNameBase, NextNumber));
                            sb.Append(string.Format("<a href='{0}_{1}.html'>Last</a>", CategoryFlieNameBase, PageCount));
                        }

                        //类别生成
                        StringBuilder categorylist = new StringBuilder();
                        categorylist.Append("<ul class='subnav'>");
                        int CategoryId = 1;
                        foreach (var cate in Categorys)
                        {
                            if (cate.Id==item.Id)
                            {
                                categorylist.Append(string.Format("<li><a href='{0}.html' class='ckd'>{1}</a></li>", "products" + CategoryId, cate.Title));
                            }
                            else
                            {
                                categorylist.Append(string.Format("<li><a href='{0}.html' >{1}</a></li>", "products" + CategoryId, cate.Title));
                            }
                            CategoryId++;
                        }
                        categorylist.Append("</ul>");

                        Encoding code = Encoding.UTF8;
                        string str = Re_html(TemplatePath);
                        str = str.Replace("$fenyebtn", sb.ToString());//替换列表标签
                        str = str.Replace("$productslist", prs.ToString());
                        str = str.Replace("$Category", categorylist.ToString());

                        int pagenumber = PageIndex + 1;
                        string CreateFileName = string.Empty;
                        if (pagenumber==1)
                        {
                            CreateFileName = CategoryFlieNameBase + ".html";
                        }
                        else
                        {
                            CreateFileName = CategoryFlieNameBase + "_" + pagenumber + ".html";
                        }

                        StreamWriter sw = null;
                        try
                        {
                            sw = new StreamWriter(HttpContext.Current.Server.MapPath(savePath) + CreateFileName, false, code);
                            sw.Write(str);
                            sw.Flush();
                        }
                        catch (Exception ex)
                        {
                            
                            throw ex;
                        }
                        finally
                        {
                            sw.Close();
                        }
                    }
                }
            }
            return pps;
        }
        public static int PageCount { get; set; }
        public static int TotalItemCount { get; set; }
        public static int PageIndex { get; set; }
        public static int PageNumber
        {
            get { return PageIndex + 1; }
        }
        public static int PageSize { get; set; }
        public static bool HasPreviousPage
        {
            get { return PageIndex > 0; }
        }
        public static bool HasNextPage
        {
            get { return PageIndex < (PageCount - 1); }
        }
        public static bool IsFirstPage
        {
            get { return PageIndex <= 0; }
        }
        public static bool IsLastPage
        {
            get { return PageIndex >= (PageCount - 1); }
        }
        /// <summary>
        ///  生成产品单页静态页面
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Categorys"></param>
        /// <param name="TemplatePath">模板路径</param>
        /// <param name="savePath">保存路径</param>
        /// <returns>文件名</returns>
        public static string SimplePage(this Product model,List<Category> Categorys,string TemplatePath,string savePath)
        {
            string html = Re_html(TemplatePath);

            //替换产品属性
            html = html.Replace("$Title", model.ProName);
            html = html.Replace("$Description", model.ProDescription);
            html = html.Replace("$LPic", model.ProPic);
            html = html.Replace("$Model", model.ProModel);

            //生成产品类目
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class='subnav'>");

            int itemId = 1;
            foreach (var item in Categorys)
            {
                if (item == model.Category)
                {
                    string href = "products" + itemId;
                    sb.Append(string.Format("<li><a href='http://www.honkanoptics.com/{0}.html' class='ckd'>{1}</a></li>", href, item.Title));
                }
                else
                {
                    string href = "products" + itemId;
                    sb.Append(string.Format("<li><a href='http://www.honkanoptics.com/{0}.html' >{1}</a></li>", href, item.Title));
                }
                itemId++;
            }
            sb.Append("</ul>");
            html = html.Replace("$Category", sb.ToString());

            Encoding code = Encoding.UTF8;
            StreamWriter sw = null;
            string FileName =  model.Id +".html";
            try
            {
                string pathbase = HttpContext.Current.Server.MapPath(savePath);
                if (!Directory.Exists(pathbase))
                {
                    Directory.CreateDirectory(pathbase);
                }
                sw = new StreamWriter(pathbase + FileName, false, code);
                sw.Write(html);
                sw.Flush();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                sw.Close();
            }
            return savePath + FileName;
        }
    }
}
