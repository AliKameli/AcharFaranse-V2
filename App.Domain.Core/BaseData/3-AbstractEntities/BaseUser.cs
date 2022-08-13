using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using App.Domain.Core.BaseData._1_Entities;
using App.Domain.Core.User.AppIdentity._1_Entitys;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.BaseData._3_AbstractEntities
{
    [Index(nameof(NationalSecurityId), IsUnique = true)]
    public abstract class BaseUser : BaseEntity<int>
    {
        [Key]
        [ForeignKey(nameof(AppIdentityUser))]
        public override int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        public string NationalSecurityId { get; set; }

        [MaxLength(256)]
        public string? HomeAddress { get; set; }

        public bool IsConfirmed { get; set; } = false;

        public DateTimeOffset? ConfirmDateTime { get; set; }

        [ForeignKey(nameof(UserCity))]
        public int? UserCityId { get; set; }

        public virtual City? UserCity { get; set; }

        public AppIdentityUser? AppIdentityUser { get; set; }
    }
}