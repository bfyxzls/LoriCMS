using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LindAgile.AutoMapper
{
    public class Source
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }
    }


    public class Destination
    {
        /// <summary>
        /// 这个值为Source.Value1+Source.Value2
        /// </summary>
        public int Total { get; set; }
    }

    public class MyValueResolver : ValueResolver<Source, int>
    {

        protected override int ResolveCore(Source source)
        {
            return source.Value1 + source.Value2;
        }
    }
    class Demo
    {
        static void Main(string[] args)
        {
            #region AutoMapper & 自定义解析
            //Mapper.CreateMap<Source, Destination>().ForMember(dest => dest.Total, opt =>
            //{
            //    opt.ResolveUsing<MyValueResolver>();
            //});

            //不使用反射，而使用回调
            Mapper.CreateMap<Source, Destination>().ForMember(dest => dest.Total, opt =>
            {
                opt.ResolveUsing<MyValueResolver>().ConstructedBy(() => new MyValueResolver());
            });


            var src = new Source { Value1 = 3, Value2 = 5 };

            var destObj = Mapper.Map<Destination>(src);
            Console.WriteLine("destObj.Total={0}", destObj.Total);
            Console.Read();
            #endregion
        }
    }
}