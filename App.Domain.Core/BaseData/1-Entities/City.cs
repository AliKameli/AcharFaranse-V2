using System;
using System.ComponentModel.DataAnnotations;
using App.Domain.Core.BaseData._3_AbstractEntities;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.BaseData._1_Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class City : BaseEntity<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}