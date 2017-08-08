using LoriCMS.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoriCMS.Domain.Repositories
{
    public interface IWorkRepository : IRepositories.IRepository<Work_Item>
    {
        /// <summary>
        /// 建立工作单
        /// </summary>
        /// <param name="entity"></param>
        void CreateWork(Work_Item entity);

    }
}