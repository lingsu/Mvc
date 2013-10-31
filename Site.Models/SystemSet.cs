using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Site.Models
{
    public class SystemSet
    {
        /// <summary>
        /// 页面标题中文
        /// </summary>
        public string pageTitleCN { get; set; }
        /// <summary>
        /// 页面标题英文
        /// </summary>
        public string pageTitleEN { get; set; }
        /// <summary>
        /// 页面关键词中文
        /// </summary>
        public string pageKeyWordCn { get; set; }
        /// <summary>
        /// 页面关键词英文
        /// </summary>
        public string pageKeyWordEn { get; set; }
        /// <summary>
        /// 页面描述中文
        /// </summary>
        public string page_miaoshuCn { get; set; }
        /// <summary>
        /// 页面描述英文
        /// </summary>
        public string page_miaoshuEn { get; set; }
    }
}
