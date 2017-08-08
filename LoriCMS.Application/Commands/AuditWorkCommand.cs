using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoriCMS.Application.Commands
{
    public class AuditWorkCommand : ServiceBus.BusData
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }
}
