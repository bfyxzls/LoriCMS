using LoriCMS.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoriCMS.Domain.Events
{
    public class ChangeWorkEventHandler :
        IBusHandler<ChangeWorkStatusEvent>
    {
        public void Handle(ChangeWorkStatusEvent evt)
        {
            string message = string.Format("DB->{0}这个人把工作单{1}修改了状态{2}", 
                evt.UserId, evt.Id,evt.WorkStatus);
            Console.WriteLine(message);
         }
    }
}