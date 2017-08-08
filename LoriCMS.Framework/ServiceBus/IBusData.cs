using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoriCMS.ServiceBus
{
    /// <summary>
    /// 总线对象[实体接口]
    /// </summary>
    public interface IBusData
    {
        /// <summary>
        /// 领域事件实体的聚合根，生命周期在会话结束后消失
        /// </summary>
        Guid AggregateRoot { get; }
        /// <summary>
        /// 事件发生时间
        /// </summary>
        DateTime EventTime { get; }
        /// <summary>
        /// 事件模型
        /// </summary>
        object EventModel { get; set; }
    }
}
