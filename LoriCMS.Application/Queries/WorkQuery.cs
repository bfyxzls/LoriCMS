using LoriCMS.Application.DTO;
using LoriCMS.Domain.AggregatesModel;
using LoriCMS.Domain.Repositories;
using LoriCMS.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoriCMS.Application.Queries
{
    /// <summary>
    /// 工作查询
    /// </summary>
    public class WorkQuery
    {
        IWorkRepository _workRepository;
        public WorkQuery(IWorkRepository workRepository)
        {
            _workRepository = workRepository;
        }
        public IEnumerable<Work_ItemDTO> GetWorkItem(ISpecification<Work_Item> spec)
        {
            return _workRepository.GetModel().Where(spec.SatisfiedBy()).Select(i => new Work_ItemDTO
            {
                Content = i.Content.Detail,
                Title = i.Content.Title,
            });
        }
    }
}
