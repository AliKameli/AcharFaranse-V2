using App.Domain.Core.BaseData._1_Entities;
using App.Domain.Core.User.AppIdentity._1_Entitys;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.BaseData._3_AbstractEntities;

public abstract class BaseUserDto : BaseEntityDto<int>
{
    public override int Id { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }

    [StringLength(10)]
    public string NationalSecurityId { get; set; }

    [MaxLength(256)]
    public string? HomeAddress { get; set; }

    public bool IsConfirmed { get; set; } = false;

    public DateTimeOffset? ConfirmDateTime { get; set; }

    public int? UserCityId { get; set; }
}