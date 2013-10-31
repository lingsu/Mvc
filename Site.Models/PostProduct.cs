using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Core.Models;

namespace Site.Models
{
    public class PostProduct
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        [Required]
        [StringLength(100)]
        [DisplayName("产品名称：")]
        public string ProName { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        [StringLength(20)]
        [DisplayName("产品型号：")]
        public string ProModel { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        [StringLength(100)]
        public string ProPic { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string ProDescription { get; set; }
        /// <summary>
        /// 产品静态页网址
        /// </summary>
        [StringLength(100)]
        public string ProUrl { get; set; }
        /// <summary>
        /// 产品类目
        /// </summary>
        public virtual Category Category { get; set; }
    }
}
