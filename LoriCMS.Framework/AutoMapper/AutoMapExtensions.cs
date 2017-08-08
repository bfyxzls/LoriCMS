using AutoMapper;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    /// AutoMapper映射扩展方法
    /// </summary>
    public static partial class AutoMapExtensions
    {
        static AutoMapExtensions()
        {
            //string->int
            Mapper.CreateMap<string, int>().ConvertUsing((x) =>
            {
                int o = 0;
                int.TryParse(x, out o);
                return o;
            });
            //int->bool
            Mapper.CreateMap<int, bool>().ConvertUsing((x) =>
            {
                return x > 0;
            });
        }
        /// <summary>
        /// 为集合进行automapper
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> MapTo<TResult>(this IEnumerable self)
        {
            if (self == null)
                throw new ArgumentNullException();
            return (IEnumerable<TResult>)Mapper.DynamicMap(self, self.GetType(), typeof(IEnumerable<TResult>));
        }

        /// <summary>
        /// 为新对象进行automapper
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static TResult MapTo<TResult>(this object self)
        {
            if (self == null)
                throw new ArgumentNullException();
            Mapper.CreateMap(self.GetType().UnderlyingSystemType, typeof(TResult));
            return (TResult)Mapper.Map(self, self.GetType(), typeof(TResult));
        }

        /// <summary>
        /// 为已经存在的对象进行automapper
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static TResult MapTo<TResult>(this object self, TResult result)
        {
            if (self == null)
                throw new ArgumentNullException();
            Mapper.CreateMap(self.GetType().UnderlyingSystemType, typeof(TResult));
            return (TResult)Mapper.Map(self, result, self.GetType(), typeof(TResult));

        }

    }
}
