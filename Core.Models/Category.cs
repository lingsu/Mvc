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
    /// 实体类——产品类目信息
    /// </summary>
    [Description("产品类目信息")]
    public class Category:EntityBase
    {
        public Category()
        {
            Pid = 0;
        }
        /// <summary>
        /// 类目名称
        /// </summary>
        [StringLength(80)]
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
        /// 产品集合
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}
