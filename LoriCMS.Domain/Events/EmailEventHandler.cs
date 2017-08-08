using LoriCMS.ServiceBus;
using System;

namespace LoriCMS.Domain.Events
{
    public class EmailEventHandler :
        IBusHandler<ChangeWorkStatusEvent>
    {
        public void Handle(ChangeWorkStatusEvent evt)
        {
            string message = string.Format("Email->{0}这个人把工作单{1}修改了状态{2}",
                evt.UserId, evt.Id, evt.WorkStatus);
            Console.WriteLine(message);

        }
    }
}