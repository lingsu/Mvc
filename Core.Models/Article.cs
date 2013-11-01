using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Component.Tools;

namespace Core.Models
{
    public class Article:EntityBase
    {
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(100)]
        public string TitleEn { get; set; }
        /// <summary>
        /// 类目排序值越大越靠前
        /// </summary>
        public int StorOrder { get; set; }
        /// <summary>
        /// 简要描述
        /// </summary>
        [StringLength(100)]
        public string Summary { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        [StringLength(70)]
        public string ImageUrl { get; set; }
        [DataType(DataType.Html)]
        /// <summary>
        /// 中文详细描述
        /// </summary>
        public string Description { get; set; }
        [DataType(DataType.Html)]
        /// <summary>
        /// 英文详细描述
        /// </summary>
        public string DescriptionEn { get; set; }
    }
}
