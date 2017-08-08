using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoriCMS.Domain
{
    /// <summary>
    /// 实体类标示接口（数据实现）
    /// </summary>
    public interface IEntity<TKey> : IEquatable<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        TKey Id { get; set; }
    }
}
