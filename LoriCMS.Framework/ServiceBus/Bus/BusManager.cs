using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace LoriCMS.ServiceBus
{
    /// <summary>
    /// 事件总线[事件功能核心代码]
    /// 发布与订阅处理逻辑
    /// 核心功能代码
    /// </summary>
    public class BusManager : IBus
    {
        #region Constructors

        protected BusManager()
        {
            _iEventBus = new MemoryBus();
        }

        #endregion

        private static object _objLock = new object();
        /// <summary>
        /// 事件总线对象
        /// </summary>
        private static BusManager _instance = null;

        /// <summary>
        /// 事件生产者
        /// </summary>
        private IBus _iEventBus = null;

        #region Singleton Instance
        /// <summary>
        /// 初始化空的事件总件,单例模式,双重锁，解决并发和性能问题
        /// </summary>
        public static IBus Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_objLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new BusManager();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region 事件订阅 & 取消订阅，可以扩展
        /// <summary>
        /// 订阅事件列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="subTypeList"></param>
        public void SubscribeAll()
        {
            _iEventBus.SubscribeAll();
        }
        public void Subscribe<TEvent>(IBusHandler<TEvent> eventHandler)
            where TEvent : class, IBusData
        {
            _iEventBus.Subscribe<TEvent>(eventHandler);
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
            _iEventBus.Publish<TEvent>(@event);
        }
         
        #endregion
    }
}
