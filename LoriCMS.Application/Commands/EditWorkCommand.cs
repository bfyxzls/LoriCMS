using LoriCMS.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoriCMS.Application.Commands
{
    public class EditWorkCommand : ServiceBus.BusData
    {
        public Work_ItemDTO Work_ItemDTO { get; set; }
    }
}
