using App.Domain.Core.BaseData._1_Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using App.Domain.Core.BaseData._3_AbstractEntities;

namespace App.Domain.Core.User.Costumer._2_Dtos;

public class CostumerAddressDto : BaseEntityDto<int>
{
    [MaxLength(256)]
    public string FullAddress { get; set; }

    [MaxLength(50)]
    public string? GpsCoordinates { get; set; }

    public bool IsReceivingByCostumer { get; set; } = true;

    [MaxLength(100)]
    public string? ReceivingPersonFullName { get; set; }

    [MaxLength(50)]
    public string? ReceivingPersonPhoneNumber { get; set; }

    public int AddressCityId { get; set; }

    public int CostumerId { get; set; }
}