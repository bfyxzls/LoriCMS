 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoriCMS.Domain.Events
{
    /// <summary>
    /// 订单状态变更－事件源
    /// </summary>
    public class ChangeWorkStatusEvent : ServiceBus.BusData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WorkStatus { get; set; }
     }
}