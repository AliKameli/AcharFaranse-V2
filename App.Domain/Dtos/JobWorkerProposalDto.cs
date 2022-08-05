using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Dtos;

public class JobWorkerProposalDto
{
    [Display(Name = "شناسه")]
    public int Id { get; set; }

    [Display(Name = "تاریخ ایجاد")]
    public DateTimeOffset CreationDateTime { get; set; }

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [RegularExpression(@"\d+(\.\d?)?", ErrorMessage = "{0} باید عدد باشد")]
    [Precision(10, 1)]
    [Display(Name = "هزینه پیشنهادی")]
    public decimal ProposedPrice { get; set; }

    [MaxLength(256, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
    [Display(Name = "توضیحات کارمند")]
    public string? WorkerComment { get; set; }

    [Editable(false)]
    [Display(Name = "شناسه کار")]
    public int JobId { get; set; }

    [Editable(false)]
    [Display(Name = "شناسه کارمند")]
    public int WorkerId { get; set; }

    [Display(Name = "نام کارمند")]
    public string? WorkerName { get; set; }

    [Display(Name = "وضعیت پیشنهاد")]
    public ProposalStatusEnum ProposalStatus { get; set; } = ProposalStatusEnum.ProposedByWorker;
}