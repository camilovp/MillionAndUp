
using Data.Entities;
using MillionAndUpApi.DTO_s;
using MillionAndUpApi.Interfaces.Mappers;

namespace MillionAndUpApi.Mappers
{
    public class PropertyMapper : IPropertyMapper
    {
        public PropertyDTO PropertyToDTO(Property property)
        {
            return new PropertyDTO
            {
                IdProperty = property.IdProperty.ToString(),
                Address = property.Address,
                Name = property.Name,
                Price = property.Price,
                Year = property.Year,
                CodeInternal = property.CodeInternal,
                IdOwner = property.IdOwner.ToString(),
            };
        }

        public List<PropertyDTO> PropertyListToDTO(List<Property> properties) {
            List<PropertyDTO> result = new List<PropertyDTO>();
            foreach (var property in properties)
            {
                result.Add(PropertyToDTO(property));
            }

            return result;
        }

        public Property DTOToProperty(PropertyDTO propertyDTO)
        {
            return new Property
            {
                IdProperty = Guid.Parse(propertyDTO.IdProperty),
                Address = propertyDTO.Address,
                Name = propertyDTO.Name,
                Price = propertyDTO.Price,
                Year = propertyDTO.Year,
                CodeInternal = propertyDTO.CodeInternal,
                IdOwner = Guid.Parse(propertyDTO.IdOwner)
            };
        }

        public PropertyImage DTOToPropertyImage(PropertyImageDTO propertyImageDTO, string fileName)
        {
            return new PropertyImage
            {
                IdProperty = Guid.Parse(propertyImageDTO.IdProperty),
                Enabled = propertyImageDTO.Enabled,
                IdPropertyImage = Guid.Parse(propertyImageDTO.IdPropertyImage),
                file = fileName
            };
        }

        public PropertyImageDTO PropertyImageToDTO(PropertyImage propertyImage, string path)
        {
            return new PropertyImageDTO
            {
                IdProperty = propertyImage.IdProperty.ToString(),
                IdPropertyImage = propertyImage.IdPropertyImage.ToString(),
                Enabled = propertyImage.Enabled,
                FileName = propertyImage.file,
                Path = path
            };
        }

        public PropertyTrace DTOToPropertyTrace(PropertyTraceDTO propertyTrace)
        {
            return new PropertyTrace
            {
                IdProperty = Guid.Parse(propertyTrace.IdProperty),
                IdPropertyTrace = Guid.Parse(propertyTrace.IdPropertyTrace),
                DateSale = propertyTrace.DateSale,
                Name = propertyTrace.Name,
                Tax = propertyTrace.Tax,
                Value = propertyTrace.Value,
            };
        }

        public PropertyTraceDTO PropertyTraceToDTO(PropertyTrace propertyTrace)
        {
            return new PropertyTraceDTO
            {
                IdProperty = propertyTrace.IdProperty.ToString(),
                IdPropertyTrace = propertyTrace.IdPropertyTrace.ToString(),
                DateSale = propertyTrace.DateSale,
                Name = propertyTrace.Name,
                Tax = propertyTrace.Tax,
                Value = propertyTrace.Value,
            };
        }
    }
}
