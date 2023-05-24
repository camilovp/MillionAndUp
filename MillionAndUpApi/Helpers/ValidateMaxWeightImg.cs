using System.ComponentModel.DataAnnotations;

namespace MillionAndUpApi.Helpers
{
    public class ValidateMaxWeightImg : ValidationAttribute
    {
        private readonly int _weight;

        public ValidateMaxWeightImg(int weight)
        {
            _weight = weight;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
                return ValidationResult.Success;

            if (formFile.Length > _weight * 1024 * 1024) return new ValidationResult($"the maximum weight of the image is {_weight}" );

            return ValidationResult.Success;
        }
    }
}
