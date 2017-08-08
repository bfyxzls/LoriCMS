using LindAgile.Web;
using LindAgile.Web.DDD.Infrastructure;
using LoriCMS.Domain.AggregatesModel;
using LoriCMS.Domain.Repositories;
using LoriCMS.IRepositories;
using LoriCMS.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoriCMS.Application.Commands
{
    /// <summary>
    /// 建立订单处理程序
    /// </summary>
    public class WorkCommandHandler :
        IBusHandler<CreateWorkCommand>,
        IBusHandler<EditWorkCommand>,
        IBusHandler<AuditWorkCommand>,
        IBusHandler<DelWorkCommand>
    {

        IWorkRepository _workRepository;

        public WorkCommandHandler()
        {
            _workRepository = new WorkRepository();

        }

        public void Handle(CreateWorkCommand evt)
        {
            var work = evt.Work_ItemDTO.MapTo<Work_Item>();
            work.ValidateByCreator();
            _workRepository.CreateWork(work);
        }

        public void Handle(EditWorkCommand evt)
        {
            throw new NotImplementedException();
        }

        public void Handle(AuditWorkCommand evt)
        {
            throw new NotImplementedException();
        }

        public void Handle(DelWorkCommand evt)
        {
            throw new NotImplementedException();
        }
    }
}