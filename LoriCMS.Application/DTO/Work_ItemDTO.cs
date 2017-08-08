using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LoriCMS.Application.DTO
{
    /// <summary>
    /// 作为工作单的－DTO模型
    /// ABP－automapper主要可以实现两种模式之间，不同属性的映射，或者由int编号映射成string的名称
    /// </summary>
    public class Work_ItemDTO
    {
        [DisplayName("编号")]
        public int Id { get; set; }
        [DisplayName("标题")]
        public string Title { get; set; }
        [DisplayName("内容")]
        public string Content { get; set; }
        [DisplayName("开始时间")]
        public DateTime StartTime { get; set; }
        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }
        [DisplayName("进度")]
        public int Schedule { get; set; }
        [DisplayName("项目名称")]
        public string ProjectName { get; set; }
        [DisplayName("提交人")]
        public string UserName { get; set; }
    }

}