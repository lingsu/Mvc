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
    /// 实体类——登录记录信息
    /// </summary>
    [Description("登录记录信息")]
    public class LoginLog:EntityBase
    {
        /// <summary>
        /// Ip地址
        /// </summary>
        [StringLength(15)]
        public string IpAddress { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public virtual Member Member { get; set; }
    }
}
