using System.ComponentModel.DataAnnotations;

namespace App.Domain.Enums;

[Flags]
public enum UserTypeEnum : short
{
    [Display(Name = "مشتری")] Customer = 1,

    [Display(Name = "کارمند")] Worker = 2,

    [Display(Name = "ادمین")] Admin = 4
}