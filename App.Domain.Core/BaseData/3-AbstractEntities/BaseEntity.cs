using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.BaseData._3_AbstractEntities
{
    public abstract class BaseEntity<TKey>
    {
        [Key]
        public virtual TKey Id { get; set; }

        [Required]
        public DateTimeOffset CreationDateTime { get; set; } = DateTimeOffset.Now;

        [Required]
        public virtual DateTimeOffset? LastUpdateDateTime { get; set; } = DateTimeOffset.Now;

        public bool IsDeleted { get; set; } = false;

        public DateTimeOffset? DeleteDateTime { get; set; }
    }
}