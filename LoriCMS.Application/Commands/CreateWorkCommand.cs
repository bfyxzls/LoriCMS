using LoriCMS.Application.DTO;
using LoriCMS.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoriCMS.Application.Commands
{
    /// <summary>
    /// 建立命令
    /// </summary>
    public class CreateWorkCommand : BusData
    {
        public Work_ItemDTO Work_ItemDTO { get; set; }

    }
}