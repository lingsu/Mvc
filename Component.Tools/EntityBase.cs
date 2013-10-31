using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Component.Tools
{
    /// <summary>
    ///     可持久到数据库的领域模型的基类。
    /// </summary>
    [Serializable]
    public abstract class EntityBase
    {
        /// <summary>
        /// 数据实体基类
        /// </summary>
        protected EntityBase()
        {
            AddDate = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 添加时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime AddDate { get; set; }
    }
}
