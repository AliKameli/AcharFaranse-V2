using System.ComponentModel.DataAnnotations;

namespace App.Domain.Common;

public abstract class BaseEntityDto<TKey>
{
    [Editable(false)]
    [Display(Name = "شناسه")]
    public virtual TKey Id { get; set; } = default!;

    [Editable(false)]
    [Display(Name = "زمان ایجاد")]
    public DateTimeOffset CreationDateTime { get; init; }

    [Editable(false)]
    [Display(Name = "زمان آخرین بروزرسانی")]
    public virtual DateTimeOffset? LastUpdateDateTime { get; init; }
}