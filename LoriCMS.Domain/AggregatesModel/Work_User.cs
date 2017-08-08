 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoriCMS.Domain.AggregatesModel
{
    /// <summary>
    /// 工作单－用户
    /// </summary>
    public class Work_User : Entity
    {
        [DisplayName("用户名"), Required]
        public string UserName { get; set; }
        [DisplayName("姓名")]
        public FullName RealName { get; set; }
        [DisplayName("密码"), Required]
        public string Password { get; set; }
        [DisplayName("电子邮件"), Required, EmailAddress]
        public string Email { get; set; }
        [DisplayName("手机"), Required, Phone]
        public string Tel { get; set; }
    }

    public class FullName : ValueObject
    {
        [DisplayName("姓")]
        public string FirstName { get; set; }
        [DisplayName("名")]
        public string LastName { get; set; }
        [DisplayName("头像")]
        public string Avatar { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
            yield return Avatar;
        }
    }
}