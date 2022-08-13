using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.BaseData._1_Entities;
using App.Domain.Core.BaseData._3_AbstractEntities;

namespace App.Domain.Core.User.Costumer._1_Entitys
{
    public class CostumerAddress : BaseEntity<int>
    {
        [Required]
        [MaxLength(256)]
        public string FullAddress { get; set; }

        [MaxLength(50)]
        public string? GpsCoordinates { get; set; }

        public bool IsReceivingByCostumer { get; set; } = true;

        [MaxLength(100)]
        public string? ReceivingPersonFullName { get; set; }

        [MaxLength(50)]
        public string? ReceivingPersonPhoneNumber { get; set; }

        [ForeignKey(nameof(AddressCity))]
        public int AddressCityId { get; set; }

        public virtual City? AddressCity { get; set; }

        [ForeignKey(nameof(Costumer))]
        public int CostumerId { get; set; }

        public virtual Costumer? Costumer { get; set; }
    }
}