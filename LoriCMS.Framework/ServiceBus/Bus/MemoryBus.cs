using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LoriCMS.ServiceBus
{
    internal class MemoryBus : IBus
    {
        /// <summary>
        /// 模式锁
        /// </summary>
        private static object _objLock = new object();
        /// <summary>
        /// 对于事件数据的存储，目前采用内存字典
        /// </summary>
        private static Dictionary<Type, List<object>> _eventHandlers = new Dictionary<Type, List<object>>();
        /// <summary>
        /// 比较两个委托Handler是否相同，以免添加重复事件
        /// </summary>
        private readonly Func<object, object, bool> _eventHandlerEquals = (o1, o2) =>
        {
            var o1Type = o1.GetType();
            var o2Type = o2.GetType();
          
            return o1Type == o2Type;
        };

        #region IEventBus 成员

        #region 事件订阅 & 取消订阅，可以扩展
        /// <summary>
        /// 订阅事件列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="subTypeList"></param>
        public void Subscribe<TEvent>(IBusHandler<TEvent> eventHandler)
            where TEvent : class, IBusData
        {
            lock (_objLock)
            {
                var eventType = typeof(TEvent);
                if (_eventHandlers.ContainsKey(eventType))
                {
                    var handlers = _eventHandlers[eventType];
                    if (handlers != null)
                    {
                        if (!handlers.Exists(deh => _eventHandlerEquals(deh, eventHandler)))
                            handlers.Add(eventHandler);
                    }
                    else
                    {
                        handlers = new List<object>();
                        handlers.Add(eventHandler);
                    }
                }
                else
                    _eventHandlers.Add(eventType, new List<object> { eventHandler });
            }
        }
         
        #endregion

        #region 事件发布
        /// <summary>
        /// 发布事件，支持异步事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="evnt"></param>
        public void Publish<TEvent>(TEvent @event)
           where TEvent : class, IBusData
        {
            if (@event == null)
                throw new ArgumentNullException("event");
            var eventType = @event.GetType();
            if (_eventHandlers.ContainsKey(eventType)
                && _eventHandlers[eventType] != null
                && _eventHandlers[eventType].Count > 0)
            {
                var handlers = _eventHandlers[eventType];
                foreach (var handler in handlers)
                {
                    var eventHandler = handler as IBusHandler<TEvent>;
                         eventHandler.Handle(@event);
                 }
            }
        }
         #endregion

        #region 订阅所有

        /// <summary>
        /// 全局统一注册所有事件处理程序，实现了IEventHandlers的
        /// </summary>
        public void SubscribeAll()
        {
            var types = AssemblyHelper.GetTypesByInterfaces(typeof(IBusHandler<>)).Where(i => i.IsPublic);
            foreach (var item in types)
            {
                if (!item.ContainsGenericParameters)
                {
                    var eventHandler = Activator.CreateInstance(item);
                    foreach (var methodParam in eventHandler.GetType().GetMethods().Where(i => i.Name == "Handle"))
                    {
                        Subscribe(methodParam.GetParameters().First().ParameterType, eventHandler);
                    }
                }
            }
        }

        void Subscribe(Type type, object eventHandler)
        {
            lock (_objLock)
            {
                var eventType = type;
                if (_eventHandlers.ContainsKey(eventType))
                {
                    var handlers = _eventHandlers[eventType];
                    if (handlers != null)
                    {
                        if (!handlers.Exists(deh => _eventHandlerEquals(deh, eventHandler)))
                            handlers.Add(eventHandler);
                    }
                    else
                    {
                        handlers = new List<object>();
                        handlers.Add(eventHandler);
                    }
                }
                else
                    _eventHandlers.Add(eventType, new List<object> { eventHandler });
            }
        }
        #endregion

        #endregion
    }

    public class AssemblyHelper
    {
        /// <summary>
        /// 黑名单
        /// </summary>
        static string[] BlackList = { "System", "FluentValidation", "StackExchange", "Microsoft", "Autofac", "Quartz", "EntityFramework", "MySql", "MongoDB", "log4net", "AutoMapper", "NPOI", "CrystalQuartz", "Gma.QrCodeNet", "HtmlAgilityPack", "Common.Logging", "NetPay", "ServiceStack", "Newtonsoft.Json", "LitJson", "Robots", "CsQuery" };
        static object lockObj = new object();
        static IEnumerable<Type> modelList;
        static AssemblyHelper()
        {
            lock (lockObj)
            {
                if (modelList == null)
                {
                    lock (lockObj)
                    {
                         var pathList = new List<string> { AppDomain.CurrentDomain.BaseDirectory };
                        List<Assembly> AssemblyList = new List<Assembly>();
                        string path = AppDomain.CurrentDomain.BaseDirectory;
                        if (!path.Contains("bin") && Directory.Exists(path))//如果当前程序目录没有bin，就添加bin，并且需要这个bin真实存在
                        {
                            pathList.Add(Path.Combine(path, "bin"));
                        }

                        try
                        {
                            foreach (var _path in pathList)
                            {

                                foreach (var dir in Directory.GetFiles(_path)
                                                 .Where(i => i.EndsWith("dll", StringComparison.CurrentCultureIgnoreCase)
                                                 || i.EndsWith("exe", StringComparison.CurrentCultureIgnoreCase)))
                                {
                                    var dll = Assembly.LoadFrom(dir);
                                    if (BlackList.Where(j => dll.FullName.StartsWith(j, StringComparison.CurrentCultureIgnoreCase)).Count() == 0)
                                        AssemblyList.Add(dll);
                                }
                            }
                            modelList = AssemblyList.SelectMany(a => a.GetTypes()).Where(i => i.IsClass && !i.IsAbstract);

                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 某个类型是否派生于别一个类型，支持泛型接口
        /// </summary>
        /// <param name="subType"></param>
        /// <param name="basicType"></param>
        /// <returns></returns>
        static bool IsAssignableToGenericType(Type subType, Type basicType)
        {
            if (basicType.IsAssignableFrom(subType))
                return true;

            var interfaceTypes = subType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == basicType)
                    return true;
            }

            if (subType.IsGenericType && subType.GetGenericTypeDefinition() == basicType)
                return true;

            Type baseType = subType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, basicType);
        }


        /// <summary>
        /// 得到BIN下面加载程序集里，实现某接口的类型集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypesByInterfaces(Type @interface)
        {
            return modelList.Where(x => IsAssignableToGenericType(x, @interface));
        }

        /// <summary>
        /// 得到BIN下面加载程序集里，实现某接口的类型名称的集合
        /// </summary>
        /// <param name="interface"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetTypeNamesByInterfaces(Type @interface)
        {
            return GetTypesByInterfaces(@interface).Select(i => i.Name);
        }


        /// <summary>
        /// 根据接口，返回程序运行时里所有实现它的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="interface"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetCurrentTypesByInterfaces<T>(Type @interface)
        {
            var handlers = @interface.Assembly.GetExportedTypes()
                .Where(x => x.GetInterfaces()
                    .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == @interface))
                    .Where(h => h.GetInterfaces().Any(ii => ii.GetGenericArguments()
                        .Any(aa => aa == typeof(T))))
                        .ToList();
            return handlers;
        }

    }

}
