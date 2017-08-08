using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoriCMS.Domain.Events
{
    /// <summary>
    /// 验证工作单事件
    /// </summary>
    public class ValidateWorkEvent : ServiceBus.BusData
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
