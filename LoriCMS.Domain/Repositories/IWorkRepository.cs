using LoriCMS.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoriCMS.Domain.Repositories
{
    public interface IWorkRepository
    {
        void CreateWork(Work_Item entity);
    }
}