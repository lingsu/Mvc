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
    /// 新闻实体模型
    /// </summary>
    public class News : EntityBase
    {
        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(100)]
        public string Title { get; set; }
        /// <summary>
        /// 类目排序值越大越靠前
        /// </summary>
        public int StorOrder { get; set; }
        /// <summary>
        /// 父id值
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 是否是静态的
        /// </summary>
        public bool IsStatic { get; set; }
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
        /// <summary>
        /// 下载链接
        /// </summary>
        [StringLength(70)]
        public string DownloadUrl { get; set; }
        /// <summary>
        /// 语言 CN|EN
        /// </summary>
        [StringLength(6)]
        public string Language { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int NewsCategoryId { get; set; }
        /// <summary>
        /// 分类实体
        /// </summary>
        public virtual NewsCategory NewsCategory{get;set;}
    }
}
