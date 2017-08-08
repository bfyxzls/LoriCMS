using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoriCMS.Domain
{
    /// <summary>
    /// 实体基类－会被持久化
    /// </summary>
    [Serializable]
    public abstract class Entity : EntityBase, IEntity<int>
    {
        public Entity()
        { }
        [DisplayName("编号"), Column("ID"), Required, Key]
        public int Id { get; set; }

        public bool Equals(int other)
        {
            return Id.Equals(other);
        }
    }
}

