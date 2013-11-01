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
    /// 实体类——产品信息
    /// </summary>
    [Description("产品信息")]
    public class Product:EntityBase
    {
        public Product()
        {
            SortOrder = 0;
        }
        /// <summary>
        /// 产品中文名称
        /// </summary>
        [StringLength(100)]
        public string ProName { get; set; }
        /// <summary>
        /// 产品英文名称
        /// </summary>
        [StringLength(100)]
        public string ProNameEn { get; set; }
        /// <summary>
        /// 产品中文型号
        /// </summary>
        [StringLength(20)]
        public string ProModel { get; set; }
        /// <summary>
        /// 产品英文型号
        /// </summary>
        [StringLength(20)]
        public string ProModelEn { get; set; }
        /// <summary>
        /// 产品大图
        /// </summary>
        [StringLength(100)]
        public string ProPic { get; set; }
        /// <summary>
        /// 产品小图
        /// </summary>
        [StringLength(100)]
        public string ProPic_small { get; set; }
        /// <summary>
        /// 产品中文详细描述
        /// </summary>
        public string ProDescription { get; set; }
        /// <summary>
        /// 产品英文详细描述
        /// </summary>
        public string ProDescriptionEn { get; set; }

        /// <summary>
        /// 产品中文简要描述
        /// </summary>
        [StringLength(300)]
        public string ProSummary { get; set; }
        /// <summary>
        /// 产品英文简要描述
        /// </summary>
        [StringLength(300)]
        public string ProSummaryEn { get; set; }

        /// <summary>
        /// 产品静态页网址
        /// </summary>
        [StringLength(100)]
        public string ProUrl { get; set; }
        /// <summary>
        /// 产品排序值越大越靠前
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// 是否是静态页
        /// </summary>
        public bool IsStatic { get; set; }
        /// <summary>
        /// 是否是推荐的
        /// </summary>
        public bool IsPromoted { get; set; }
        /// <summary>
        /// 是否是新产品
        /// </summary>
        public bool IsNew { get; set; }
        /// <summary>
        /// 中文关键词
        /// </summary>
        [StringLength(100)]
        public string Tags { get; set; }
        /// <summary>
        /// 英文关键词
        /// </summary>
        [StringLength(100)]
        public string TagsEn { get; set; }
        /// <summary>
        ///  分类id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 产品类目
        /// </summary>
        public virtual Category Category { get; set; }

    }
}
