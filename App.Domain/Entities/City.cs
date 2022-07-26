using System.ComponentModel.DataAnnotations;
using App.Domain.Common;

namespace App.Domain.Entities;

public class City : BaseEntity<int>
{
    [Display(Name = "نام شهر")]
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Costumer> Costumers { get; set; } = new HashSet<Costumer>();
    public virtual ICollection<Worker> Workers { get; set; } = new HashSet<Worker>();
    public virtual ICollection<Job> Jobs { get; set; } = new HashSet<Job>();
    public virtual ICollection<CostumerAddress> CostumerAddresses { get; set; } = new HashSet<CostumerAddress>();
}