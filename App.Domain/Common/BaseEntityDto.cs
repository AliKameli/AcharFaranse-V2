using System.ComponentModel.DataAnnotations;

namespace App.Domain.Common;

public abstract class BaseEntityDto<TKey>
{
    [Display(Name = "شناسه")]
    public virtual TKey Id { get; set; } = default!;

    [Display(Name = "زمان ایجاد")]
    public DateTimeOffset CreationDateTime { get; set; }=DateTimeOffset.Now;

    [Display(Name = "زمان آخرین بروزرسانی")]
    public virtual DateTimeOffset? LastUpdateDateTime { get; set; }
}