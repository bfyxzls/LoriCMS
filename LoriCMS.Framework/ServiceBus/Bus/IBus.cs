using System;
namespace LoriCMS.ServiceBus
{
    /// <summary>
    /// 事件总线，生产者接口
    /// </summary>
    public interface IBus
    {
        /// <summary>
        ///  发布事件，支持异步事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        void Publish<TEvent>(TEvent @event) where TEvent : class, IBusData;
         /// <summary>
        ///  订阅事件列表
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventHandler"></param>
        void Subscribe<TEvent>(IBusHandler<TEvent> eventHandler) where TEvent : class, IBusData;
 
        /// <summary>
        /// 订阅全部事件，实现了IEventHandler的类型
        /// </summary>
        void SubscribeAll();
    }
}
