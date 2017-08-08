using AutoMapper;
using LoriCMS.Application.DTO;
using LoriCMS.Domain.AggregatesModel;
using LoriCMS.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LoriCMS.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //注册所有事件
            BusManager.Instance.SubscribeAll();

            //注册映射
            Mapper.CreateMap<Work_Item, Work_ItemDTO>().ForMember(dest => dest.Title, opt =>
            {
                opt.ResolveUsing<Work_ItemTitleResolver>().ConstructedBy(() => new Work_ItemTitleResolver());
            });
            Mapper.CreateMap<Work_Item, Work_ItemDTO>().ForMember(dest => dest.Content, opt =>
            {
                opt.ResolveUsing<Work_ItemContentResolver>().ConstructedBy(() => new Work_ItemContentResolver());
            });
        }
    }
}
