using AutoMapper;
using LoriCMS.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoriCMS.Application.DTO
{
    /// <summary>
    /// 自定义解析器Title
    /// </summary>
    public class Work_ItemTitleResolver : ValueResolver<Work_Item, string>
    {

        protected override string ResolveCore(Work_Item source)
        {
            return source.Content.Title;
        }
    }
    /// <summary>
    /// 自定义解析器Content
    /// </summary>
    public class Work_ItemContentResolver : ValueResolver<Work_Item, string>
    {

        protected override string ResolveCore(Work_Item source)
        {
            return source.Content.Detail;
        }
    }

}
