using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseData._3_AbstractEntities
{
    public abstract class BaseEntityDto<TKey>
    {
        public virtual TKey Id { get; set; }
        public DateTimeOffset CreationDateTime { get; init; }
        public virtual DateTimeOffset? LastUpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeleteDateTime { get; set; }
    }
}