using System.ComponentModel.DataAnnotations;
using App.Domain.Common;

namespace App.Domain.Entities;

public class JobCategory : BaseEntity<int>
{
    [Display(Name = "نام دسته‌بندی کار")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "توضیحات")]
    public string? Description { get; set; }

    [Display(Name = "آدرس تصویر ذخیره شده")]
    public string? PictureFilePath { get; set; }

    [Display(Name = "هزینه احتمالی")]
    public decimal? EstimatedWageCost { get; set; }

    [Display(Name = "شناسه دسته‌بندی سرگروه")]
    public int? ParentJobCategoryId { get; set; }

    [Display(Name = "دسته‌بندی سرگروه")]
    public virtual JobCategory? ParentJobCategory { get; set; }

    public virtual ICollection<JobCategory> ChildrenJobCategories { get; set; } = new HashSet<JobCategory>();

    public virtual ICollection<Job> Jobs { get; set; } = new HashSet<Job>();

    public virtual ICollection<Worker> Workers { get; set; } = new HashSet<Worker>();

    public virtual ICollection<JobCategoryWorker> JobCategoryWorkers { get; set; } =
        new HashSet<JobCategoryWorker>();
}