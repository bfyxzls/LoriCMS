using LoriCMS.Domain.AggregatesModel;
using LoriCMS.Domain.Repositories;
using LoriCMS.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LindAgile.Web.DDD.Infrastructure
{
    /// <summary>
    /// 工作单仓储
    /// </summary>
    public class WorkRepository : EFRepository<Work_Item>, IWorkRepository
    {
        private readonly IRepository<Work_Item> _workRepository = new LindRepository<Work_Item>();

        public void CreateWork(Work_Item entity)
        {
            _workRepository.Insert(entity);
        }

    }
}