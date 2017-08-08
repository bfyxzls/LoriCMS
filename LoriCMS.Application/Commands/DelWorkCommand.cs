using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoriCMS.Application.Commands
{
    public class DelWorkCommand : ServiceBus.BusData
    {
        public int Id { get; set; }
    }
}
