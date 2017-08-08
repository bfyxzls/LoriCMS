using LoriCMS.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoriCMS.Domain.Events
{
    public class ValidateWorkEventHandler : IBusHandler<ValidateWorkEvent>
    {
        public void Handle(ValidateWorkEvent evt)
        {
            if (evt.Title == "")
                throw new ArgumentNullException("工作单标题为空！");
            if(evt.Content=="")
                throw new ArgumentNullException("工作单内容为空！");
        }
    }
}
