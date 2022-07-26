using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Dtos;

public class JobWorkerProposalDto
{
    [Display(Name = "شناسه")]
    public int Id { get; init; }

    [Display(Name = "تاریخ ایجاد")]
    public DateTimeOffset CreationDateTime { get; set; }

    [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
    [RegularExpression(@"\d+(\.\d?)?", ErrorMessage = "{0} باید عدد باشد")]
    [MaxLength(11, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
    [Precision(10, 1)]
    [Display(Name = "هزینه پیشنهادی")]
    public decimal ProposedPrice { get; set; }

    [MaxLength(256, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
    [Display(Name = "توضیحات کارمند")]
    public string? WorkerComment { get; set; }

    [Editable(false)]
    [Display(Name = "شناسه کار")]
    public int JobId { get; init; }

    [Editable(false)]
    [Display(Name = "شناسه کارمند")]
    public int WorkerId { get; init; }

    [Display(Name = "نام کارمند")]
    public string? WorkerName { get; init; }
}