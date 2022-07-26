using System.ComponentModel.DataAnnotations;
using App.Domain.Common;

namespace App.Domain.Entities;

public class CostumerAddress : BaseEntity<int>
{
    [Display(Name = "نام")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "آدرس کامل")]
    public string FullAddress { get; set; } = string.Empty;

    [Display(Name = "مختصات روی نقشه")]
    public string? GpsCoordinates { get; set; }

    [Display(Name = "نام پذیرنده")]
    public string ReceivingPersonFullName { get; set; } = string.Empty;

    [Display(Name = "شماره تلفن پذیرنده")]
    public string ReceivingPersonPhoneNumber { get; set; } = string.Empty;

    [Display(Name = "شناسه شهر")]
    public int AddressCityId { get; set; }

    [Display(Name = "شهر")]
    public virtual City? AddressCity { get; set; }

    [Display(Name = "شناسه مشتری")]
    public int CostumerId { get; set; }

    [Display(Name = "مشتری")]
    public virtual Costumer? Costumer { get; set; }

    [Display(Name = "کارها")]
    public virtual ICollection<Job> Jobs { get; set; } = new HashSet<Job>();

    [Display(Name = "حذف شده")]
    public bool IsDeleted { get; set; } = false;
}