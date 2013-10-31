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
    /// 实体类——用户信息
    /// </summary>
    [Description("用户信息")]
    public class Member:EntityBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(20)]
        public string NickName { get; set; }
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        /// <summary>
        /// 会员头像链接
        /// </summary>
        [StringLength(100)]
        public string Portraits_url { get; set; }
        /// <summary>
        /// 用户类型 管理员 1|用户 2|商家 3
        /// </summary>
        [StringLength(3)]
        public string Account_type { get; set; }

        /// <summary>
        /// 简要说明
        /// </summary>
        [StringLength(100)]
        public string Summary { get; set; }
        /// <summary>
        ///  邮件验证截至日期
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime EndCheck { get; set; }

        /// <summary>
        /// 邮件验证码
        /// </summary>
        [StringLength(100)]
        public string CodePassword { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [StringLength(20)]
        public string Tel { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(20)]
        public string Phone { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 登入日志
        /// </summary>
        public virtual ICollection<LoginLog> LoginLogs { get; set; }
    }
}
