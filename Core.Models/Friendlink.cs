using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Component.Tools;

namespace Core.Models
{
    /// <summary>
    /// 友情链接
    /// </summary>
    public class Friendlink:EntityBase
    {
        /// <summary>
        /// 中文标题
        /// </summary>
        [StringLength(50)]
        public string Title { get; set; }
        /// <summary>
        /// 英文标题
        /// </summary>
        [StringLength(50)]
        public string TitleEn { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        [StringLength(80)]
        public string Link { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        [StringLength(80)]
        public string Url { get; set; }
        /// <summary>
        /// 类目排序值越大越靠前
        /// </summary>
        public int StorOrder { get; set; }
    }
}
