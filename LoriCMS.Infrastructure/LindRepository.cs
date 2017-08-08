using LoriCMS.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LindAgile.Web.DDD.Infrastructure
{
    public class LindRepository<T> : EFRepository<T> where T : class
    {
        public LindRepository() : base(new LindContext()) { }
    }
}