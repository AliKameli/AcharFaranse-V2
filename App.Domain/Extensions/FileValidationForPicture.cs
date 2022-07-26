using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace App.Domain.Extensions
{
    [AttributeUsage(AttributeTargets.Property |
                    AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class FileValidationForPicture : ValidationAttribute
    {
        private static readonly string[] validExtensions = new string[] { ".png", ".jpg", ".jpeg", ".gif" };

        public FileValidationForPicture(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public override bool IsValid(object? value)
        {
            var result = true;

            if (value == null) return result;
            var item = (IFormFile)value;
            var extension = Path.GetExtension(item.FileName);
            if (!validExtensions.Contains(extension)) result = false;

            return result;
        }
    }
}
