using System.ComponentModel.DataAnnotations;

namespace App.Domain.Enums;

public enum ProposalStatusEnum : byte
{
    [Display(Name = "پیشنهاد شده توسط کارمند")]
    ProposedByWorker = 1,

    [Display(Name = "رد شده توسط مشتری")] RejectedByCostumer = 2,

    [Display(Name = "پذیرفته شده توسط مشتری")]
    AcceptedByCostumer = 3
}