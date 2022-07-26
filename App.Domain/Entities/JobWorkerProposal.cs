using System.ComponentModel.DataAnnotations;
using App.Domain.Common;
using App.Domain.Enums;

namespace App.Domain.Entities;

public class JobWorkerProposal : BaseEntity<int>
{
    [Display(Name = "وضعیت پیشنهاد")]
    public ProposalStatusEnum ProposalStatus { get; set; } = ProposalStatusEnum.ProposedByWorker;

    [Display(Name = "هزینه پیشنهادی")]
    public decimal ProposedPrice { get; set; }

    [Display(Name = "توضیحات کارمند")]
    public string? WorkerComment { get; set; }

    [Display(Name = "شناسه کار")]
    public int JobId { get; set; }

    [Display(Name = "کار")]
    public virtual Job? Job { get; set; }

    [Display(Name = "شناسه کارمند")]
    public int WorkerId { get; set; }

    [Display(Name = "کارمند")]
    public virtual Worker? Worker { get; set; }
}