using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LoriCMS.Domain.AggregatesModel
{
    /// <summary>
    /// 工作单－项目
    /// </summary>
    public class Work_Project : Entity
    {
        [DisplayName("项目名称")]
        public string Name { get; set; }
        [DisplayName("项目状态")]
        public ProjectStatus ProjectStatus { get; set; }
        [DisplayName("开始时间")]
        public DateTime StartTime { get; set; }
        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }
        [DisplayName("项目负责人")]
        public ProjectLeader ProjectLeader { get; set; }
    }
    /// <summary>
    /// 项目负责人
    /// </summary>
    public class ProjectLeader : ValueObject
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return UserId;
            yield return UserName;
        }
    }

    /// <summary>
    /// 项目状态
    /// </summary>
    public enum ProjectStatus
    {
        /// <summary>
        /// 未开始
        /// </summary>
        NotStart,
        /// <summary>
        /// 进行中
        /// </summary>
        Starting,
        /// <summary>
        /// 已完成
        /// </summary>
        End,
    }
}