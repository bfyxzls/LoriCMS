using LoriCMS.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LoriCMS.Domain.AggregatesModel
{
    /// <summary>
    /// 工作单-聚合
    /// </summary>
    public class Work_Item : Entity, IAggregateRoot
    {
        [DisplayName("内容-值对象")]
        public Content Content { get; set; }
        [DisplayName("开始时间")]
        public DateTime StartTime { get; set; }
        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }
        [DisplayName("进度")]
        public int Schedule { get; set; }
        [DisplayName("用户ID")]
        public int UserId { get; set; }
        [DisplayName("工作单状态")]
        public int WorkStatus { get; set; }
        [DisplayName("项目－实体")]
        public Work_Project Work_Project { get; set; }

        public void ValidateByCreator()
        {
            ServiceBus.BusManager.Instance.Publish(new ValidateWorkEvent
            {
                Title = Content.Title,
                Content = Content.Detail,
            });
        }
    }
    public class Content
    {
        public string Title { get; set; }
        public string Detail { get; set; }
    }
}