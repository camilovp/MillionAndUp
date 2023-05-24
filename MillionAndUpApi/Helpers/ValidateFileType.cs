using MillionAndUpApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace MillionAndUpApi.Helpers
{
    public class ValidateFileType : ValidationAttribute
    {
        private readonly string[] _fileTypes;

        public ValidateFileType(string[] fileTypesArray)
        {
            _fileTypes = fileTypesArray;
        }

        public ValidateFileType(FileTypes fileTypes)
        {
            if (fileTypes == FileTypes.image)
            {
                _fileTypes = new string[]
                {
                    "image/png",
                    "image/jpeg",
                    "image/gif"
                };
            }
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
                return ValidationResult.Success;

            if (!_fileTypes.Contains(formFile.ContentType)) return new ValidationResult("Error File Types");

            return ValidationResult.Success;
        }
    }
}
